# Unity

## Unity生命周期

https://www.jianshu.com/p/20c39ccd7ff8

https://zhuanlan.zhihu.com/p/374028750

https://docs.unity.cn/cn/current/Manual/ExecutionOrder.html#WhenTheObjectIsDestroyed

**场景第一次加载阶段**

场景开始时被调用，场景中的每个对象调用一次

- **Awake** 方法：始终在任何 Start 方法之前并在实例化预制体之后调用。（如果游戏对象在启动期间处于非活动状态，则在激活之后才会调用 Awake 。）
- **OnEnable** 方法：（仅在对象处于激活状态时调用）在启用对象后立即调用此函数。当一个MonoBehaviour 实例被创建时（例如加载关卡或实例化具有脚本组件的游戏对象时）调用。
- **OnLevelWasLoaded** 方法：执行此函数可告知游戏已加载新关卡。

**编辑器阶段**

- **Reset 方法：**当脚本第一次添加到游戏对象或当执行 Reset 命令时会调用 Reset 方法，用来初始化脚本的各个属性。

**第一帧更新之前的阶段**

- **Start** 方法**：**启用脚本实例后，在第一帧更新之前调用 Start 方法。

注意：对于当前场景中所有的游戏对象而言，Start 方法会在所有脚本中的 Update 方法之前调用。在游戏运行过程中实例化对象时，不能强制执行此调用。

**帧之间的执行阶段**

该阶段就是当前帧执行完毕，需要开始执行下一帧的阶段。

- **OnApplicationPause** 方法**：**当检测到暂停状态时，在当前帧结束之后调用该方法。在调用该方法后，将发出一个额外帧，从而允许游戏显示图形来指示暂停状态。

**更新顺序**

- **FixedUpdate** 方法**：**调用 FixedUpdate 的次数比 Update 更多。帧率比较低时，该方法每帧会被调用多次；帧率比较高时，可能不会每帧都被调用。这也是因为 FixedUpdate 是基于可靠的计时器（独立于帧率，不受帧率影响）。因此，FixedUpdate 主要用来处理物理计算和更新的相关逻辑，例如处理刚体。在 FixedUpdate 内应用运动计算时，无需将值乘以 Time.deltaTime。
- **Update** 方法**：**每帧调用一次 Update。按帧更新的主要方法。
- **LateUpdate** 方法**：**每帧调用一次 LateUpdate（在 Update 执行完成后）。LateUpdate 的一个常见用途就是第三人称的摄像机跟随。如果你把角色的移动和选择放到 Update 中，那么就可以把所有摄像机的移动和旋转放在 LateUpdate 中计算执行。这是为了摄像机追踪角色位置之前，确保角色已经完成移动。

**动画更新循环部分**

略。（详解见官网）

**渲染部分**

- **OnPreCull** 方法**：**在摄像机剔除场景之前调用。剔除取决于物体在摄像机中是否可见。在进行剔除之前调用 OnPreCull。
- **OnBecameVisible 和 OnBecameInvisible** 方法**：**当物体在任何摄像机中可见或不可见时调用。
- **OnWillRenderObject** 方法**：**如果物体可见，则为每个摄像机调用一次。
- **OnPreRender** 方法**：**在摄像机开始渲染场景之前调用。
- **OnRenderObject** 方法**：**所有固定场景渲染之后调用。此时，可以使用 GL 类或 Graphics.DrawMeshNow 来绘制自定义几何形状。
- **OnPostRender** 方法**：**在摄像机完成场景渲染后调用。
- **OnRenderImage** 方法**：**在场景渲染完成后调用，用来对屏幕的图像进行处理。
- **OnGUI** 方法**：**每帧调用，多次用来响应 GUI 事件。布局和重绘事件先被执行，然后为每一次的输入事件执行布局和键盘、鼠标事件。
- **OnDrawGizmos** 方法：用于在场景视图中绘制辅助图标以实现可视化。

