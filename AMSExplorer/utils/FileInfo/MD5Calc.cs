using System;
using System.IO;
using System.Security.Cryptography;

namespace AMSExplorer.Utils.FileInfo
{
    class MD5Calc
    {
        public static string GetFileContentMD5(string filename)
        {
            using (FileStream fs = File.Open(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                MD5 md5 = MD5.Create();
                byte[] md5Hash = md5.ComputeHash(fs);

                return Convert.ToBase64String(md5Hash);
            }
        }
    }
}
