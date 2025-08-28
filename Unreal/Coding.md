`[BeginPlay | Learning path](https://dev.epicgames.com/community/learning/paths/0w/unreal-engine-beginplay)

# Engine Structe

![Engine Structure](.\Pictures\BeginPlay_Engine_Structure_Schematic.png)

Config 有特殊层次

Engine下为    Base*.ini

项目下为        Default*.ini

简单逻辑覆盖

Engine\Config\Base =>  Engine\Platform => Project\Config  =>  Project\Platform => Project\Save\Config\Platform ...

# Programming

![Image](.\Pictures\BeginPlay_Programming_Schematic.png)

# 

### Macros 宏

UCLASS, USTRUCT, UPROPERTY, UFUNCTION, UENUM

主要是将原生类暴露给蓝图/反射系统 (?)

## Memory

Loadiing Assets 加载资源

Garbage Collector GC

=> Memory Management 自动内存管理器

## Core Features

### Numbers

float/double 浮点

uint 8, 16, 32, 64

Enum,Struct

### Strings

FString 简单的String

FName 名字

FText 本地化String 任何面向用户的文本

### Collections

TArray TMap TSet

TMap 底层也是用TSet  哈希表桶

`TMap` 是基于 `TSet<TPair<Key, Value>>` 构建的，`TSet` 使用哈希表 + 冲突链（NextHashIndex）+ 稀疏数组存储，具备高效的查找性能、良好的内存管理，以及对引擎功能（GC、序列化、蓝图）的良好支持。

TMap<Key, Value>
 └─ TSet<TTuple<Key, Value>, TDefaultMapHashableKeyFuncs>
     └─ TSparseArray<TSetElement<TTuple<Key, Value>>>

SourceCode  Set.h

template<typename ComparableKey>
static FORCEINLINE bool Matches(KeyInitType A, ComparableKey B)
{
    return A == B;
}

MyMap.Find("PlayerA");  // 传入的是 const TCHAR* 或 FString

// 你可能查找的是 KeyType 本身：
FString Key = "PlayerA";
Map.Contains(Key);  // Matches(FString, FString)

// 也可能是一个兼容的类型：
Map.Contains(TEXT("PlayerA"));  // Matches(FString, const TCHAR*)

✅ C++ 术语：这是函数模板的 **模板参数重载技巧（Template Argument Deduction）**

是现代 C++ 中提升接口灵活性、减少冗余构造的一种常见方式。

## ✅ 小总结：为什么这么设计？

| 目的                 | 做法                                      |
| ------------------ | --------------------------------------- |
| 支持不同类型但可比较的 Key 查找 | 使用模板 `template<typename ComparableKey>` |
| 避免构造完整 Key 类型      | 允许兼容类型通过 `==` 判断                        |
| 提高泛用性              | 编译期多态，自动匹配正确比较函数                        |

### Transform Types

FVector

FRotator

FQuat

FTransform

以上组成基础数据结构

### UObject

所有引擎类的基类,为引擎数据可见, 反射可见以及 自动内存管理等功能

### Components

UActorComponents没有transform 但 UvvComponent(?)有对应的父Actor

### Actor

处理会话, 链接   而且是Levels里的基类.是Component的基础容器

## Smart Pointers

TObjectPtr

TWeekObjectPtr

TSubclassOf

TSoftPtr

TSoftClassPtr

TUniquePtr

TSharePtr

TSharedRef

TWeakPtr

### ✅ **一、对象生命周期管理指针**

这类智能指针具备**所有权**，会负责对象的**释放或 GC 管理**。

| 指针类型            | 说明                                           | 适用场景                             |
| --------------- | -------------------------------------------- | -------------------------------- |
| `TUniquePtr<T>` | 独占所有权的智能指针，不可复制，仅可移动。类似 `std::unique_ptr`。   | 管理非 `UObject` 的纯 C++ 对象，拥有独占控制权。 |
| `TSharedPtr<T>` | 多所有者引用计数指针，类似 `std::shared_ptr`。释放由最后一个引用决定。 | 管理非 `UObject` 的纯 C++ 对象，多个共享控制权。 |
| `TSharedRef<T>` | 不可为 null 的 `TSharedPtr`，保证始终有效。              | 逻辑上不允许为空的场景，如配置、核心系统对象。          |

---

### ✅ **二、弱引用与非拥有指针**

这类指针**不拥有对象所有权**，只是**安全追踪**或**临时访问**。

| 指针类型                | 说明                                         | 适用场景                                          |
| ------------------- | ------------------------------------------ | --------------------------------------------- |
| `TWeakPtr<T>`       | 与 `TSharedPtr` 搭配使用，弱引用对象，不增加引用计数。         | 避免循环引用（如 A 持有 B，B 弱引用 A）。                     |
| `TWeakObjectPtr<T>` | 针对 `UObject` 的弱引用，不影响 GC，但可在对象有效时访问。       | 安全追踪 `UObject` 生命周期，如延迟执行。                    |
| `TObjectPtr<T>`     | 用于反射系统中新的 GC-safe 指针（UE5 引入），支持原生类型反射和序列化。 | 替代原始裸指针，在 `USTRUCT` / `UCLASS` 中引用 `UObject`。 |

---

### ✅ **三、GC 引用与 `UObject` 体系专属指针**

这些用于配合 Unreal 的 GC（垃圾回收）机制，专为 `UObject` 管理。

| 指针类型                | 说明                                        | 适用场景                     |
| ------------------- | ----------------------------------------- | ------------------------ |
| `TSoftObjectPtr<T>` | 弱引用 + 支持 Asset 软路径，支持延迟加载资源。              | 引用尚未加载的资源（如蓝图、贴图、关卡等）。   |
| `TSoftClassPtr<T>`  | 类似于 `TSoftObjectPtr`，但专门引用 `UClass` 类型资源。 | 延迟加载蓝图类、DataAsset 类型等资源。 |
| `FObjectPtr`（底层）    | UE5 新增底层结构，用于支持 `TObjectPtr` 的压缩型存储。      | 内部用途，不直接使用。              |

---

### ✅ 总结对比（按对象类型分类）

| 对象类型          | 使用指针                                                              | 是否 GC 管理         | 支持反射 | 说明                    |
| ------------- | ----------------------------------------------------------------- | ---------------- | ---- | --------------------- |
| C++ 普通对象      | `TUniquePtr`, `TSharedPtr`, `TSharedRef`, `TWeakPtr`              | ❌                | ❌    | 标准 C++ 管理对象           |
| `UObject` 派生类 | `TWeakObjectPtr`, `TSoftObjectPtr`, `TSoftClassPtr`, `TObjectPtr` | ✅                | ✅    | 配合 GC 系统、支持反射、序列化、蓝图等 |
| 混合场景（如自定义管理）  | `TSharedPtr` + `TWeakObjectPtr`                                   | 看是否继承自 `UObject` | ❌/✅  | 自定义生命周期与跟踪逻辑          |

---

### ✅ 使用建议与典型场景

| 场景                           | 使用指针                               | 原因               |
| ---------------------------- | ---------------------------------- | ---------------- |
| 单一所有者管理资源                    | `TUniquePtr`                       | 简单高效，无需引用计数      |
| 多模块共享逻辑数据                    | `TSharedPtr` / `TSharedRef`        | 多方持有控制权，自动释放     |
| 持有但不想影响生命周期                  | `TWeakPtr` / `TWeakObjectPtr`      | 不影响引用计数或 GC 生命周期 |
| 延迟加载资源或类                     | `TSoftObjectPtr` / `TSoftClassPtr` | 支持资源路径持久化与异步加载   |
| 在 USTRUCT/UCLASS 中引用 UObject |                                    |                  |

## 🧠 各类使用场景对比

| 场景                     | 建议指针类型                                   |
| ---------------------- | ---------------------------------------- |
| 管理普通 C++ 类             | `TUniquePtr` / `TSharedPtr` / `TWeakPtr` |
| 管理 UObject 派生类         | `TObjectPtr` / `TWeakObjectPtr`          |
| 只在需要时加载资源              | `TSoftObjectPtr` / `TLazyObjectPtr`      |
| 在多个系统中共享数据             | `TSharedPtr`                             |
| 避免 UObject 强引用导致无法被 GC | `TWeakObjectPtr`                         |
| 引用蓝图资源或外部资源            | `TSoftObjectPtr`                         |

---

## 🧪 日常使用频率和习惯

| 智能指针                              | 常用频率 | 备注                       |
| --------------------------------- | ---- | ------------------------ |
| `TObjectPtr<T>`                   | ⭐⭐⭐⭐ | Unreal 5 推荐替代裸指针         |
| `TWeakObjectPtr<T>`               | ⭐⭐⭐⭐ | 常用于非拥有关系的引用              |
| `TSoftObjectPtr<T>`               | ⭐⭐⭐  | 蓝图、配置表、资源引用场景            |
| `TSharedPtr<T>` / `TSharedRef<T>` | ⭐⭐⭐  | 常用于逻辑数据模型或 UI 层（如 Slate） |
| `TUniquePtr<T>`                   | ⭐⭐   | 一般用于工具类、Editor 插件        |
| `TLazyObjectPtr<T>`               | ⭐    | 特殊资源优化需求下使用              |

## GamePlay Framework

### Input Component

将玩家输入发送给任何设计了的listener

会根据玩家Id setup, 类型SourcePawn 的PlayerId

### PlayerState

PlayerController的辅助类,存在于Server和所有clients

用于分享player info 在各个client中

### Player/ AI Controller

Pawn的大脑    输入被解释为actions

Controller easily possess(具有) and unpossess Pawns. Server和各自客户端都有

### Pawn / Character

物理身体 用于表现action和behavior

Can be possess by Controller and perform commands

### Movement Component

...

### GameState

GameMode辅助类

Sharing Global Info and function for current level

### GameMode

Server上   LifeTime Dependant on level

Level执行流程

GAS, ticking, Mass, Game Features

## Debug and Profile工具

### Logging

UE_LOG macro

### Visual Logger

Timeline-base Logging 可视化展示游戏内的events

### Console

Cvars(Console Variables)

### Unreal Insights

最新的,详细的有层级的分析工具 ?

### CSV Profiler

Pre-frame timings for render and game threads

### LLM

Low Level Memory tracker base in tags

### ProfileGPU

DetailGPU time

### DumpGPU

dumps a frame's internal resources to disk for inspecting

### Gauntlet

Framework to run builds and validate ?

# BluePrints

# Rendering

![Rendering](.\Pictures\BeginPlay_Rendering_Schematic.png)

# GamePlay

![GamePlay](.\Pictures\Beginplay_Gameplay_Schematic.png)
