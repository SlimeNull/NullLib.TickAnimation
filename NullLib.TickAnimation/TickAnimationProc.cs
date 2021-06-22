using System;
using System.Threading.Tasks;

namespace NullLib.TickAnimation
{
    public class TickAnimationProc
    {
        public static void SyncFrameAnimate<T>(ITicker ticker, Func<double, T> tickGetter, TimeSpan timeSpan, Func<T, bool> callback)
        {
            double[] ticks = ticker.GetTicks();

            TimeSpan perSpan = TimeSpan.FromTicks(timeSpan.Ticks / ticks.Length);
            DateTime nextTime = DateTime.Now + perSpan;

            int index = 0, tickend = ticks.Length;
            bool goon = true;

            while (goon && index < tickend)
            {
                while (DateTime.Now < nextTime) ;
                goon = callback.Invoke(tickGetter.Invoke(ticks[index]));
                nextTime += perSpan;
                index++;
            }
        }
        public static void SyncSmoothAnimate<T>(ITicker ticker, Func<double, T> tickGetter, TimeSpan timeSpan, Func<T, bool> callback)
        {
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime + timeSpan;
            DateTime now;

            double xrate;
            double
                startTicks = startTime.Ticks,
                spanTicks = timeSpan.Ticks;
            bool goon = true;

            while (goon && (now = DateTime.Now) < endTime)
            {
                xrate = (now.Ticks - startTicks) / spanTicks;
                goon = callback.Invoke(tickGetter.Invoke(ticker.CalcTick(xrate)));
            }
            callback.Invoke(tickGetter.Invoke(1));
        }
        public static void SyncFrameAnimate<T>(ITicker ticker, Func<double, T> tickGetter, TimeSpan timeSpan, Action<T> callback)
        {
            double[] ticks = ticker.GetTicks();

            TimeSpan perSpan = TimeSpan.FromTicks(timeSpan.Ticks / ticks.Length);
            DateTime nextTime = DateTime.Now + perSpan;

            int index = 0, tickend = ticks.Length;
            while (index < tickend)
            {
                while (DateTime.Now < nextTime) ;
                callback.Invoke(tickGetter.Invoke(ticks[index]));
                nextTime += perSpan;
                index++;
            }
        }
        public static void SyncSmoothAnimate<T>(ITicker ticker, Func<double, T> tickGetter, TimeSpan timeSpan, Action<T> callback)
        {
            DateTime startTime = DateTime.Now;
            DateTime endTime = startTime + timeSpan;
            DateTime now;

            double xrate;
            double
                startTicks = startTime.Ticks,
                spanTicks = timeSpan.Ticks;

            while ((now = DateTime.Now) < endTime)
            {
                xrate = (now.Ticks - startTicks) / spanTicks;
                callback.Invoke(tickGetter.Invoke(ticker.CalcTick(xrate)));
            }
            callback.Invoke(tickGetter.Invoke(1));
        }
        public static async Task FrameAnimate<T>(ITicker ticker, Func<double, T> tickGetter, TimeSpan timeSpan, Func<T, bool> callback)
        {
            await Task.Run(() => SyncFrameAnimate(ticker, tickGetter, timeSpan, callback));
        }
        public static async Task SmoothAnimate<T>(ITicker ticker, Func<double, T> tickGetter, TimeSpan timeSpan, Func<T, bool> callback)
        {
            await Task.Run(() => SyncSmoothAnimate(ticker, tickGetter, timeSpan, callback));
        }
        public static async Task FrameAnimate<T>(ITicker ticker, Func<double, T> tickGetter, TimeSpan timeSpan, Action<T> callback)
        {
            await Task.Run(() => SyncFrameAnimate(ticker, tickGetter, timeSpan, callback));
        }
        public static async Task SmoothAnimate<T>(ITicker ticker, Func<double, T> tickGetter, TimeSpan timeSpan, Action<T> callback)
        {
            await Task.Run(() => SyncSmoothAnimate(ticker, tickGetter, timeSpan, callback));
        }
    }
}
