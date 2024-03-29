## C# 脚本

### 重载与重写的区别
1. 所处位置不同 **重载**在同类中，**重写**在父类
2. 定义方式不同 **重载**方法名相同,参数列表不同     **重写**方法名和参数列表都相同
3. 调用方式不同 **重载**使用相同对象以不同参数调用  **重写**用不同对象以相同参数调用
4. 多态时机不同 **重载**时编译时多态               **重写**是运行时多态

### 面向对象三大特点
1. 封装
   将数据和行为结合，通过行为约束代码修改数据的程度。属性是C#封装实现的最好体现
2. 继承：
    提高代码重用度，增强软件可维护性的重要手段，符合开闭原则。
3. 多态：
    多态性指同名方法在不同环境下，自适应反应出不同的表现，是方法动态展示的重要手段。

### 值类型和引用类型区别
1. 值类型存储在内存栈中，引用类型数据存储在内存堆中，而内存单元中存放的是堆中存放的地址。
2. 值类型存取快，引用类型存取慢。
3. 值类型表示实际数据，引用类型表示指向存储在内存堆中的数据的指针和引用。
4. 栈的内存是自动释放的，堆内存是.NET 中会由 GC 来自动释放。
5. 值类型继承自 System.ValueType,引用类型继承自 System.Object。"

### private, public, internal, protected
1. public：对任何类和成员都公开，无限制访问
2. private：仅对该类公开
3. protected：对该类和其派生类公开
4. internal：只能在包含该类的程序集中访问该类
5. protected internal：protected + internal"

### 请简述 GC（垃圾回收）产生的原因，并描述如何避免？
"GC 为了避免内存溢出而产生的回收机制  
避免：  
1）减少 new 产生对象的次数  
2）使用公用的对象（静态成员）  
3）将 String 换为 StringBuilder"  

### 请描述 Interface 与抽象类之间的不同
1. 接口不是类 不能实例化 抽象类可以间接实例化
2. 接口是完全抽象 抽象类为部分抽象
3. 接口可以多继承 抽象类是单继承"

### 反射的实现原理？
可以在加载程序运行时，动态获取和加载程序集，并且可以获取到程序集的信息  
反射即在运行期动态获取类、对象、方法、对象数据等的一种重要手段  
主要使用的类库：System.Reflection  
核心类：  
1. Assembly描述了程序集
2. Type描述了类这种类型
3. ConstructorInfo描述了构造函数
4. MethodInfo描述了所有的方法
5. FieldInfo描述了类的字段
6. PropertyInfo描述类的属性  
通过以上核心类可在运行时动态获取程序集中的类，并执行类构造产生类对象，动态获取对象的字段或属性值，更可以动态执行类方法和实例方法等。

### C# String 类型比 stringBuilder 类型的优势是什么?
如果是处理字符串的话，用 string 中的方法每次都需要创建一个新的字符串对象并且分配新的内存地址，而 stringBuilder 是在原来的内存里对字符串进行修改，所以在字符串处理方面还是建议用 stringBuilder 这样比较节约内存。但是 string 类的方法和功能仍然还是比 stringBuilder 类要强。  
string 类由于具有不可变性（即对一个 string 对象进行任何更改时，其实都是创建另外一个 string 类的对象），所以当需要频繁的对一个 string 类对象进行更改的时候，建议使用StringBuilder 类，StringBuilder 类的原理是首先在内存中开辟一定大小的内存空间，当对此 StringBuilder 类对象进行更改时， 如果内存空间大小不够， 会对此内存空间进行扩充，而不是重新创建一个对象，这样如果对一个字符串对象进行频繁操作的时候，不会造成过多的内存浪费，其实本质上并没有很大区别，都是用来存储和操作字符串的，唯一的区别就在于性能上。  
String 主要用于公共 API，通用性好、用途广泛、读取性能高、占用内存小。  
StringBuilder 主要用于拼接 String，修改性能好。  
不过现在的编译器已经把 String 的 + 操作优化成 StringBuilder 了， 所以一般用
String 就可以了  
String 是不可变的，所以天然线程同步。  
StringBuilder 可变，非线程同步。  

### C#中委托和接口有什么区别？各用在什么场合？
接口（interface）是约束类应该具备的功能集合，约束了类应该具备的功能，使类从千变万化的具体逻辑中解脱出来，便于类的管理和扩展，同时又合理解决了类的单继承问题。   
C#中的委托是约束方法集合的一个类，可以便捷的使用委托对这个方法集合进行操作。  
在以下情况中使用接口：  
1. 无法使用继承的场合
2. 完全抽象的场合
3. 多人协作的场合  
在以下情况中使用委托： 多用于事件处理中

### JIT 和AOT区别
Just-In-Time - 实时编译  
执行慢 安装快 占空间小一点  
Ahead-Of-Time - 预先编译  
执行快 安装慢 占内存占外存大    
  
  
  
#### GameObject a=new GameObject()  GameObject b=a  实例化出来了A，将A赋给B，现在将B删除，问A还存在吗？
存在，b删除只是将它在栈中的内存删除，而A对象本身是在堆中，所以A还存在

## Unity编辑器