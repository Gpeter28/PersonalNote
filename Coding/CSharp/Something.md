```
bool SetStruct<T>(ref T currentValue, T newValue) where T : struct

{

    if (currentValue.Equals(newValue))

        return false;

    currentValue = newValue;

    return true;

}


```



where T :　struct 限制了泛型T必须是值类型(struct)

比如(int, float, DateTime, 自定义struct等)



### 使用场景

1. **状态更新**：
   
   - 在 UI 或游戏开发中，避免重复更新状态。
   
   - ```
     if (SetStruct(ref playerPosition, newPosition))
     
     {
     
         // 只有位置发生变化时才更新渲染
     
         UpdatePlayerPosition(playerPosition);
     
     }
     
     ```
   
   - 

2. **性能优化**：
   
   - 避免不必要的操作，例如重复写入数据库或文件。

3. **数据同步**：
   
   - 在数据同步场景中，确保只有数据变化时才触发同步逻辑。
