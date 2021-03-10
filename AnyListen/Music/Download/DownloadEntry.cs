using AnyListen.Music.Track;
using AnyListen.ViewModelBase;

namespace AnyListen.Music.Download
{
    public class DownloadEntry : PropertyChangedBase, IDownloadable
    {
        public DownloadSettings DownloadSettings { get; set; }

        private bool _isWaiting;
        public bool IsWaiting
        {
            get { return _isWaiting; }
            set
            {
                SetProperty(value, ref _isWaiting);
            }
        }
        
        private double _progress;
        public double Progress
        {
            get { return _progress; }
            set
            {
                SetProperty(value, ref _progress);
            }
        }
        
        private bool _isDown