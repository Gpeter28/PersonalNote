## Functionality Provided by UObjects

- Garbage collection

- Reference updating

- Reflection

- Serialization

- Automatic updating of default property changes

- Automatic property initialization

- Automatic editor integration

- Type information available at runtime

- Network replication

A **struct** is a data structure that helps you organize and manipulate its member properties. Unreal Engine's reflection system recognizes structs as a `UStruct`, but they are not part of the [UObject](https://dev.epicgames.com/documentation/en-us/unreal-engine/objects-in-unreal-engine) ecosystem, and cannot be used inside of [UClasses](https://dev.epicgames.com/documentation/en-us/unreal-engine/API/Runtime/CoreUObject/UObject/UClass).

[Smart Pointers in Unreal Engine | Unreal Engine 5.6 Documentation | Epic Developer Community](https://dev.epicgames.com/documentation/en-us/unreal-engine/smart-pointers-in-unreal-engine)

智能指针: 针对非Object对象

- **`TSharedPtr`**（共享指针）
  
  - 引用计数智能指针，会管理所引用对象的生命周期，直到最后一个引用被释放。可以为空。
  
  - **使用场景**：管理普通 C++ 对象，多个地方共享访问某个对象。

- **`TSharedRef`**（共享引用）
  
  - 类似 `TSharedPtr`，但不能为空（必须引用有效对象），更安全。
  
  - **使用场景**：确保引用对象一定存在，通常用于函数参数或复杂逻辑中，避免空指针风险。

- **`TWeakPtr`**（弱指针）
  
  - 不拥有引用对象，不参与引用计数，可避免循环引用；使用前需将其转换为 `TSharedPtr` （通常用 `.Pin()`），并检查其是否有效。
  
  - **使用场景**：观察对象生命周期，不延长它的存活时间，如缓存或回调引用。

- **`TUniquePtr`**（唯一指针）
  
  - 独占所有权，不可复制，只能移动，超出作用域自动清理对象。
  
  - **使用场景**：管理独占所有权资源，类似标准 C++ 的 `std::unique_ptr`。

这些智能指针只适用于**非 UObject 类型**，即普通 C++ 类或结构体，并不适合用于 UObject 或 Actor 等存在垃圾回收机制的对象 [Epic Games Developers](https://dev.epicgames.com/documentation/zh-cn/unreal-engine/smart-pointers-in-unreal-engine?utm_source=chatgpt.com)[掘金](https://juejin.cn/post/7127952891351924772?utm_source=chatgpt.com)。

## 与 UObject 体系相关的指针类型（你最初提到的 `AActor*` 等）

平时我们在 Unreal 中使用如下指针类型来管理 UObject 实例（如 `AActor`）：

- **裸指针（例如 `AActor*`）**
  
  - 没有生命周期管理，有风险：对象销毁后不会自动清空指针，可能造成悬空指针事故。

- **`TObjectPtr<UObjectType>`**（UE5 引入）
  
  - 类似于 `UObject*`，但增强了类型安全性并可被 GC 追踪。**UPROPERTY 标记时最佳实践**。

- **`TWeakObjectPtr<UObjectType>`**
  
  - 对 UObject 的弱指针引用，GC 后自动失效，避免悬挂。

- **`TSoftObjectPtr<UObjectType>`**
  
  - 存储 UObject 的路径，可延迟加载对象，减少内存占用，避免硬依赖。

这些都是用于 Unreal 内建对象系统（UObject 架构）中、更安全、更高效地管理对象生命周期的类型。

## 1. Unreal 的四种常见指针类型

在 Unreal C++ 中，最常见的指针管理封装有这几种：

### ✅ `TWeakObjectPtr<T>`

- **弱引用指针**，不会阻止对象被 GC 回收。

- 如果对象被销毁，`TWeakObjectPtr` 会自动失效（`IsValid()` 返回 false）。

- **使用场景：**
  
  - 临时缓存对象（比如 UI 里指向一个角色，角色随时可能消失）。
  
  - 避免循环引用。
  
  - 不保证对象存活，只在需要的时候判断。

---

### ✅ `TSoftObjectPtr<T>`

- **软引用指针**，保存的是对象的路径（Asset Path），而不是对象本身。

- 在访问时，如果对象还没加载，会触发异步/同步加载。

- **使用场景：**
  
  - 常用于指向资源（比如关卡配置里引用了一张 Texture 或 Sound，但不想一开始就加载进内存）。
  
  - 避免硬依赖（减少打包体积，提高初始加载速度）。

---

### ✅ `TObjectPtr<T>`

- **UObject 的强引用指针**（UE5 引入）。

- 本质还是裸指针（`T*`），但增加了类型安全和垃圾回收标记，避免直接用 `UObject*` 时可能出现的问题。

- **使用场景：**
  
  - UE5 推荐替代 `UObject*` 的写法，比如：
    
    `UPROPERTY() TObjectPtr<AActor> OwnerActor;`
  
  - 一般用在 UPROPERTY 中，保证对象能被 GC 正确追踪。

---

### ✅ `TSharedPtr<T>` / `TWeakPtr<T>`

- **非 UObject 类型的智能指针**，和标准 C++ 的 `shared_ptr/weak_ptr` 类似。

- `TSharedPtr` 用于堆对象的共享所有权管理，`TWeakPtr` 用于弱引用。

- **使用场景：**
  
  - 非 UObject 类型（比如 Slate UI 元素 `SWidget`、数据结构、工具类对象）。
  
  - 生命周期明确、需要引用计数的情况。
  
  - **注意**：UObject 不要用 `TSharedPtr` 管理！UObject 的生命周期由 GC 负责。

---

## 2. 和普通的 `AActor*` 的关系

平时我们写 `AActor*` 或 `AMyCharacter*`，其实就是一个**裸指针**，没有任何生命周期管理：

- **优点**：灵活、C++ 原生操作。

- **缺点**：
  
  - 如果 Actor 被销毁，指针会悬空，可能导致 Crash。
  
  - 不会参与 UE 的 GC，除非放到 `UPROPERTY()` 中。

而上面那四种指针，就是对裸指针的**封装和管理**：

- `TObjectPtr<AActor>` 👉 更安全的 `AActor*`（推荐在 UPROPERTY 里用）。

- `TWeakObjectPtr<AActor>` 👉 不阻止回收的弱引用。

- `TSoftObjectPtr<AActor>` 👉 用于资源/对象的路径引用。

- `TSharedPtr<T>` 👉 用于非 UObject 的内存管理。

---

## 3. 使用场景总结对比表

| 类型                  | 是否 GC 管理       | 是否阻止回收 | 是否可延迟加载 | 典型场景               |
| ------------------- | -------------- | ------ | ------- | ------------------ |
| `AActor*` (裸指针)     | ❌ 否            | ❌ 否    | ❌ 否     | 临时变量、函数参数          |
| `TObjectPtr<T>`     | ✅ 是            | ✅ 是    | ❌ 否     | UE5 推荐替代 UObject*  |
| `TWeakObjectPtr<T>` | ✅ 是            | ❌ 否    | ❌ 否     | 缓存引用、避免循环引用        |
| `TSoftObjectPtr<T>` | ✅ 是            | ✅ 是    | ✅ 是     | 配置资源、延迟加载          |
| `TSharedPtr<T>`     | N/A（非 UObject） | 引用计数   | ❌ 否     | Slate UI、普通 C++ 对象 |

Shared Pointers are similar to [Shared References](https://dev.epicgames.com/documentation/en-us/unreal-engine/shared-references-in-unreal-engine), the main distinction being that Shared References are not nullable and therefore always reference valid objects. When choosing between Shared References and Shared Pointers, Shared References are the preferred option unless you need an empty or nullable object.

SharedRef 不可以为null  永远指向 valid ob

dereference 解引用

```
AACtor* actor = GetWorld()->SpawnActor<AActor>();
actor->GetController();  // 解引用
```

Unreal 中常见的指针类型有不同的解引用方式：

- **裸指针 (`AActor*`)**
  
  - 直接解引用，和普通 C++ 一样。
  
  - 成本：1 次内存读取（极快，基本无消耗）。
  
  - 风险：对象被 Destroy/GC 后就悬空了。

---

- **`TObjectPtr<AActor>` (UE5 引入)**
  
  - 内部封装了一个裸指针（其实是 `AActor*`），加了一点类型安全和 GC 标记。
  
  - 解引用时基本就是直接取出裸指针并用。
  
  - 成本：和裸指针几乎一样，可能会多一道内联函数调用（编译器通常会优化掉）。

---

- **`TWeakObjectPtr<AActor>`**
  
  - 内部存的不是直接的裸指针，而是 `FWeakObjectPtr`，包含了 **对象索引 + 弱引用标记**。
  
  - `Get()` 时会检查对象是否还活着，如果对象被 GC，则返回 `nullptr`。
  
  - 成本：比裸指针多一次 **有效性检查**（哈希表/数组查找）。通常是 O(1)，比直接裸指针略慢。

---

- **`TSoftObjectPtr<AActor>`**
  
  - 内部保存的是 **FSoftObjectPath**（字符串路径），并不会直接持有对象。
  
  - 调用 `.Get()` 时，如果对象没加载，会触发同步/异步加载。
  
  - 成本：
    
    - 如果对象已在内存中：基本上就是查表 → 返回指针，开销 ≈ `TWeakObjectPtr`。
    
    - 如果对象未加载：可能触发磁盘 IO，代价极大（毫秒级甚至秒级）。

---

- **`TSharedPtr` / `TUniquePtr` / `TWeakPtr`**（非 UObject）
  
  - 解引用成本和标准 C++ 智能指针相同：
    
    - `TSharedPtr` 内部有一个引用计数控制块，多了一次引用计数的读取。
    
    - `TUniquePtr` 基本就是裸指针。
    
    - `TWeakPtr` 需要 `.Pin()` 成功才能解引用，会做一次引用计数检查。

和standard C++ references 不同     Unreal Shared References 创建后可以被重新分配

Week Pointer 类似观察者    可以指向 object 但是不拥有它,而且也不管理它的生命周期

FString标准普通字符串类型                   可变
FName 使用字符串池 和哈希表  存储和查找     不可变
FTExt 文本化翻译                          不可变

虚幻引擎的接口函数可以在任意对象上调用 不用全部实现

GC 用了标记清除方法 分散多个帧 执行



## TArray

`TArray` is a value type, meaning that it should be treated similarly as any other built-in type, like `int32` or `float`. It is not designed to be extended, and creating or destroying `TArray` instances with `new` and `delete` is not a recommended practice

The elements are also value types, and the array owns them. Destruction of a `TArray` will result in the destruction of any elements it still contains. Creating a TArray variable from another will copy its elements into the new variable; there is no shared state.

同一类型, 值类型. 



```
// 声明
TArray<int32> IntArray;

// Init
IntArray.Init(10, 5);


TArray<string> StrArr;
StrArr.Add(TEXT("Hello");
StrArr.Emplace(TEXT("World"));
// StrArr = ["Hello", "World"]

```

- `Add` (or `Push`) will copy (or move) an instance of the element type into the array.

- `Emplace` will use the arguments you give it to construct a new instance of the element type.



以上例子中, Add 会创建一个临时的FString 然后把这个临时的FString移动到一个心得FString中, 而Emplace会直接用字符创建一个新的FString. 结果是一样的 但是Emplace避免了临时变量



As a rule of thumb, use `Add` for trivial types and `Emplace` otherwise. `Emplace` will never be less efficient than `Add`, but `Add` may read better.



```cpp
FString Arr[] = {TEXT("of", TEXT("Tomorrow")};
StrArr.Append(Arr, ARRAY_COUNT(Arr));
// StrArr == ["Hello","World","of","Tomorrow"]
```

AddUnique只会添加container中没有存在的元素 

```cpp
StrArr.AddUnique(TEXT("!"));
// StrArr == ["Hello","World","of","Tomorrow","!"]


StrArr.Insert(TEXT("Brave"), 1);
// StrArr == ["Hello","Brave","World","of","Tomorrow","!"]

```



```
StrArr.SetNum(8);
// StrArr == ["Hello","Brave","World","of","Tomorrow","!","",""]
```





迭代 Iteration

```cpp
FString JoinedStr;
for (auto& Str: JoinedStr)
{
    JoinedStr += Str;
    JoinedStr += TEXT("");
}


for (int32 Index = 0; Index != StrArr.Num(); ++Index)
{
	JoinedStr += StrArr[Index];
	JoinedStr += TEXT(" ");
}

// CreateIterator      read-write access
// CreateConstIterator read-only access
for (auto It = StrArr.CreateConstIterator(); It; ++It)
	{
		JoinedStr += *It;
		JoinedStr += TEXT(" ");
	}
```



排序

```cpp
StrArr.Sort();
// StrArr == ["!","Brave","Hello","of","Tomorrow","World"]


FString是大小写不敏感的字典比较
StrArr.Sort([](const FString& A, const FString& B) {
		return A.Len() < B.Len();
	});
	// StrArr == ["!","of","Hello","Brave","World","Tomorrow"]
```

Sort是不稳定的    而且是以快排Quick sort的形式实现的

HeapSort也是不稳定的



StableSort是稳定的    以merge sort 实现的?



```cpp
int32 Count = StrArr.Num();

FString* StrPtr = StrArr.GetData();
	// StrPtr[0] == "!"
	// StrPtr[1] == "of"
	// ...	// StrPtr[5] == "Tomorrow"
	// StrPtr[6] - undefined behavior


	FString Elem1 = StrArr[1];
	// Elem1 == "of"

	bool bValidM1 = StrArr.IsValidIndex(-1); // false


FString ElemEnd  = StrArr.Last();  // Tomorrow
	FString ElemEnd1 = StrArr.Last(1); // World
	FString ElemTop  = StrArr.Top(); // Tomorrow


	bool bLen5 = StrArr.ContainsByPredicate([](const FString& Str){
		return Str.Len() == 5;
	});


int32 Index = StrArr.IndexOfByKey(TEXT("Hello"));
	// Index == 3

int32 Index = StrArr.IndexOfByPredicate([](const FString& Str){
		return Str.Contains(TEXT("r"));
	});


	auto* OfPtr  = StrArr.FindByKey(TEXT("of"))); 返回指针 空时// OfPtr  == &StrArr[1]
```

Last(int idx = 0)   ==  Top()

Cotains

Find 

FindLast



Remove(value)会移除所有

RemoveSingle(value) erase the first

RemoveAt(index)

RemoveAll([] int 32 Val) {

    return Val % 3 == 0;

}



RemoveSwap   删除后不会保证顺序  但效率好很多

RmoveAtSwap   RemoveAllSwap



可以Call   Heapify 转为Heap

```
// HeapArr == [10,9,8,7,6,5,4,3,2,1]
	HeapArr.Heapify();
	// HeapArr == [1,2,4,3,6,5,8,10,7,9]

```

`HeapPop`   会return copy of top element

`HeapPopDiscard`  no return



Num() 当前元素个数

Max()  已分配容量 类似 capacity()

GetSlack()  Max - Num



Empty(3) 创建容量为三

Reset 不会减少 但会增加 Capacity

Shrink 调整至Num 数量





UE 里多用 `check()` 或 `ensure()` 来断言



Addxxxx, Insertxxxx 会增加未初始化的空间,但不会调用类型的构造器.

```
int32 SrcInts[] = { 2, 3, 5, 7 };
    TArray<int32> UninitInts;
    UninitInts.AddUninitialized(4);
    FMemory::Memcpy(UninitInts.GetData(), SrcInts, 4*sizeof(int32));
    // UninitInts == [2,3,5,7]
```

SetNumZeroed

SetNumUninitialized

AddZeroed





## TMap

TMap类似TSet 结构是类似于HasingKeys     Tmap K-V Pairs



两种Map   TMap  TMultiMap

TMap的element 指 kv-pair

TMap也是值类型  类似TArray也是homogeneous container   所有元素都是相同的类型



如果增加新的kv-pairs 会覆盖旧的  MultiMap 都保存



## TMultiMap

Key可重复，  一对多的关系， 内部用为Key维护一个链表                    







Add()

Emplace()



Append() 合并两个Map



MapA.Append(MapB) 

// 所有B的元素会在A的后面加进去 类似C#  

MapB会变成Empty



AddRange         Concat  Array.Copy



```cpp
for (auto& Elem : FruitMap)
{
	FPlatformMisc::LocalPrint(
		*FString::Printf(
			TEXT("(%d, \"%s\")\n"),
			Elem.Key,
			*Elem.Value
		)
	);


for (auto It = FruitMap.CreateConstIterator(); It; ++It)}
{
    It.Key()   // It->Key
    *It.Value() // *It->Value
}


```



Contains()

Find()

FindOrAdd() 会创建新element 不能用于const map

FindRef()   不创建新element 都可用

 

FindKey()



// 保证数量是key >= value 

GenerateKeyArray()

GenerateValueArray()



Remove()

FindAndRemoveChecked()