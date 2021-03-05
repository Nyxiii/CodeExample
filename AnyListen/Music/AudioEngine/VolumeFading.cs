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
        private readonly AutoResetEvent _canceledWaiter;

        protected async Task Fade(float from, float to, TimeSpan duration, bool getLouder, ISoundOut soundout)
        {
            IsFading = true;
            float different = Math.Abs(to - from);
            float step = different / ((float)duration.TotalMilliseconds / 20);
            float currentvolume = from;

            for (int i = 0; i < duration.TotalMilliseconds / 20; i++)
            {
                if (_cancel) { _cancel = false; OnCancelled(); break; }
                await Task.Delay(20);
                if (getLouder) { currentvolume += step; } else { currentvolume -= step; }
                if (currentvolume < 0 || currentvolume > 1) break;
                try
                {
                    soundout.Volume = currentvolume;
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
            }
            IsFading = false;
        }

        #region Cancel
        protected virtual void OnCancelled()
        {
            if (!_isDisposed) _canceledWaiter.Set();
        }

        public void WaitForCancel()
        {
            if (IsFading)
            {
                try
                {
                    _canceledWaiter.WaitOne(50);
                }
                catch (ObjectDisposedException)
                {
                    //ignore
                }
            }
        }

        public void CancelFading()
        {
     