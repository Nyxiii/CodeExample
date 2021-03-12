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
            set { _downloadFolder = value; }
        }

        [XmlElement(ElementName = "DownloadFolder")]
        public string DownloadFolder2Serialize
        {
            get { return _downloadFolder; }
            set { _downloadFolder = value; }
        }


        public bool IsConverterEnabled { get; set; }
        public AudioBitrate Bitrate { get; set; }
        public AudioFormat Format { get; set; }

        public string GetExtension(IDownloadable track)
        {
            return GetExtension(DownloadManager.GetExtension(track));
        }

        public string GetExtension(string defaultExtension)
        {
            switch (Format)
            {
                cas