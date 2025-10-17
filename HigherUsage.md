



## C#





### Func, Expression<Func>

```csharp
void Func(string data, System.Func<RectTrasnform, float> defautlValue)
{
    xxxx;
    // 使用
    defaultValue((target as ContentSizeFitter).transform as RectTransform));
}

调用
void Use()
{
    Func("zz", t => t.rect.width);
    Func("zzx", t => t.rect.height);
}




等同于
float GetWidget(RectTransform t)
{
    return t.rect.width;
}
但这样的话 就要写多个函数分别返回 对应所需的属性
```



Expression<Func>

```csharp
void ElementField(Expression<Func<RectTransform, float>> defaultValue)
{
    var body = (MemberExpression)defaultValue.Body;
    string fieldName = body.Member.Name; // 比如 "width"

    EditorGUILayout.LabelField($"限制最大 {fieldName}");

    // 获取默认值
    var func = defaultValue.Compile();
    float defaultVal = func((target as M96ContentSizeFitter).transform as RectTransform);

    // ... 后续 Inspector 绘制逻辑 ...
}


ElementField(t => t.rect.width);
ElementField(t => t.rect.height);

```





## 🔹1. 基本区别：`Func` vs `Expression<Func>`

| 特性   | `Func<RectTransform, float>` | `Expression<Func<RectTransform, float>>` |
| ---- | ---------------------------- | ---------------------------------------- |
| 存储内容 | 是一个**可执行的委托**（函数指针）          | 是一个**可解析的表达式树**（代码结构本身）                  |
| 执行方式 | 调用时直接运行代码                    | 不能直接运行，需要编译成委托                           |
| 用途   | 逻辑运算、传递函数                    | 序列化、反射、分析代码结构（如属性名）                      |
| 示例   | `t => t.rect.width`          | `t => t.rect.width`（但存成可解析表达式）           |
