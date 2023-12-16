using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace IgniteX.Common
{
    public class MD5Encrypt
    {
        private MD5 md5;
        /// <summary>
        /// 构造函数
        /// </summary>
        public MD5Encrypt()
        {
            md5 = new MD5CryptoServiceProvider();
        }
        /// <summary>
        /// 从字符串中获取散列值
        /// </summary>
        /// <param name="str">要计算散列值的字符串</param>
        /// <returns></returns>
        public string GetMD5FromString(string str)
        {
            byte[] toCompute = Encoding.Unicode.GetBytes(str);
            byte[] hashed = md5.ComputeHash(toCompute, 0, toCompute.Length);
            return Encoding.ASCII.GetString(hashed);
        }
        /// <summary>
        /// 根据文件来计算散列值
        /// </summary>
        /// <param name="filePath">要计算散列值的文件路径</param>
        /// <returns></returns>
        public string GetMD5FromFile(string filePath)
        {
            bool isExist = File.Exists(filePath);
            if (isExist)//如果文件存在
            {
                FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                StreamReader reader = new StreamReader(stream, Encoding.Unicode);
                string str = reader.ReadToEnd();
                byte[] toHash = Encoding.Unicode.GetBytes(str);
                byte[] hashed = md5.ComputeHash(toHash, 0, toHash.Length);
                stream.Close();
                return Encoding.ASCII.GetString(hashed);
            }
            else//文件不存在
            {
                throw new FileNotFoundException("File not found!");
            }
        }

        /// <summary>
        /// 使用MD5加盐加密
        /// </summary>
        /// <param name="source"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public string GetMD5Provider(string source, string salt, [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0)
        {

            string result = "";
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(source + salt));
                result = BitConverter.ToString(bytes).Replace("-", "");
            }
            return result;
        }
    }
}
