
global using System;
global using System.Collections.Generic;
global using System.Linq;
global using System.Text;
global using System.Threading.Tasks;
global using Amib.Threading;
global using HslCommunication;
global using HslCommunication.Profinet.Siemens;
global using System.Collections.Concurrent;
global using System.Threading;
using IgniteX.PlcLib.Utils;
using IgniteX.PlcLib.PlcModel;

namespace IgniteX.PlcLib.SiemensPlc
{
    public class ConnectionSiemensPLC : IConnectionSiemensPLC
    {
        private SiemensS7Net _plc;
        private Thread _workThread;
        private ManualResetEvent _mre;
        private SmartThreadPool _smartThreadPool;
        public PlcInfo _plcInfo { get; set; }

        //定义回调事件
        public delegate void Err(string errMsg);

        public event Err OnErr;

        public delegate void Info(string Info);

        public event Info OnInfo;

        public delegate void PublicCallback(string op, PublicInfo publicInfo);

        public event PublicCallback OnPublicCallback;

        public delegate Task EventCallback(string op, EventInfo ei);

        public event EventCallback OnEventCallback;

        private ConcurrentQueue<EventInfo> _queueHandleEventCompleted;

        public ConnectionSiemensPLC()
        {
            STPStartInfo ssi = new STPStartInfo() { CallToPostExecute = CallToPostExecute.Always, FillStateWithArgs = true };
            _smartThreadPool = new SmartThreadPool(ssi) { };

            _mre = new ManualResetEvent(false);
            _queueHandleEventCompleted = new ConcurrentQueue<EventInfo>();
        }

        /// <summary>
        /// 设置PLCInfo
        /// </summary>
        /// <param name="plcInfo"></param>
        public void SetPlcInfo(PlcInfo plcInfo)
        {
            _plcInfo = plcInfo;
        }

        /// <summary>
        /// 启动plc
        /// </summary>
        /// <returns></returns>
        public string StartWork()
        {
            if (_plcInfo == null)
                return "请实例化PlcInfo对象";
            if (_plc == null)
            {
                Authorization.SetAuthorizationCode("4672fd9a-4743-4a08-ad2f-5cd3374e496d");

                _plc = new SiemensS7Net((SiemensPLCS)Enum.Parse(typeof(SiemensPLCS), _plcInfo.Version));
                _plc.IpAddress = _plcInfo.IP;
                _plc.Port = _plcInfo.Port;
                _plc.Rack = _plcInfo.Rack;
                _plc.Slot = _plcInfo.Slot;
                _plc.SetPersistentConnection();
            }
            if (!_plcInfo.IsConn)
            {
                if (!_plc.ConnectServer().IsSuccess)
                {
                    _plcInfo.IsConn = false;
                    _plcInfo.Remark = "连接失败";
                    return "Fail";
                }
                //开始线程
                _workThread = new Thread(new ThreadStart(WorkThread)) { IsBackground = true };
                _workThread.Start();
                _plcInfo.IsConn = true;
                _plcInfo.Remark = "连接成功";
                return "OK";
            }
            else
            {
                return "已连接";
            }
        }

        public string StopWork()
        {
            string ret = "OK";
            if (_plcInfo != null && _plcInfo.IsConn)
            {
                _mre.Set();
            }

            return ret;
        }

