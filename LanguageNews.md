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
关系模式（Relational Pattern）

int x = 5;

if (x is > 0 and < 10)   // 逻辑模式 and
    Console.WriteLine("1-9");

if (x is < 0 or > 100)   // 逻辑模式 or
    Console.WriteLine("范围外");

if (x is not 0)          // not
    Console.WriteLine("非零");



属性模式（Property Pattern）

string hello = "abc";

if (hello is { Length: > 0 })
    Console.WriteLine("非空字符串");

if (hello is { Length: 5 })
    Console.WriteLine("长度为 5");



嵌套属性模式

if (person is { Address.City: "Tokyo", Age: > 18 })
    Console.WriteLine("成年东京人");



位置模式（Positional Pattern）

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
