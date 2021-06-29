namespace NullLib.TickAnimation
{
    public abstract class FuncTickerBase : ITicker
    {
        private EasingMode easingMode;

        public EasingMode EasingMode
        {
            get => easingMode; set
            {
                easingMode = value;
            }
        }

        public double CalcTick(double x)
        {
            switch (EasingMode)
            {
                case EasingMode.EaseIn:
                    return CalcInTick(x);
                case EasingMode.EaseOut:
                    return 1.0 - CalcInTick(1.0 - x);
                default:
                    if (!(x < 0.5))
                        return (1.0 - CalcInTick((1.0 - x) * 2.0)) * 0.5 + 0.5;
                    return CalcInTick(x * 2.0) * 0.5;
            }
        }

        protected abstract double CalcInTick(double x);

        protected FuncTickerBase() { }
        protected FuncTickerBase(EasingMode mode) => easingMode = mode;
    }
}