        private void WorkThread()
        {
            _mre.Reset();
            while (!_mre.WaitOne(10))
            {
                /*
                 * 先处理事件回写
                 */
                if (!_queueHandleEventCompleted.IsEmpty)
                {
                    EventInfo ets = null;
                    if (_queueHandleEventCompleted.TryDequeue(out ets))
                    {
                        WirteToPLC(ets);
                    }
                }
                //读取公共区
                if (_plcInfo.PI.ReadLen == 0) _plcInfo.PI.ResetReadClassLenth();

                OperateResult<byte[]> operateResult01 = _plc.Read(_plcInfo.PI.ReadAddr, _plcInfo.PI.ReadLen);
                if (!operateResult01.IsSuccess)
                {
                    OnErr?.Invoke($"{_plcInfo.OP}PLC公共区读取失败：{operateResult01.Message}");
                    continue;
                }
                //判断长度是否与定义的一致
                if (operateResult01.Content.Length != _plcInfo.PI.ReadLen)
                {
                    //在读一次
                    OnErr?.Invoke($"{_plcInfo.OP}PLC公共区读取长度[{operateResult01.Content.Length}]与定义[{_plcInfo.PI.ReadLen}]的不一致");
                    continue;
                }
                //解析byte[]到指定特性对象种
                _plcInfo.PI.UpdateReadContent(operateResult01.Content);
                //读取PC公共区 只需要读取一次
                if (_plcInfo.PI.WriteLen == 0)
                {
                    _plcInfo.PI.ResetWriteClassLenth();
                    OperateResult<byte[]> operateResult02 = _plc.Read(_plcInfo.PI.WriteAddr, _plcInfo.PI.WriteLen);
                    if (!operateResult02.IsSuccess)
                    {
                        OnErr?.Invoke($"{_plcInfo.OP}PLC公共区读取失败：{operateResult02.Message}");
                        continue;
                    }
                    //判断长度是否与定义的一致
                    if (operateResult02.Content.Length != _plcInfo.PI.WriteLen)
                    {
                        //在读一次
                        OnErr?.Invoke($"{_plcInfo.OP}PLC公共区读取长度[{operateResult02.Content.Length}]与定义[{_plcInfo.PI.WriteLen}]的不一致");
                        continue;
                    }
                    _plcInfo.PI.WriteBuffer = operateResult02.Content;
                    //解析byte[]到指定特性对象种
                    _plcInfo.PI.UpdateWriteContent(operateResult02.Content);
                }
                OnPublicCallback?.Invoke(_plcInfo.OP, _plcInfo.PI);

                if (_plcInfo.PI.WriteLen != 0)
                {
                    SiemensRefelectionUtil.Object2Buffer(_plcInfo.PI.ObjW, _plcInfo.PI.WriteBuffer);
                    _plc.Write(_plcInfo.PI.WriteAddr, _plcInfo.PI.WriteBuffer);
                }

                var bitTarger = _plcInfo.PI.ObjR.EventTriggerBit;
                for (int i = 0; i < bitTarger.Length; i++)
                {
                    bool bit = bitTarger[i];
                    if (bit)
                    {
                        if (i >= _plcInfo.EIs.Count)
                        {
                            //调用错误回调事件
                            OnErr?.Invoke($"PLC触发事件索引超出定义范围，触发位{i}，定义个数{_plcInfo.EIs.Count}");
                            break;
                        }
                        if (!_plcInfo.EIs[i].TriggerCompleted)
                        {
                            if (_plcInfo.EIs[i].ReadLen == 0)
                            {
                                _plcInfo.EIs[i].ResetReadClassLenth();
                            }

                            if (_plcInfo.EIs[i].WriteLen == 0)
                            {
                                _plcInfo.EIs[i].ResetWriteClassLenth();
                                var opW = _plc.Read(_plcInfo.EIs[i].WriteAddr, _plcInfo.EIs[i].WriteLen);
                                if (!opW.IsSuccess)
                                {
                                    OnErr?.Invoke($"{_plcInfo.EIs[i].Idx}事件读取PLC->PC失败：{opW.Message}");
                                    continue;
                                }
                                _plcInfo.EIs[i].UpdateWriteContent(opW.Content);
                                _plcInfo.EIs[i].SequenceIDW = _plcInfo.EIs[i].ObjW.SequenceID;
                            }

                            var opR = _plc.Read(_plcInfo.EIs[i].ReadAddr, _plcInfo.EIs[i].ReadLen);
                            if (!opR.IsSuccess)
                            {
                                OnErr?.Invoke($"{_plcInfo.EIs[i].Idx}事件读取PLC->PC失败：{opR.Message}");
                                continue;
                            }

                            //判断长度是否与定义的一致
                            if (opR.Content.Length != _plcInfo.EIs[i].ReadLen)
                            {
                                //在读一次
                                OnErr?.Invoke($"{_plcInfo.OP}PLC事件读取区读取长度[{opR.Content.Length}]与定义[{_plcInfo.EIs[i].ReadLen}]的不一致");
                                continue;
                            }

                            //更新事件实例内容
                            _plcInfo.EIs[i].UpdateReadContent(opR.Content);

                            _plcInfo.EIs[i].SequenceIDR = _plcInfo.EIs[i].ObjR.SequenceID;
                            //查看 SequenceID
                            if (_plcInfo.EIs[i].SequenceIDW != _plcInfo.EIs[i].SequenceIDR)
                            {
                                _plcInfo.EIs[i].TriggerCompleted = true;
                                //回调事件处理

                                _smartThreadPool.QueueWorkItem(new WorkItemCallback(EventCallBack), _plcInfo.EIs[i], HandleEventCaLLBack);
                            }
                            else
                            {
                                OnInfo?.Invoke($"{_plcInfo.EIs[i].Idx}事件SequnenceID相等：SequnenceID = {_plcInfo.EIs[i].SequenceIDR}");

                                //该情况可以考虑再次事件写回PLC
                                WirteToPLC2(_plcInfo.EIs[i]);
                            }
                        }
                        else
                        {
                            //已经有线程处理当前事件 当前事件正在处理中
                            OnInfo?.Invoke($"{_plcInfo.EIs[i].Idx}当前事件正在处理中");
                        }
                    }
                }
            }
            //断开PLC
            _plc.ConnectClose();
            _plc.Dispose();
            _plc = null;
            _plcInfo.IsConn = false;
        }

        private object EventCallBack(object obj)
        {
            EventInfo ei = obj as EventInfo;
            if (OnEventCallback != null)
            {
                OnEventCallback(_plcInfo.OP, ei).GetAwaiter().GetResult();
            }
            return obj;
        }

        private void HandleEventCaLLBack(IWorkItemResult wir)
        {
            EventInfo ei = wir.Result as EventInfo;
            //放入待处理队列中
            _queueHandleEventCompleted.Enqueue(ei);
        }

        private void WirteToPLC(EventInfo ei)
        {
            //SequenceID自动相等
            ei.SequenceIDW = ei.ObjW.SequenceID = ei.ObjR.SequenceID;
            SiemensRefelectionUtil.Object2Buffer(ei.ObjW, ei.WriteBuffer);

            var result = _plc.Write(ei.WriteAddr, ei.WriteBuffer);
            if (!result.IsSuccess)
            {
                OnInfo?.Invoke($"{_plcInfo.OP}第{ei.Idx}个事件{ei.WriteClassName}写入PLC失败");
            }
            else
            {
                ei.TriggerCompleted = false;
            }
        }

        private void WirteToPLC2(EventInfo ei)
        {
            SiemensRefelectionUtil.Object2Buffer(ei.ObjW, ei.WriteBuffer);
            var result = _plc.Write(ei.WriteAddr, ei.WriteBuffer);
            if (!result.IsSuccess)
            {
                OnInfo?.Invoke($"{_plcInfo.OP}第{ei.Idx}个事件{ei.WriteClassName}非第一次写入PLC失败");
            }
            else
            {
                ei.TriggerCompleted = false;
            }
        }
    }
}