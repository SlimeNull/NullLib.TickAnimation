using System;
using System.ComponentModel.DataAnnotations;
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
        static async Task FuckMethod(FuckClass fuckClass)
        {
            await Task.Run(async () =>
            {
                var draw = () =>
                {
                    const int max = FuckClass.MAX_CHAR;
                    const int neg = FuckClass.NEG_CHAR;
                    int len = FuckClass.PROGRESS.Length;
                    int progress = (int)(fuckClass.ToChange * max * len);
                    Console.Write('\r');
                    if (progress >= 0)
                    {
                        int char_len = progress / len;

                        Console.Write(new string(' ', neg));
                        Console.Write(new string(FuckClass.PROGRESS[len - 1], char_len));
                        Console.Write(FuckClass.PROGRESS[progress % len]);
                        Console.Write(new string(' ', Math.Max(0, max - char_len)));
                        Console.Write($"{fuckClass.ToChange * 100:###.00}%    ");
                    }
                    else
                    {
                        int char_len = Math.Min(neg, -progress / len);
                        Console.Write(new string(' ', neg - char_len - 1));
                        Console.Write(FuckClass.PROGRESS[-progress % len]);
                        Console.Write(new string(FuckClass.PROGRESS[len - 1], char_len));
                        Console.Write(new string(' ', max));
                        Console.Write($"{fuckClass.ToChange * 100:###.00}%    ");
                    }
                };
                //int randint = new Random().Next(100);
                while (fuckClass.Conitnue)
                {
                    draw();
                    await Task.Delay(100);
                }
                draw();
            });
        }
        class FuckClass
        {
            public const int MAX_CHAR = 60;
            public const int NEG_CHAR = 5;
            public const string PROGRESS = " :\\|";
            public bool Conitnue { get; set; } = true;
            public double ToChange { get; set; } = 0;
        }
        static async Task Main(string[] args)
        {
            Console.ReadLine();
            FuckInit(out var fuck);
            var task = FuckMethod(fuck);
            var endTask = AnimatorBuilder
                .Builder()
                .Binding(fuck, nameof(fuck.Conitnue))
                .Linear()
                .Animate(false, 5000);
            await AnimatorBuilder
                .Builder()
                .Binding(fuck, nameof(fuck.ToChange))
                .Ticker
                (builder =>
                    builder
                        .CubicBezier(CubicBezierCurve.Back, EasingMode.Ease)
                        .Build()
                )
                .Animate(1.0, 5000);

            await Task.Delay(TimeSpan.FromSeconds(0.3));
            await task;

            Console.WriteLine();

            await endTask;
        }
    }
}