![img](https://pic4.zhimg.com/80/v2-fc288dca9d3f5ee1e5f2dbe5dfbe3a3f_720w.jpg)

## RigidBody2d

### Unity某个类访问另一个类成员的几种方式

http://www.ileichun.com/blog/unity3d/736.html

1.单例

```c#
public class PlayerAttack : MonoBehaviour {  

  public static PlayerAttack _instance; // static关键字，单例模式  



  void Awake(){  

​    _instance = this;// 确保在使用前已被初始化  

  }  



  public void TakeDamage(int damage){  // 可被外部调用的方法

​     

  }  

}



public class Enemy : MonoBehaviour {

  void Attack(){    // 当敌人攻击主角时

   PlayerAttack._instance.TakeDamage(20);

  }

}
```

2. static

```c#
public class PlayerAttack : MonoBehaviour {  

  public static void TakeDamage(int damage){  // 可被外部调用的方法

​     

  }  

}
```

3.使用GameObject的SendMessage()

```c#
public class PlayerAttack : MonoBehaviour {  

  public void TakeDamage(int damage){  // 可被外部调用的方法

​     

  }  

}



public class Enemy : MonoBehaviour {  

  void Attack(){  

   // 主角模型的tag为PlayerBoy

​    GameObject p = GameObject.FindGameObjectWithTag("PlayerBoy");

​    p.SendMessage("TakeDamage", 20);  

  }  

}
```

4. 获取到某个对象的Script组件

这个脚本放在cube上 

```c#
public class Score : MonoBehaviour {  

  public  int allScore=100;  

  void Update () {  

​    ...

  }  

}  



访问Score.allScore

public class Text : MonoBehaviour {  

  public GameObject Obj1;  

  void Start () {  

​    Obj1 = GameObject.Find("Cube");  

​    Score script=Obj1.GetComponent<Score>(); // 取对象的Script组件

​    Debug.Log(script.allScore);  

  }  

} 
```

# Unity优化

https://docs.unity.cn/cn/current/Manual/BestPracticeUnderstandingPerformanceInUnity.html

## 性能分析

https://docs.unity.cn/cn/current/Manual/BestPracticeUnderstandingPerformanceInUnity1.html

### 解析启动跟踪

![img](https://docs.unity.cn/cn/current/uploads/Main/UnderstandingPerformanceinUnity-ProfilingSection_image_0.png)

startUnity中含 UnityInitApplicationGraphics, UnityLoadApplication

UnityInitApplicationGraphics: 执行大量内部工作，例如设置图形设备和初始化 Unity 的大量内部系统。此外，它还初始化资源系统 (Resources system)。为此，它必须加载资源系统包含的所有文件的索引。

UnityLoadApplication: 包含加载并初始化项目的第一个场景的方法。它包括反序列化并实例化显示第一个场景所需的所有数据，例如编译着色器、上传纹理和实例化游戏对象。此外，第一个场景中的所有 MonoBehaviour 都在此时执行 `Awake` 回调。

### 解析运行时跟踪

PlayerLoop (Unity主循环，每帧运行一次)

![img](https://docs.unity.cn/cn/current/uploads/Main/UnderstandingPerformanceinUnity-ProfilingSection_image_1.png)

`PlayerRender` 是运行 Unity 渲染系统的方法。此方法进行的操作包括剔除对象、计算动态批次以及向 GPU 提交绘图指令。任何图像效果或基于渲染的脚本回调（例如 `OnWillRenderObject`）也在此时运行。通常情况下，当项目进行交互时，此方法应该对 CPU 时间的消耗最大。

`BaseBehaviourManager` 调用三个模板化版本的 `CommonUpdate`。这些调用的某些回调存在于当前场景中激活的游戏对象的 MonoBehaviour 中。

- `CommonUpdate<UpdateManager>` 调用 `Update` 回调
- `CommonUpdate<LateUpdateManager>` 调用 `LateUpdate` 回调
- `CommonUpdate<FixedUpdateManager>` 调用 `FixedUpdate`（如果物理系统已勾选）

通常情况下，`BaseBehaviourManager::CommonUpdate<UpdateManager>` 是最值得关注和检查的方法族，因为它是 Unity 项目中运行的大多数脚本代码的入口点。

还有其他几个需要关注的方法：

`UI::CanvasManager` 如果项目使用 Unity UI，它将调用几个不同的回调。包括 Unity UI 的批量计算和布局更新；这两种操作是 `CanvasManager` 出现在性能分析器中的最常见原因。

`DelayedCallManager::Update` 运行协程。该内容在本文档的“协程”章节中有更详细的介绍。

`PhysicsManager::FixedUpdate` 运行 PhysX 物理系统。这主要涉及运行 PhysX 的内部代码，并受当前场景中物理对象（例如刚体和碰撞体）数量的影响。但是，基于物理系统的回调也会出现在此处，例如 `OnTriggerStay` 和 `OnCollisionStay`。

如果项目使用的是 2D 物理设置，那么会在 `Physics2DManager::FixedUpdate` 下显示为一组与上面相似的调用。

### 解析脚本方法

在使用 IL2CPP 进行跨平台编译调用脚本时，应查找包含 `ScriptingInvocation` 对象的跟踪行。它是将 Unity 的内部原生代码转换到脚本运行时以便执行脚本代码的命令 (2)（注意： 从技术上讲，在经过 IL2CPP 编译后，C#/JS 脚本代码也会成为原生代码。但是，这种交叉编译后的代码的方法主要通过 IL2CPP 运行时框架来执行，与手写的 C++ 不太一样）。

![img](https://docs.unity.cn/cn/current/uploads/Main/UnderstandingPerformanceinUnity-ProfilingSection_image_2.png)

跟踪行相当易读：每一行都是原始类的名称，后跟下划线和原始方法的名称

### 资源加载

CPU 跟踪记录中也可以识别出资源加载记录。标识资源加载的主要方法是 `SerializedFile::ReadObject`。此方法将二进制数据流（从文件）通过运行名为 `Transfer` 的方法连接到 Unity 的序列化系统中。可以在所有资源类型（例如纹理、MonoBehaviour 和粒子系统）上找到 `Transfer` 方法。

![img](https://docs.unity.cn/cn/current/uploads/Main/UnderstandingPerformanceinUnity-ProfilingSection_image_3.png)

在上面的截屏中，正在加载场景文件。这需要 Unity 读取并反序列化场景中的所有资源，如 `SerializedFile::ReadObject` 中包含的各种调用 `Transfer` 的方法。

通常，如果在运行时期间看到性能不稳定，并且性能跟踪记录显示 `SerializedFile::ReadObject` 使用了大量时间，那么帧率降低的原因是由于资源加载。请注意，在大多数情况下，只有在通过 `SceneManager`、`Resources` 或 AssetBundle API 来请求同步的资源加载时，才能在主线程上找到 `SerializedFile::ReadObject`。

这种性能不稳问题的最常见的修正方式是，将资源加载异步进行（将资源消耗严重的 `ReadObject` 调用移到工作线程），或预加载某些大型资源。

请注意，在克隆对象（在跟踪记录中以 `CloneObject` 方法表示）时也会出现 `Transfer` 调用。如果在 `CloneObject` 调用下面出现 `Transfer` 调用，该调用是不会从存储中加载资源的。而是将旧对象的数据传输到新对象。为此，Unity 会序列化旧对象，并将结果数据反序列化为新对象。

## 内存分析

https://docs.unity.cn/cn/current/Manual/BestPracticeUnderstandingPerformanceInUnity2.html

### 分析内存消耗

使用 Unity Bitbucket 提供的[开源内存可视化工具](https://bitbucket.org/Unity-Technologies/memoryprofiler)可实现 Unity 中的内存问题的最佳诊断。此工具的集成非常简单，只需下载链接的代码仓库并将包含的 *Editor* 文件夹放入项目中。

![img](https://docs.unity.cn/cn/current/uploads/Main/UnderstandingPerformanceinUnity-MemorySection_image_0.jpg)

### 识别重复的纹理

一个常见的内存问题是内存中的资源重复。由于纹理通常是项目中消耗内存最多的资源，因此纹理重复是 Unity 项目中最常见的内存问题之一。

识别重复资源可采用的方法是查找两个相同类型和相同大小的对象，这些对象看起来是从同一资源加载的。在新的内存性能分析器的详细信息面板中，检查看似相同的对象的 **Name** 和 **InstanceID** 字段。

**Name** 字段基于资源文件（对象加载自该文件）的名称；通常，该名称是不包含文件路径和扩展名的文件名。**InstanceID** 字段表示 Unity 运行时分配的内部标识号；此数字在 Unity 游戏的单次运行中是独一无二的(1)。

![img](https://docs.unity.cn/cn/current/uploads/Main/UnderstandingPerformanceinUnity-MemorySection_image_1.png)

**此图演示了此问题的一个简单示例。在图的左侧和右侧是从 5.4 版内存性能分析器的详细信息面板中截取的截屏。这些截屏中显示的资源是在内存中单独加载的两个纹理。纹理具有相同的名称和大小，表明它们可能是重复的。通过检查项目的“Assets”文件夹，可以确定只有一个名为 *wood-floorboards-texture* 的资源文件，这更加证明了资源被重复加载。**

内存中的每个 UnityEngine.Object 都在对象被创建时分配一个唯一的实例 ID。由于这两个纹理具有不同的实例 ID，因此可以肯定它们代表了加载到内存中的两组不同纹理数据。

由于文件名和资源大小相同，而实例 ID 不同，可以肯定这两个对象表示内存中重复的纹理（注意： 如果项目中存在具有相同文件名的纹理，那么此判断将不是绝对准确的，但在文件大小也相同时则可能性非常高）。

### 检查图像缓冲区、图像效果和 RenderTexture 内存使用情况

可以通过内存可视化工具显示需要向图像和 RenderTexture 对象提供的渲染缓冲区的内存需求情况。

![img](https://docs.unity.cn/cn/current/uploads/Main/UnderstandingPerformanceinUnity-MemorySection_image_2.png)

上面的截屏演示了一个简单的场景，场景中应用了 Unity 的一些电影图像效果。图像效果会分配临时渲染缓冲区来执行计算；尤其是泛光特效分配了几个大小递减的缓冲区。由于 Retina iOS 设备的高分辨率，这些临时缓冲区消耗的内存远远超过项目其余部分的总量。

例如 iPad Air 2 的分辨率为 2048x1536，这超出了通常针对现代游戏主机和 PC 的 1080p 分辨率，但却是在平板设备上运行。一个全屏临时渲染缓冲区将占用全部的 24 或 36 兆字节内存（具体取决于缓冲区的格式）。**通过将渲染缓冲区的像素大小减半，可使该内存减少 75%。**这样做通常不会显著降低显示结果的视觉品质。

优化图像效果对临时渲染缓冲区和其他 GPU 资源的使用的方法之一是创建单个“超级”(uber) 图像效果，此特效将同时执行所有不同的计算。使用 Unity 5.5 或更高版本时，可使用新的 [UberFX](https://github.com/Unity-Technologies/PostProcessing)（可从 [github](https://github.com/Unity-Technologies/PostProcessing) 获取）软件包；该软件包提供了一个可配置的“超级”图像效果，此特效可以执行电影图像效果提供的所有操作，而且开销比单个图像效果更低。

## Asset

https://developer.unity.cn/projects/61558c31edbc2a00260cb41c

# Unity-Architecture

https://docs.unity.cn/cn/2021.2/Manual/unity-architecture.html

### 脚本后端

**Mono**

使用即时 (JIT) 编译，在运行时按需编译代码。

**IL2CPP (Intermediate Language To C++)**

使用提前 (AOT) 编译，在运行之前编译整个应用程序。

使用基于 JIT 的脚本后端的好处是编译时间通常比 AOT 快得多，而且它与平台无关。

# Unity3D高级编程（主程手记）笔记

架构的好坏评估点

1.承载力

2.可拓展性

3.易用性

4.可伸缩性

5.容错性以及错误的感知力

## C# 技术要点

### List源码

不是高效的组件，效率比数组还差，兼容性较强的组件，好用但效率不高。线程不安全，加锁机制

### Dictionary源码

### 2.5委托、事件、装箱、拆箱

#### 2.5.1 委托与事件

所有函数指针的功能都以委托的方式来完成。

委托可以视为一个更高级的函数指针，不仅能吧地址指向另一个函数，而且还能传参、获取返回值等多个信息。

创建委托=>其实就是创建一个delegate**类实例**，继承了System.MulticastDelegate类，类实例里有BeginInvoke(), EndInvoke(), Invoke(). 分别为异步开始调用，结束异步调用及直接调用。

什么是事件(event) ?

event是在delegate上又做了一次封装，这次封装的意义是，限制用户直接操作delegate实例中的变量权限。（不再提供 "="操作符。‘

### 2.6 排序算法

写程序比的是谁**基础能力**强，谁的**算法效率**高，对**底层原理**更加熟知于心，

### 

#### 2.6.1 快排

最好O(nlgn) 最坏 O(n²)

## 用户界面

### 4.1用户界面系统的比较

主流: NGUI和UGUI。后面出现了FairyGUI

<mark>(周末补充各个GUI的特点 缺点等)</mark>

UGUI 性能最好（部分支持修改。原组件上扩展、重载）

NGUI 支持定制

### 4.2 UGUI系统的原理及其组件的使用

#### 4.2.1 UGUI系统的运行原理

#### 4.2.2 UGUI系统的组件

##### 1.Canvas组件

RenderMode（渲染模式）

1. Overlay模式。用于纯UI

2. Screen Camera。实际项目中，制作UI最常用的模式

3. World Space。主要用于当UI物体放在3D世界中时

##### 2.





### 4.6.14 GC的优化

GC(Garbage Collection)。

游戏运行，数据主要贮存在内存中，当数据不再需要时，这部分内存可以被回收再次利用。

内存垃圾指当前废弃数据所占用的内存，GC是指将废弃的内存进行回收，再次使用的过程。

进行GC操作时，首先会检查被GC记录的每个变量，确定变量是否处于孤岛状态，或引用是否处于激活状态。如果不处于激活/孤岛状态，则标记为可回收，标记的变量会在回收程序中被移除，内存也会被回收。



GC相当耗时，对象、变相越多，检查操作越多，耗时越长。

内存分配和申请系统内存是如何影响耗时的。

1. Unity3D内部有两个内存管理池:堆栈内存和堆内存。堆栈(stack)内存主要用来存储较小和短暂的数据。堆(heap)内存主要用来存储较大和存储时间较长的数据。

2. Unity3D中的变量只会在堆栈内存和堆内存上进行内存分配，变量要么存储在堆栈内存上，要么存储在堆内存上。

3. 只要变量处于激活状态，占用的内存就会被标记为使用状态，该内存则处于被分配的状态。

4. 一旦变量不再处于激活状态，其占用的内存不再需要，该内存可被回收到内存池中再次使用，这就是**内存回收**。处于堆栈上的内存回收很迅速，处于堆内存上的垃圾并不是及时回收的，此时其对应的内存会被标记为使用状态。

5. GC主要是指堆上的内存分配和回收，Unity3D会定时对堆内存进行GC

6. Unity3D的堆内存只会增加，不会减少。当堆内存不足时，会向系统申请更多内存，但不会在空闲时归还给系统，除非应用结束重新开始。









Learning 08_22

[Unity Roguelike Tutorial : Part 8 - Items and Inventory - YouTube](https://www.youtube.com/watch?v=Mjpx0o0GxR4)
