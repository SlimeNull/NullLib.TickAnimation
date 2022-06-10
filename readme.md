NullLib.TickAnimation 用于实现对一个对象的属性(Property), 以指定的计时函数, 在一定时间内从一个值, 平滑的过渡到另一个值.

例如你可以使用它将一个 WinForm 窗体的 `Bounds` 从一个值过渡到另一个值以实现窗体位置与尺寸的过渡动画. 下面是一个简单示例:

![preview](img/preview.gif)

## 快速开始

1. 首先, 创建一个 WinForm 项目用于测试, 向主窗体中添加一个按钮, 并为按钮的点击事件添加事件处理器

2. 在解决方案管理器中右击项目, 选择 "管理 nuget 程序包", 然后安装 NullLib.TickAnimation.Drawing (它会自动安装依赖项 NullLib.TickAnimation)

3. 转到主窗体的代码, 在 using 部分添加 `using NullLib.TickAnimation`, 这样我们就可以直接使用关于动画的一些类了

4. 编辑刚刚添加的按钮的点击事件处理器代码, 添加以下内容:
   
   ```csharp
   Rectangle workingArea = Screen.PrimaryScreen.WorkingArea;        // 获取主屏幕的工作区矩形
   DrawingTickAnimator animator = new DrawingTickAnimator(new SineTicker(), this, nameof(Bounds));   // 创建动画控制器实例
   animator.SetPropertySetter((setAction) => Invoke(setAction));    // 解决窗体程序的跨线程资源访问问题
   animator.SetTickDelay(1);                                        // 在每一帧后进行 1ms 的延时
   animator.Animate(workingArea, 200);                              // 进行动画, 时间是 200ms
   ```

5. 运行程序, 点击按钮, 查看效果

> 本库已在 GitHub 开源, [github.com/SlimeNull/NullLib.TickAnimation](https://github.com/SlimeNull/NullLib.TickAnimation)

## 基本原理

在 NullLib.TickAnimation 中, ITickAnimator 是最基本的, 用于运行动画的接口, 它依赖于 ITicker 来提供计时函数, ITIcker 可表示运动曲线, 不同的类有不同的特征, 例如使用 BackTicker 的 ITickAnimator 进行动画时将具有回弹效果, 使用 BounceTicker 的 ITickAnimator 进行动画时将具有弹跳效果.

其中的技术细节, 可参考 CSS3 过渡中的 timing-function, WPF 窗体程序中的 EasingFunction. 另外, 也推荐对贝塞尔曲线作基本了解, 这样, 你可以通过库中提供的 CubicBezierTicker 来创建自定义的三次贝塞尔曲线计时函数, 进而实现自定义动画效果.

> 推荐的在线三次贝塞尔曲线编辑工具: [Cubic-Bezier](https://cubic-bezier.com/)

## 类型

### - ITicker

ITicker 是动画计时函数, 它分为两种, 贝塞尔曲线函数以及原生函数, 其中贝塞尔曲线是可以由用户指定控制点从而控制曲线形状的, 原生函数是一些定义好的, 遵循 ITicker 接口的函数

1. 贝塞尔曲线函数: 分两种, 三次贝塞尔曲线(CubicBezierTicker)与二次贝塞尔曲线(QuadraticBezierTicker), 你可以手动指定控制点, 也可以使用默认提供的一些曲线, 例如 Ease, EaseIn, EaseOut, EaseInOut, InSine, OutSine 等. 
   
   指定 EaseInOut 的方式是: 指定 CubicBezierCurve.Ease 与 EasingMode.EaseInOut, 同理, 指定 InOutBack 的方式是: CubicBezierCurve.Back 与 EasingMode.EaseInOut, 需要知道的是, 这些曲线都是源自于 CSS3 内置曲线(Ease, EaseIn, EaseOut, EaseInOut, Linear) 以及 Microsoft Edge 浏览器调试工具中的预设曲线(InSIne, OutSine, InOutSine, InBack, OutBack, ......)

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