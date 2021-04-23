using AnyListen.Music.Download;

namespace AnyListen.Music.Track
{
    public interface IDownloadable
    {
        string DownloadParameter { get; }
        st