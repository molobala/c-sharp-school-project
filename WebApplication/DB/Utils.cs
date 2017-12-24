using System;
using System.Security.Cryptography;
using System.Text;

namespace DB
{
    public static class Utils
    {
        public static string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "Molobala" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }
    }
}