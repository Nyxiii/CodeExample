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
            }
        }

        /// <summary>
        /// Check if the current user has administrator rights
        /// </summary>
        /// <returns>If the current user has administrator rights</returns>
        public static bool IsRunningWithAdministratorRights()
        {
            var localAdminGroupSid = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
            var windowsIdentity = WindowsIdentity.GetCurrent();
            if (windowsIdentity == null || windowsIdentity.Groups == null) return false;
            return windowsIdentity.Groups.Select(g => (SecurityIdentifier)g.Translate(typeof(SecurityIdentifier))).Any(s => s == localAdminGroupSid);
        }

        /// <summary>
        /// Creates a new short cut for to the <see cref="targetPath"/>
        /// </summary>
        /// <param name="path">The save location for the shortcut</param>
        /// <param name="targetPath">The destination of the shortcut</param>
        /// <param name="iconLocation">The location of the icon for the shortcut</param>
        public static void CreateShortcut(string path, string targetPath, string iconLocation)
        {
            var t = Type.GetTypeFromCLSID(new Guid("72C24DD5-D70A-438B-8A42-98424B88AFB8")); //Windows Script Host Shell Object
            dynamic shell = Activator.CreateInstance(t);
            try
            {
                var lnk = shell.CreateShortcut(path);
                try
                {
                    lnk.TargetPath = targetPath;
                    lnk.IconLocation = iconLocation;
                    lnk.Save();
                }
                finally
                {
                    Marshal.FinalReleaseComObject(lnk);
                }
            }
            finally
            {
                Marshal.FinalReleaseComObject(shell);
            }
        }

        /// <summary>
        /// Check if the file can be opened
        /// </summary>
        /// <param name="fileName">The path to the file</param>
        /// <returns>If the file can be opened</returns>
        public static bool IsFileReady(string fileName)
        {
            