## 元组Tuple

多个值组合   C# 7+



```csharp
// 元组字面量
var tuple = (10, "hello", false);
(int, string, bool) tuple2 = (10, "hi", true);

tuple.Item1; // 10
tuple.Item2; // "hello"


// 命名元组
var tuple = (Number: 10, Message: "Hi", IsActive : true);
(int Number, string Message, bool IsActive) tuple2 = (10, "2aaa", false);

// 也可以用Item1, Item2 etc...
tuple.Number; // 10
tuple.Message; // "Hi"


var tuple = (Number: 10, Message: "Hi", IsActive: true);
// 元组的解构
解构是把元组的各个成员分别赋值给不同的变量
    
// 方式一: 使用var
var (number, message, isActive) = tuple
    
// 方式二： 指定类型
(int number, string message, bool isActive) = tuple;

// 方式三: 使用先有变量
int number;
string message;
bool isActive;
(number, message, isActive) = tuple;


示例：
var tuple = (10, "Hi", true);
var (num, msg, active) = tuple;
Console.WriteLine(num); // 10
```

优势：

不需要用到out  不需要创建类或结构体

```csharp
拓展用法
    
var tuple = (10, "hello", false);
    
// 忽略
var (num, _, active) = tuple;

// 部分
var (num, msg) = tuple;

// 嵌套
var tuple2 = ((10, 20), "Hello");
var ((x, y), msg) = tuple;

```







| 方式      | 优点             | 缺点                       |
| --------- | ---------------- | -------------------------- |
| out参数   | 简单直接         | 需要预先声明变量，不够优雅 |
| 类/结构体 | 类型明确，可复用 | 需要定义类型，代码量大     |
| 元组      | 简洁，支持结构   | 不适合复杂数据函数         |

简单多返回、临时组合：元组

复杂/复用情况：类/结构体