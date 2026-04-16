## C#

### 9.0

record 类型

新的引用类型 默认不可变,表达"值对象"

```csharp
public record Person(string Name, int Age);

Person p1 = new("Alice", 20);
var p2 = p1 with {Age = 21}
Console.WriteLine(p1 == p2); // false 值比较
```

init setter

类似set 只能在对象初始化的时候设置(之后是只读的)

用于创建不可变对象

```csharp
public class Car
{
    public string Brand {get; init;}
    public int Year {get; init;}
}

var car = new Car {Brand = "Toyota", Year = 2020}; 
// car.Brand = "abc"  Not Allowed
```

with表达式 参考record内代码

模式匹配增强

逻辑模式  and, or, not

关系模式  < , > , <=, >=

组合模式

```csharp
// 关系模式（Relational Pattern）

int x = 5;

if (x is > 0 and < 10)   // 逻辑模式 and
    Console.WriteLine("1-9");

if (x is < 0 or > 100)   // 逻辑模式 or
    Console.WriteLine("范围外");

if (x is not 0)          // not
    Console.WriteLine("非零");



// 属性模式（Property Pattern）

string hello = "abc";

if (hello is { Length: > 0 })
    Console.WriteLine("非空字符串");

if (hello is { Length: 5 })
    Console.WriteLine("长度为 5");



// 嵌套属性模式

if (person is { Address.City: "Tokyo", Age: > 18 })
    Console.WriteLine("成年东京人");



// 位置模式（Positional Pattern）
public record Point(int X, int Y);

var p = new Point(1, 2);

if (p is (0, 0))
    Console.WriteLine("原点");

if (p is ( >0, >0 ))
    Console.WriteLine("第一象限");
```

## 10.0

```csharp
global using System;

global using System.Collections.Generic;
```

### 11.0

required 成员, 初始化时必须些

```csharp
public class Person
{
    public required string Name { get; init; }
    public int Age { get; init; }
}

// 必须在初始化时写 Name
var p = new Person { Name = "Alice" };  // OK
var p2 = new Person { Age = 30 };       // 编译错误，缺 “Name”

```

raw string literals

```csharp
var text = """
   This is a raw string literal.
   You can have "quotes", \ backslashes, new lines etc.
   """;
```

list pattern

```csharp
int[] arr = { 1, 2, 3 };

if (arr is [1, 2, 3])
{
    Console.WriteLine("exact match");
}
```

### 12.0

#### 主构造函数(Primary Constructors)

有类 & 结构体

```C#
class Person(string name, int age)
{
    public string Name => name;
    public int Age => age;
}

struct Rectangle(int width, int height)
{
    public int Width => width;
    public int Height => height;
    public int Area => width * height;
}
```

主构造函数参数   

实际上不是字段  只是供函数内部使用 

```C#
class Student(string name, int age)
{
    public void Display() { Console.WriteLine($"{name} {age}"); }
    
    // 给字段
    private string studentName = name;
    private string studentAge = age;
    
    // 给属性
    public string Name {get; set;} = name;
    public string Age  {get; set;} = age;
}
```



#### 集合表达式(Collections Expressions)

传统

```C#
int[] numbers = new int[] {1, 2, 3};

List<string> names =  new List<string>{"Alice", "Bob"};

Span<int> span = new int[] {1, 2, 3};
```

最新

```C#
int[] numbers = [1, 2, 3, 4];

List<string> names = ["Aliace", "Bob"];

// 空集合
int[] empty = [];

// 集合展开

int[] first = [1, 2];
int[] second = [3, 4];
int[] combined = [..first, ..second];   // 1, 2, 3, 4
// .. 展开运算符 
```



### 内联数组  (InLine Arrays)

是一种固定大小的数组类型，元素直接存储在结构体中，不在堆中。（占内存）  可以提升性能，特别是需要固定大小数组的场景

```C#
[System.Runtime.ComplierServices.InlineArray(10)]
public struct Buffer10
{
    private int _element0;
}

var buffer = new Buffer10();
buffer[0] = 1;
// ... etc 
```



### 默认Lambda参数(Default Lambda Parameters)

允许Lambda表达式中使用默认参数，就像普通方法一样

```C#
// 传统方法
Func<int, int, int> add = (a, b) => a + b;
int result = add(10, 20);

// C# 12 默认参数写法
var add = (int a = 0, int b = 0) => a + b;

int result1 = add(0, 10);

var greet = (string name = "Guest") => $"Hello, {name}!";
```



### 别名任意类型(Alias Any Types)

允许使用using 别名来定义任意类型的别名， 而不只是命名空间别名

```C#
// 传统用法
using System;
using Sys = System;

// C# 12

// 定义别名
using IntList = Systesm.Collections.Generic.List<int>;
using StringArray = string[];

// 使用别名
IntList numbers = new IntList {1, 2, 3};
StringArray names = ["Alice", "obbo"];

// 实例
using Point = (int x, int y);
using Number = int;

Point p = (10, 20);
Number n = 42;
```



新特性对比

| 特性           | 传统写法               | C#12写法                 | 优势         |
| -------------- | ---------------------- | ------------------------ | ------------ |
| 主构造函数     | 限时定义构造函数和字段 | 类型声明中定义参数       | 减少样板代码 |
| 集合表达式     | new int[]{1, 2, 3}     | [1, 2, 3]                | 简洁         |
| 默认Lambda参数 | 需要包装方法           | 直接在Lambda中用默认参数 | 灵活         |
| 别名任意类型   | 只能namespace          | 可以任意类型             | 可读性       |

