using System;
using System.Reflection;
using System.Threading.Tasks;

namespace NullLib.TickAnimation
{
    public interface ITickAnimator
    {
        void SetTicker(ITicker ticker);
        void SetTargetInstance(object obj);
        void SetTargetProperty(string propName);
        void SetTargetProperty(PropertyInfo prop);
        void SetPropertySetter(Action<Action> setter);

        void SyncFrameAnimate<VT>(Func<double, VT> tickPicker, TimeSpan timeSpan);
        void SyncSmoothAnimate<VT>(Func<double, VT> tickPicker, TimeSpan timeSpan);
        Task FrameAnimate<VT>(Func<double, VT> tickPicker, TimeSpan timeSpan);
        Task SmoothAnimate<VT>(Func<double, VT> tickPicker, TimeSpan timeSpan);

        Task FrameAnimate(double start, double end, int dur);
        Task SmoothAnimate(double start, double end, int dur);
        Task Animate(double start, double end, int dur);
        void SyncFrameAnimate(double start, double end, int dur);
        void SyncSmoothAnimate(double start, double end, int dur);
        void SyncAnimate(double start, double end, int dur);
    }
    public interface ITickAnimator<T> : ITickAnimator
    {
        Task FrameAnimate(T start, T end, int dur);
        Task SmoothAnimate(T start, T end, int dur);
        Task Animate(T start, T end, int dur);
        void SyncFrameAnimate(T start, T end, int dur);
        void SyncSmoothAnimate(T start, T end, int dur);
        void SyncAnimate(T start, T end, int dur);
    }
}
