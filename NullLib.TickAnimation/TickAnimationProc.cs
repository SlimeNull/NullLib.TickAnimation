using System;
using System.Threading.Tasks;

namespace NullLib.TickAnimation
{
    public class TickAnimationProc
    {
        public static void SyncAnimate(ITicker ticker, TimeSpan timeSpan, Action<double> callback)
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
                callback.Invoke(ticker.CalcTick(xrate));
            }
            callback.Invoke(1);
        }
        public static void SyncAnimate(ITicker ticker, TimeSpan timeSpan, Func<double, bool> callback)
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
                goon = callback.Invoke(ticker.CalcTick(xrate));
            }
            callback.Invoke(1);
        }
        public static void SyncAnimate<T>(ITicker ticker, Func<double, T> tickGetter, TimeSpan timeSpan, Action<T> callback)
        {
            SyncAnimate(ticker, timeSpan, v => callback(tickGetter(v)));
        }
        public static void SyncAnimate<T>(ITicker ticker, Func<double, T> tickGetter, TimeSpan timeSpan, Func<T, bool> callback)
        {
            SyncAnimate(ticker, timeSpan, v => callback(tickGetter(v)));
        }


        public static Task Animate(ITicker ticker, TimeSpan timeSpan, Func<double, bool> callback)
        {
            return Task.Run(() => SyncAnimate(ticker, timeSpan, callback));
        }
        public static Task Animate(ITicker ticker, TimeSpan timeSpan, Action<double> callback)
        {
            return Task.Run(() => SyncAnimate(ticker, timeSpan, callback));
        }
        public static Task Animate<T>(ITicker ticker, Func<double, T> tickGetter, TimeSpan timeSpan, Func<T, bool> callback)
        {
            return Task.Run(() => SyncAnimate(ticker, tickGetter, timeSpan, callback));
        }
        public static Task Animate<T>(ITicker ticker, Func<double, T> tickGetter, TimeSpan timeSpan, Action<T> callback)
        {
            return Task.Run(() => SyncAnimate(ticker, tickGetter, timeSpan, callback));
        }
    }
}
