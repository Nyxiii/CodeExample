using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AnyListen.Utilities
{
    public static class GeneralHelper
    {
        /// <summary>
        /// Function to get file impression in form of string from a file location.
        /// </summary>
        /// <param name="filename">File Path to get file impression.</param>
        /// <returns>Byte Array</returns>
        // ReSharper disable once InconsistentNaming
        public static string FileToMD5Hash(string filename)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filename))
                {
                    return BitConverter.ToString(md5.ComputeHash(stream));
                }
            }
        }

        /// <summary>
        /// Check if the internet is available. It connects to google.com
        /// </summary>
        /// <returns>If the connection to google was successful</returns>
        public async static Task<bool> CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient { Proxy = null })
                using (await client.OpenReadTaskAsync("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
   