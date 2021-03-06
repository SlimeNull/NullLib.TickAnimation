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

2. ????????????: ??????????????? WPF ??????????????????, ?????? BounceTicker, ????????? WPF ?????? BounceEase, ??????????????????????????????, ?????????????????????, ??????????????? WPF ?????????????????????????????????.
   
   ????????????, ?????? BackTicker ???????????????????????????????????????, ???????????????????????????, ?????? BackTicker ????????????????????????????????? WPF ?????????, ?????? BackTicker ??? CubicBezierCurve.Back ???????????????????????????????????????, ????????????????????????, ??????????????????????????????????????????????????????.

?????? ITicker ???????????????, ?????????????????????, ????????? Visual Studio ??????????????????????????????????????????.

### - ITickAnimator

ITickAnimator ?????????????????????????????????, ??????????????? TickAnimator ????????? double float int ?????????????????????????????????,  ???????????????????????????????????????????????????????????????, ??????, ????????????????????????????????????????????????, ?????????, ??????, ????????????, ????????????, ???????????? DrawingTickAnimator (nuget???: NullLib.TickAnimator.Drawing, ????????????: NullLib.TickAnimator)

> TickAnimator ????????????????????????????????????: ???????????????????????????????????????????????????, ???????????????????????????????????????????????????????????????, ?????????????????? TickAnimator ?????????????????????????????????, ???????????????????????????????????????????????????, ?????????????????????????????????. (?????????????????????, ??????????????????????????????????????????????????????????????????, ?????????????????? Location ??? Size ???????????????????????? ITickAnimator ??????, WinForm ??????????????????????????????????????????, ???????????????????????? Bounds ???????????? ITickAnimator ?????????????????? Rectangle ?????????)

### - TickAnimationProc

TickAnimationProc ??? NullLib.TickAnimation ???????????????????????????????????????, ?????????????????????????????????????????????, ???????????? ITicker(????????????), Fucn<double, T>(tick picker????????????????????????????????????????????????), TimeSpan(?????????????????????), Func<T, bool>(??????????????????????????????, ?????????????????????????????????????????????), ??????????????????????????????????????????. ????????????????????????????????????, ????????????????????? TickAnimator ???????????????.

### - TickAnimatorBase

TickAnimator ?????????, ??????????????? TickAnimator ?????????????????????, ????????????????????????????????????

## ???????????????

????????????????????? ITicker ??????????????? ITickAnimator ?????????, ????????????????????????????????????:

1. ?????? ITicker ?????????, ???????????????????????? CalcTick(double x), x ??? 0 ??? 1 ???, ?????????????????? 0 ??? 1, ???: CalcTick(0) ???????????? 0, CalcTick(1) ???????????? 1.
   
   ??????, ???????????????????????????????????????????????????, ???????????? BezierTickerBase, ?????????????????????????????? WPF ??????????????????????????????????????????, ???????????? FuncTickerBase ????????????????????? CalcInTick(double x) ??????.

2. ?????? ITickAnimator ?????????, ?????????????????????????????? TickAnimator, ??????????????? Animate ??? SyncAnimate ?????????, ?????????????????? TickAnimatorBase ??? Animate ??? SyncAnimate ?????????????????????, ?????????????????????????????????, ??????????????? prop.GetValue ??????, ???????????? TickAnimatorBase ??? GetPropertyValue ??????. ????????? TickAnimator ??? double ???????????????:
   
   ```csharp
   public Task Animate(float start, float end, int dur)
   {
       double diff = end - start;
       return Animate((t) => (float)(start + diff * t), TimeSpan.FromMilliseconds(dur));    // ?????????????????? TickAnimatorBase ?????????
   }
   public Task Animate(double end, int dur) => Animate(GetPropertyValue<double>(), end, dur);    // ??????????????????????????????, ?????????
   public ITickAnimator<double> SyncAnimate(double start, double end, int dur)                   // ???, ?????? GetPropertyValue ??????
   {
       double diff = end - start;
       SyncAnimate((t) => (double)(start + diff * t), TimeSpan.FromMilliseconds(dur));      // SyncAnimate ?????????
       return this;
   }
   public ITickAnimator<double> SyncAnimate(double end, int dur) => SyncAnimate(GetPropertyValue<double>(), end, dur);  // ??????
   ```