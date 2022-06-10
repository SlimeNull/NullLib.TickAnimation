using System;
using System.Collections;
using System.Collections.Generic;

namespace NullLib.TickAnimation
{
    public interface ITicker
    {
        double CalcTick(double x);
    }
    public abstract class TickerBase : ITicker
    {
        public abstract double CalcTick(double x);

        public class TickEnumerator : IEnumerator<double>
        {
            private readonly ITicker ticker;
            private readonly int duration;

            double startTicks = DateTime.Now.Ticks;
            double spanTicks;
            double endTicks;
            double nowTicks;

            bool end = false;

            public ITicker Ticker => ticker;
            public int Duration => duration;
            public TickEnumerator(ITicker ticker, int dur)
            {
                this.ticker = ticker;
                this.duration = dur;

                spanTicks = TimeSpan.TicksPerMillisecond * dur;
                endTicks = startTicks + spanTicks;
            }


            double xrate;
            public double GetCurrent()
            {
                xrate = (nowTicks - startTicks) / spanTicks;
                return ticker.CalcTick(xrate);
            }

            object IEnumerator.Current => GetCurrent();

            public double Current => GetCurrent();

            public void Dispose()
            {
                // nothing to do.
            }

            public bool MoveNext()
            {
                nowTicks = DateTime.Now.Ticks;
                if (nowTicks < startTicks)
                    return true;

                if (end)
                    return false;

                nowTicks = startTicks;
                end = true;
                return true;
            }

            public void Reset()
            {
                startTicks = DateTime.Now.Ticks;
                end = false;
            }
        }

        public IEnumerator<double> Enumerate(int dur)
        {
            return new TickEnumerator(this, dur);
        }
    }
}
