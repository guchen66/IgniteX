using Bogus;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlSugarDB.Extensions
{
    public static class RandomizerExtension
    {
        public static double GetDoubleNumber(this Randomizer randomizer, double minimum, double maximum, int Len)   //Len小数点保留位数
        {
            Random random = new Random();
            return Math.Round(random.NextDouble() * (maximum - minimum) + minimum, Len);
        }

        public static bool GetBoolean(this Randomizer randomizer)   
        {
            var result = new Random().Next(0,2);
            return result == 1 ? true : false;
        }

        public static int GetInt(this Randomizer randomizer,int len)  
        {
            var result = new Random().Next(0, len);
            return result;
        }

        public static byte GetByte(this Randomizer randomizer)
        {
            byte[] buffer = new byte[1];
            new Random().NextBytes(buffer);
            return buffer[0];
        }


        //100以内随机数并且补全
        public static string GetString(this Randomizer randomizer, int length, char pad = '0')
        {
            int result = new Random().Next(0, 100);
            return result.ToString().PadLeft(length, pad);
        }

        //自定义工单号
        public static string GetOrderNumber(this Randomizer randomizer)
        {
            //获取时间戳
            DateTime now = DateTime.Now;
            long timeStamp = new DateTimeOffset(now).ToUnixTimeSeconds();

            var result=timeStamp+ randomizer.GetString(3);
           
            return result.ToString();
        }

        //自定义UUID
        public static string GetUUID(this Randomizer randomizer, string batchNumber, string serialNumber)
        {
            //获取时间戳
            DateTime now = DateTime.Now;
            long timeStamp = new DateTimeOffset(now).ToUnixTimeSeconds();
            var result = now.Year + batchNumber + serialNumber;
            return result.ToString();
        }

        public static string GetColor(this Randomizer randomizer)
        {
            string[] colors = { "红色", "蓝色", "黄色", "绿色" };
            int index = randomizer.Number(0, colors.Length - 1); // 随机生成一个索引

            return colors[index];
        }

    }
    public static class FakerExtension
    {

        public static double CustomDouble(this Faker faker, double min = 0.0, double max = 1.0, int decimalPlaces = 2)
        {
            Randomizer randomizer = new Randomizer();
            return randomizer.GetDoubleNumber(min, max, decimalPlaces);
        }
    }
}
