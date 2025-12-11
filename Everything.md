## 12.6

### Unity

[AudioSettings](https://docs.unity3d.org.cn/6000.0/Documentation/ScriptReference/AudioSettings.html).dspTime

返回音频系统的当前时间。

这是一个以秒为单位的值，基于音频系统处理的实际样本数量，因此比通过 [Time.time](https://docs.unity3d.org.cn/6000.0/Documentation/ScriptReference/Time-time.html) 属性获得的时间更加精确。

Input System 

### Notification behaviors

SendMessage  如果是 activeSelf / activeInHIerarchy 为false 不会发

SendMessageUpwards 递归向上

BroadcastMessage 递归向下

这三个方法都是基于字符串反射的，编译时不检查、性能差、容易拼错方法名，现代 Unity 强烈建议用以下方式替代：

用 **事件 / delegate**（C# Event）

用 **UnityEvent**（Inspector 可视化配置）

用 **接口 + GetComponent**

用 **ScriptableObject 事件系统**

用 **UniRx、Cinemachine、DOTS Event** 等更现代方案



https://www.sebaslab.com/whats-wrong-with-sendmessage-and-broadcastmessage-and-what-to-do-about-it/?utm_source=chatgpt.com

https://gwb.tencent.com/community/detail/124648?utm_source=chatgpt.com

InvokeEvent    InvokeCSharpEvent

后者不能配置 只能在C#里



### Other

外观模式 （AudioManager  通过这个Manager处理底层的音频 不需要考虑是Wwise还是其他）

为一个复杂的子系统提供一个一致的高层接口。这样，客户端代码就可以通过这个简化的接口与子系统交互，而不需要了解子系统内部的复杂性。





算法：

C#

SortedList

LInkedList 