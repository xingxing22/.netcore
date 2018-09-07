using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public static class Utility
    {
        private static readonly byte[] _rgbKey = Encoding.ASCII.GetBytes("gsdgd");//密钥
        private static readonly byte[] _rgbIV = Encoding.ASCII.GetBytes("1236gfdhf");//向量
        
        #region 加解密
        
        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetMD5String(string value)
        {
            using (var md5 = MD5.Create())
            {
                var result = md5.ComputeHash(Encoding.UTF8.GetBytes(value));
                var strResult = BitConverter.ToString(result);
                return strResult.Replace("-", "");
            }
        }

        #region des加解密

        /// <summary>
        /// des加密
        /// </summary>
        /// <param name="plain"></param>
        /// <returns></returns>
        public static string EncryptDES(string plain)
        {
            return EncryptDES(plain, _rgbKey, _rgbIV);
        }

        public static string EncryptDES(string plain, byte[] key64, byte[] vector64)
        {
            if (key64.Length != 8) throw new ArgumentException("长度必须为8。", "key64");
            if (vector64.Length != 8) throw new ArgumentException("长度必须为8。", "vector64");
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key64, vector64), CryptoStreamMode.Write);
            using (StreamWriter sw = new StreamWriter(cs))
            {
                sw.Write(plain);
                sw.Flush();
                cs.FlushFinalBlock();
                ms.Flush();
                return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
            }
        }

        /// <summary>
        /// des解密
        /// </summary>
        /// <param name="cipher"></param>
        /// <returns></returns>
        public static string DecryptDES(string cipher)
        {
            return DecryptDES(cipher, _rgbKey, _rgbIV);
        }

        public static string DecryptDES(string cipher, byte[] key64, byte[] vector64)
        {
            string result = string.Empty;
            byte[] byteCipher = Convert.FromBase64String(cipher);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream(byteCipher);
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key64, vector64), CryptoStreamMode.Read);
            using (StreamReader reader = new StreamReader(cs))
            {
                result = reader.ReadToEnd();
            }
            ms.Close();
            ms.Dispose();
            cs.Close();
            cs.Dispose();
            return result;
        }

        #endregion

        #endregion

        #region DataTable扩展

        /// <summary>
        /// 判断datatable是否有数据
        /// </summary>
        /// <param name="dt">需要判断的datatable</param>
        /// <returns>有数据返回true</returns>
        public static bool TableHelper(DataTable dt)
        {
            return (dt != null && dt.Rows.Count > 0);
        }

        #endregion
    }
}
