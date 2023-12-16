using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugarDB.Helpers
{
    public  class JsonHelper
    {
        // 读取指定名称（包括文件路径）的json文件，返回json字符串
        public static string GetJsonFromResource(string resourceName)
        {
           
            var assembly = Assembly.GetExecutingAssembly();      //当前正在执行的代码的程序集
            string folder = Directory.GetCurrentDirectory();      //获取应用程序当前工作目录
            string path=folder + "\\"+resourceName;
            using (var stream = File.OpenText(path))
            {
                if (stream == null) return null;
                JsonTextReader reader = new JsonTextReader(stream);
                JObject jsonObject=(JObject)JToken.ReadFrom(reader);

                string json = jsonObject["SystemSettings"]["InitSeedData"].ToString();
                string json2 = jsonObject["SystemSettings"].ToString();
                return json;
            }
        }

        /// <summary>
        /// 获取根目录下的所有json文件
        /// </summary>
        /// <returns></returns>
        public static string[] GetJsonFileNames()
        {
            // 获取当前类库根目录路径
            //  string rootDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //获取json所在的目录
           //  string rootDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SqlSugarDB");
            string rootDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
          

            // 获取当前类库根目录下的所有json文件的路径
            string[] jsonFilePaths = Directory.GetFiles(rootDirectory, "*.json");

            // 提取json文件的名称并返回
             return jsonFilePaths.Select(Path.GetFileName).ToArray();
        }
    }

   
}
