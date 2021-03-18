using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;
using AnyListen.Settings;

namespace AnyListen.Music.Download
{
    class ffmpeg
    {
        /// <summary>
        /// Converts the file
        /// </summary>
        /// <param name="fileName">The path to the file which should become converted</param>
        /// <param name="newFileName">The name of the new file WITHOUT extension</param>
        /// <param name="settings"></param>
        public static Task ConvertFile(string fileName, string newFileName, DownloadSettings settings)
        {
            return 