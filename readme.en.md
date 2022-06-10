NullLib.TickAnimation is used to apply a smooth transition from one value to another value to some Object's Property with a specified timing function.

For example, you can use it to apply a transition to the `Bounds` Property of a WinForm Form to make a simple animation, this is a simple example.

![preview](img/preview.gif)

## Getting started

1. First of all, create a WinForm project for testing, add a button to the main form, and an event listener to the button for click event.

2. Right click the project in solution manager, select 'Manage nuget Packages', then install NullLib.TickAnimation.Drawing (It will automatic install the dependency NullLib.TickAnimation)

3. Open source code of main form, add `using NullLib.TickAnimation` to `using` block, and then we can use the classes about animation.

4. Edit the event listener code we added, and paste these code:
   
   ```csharp
   Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;        // get the main screen working area rectangle
   DrawingTickAnimator animator = new DrawingTickAnimator(new SineTicker(), this, nameof(Bounds));   // create animation controller instance
   animator.SetPropertySetter((setAction) => Invoke(setAction));    // solve resource cross-thread problem
   animator.SetTickDelay(1);                                        // delay 1ms per frame
   animator.Animate(workingArea, 200);                              // begin animation, time is 200ms
   ```

5. Run this program, click the button, and see what happened

> open sourced in GitHub, [github.com/SlimeNull/NullLib.TickAnimation](https://github.com/SlimeNull/NullLib.TickAnimation)

## Basic principle

In NullLib.TickAnimation, ITickAnimator is the basic interface for running animation, it depened ITicker to provide timing functions, ITicker can represent a motion curve, different classes has different effects, for example, when the ITickAnimator running a animation with a BackTicker, it will has a springback effect, and if it's using BounceTicker, it has a bounce effect.

The technical details of it, see CSS transition timing-function, WPF Application EasingFunction. By the way, propose to look at bezier curves, so that you can use CubicBezierTicker to create custom timing function, and implement custom animation effects.

> Recommended cubic bezier curve online edit tool: [Cubic-Bezier](https://cubic-bezier.com/)

## Types

### - ITicker

ITicker is animation timing function, there are two types of it, bezier curve function and build-in function. And bezier curve function can be change it's form by specify it's control points, build-in function is some function defined in the library, which implement ITicker interface.

1. Bezier curve funtion: Two kinds, cubic bezier curve(CubicBezierTicker)and quadratic bezier curve(QuadraticBezierTicker), you can specify the control points, or use curves provided by default, such as Ease, EaseIn, EaseOut, EaseInOut, InSine, OutSine and so on. 
   
   To use EaseInout, specify CubicBezierCurve.Ease and EasingMode.EaseInOut, by the same way, to use InOutBack, you should specify CubicBezierCurve.Back and EasingMode.EaseInOut. Something need to know is these curves are from CSS3 build-in curves(Ease, EaseIn, EaseOut, EaseInOut, Linear), and Microsoft Edge Browser debug tools build-in CSS3 transition build-in curves(InSine, OutSine, InOutSine, InBack, OutBack, ......)

2. 原生函数: 它们都是从 WPF 中移植过来的, 例如 BounceTicker, 它来自 WPF 中的 BounceEase, 并且与它算法完全一致, 关于它们的使用, 我建议查阅 WPF 动画缓动函数的相关文档.
   
   最简单的, 使用 BackTicker 函数就是直接实例化一个对象, 在这里需要提到的是, 关于 BackTicker 实例的属性默认值也是与 WPF 一致的, 虽然 BackTicker 和 CubicBezierCurve.Back 都是表示带有回弹效果的曲线, 但是两者并不相等, 贝塞尔曲线函数和原生函数是截然不同的.

关于 ITicker 的更多信息, 我建议查阅源码, 或者在 Visual Studio 中使用对象浏览器概览所有类型.

### - ITickAnimator

ITickAnimator 是驱动动画的最主要的类, 默认包含的 TickAnimator 中支持 double float int 三种数字类型的动画处理,  它其中还包括了对于动画同时进行时的中断处理, 当然, 如果你要使用针对于绘画的动画处理, 例如点, 尺寸, 矩形区域, 颜色这类, 可以使用 DrawingTickAnimator (nuget包: NullLib.TickAnimator.Drawing, 命名空间: NullLib.TickAnimator)

> TickAnimator 的动画处理中包含这样一点: 当一个动画还未结束就启动另一个动画, 那么第一个动画会自动停止以保证不会造成冲突, 这是因为一个 TickAnimator 应该针对于唯一一个属性, 但针对同一个属性的两个动画同时启动, 将会造成严重的冲突问题. (这里需要提一下, 如果你要同时对一个控件进行位置过渡和尺寸过渡, 我不推荐你为 Location 和 Size 属性分别创建一个 ITickAnimator 实例, WinForm 的内部实现问题仍然会导致冲突, 更安全的做法是为 Bounds 属性创建 ITickAnimator 并且使用针对 Rectangle 的过渡)

### - TickAnimationProc

TickAnimationProc 是 NullLib.TickAnimation 中对于动画驱动的最核心部分, 它提供了最为基本的动画实现方式, 通过指定 ITicker(计时函数), Fucn<double, T>(tick picker用于从动画进度中取得对应类型的值), TimeSpan(动画的时间间隔), Func<T, bool>(动画每一帧的回调函数, 返回值表示动画是否应该继续进行), 这四个参数来运行最基本的动画. 但是一般的不推荐使用这个, 因为其中没有像 TickAnimator 的互斥措施.

### - TickAnimatorBase

TickAnimator 的基类, 其中包含了 TickAnimator 基础功能的实现, 例如对于值过渡的方法封装

## 自定义类型

如果你要自定义 ITicker 实现类或者 ITickAnimator 实现类, 则需要遵守下面的一些规范:

1. 对于 ITicker 的实现, 必须保证对于方法 CalcTick(double x), x 从 0 到 1 时, 返回值也是从 0 到 1, 即: CalcTick(0) 应该返回 0, CalcTick(1) 应该返回 1.
   
   并且, 如果你要创建更高阶的贝塞尔曲线函数, 建议继承 BezierTickerBase, 如果你要创建一些遵循 WPF 原生过渡函数规则的自定义函数, 必须继承 FuncTickerBase 并在字类中重写 CalcInTick(double x) 方法.

2. 对于 ITickAnimator 的实现, 推荐的方式是直接继承 TickAnimator, 并且在实现 Animate 和 SyncAnimate 方法时, 应该通过调用 TickAnimatorBase 的 Animate 和 SyncAnimate 方法来实现动画, 而获取对应属性的当前值, 不应该使用 prop.GetValue 方法, 而是使用 TickAnimatorBase 的 GetPropertyValue 方法. 下面是 TickAnimator 中 double 的动画实现:
   
   ```csharp
   public Task Animate(float start, float end, int dur)
   {
       double diff = end - start;
       return Animate((t) => (float)(start + diff * t), TimeSpan.FromMilliseconds(dur));    // 内部调用基类 TickAnimatorBase 的方法
   }
   public Task Animate(double end, int dur) => Animate(GetPropertyValue<double>(), end, dur);    // 从当前值开始执行动画, 按照规
   public ITickAnimator<double> SyncAnimate(double start, double end, int dur)                   // 范, 使用 GetPropertyValue 方法
   {
       double diff = end - start;
       SyncAnimate((t) => (double)(start + diff * t), TimeSpan.FromMilliseconds(dur));      // SyncAnimate 也一样
       return this;
   }
   public ITickAnimator<double> SyncAnimate(double end, int dur) => SyncAnimate(GetPropertyValue<double>(), end, dur);  // 一致
   ```