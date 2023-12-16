using Bogus;
using SqlSugar;
using System;
using System.Collections.Generic;
using SqlSugarDB.Extensions;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using SqlSugarDB.Models;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Windows;
using Newtonsoft.Json;
using SqlSugarDB.Helpers;

namespace SqlSugarDB.Config
{
    public class CodeFirst
    {
        #region  通过代码创建数据库和表
        public static void GreateDbAndTableByCode()
        {

            //代码先行，通过代码创建数据库和表
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                DbType = DbType.SqlServer,
                ConnectionString = "Data Source=.;" +
                                    "Initial Catalog=DeviceCheckDB;" +
                                    //"Integrated Security=True"+集成验证
                                    "User ID=sa;" +
                                    "Password=211314",
                InitKeyType = InitKeyType.Attribute,//释放事务
                IsAutoCloseConnection = true//读取主键自增
            });
            var isExecute = IsCanExecute();
            using (db)
            {
                db.DbMaintenance.CreateDatabase(); // 注意 ：Oracle和个别国产库需不支持该方法，需要手动建库 
                //获取当前类库名称
                string assemblyPath = Assembly.GetExecutingAssembly().Location;
                // 加载程序集并获取所有类型
                Assembly assembly = Assembly.LoadFrom(assemblyPath);
                Type[] types = assembly.GetTypes()
                    .Where(type => type.Namespace == "SqlSugarDB.Models" && !type.Name.EndsWith("Entity") && !type.Name.Contains("Attribute"))
                    .ToArray();
                if (isExecute)
                {
                    db.CodeFirst.SetStringDefaultLength(200).InitTables(types);//根据types创建表
                }               

            }

            var bogusIgniteDatas = GetIgniteFakeData();
            var bogusWorkPlaceDatas = GetWorkPlaceFakeData();
            var bogusRepices = GetRepiceFakeData();
            var bogusOrders = GetOrderFakeData();
          

