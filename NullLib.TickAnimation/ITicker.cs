namespace NullLib.TickAnimation
{
    public interface ITicker
    {
        double[] GetTicks();
        double CalcTick(double x);
    }
}
