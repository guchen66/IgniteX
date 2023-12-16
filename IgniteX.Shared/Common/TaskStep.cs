using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace IgniteX.Shared.Common
{
    public class TaskStep
    {
        public static string Fild { get; set; }
        public static void FildSettings()
        {
            string jsonFilePath = "settings.json";  // JSON 文件的路径
            bool initTable, initSeedData;

            try
            {
                // 读取 JSON 文件中的数据
                string json = File.ReadAllText(jsonFilePath);
                JObject jObject = JObject.Parse(json);

                // 获取 SystemSettings 节点
                JToken systemSettingsToken = jObject["SystemSettings"];
                if (systemSettingsToken != null)
                {
                    // 获取 InitTable 和 InitSeedData 值，并转换为布尔类型
                    initTable = systemSettingsToken.Value<bool>("InitTable");
                    initSeedData = systemSettingsToken.Value<bool>("InitSeedData");

                    // 根据条件执行相关代码
                    if (initTable)
                    {
                        // 执行初始化表结构的代码
                    }
                    if (initSeedData)
                    {
                        // 执行初始化种子数据的代码
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show($"JSON 文件未找到：{ex.Message}");
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"JSON 文件格式错误：{ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"读取 JSON 文件时发生错误：{ex.Message}");
            }
        }
    }
}
