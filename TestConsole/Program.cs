using System;
using System.Threading;
using System.Threading.Tasks;
using NullLib.TickAnimation;

namespace TestConsole
{
    class Program
    {
        static FuckClass obj;
        static void FuckInit(out FuckClass state)
        {
            if (obj != null)
                obj.Conitnue = false;
            state = obj = new FuckClass();
        }
        static async void FuckMethod()
        {
            FuckInit(out var newObj);
            await Task.Run(() =>
            {
                int randint = new Random().Next();
                while (newObj.Conitnue)
                {
                    Console.WriteLine(randint);
                    Thread.Sleep(1000);
                }
            });
        }
        class FuckClass
        {
            public bool Conitnue = true;
            public int ToChange { get; set; }
        }
        static void Main(string[] args)
        {
            CubicBezierTicker ticker = new CubicBezierTicker(CubicCurves.OutBack);
        }
    }
}
