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
                using (var stream = File.OpenRead(fi