            using (db)
            {
                if (isExecute)
                {
                    foreach (var item in bogusIgniteDatas)
                    {
                        db.Insertable(item).ExecuteCommand();
                    }
                    foreach (var item in bogusWorkPlaceDatas)
                    {
                        db.Insertable(item).ExecuteCommand();
                    }
                    foreach (var item in bogusRepices)
                    {
                        db.Insertable(item).ExecuteCommand();
                    }
                    foreach (var item in bogusOrders)
                    {
                        db.Insertable(item).ExecuteCommand();
                    }
                }
               
            }
        }


        #endregion
        public static bool IsCanExecute()
        {
           
            string[] jsonFileName = JsonHelper.GetJsonFileNames();  //先找到所有的json文件
            var json = JsonHelper.GetJsonFromResource(jsonFileName.FirstOrDefault(x=>x.Contains("Start")));       //读取关于包含Start的 
            // 读取配置文件，获取是否需要初始化种子数据
            return  bool.Parse(json);
           
        }
      

        public static List<IgniteTubeInfo> GetIgniteFakeData()
        {
            double dbdata = 0.335333;
            double num = Convert.ToDouble(String.Format("{0:F}", dbdata));     //保留两位小数
            Randomizer.Seed = new Random(123456);
            var igniteData = new Faker<IgniteTubeInfo>("zh_CN")
                .RuleFor(o => o.Id, f => f.IndexFaker)
                .RuleFor(o => o.OrderNumber, f => f.IndexFaker.ToString())                     //随机字符串模拟工单号
                .RuleFor(p => p.IgniteName, f => f.Random.Word())                      //随机字符串模拟点火管名称
                .RuleFor(p => p.CreateTime, f => f.Date.Past(3))                        // 随机创建日期，约束为过去3年
                .RuleFor(c => c.InsulResistive, f => f.Random.GetDoubleNumber(1, 100, 2))            //双精度浮点数  //单精度浮点数Float
                .RuleFor(c => c.BridgeResistive, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.ThermalInitState, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.ThermalState, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.ThermalFinalState, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.ResChange, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.CCDDefect, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.CCDBig, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.CCDSmall, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.CCDHeight, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.CCDPin, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.InsulResistiveResult,1)
                .RuleFor(c => c.BridgeResistiveResult, 1)
                .RuleFor(c => c.ThermalInitStateResult, 1)
                .RuleFor(c => c.ThermalStateResult, 1)
                .RuleFor(c => c.ThermalFinalStateResult, 1)
                .RuleFor(c => c.ResChangeResult, 1)
                .RuleFor(c => c.CCDDefectResult, 1)
                .RuleFor(c => c.CCDBigResult, 1)
                .RuleFor(c => c.CCDSmallResult, 1)
                .RuleFor(c => c.CCDHeightResult, 1)
                .RuleFor(c => c.CCDPinResult, 1);                           
            return igniteData.Generate(200);
        }

        public static List<RepiceInfo> GetRepiceFakeData()
        {
            Randomizer.Seed = new Random(123456);
            var repiceData = new Faker<RepiceInfo>("zh_CN")
                .RuleFor(o => o.Id, f => f.IndexFaker)
                .RuleFor(o => o.RepiceName, f => f.Vehicle.Fuel())
                .RuleFor(o=>o.ProductColor,f=>f.Random.GetColor())
                .RuleFor(c => c.InsulResistiveLow, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.InsulResistiveUp, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.BridgeResistiveLow, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.BridgeResistiveUp, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.ThermalStateLow, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.ThermalStateUp, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.CCDBigLow, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.CCDBigUp, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.CCDSmallLow, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.CCDSmallUp, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.CCDHeightLow, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.CCDHeightUp, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.CCDPinLow, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.CCDPinUp, f => f.Random.GetDoubleNumber(1, 100, 2))
                .RuleFor(c => c.IsMaxDosage, f => f.Random.GetByte());
              return repiceData.Generate(200);
        }

        public static List<WorkPlace> GetWorkPlaceFakeData()
        {
            Randomizer.Seed = new Random(123456);
            var repiceData = new Faker<WorkPlace>("zh_CN")
                .RuleFor(o => o.Id, f => f.IndexFaker)
                .RuleFor(o=>o.WorkPlaceId,f=>f.IndexGlobal)
                .RuleFor(o=>o.WorkPlaceName,f=>f.Person.LastName+ f.Person.FirstName)    //Bogus默认英文，名在前，改为中文 姓在前
                .RuleFor(o=>o.IsWorkInProgress,f=>f.Random.GetBoolean())
                .RuleFor(o=>o.CreateTime,f=>f.Date.Past(3));
            return repiceData.Generate(200);
        }

        public static List<Order> GetOrderFakeData()
        {
            Randomizer.Seed = new Random(123456);
            var orderData = new Faker<Order>("zh_CN")
                .RuleFor(o => o.Id, f => f.IndexFaker)
                .RuleFor(o => o.OrderNumber, f => f.Random.GetOrderNumber())
                .RuleFor(o => o.ProductName, f => f.Random.GetOrderNumber())
                .RuleFor(o => o.ProductBatchNumber, f => f.Random.GetOrderNumber())
                .RuleFor(o => o.CodeBatchNumber, f => f.Random.GetOrderNumber())
                .RuleFor(o => o.SerialNumber, f => f.Random.GetInt(3))
                .RuleFor(o => o.OrderState, f => f.Random.GetByte())
                .RuleFor(o => o.CreateTime, f => f.Date.Past(3));
              return orderData.Generate(50);
        }

       /* public static string GetProuuctSerial()
        {

            
        }*/

        /*  public static List<Amount> GetUserFakeData()
          {
              Randomizer.Seed = new Random(123456);
              var userData = new Faker<Amount>("zh_CN")
                  .RuleFor(o => o.Id, f => f.IndexFaker)
                  .RuleFor(o => o.UserId, f => f.Random.Number(1, 100))
                  .RuleFor(p => p.Age, f => f.Random.Number(1, 100))                      // 随机年龄(1-100岁)
                  .RuleFor(p => p.Gender, f => f.PickRandom<Name.Gender>())               // 随机性别
                  .RuleFor(c => c.UserName, (f, c) => f.Name.FullName())
                  .RuleFor(c => c.Password, f => f.Internet.Password())
                  .RuleFor(c => c.Phone, f => f.Phone.PhoneNumber())                      //随机手机号也可以这样写.RuleFor(p => p.Phone, p => p.Phone.PhoneNumber("1##########"))
                  .RuleFor(o => o.Email, f => f.Internet.Email())
                  .RuleFor(o => o.Address, f => f.Address.City())                         //地址选择了城市名，也可以写国家名称，邮编名称
                  .RuleFor(o => o.CreateTime, f => f.Date.Past(3));                       //时间选择过去三年的随机时间
              return userData.Generate(100);
          }


          public static List<RepiceInfo> GetRepiceInfoFakeData()
          {
              Randomizer.Seed = new Random(123456);
              var userScoreData = new Faker<RepiceInfo>("zh_CN")
                  .RuleFor(o=>o.CCDHeightLow,f=>f.Vehicle.Fuel())
              return userScoreData.Generate(100);
          }

          public static List<IgniteXInfo> GetIgniteXInfoFakeData()
          {
              Randomizer.Seed = new Random(123456);
              var userScoreData = new Faker<RepiceInfo>("zh_CN")
                  .RuleFor(p => p.ResultId, f => f.Random.Number(1, 100))
                  .RuleFor(p => p.Result, f => f.Random.Number(1, 100))
                  .RuleFor(o => o.CreateTime, f => f.Date.Past(3));
              return userScoreData.Generate(100);
          }*/
    }
}
