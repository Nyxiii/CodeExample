using System;
using System.Threading;
using System.Threading.Tasks;
using CSCore.SoundOut;

namespace AnyListen.Music.AudioEngine
{
    public class VolumeFading : IDisposable
    {
        public bool IsFading { get; set; }
        private bool _cancel;
        public double OutDuration { get; set; }
        private readonly AutoResetEvent _c