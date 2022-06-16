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


        public static async Task Animate(ITicker ticker, TimeSpan timeSpan, Func<double, Task<bool>> callback)
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
                goon = await callback.Invoke(ticker.CalcTick(xrate));
            }
            await callback.Invoke(1);
        }
        public static async Task Animate(ITicker ticker, TimeSpan timeSpan, Func<double, Task> callback)
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
                await callback.Invoke(ticker.CalcTick(xrate));
            }
            await callback.Invoke(1);
        }
        public static Task Animate<T>(ITicker ticker, Func<double, T> tickGetter, TimeSpan timeSpan, Func<T, Task<bool>> callback)
        {
            return Animate(ticker, timeSpan, v => callback(tickGetter(v)));
        }
        public static Task Animate<T>(ITicker ticker, Func<double, T> tickGetter, TimeSpan timeSpan, Func<T, Task> callback)
        {
            return Animate(ticker, timeSpan, v => callback(tickGetter(v)));
        }
    }
}
