using System;
using System.IO;
using System.Xml.Serialization;
using AnyListen.Music.Track;

namespace AnyListen.Music.Download
{
    public class DownloadSettings
    {
        public bool AddTags { get; set; }

        private string _downloadFolder;
        [XmlIgnore]
        public string DownloadFolder
        {
            get
            {
                if (string.IsNullOrEmpty(_downloadFolder) || !Directory.Exists(_downloadFolder))
                    return Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);

                return _downloadFolder;
            }