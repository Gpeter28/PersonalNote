https://www.youtube.com/watch?v=HrSNDH3X1Ms&t=1s

ShaderGraph 节点



Preview

Negate?

DDXY?



如果直接Negate & Step 的话 ⚪有点锯齿

![image-20251201151633594](SDF.assets\image-20251201151633594.png)

可以用 DDXY + Negate => SmoothStep 好很多

![image-20251201151736639](SDF.assets/image-20251201151736639.png)

查下原理?



Saturate 把大于1的 clamp到1内