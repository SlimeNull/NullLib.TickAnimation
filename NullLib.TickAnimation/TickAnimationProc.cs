using System;
using System.Threading.Tasks;

namespace NullLib.TickAnimation
{
    public class TickAnimationProc
    {
        public static void SyncAnimate<T>(ITicker ticker, Func<double, T> tickGetter, TimeSpan timeSpan, Func<T, bool> callback)
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
        public static void SyncAnimate<T>(ITicker ticker, Func<double, T> tickGetter, TimeSpan timeSpan, Action<T> callback)
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
        public static async Task Animate<T>(ITicker ticker, Func<double, T> tickGetter, TimeSpan timeSpan, Func<T, bool> callback)
        {
            await Task.Run(() => SyncAnimate(ticker, tickGetter, timeSpan, callback));
        }
        public static async Task Animate<T>(ITicker ticker, Func<double, T> tickGetter, TimeSpan timeSpan, Action<T> callback)
        {
            await Task.Run(() => SyncAnimate(ticker, tickGetter, timeSpan, callback));
        }
    }
}
