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
            return ConvertFile(fileName, newFileName, settings.Bitrate, settings.Format);
        }

        /// <summary>
        /// Converts the file
        /// </summary>
        /// <param name="fileName">The path to the file which should become converted</param>
        /// <param name="newFileName">The name of the new file WITHOUT extension</param>
        /// <param name="bitrate">The audio bitrate</param>
        /// <param name="format"></param>
        public static async Task ConvertFile(string fileName, string newFileName, AudioBitrate bitrate, AudioFormat format)
        {
            var fileToConvert = new FileInfo(fileName);

            var p = new Process
            {
                StartInfo =
   