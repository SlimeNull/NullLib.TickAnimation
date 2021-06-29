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

        ITickAnimator SyncAnimate<VT>(Func<double, VT> tickPicker, TimeSpan timeSpan);
        Task Animate<VT>(Func<double, VT> tickPicker, TimeSpan timeSpan);
    }
    public interface ITickAnimator<T> : ITickAnimator
    {
        Task Animate(T start, T end, int dur);
        ITickAnimator<T> SyncAnimate(T start, T end, int dur);

        Task Animate(T end, int dur);
        ITickAnimator<T> SyncAnimate(T end, int dur);
    }
}
