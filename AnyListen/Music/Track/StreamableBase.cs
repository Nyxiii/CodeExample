using System.Diagnostics;
using System.Windows.Media;
using AnyListen.ViewModelBase;

namespace AnyListen.Music.Track
{
    public abstract class StreamableBase : PlayableBase, IDownloadable
    {
        public override TrackType TrackType => TrackType.Stream;

        publ