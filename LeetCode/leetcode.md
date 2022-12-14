# Title

#### 754

0 =>  a - b

k = 1 每次可以+k -k    然后k++

求最小次数

首先1 ... N任意加减组成的数字的奇偶性一定与NE:\src\C#\VSCode\LeetCode\PersonalLeetCodeSrc\63.不同路径-ii.cs % 4相关，N%4 < 2就是奇数，否则是偶数

先算a - b是奇数还是偶数，找到(1 + N) * N / 2恰好大于等于|a - b|那项，并且奇偶性相等就可以



### 1024 DP 视频拼接

[力扣](https://leetcode.cn/problems/video-stitching/)





## 线段树？

[力扣](https://leetcode.cn/problems/my-calendar-ii/solution/wo-de-ri-cheng-an-pai-biao-ii-by-leetcod-wo6n/)

# 链表

递归、迭代

21.合并两个有序链表

递归

### 142.环形链表Ⅱ

```c#
public ListNode DetectCycle(ListNode head)
    {
        var pos = head;
        HashSet<ListNode> set = new HashSet<ListNode>();

        while (pos != null)
        {
            if (!set.Contains)
            {
                set.Add(pos);
            }
            else
            {
                return pos;
            }
            pos = pos.next;
        }
        return null;
    }


快慢指针
public class Solution {
    public ListNode detectCycle(ListNode head) {
        if (head == null) {
            return null;
        }
        ListNode slow = head, fast = head;
        while (fast != null) {
            slow = slow.next;
            if (fast.next != null) {
                fast = fast.next.next;
            } else {
                return null;
            }
            if (fast == slow) {
                ListNode ptr = head;
                while (ptr != slow) {
                    ptr = ptr.next;
                    slow = slow.next;
                }
                return ptr;
            }
        }
        return null;
    }
}
```

![image-20220713222611444](E:\DeskTop\Learning\AllAreMD\FromDay0\Total\leetcode.assets\image-20220713222611444.png)

![image-20220713222627373](E:\DeskTop\Learning\AllAreMD\FromDay0\Total\leetcode.assets\image-20220713222627373.png)

快慢指针

时间复杂度：O(N)，其中 NN 为链表中节点的数目。在最初判断快慢指针是否相遇时，slow 指针走过的距离不会超过链表的总长度；随后寻找入环点时，走过的距离也不会超过链表的总长度。因此，总的执行时间为 O(N)+O(N)=O(N)。

空间复杂度：O(1)。我们只使用了slow, fast, \textit{ptr}slow,fast,ptr 三个指针。

# 位运算

```c#
https://blog.csdn.net/weixin_36238706/article/details/112448946

a & 1  = 0 偶数
a & 1 != 0 奇数

原理：任何偶数二进制第一位数必定0，而奇数必定是1，而1的二进制就是1，所以可以用这个判断




乘以2的n次方等价于 x << n

2进制移动一位相当于乘于2
```

# HashTable

#### 205 同构字符串

![image-20220711212649212](E:\DeskTop\Learning\AllAreMD\FromDay0\Total\leetcode.assets\image-20220711212649212.png)

方法一：使用一个（二个）字典来存相应的关系。   

方法二：使用IndexOf

```c#
public class Solution

{

  public bool IsIsomorphic(string s, string t)

  {

​    for (int i = 0; i < s.Length; ++i)

​    {

​      if (s.IndexOf(s[i]) != t.IndexOf(t[i]))

​      {

​        return false;

​      }

​    }

​    return true;

  }


  public bool IsIsomorphic2(string s, string t)

  {

​    Dictionary<char, char> dict = new Dictionary<char, char>();



​    for (int i = 0; i < s.Length; ++i)

​    {

​      if (dict.ContainsKey(s[i]))

​      {

​        if (dict[s[i]] != t[i])

​        {

​          return false;

​        }

​      }

​      else

​      {

​        if (dict.ContainsValue(t[i]))

​        {

​          return false;

​        }

​        dict.Add(s[i], t[i]);

​      }

​    }

​    return true;

  }

}
```

# 双指针

#### 392判断子序列

![image-20220711214037554](E:\DeskTop\Learning\AllAreMD\FromDay0\Total\leetcode.assets\image-20220711214037554.png)

方法一：双指针 

复杂度分析

时间复杂度：O(n+m)，其中 n 为 s 的长度，m 为 t 的长度。每次无论是匹配成功还是失败，都有至少一个指针发生右移，两指针能够位移的总距离为 n+m。

空间复杂度：O(1)

方法二：动态规划

![image-20220711214140180](E:\DeskTop\Learning\AllAreMD\FromDay0\Total\leetcode.assets\image-20220711214140180.png)

# 字符串

## 滑动窗口

用来解决字符串（数组）的子元素问题

3.给定一个字符串 `s` ，请你找出其中不含有重复字符的 **最长子串** 的长度。

```c#
public class Solution {
  public int LengthOfLongestSubstring(string s) {
​    int left = 0, right = 0;
​    int ans = 0;
​    int n = s.Length;
​    var hashSet = new HashSet<char>();
​    while(right < n)
​    {
​      if(!hashSet.Contains(s[right])){
​        hashSet.Add(s[right++]);
​        ans = Math.Max(ans, right - left);
​      }else{
​        hashSet.Remove(s[left++]);
​      }
​    }
​    return ans;
  }
}
```

![image-20220307170024954](I:\Desktop\Work\AllAreMD\FromDay0\Total\leetcode.assets\image-20220307170024954.png)

4.寻找两个正序数组的中位数

https://leetcode-cn.com/problems/median-of-two-sorted-arrays/solution/leetcode-4-median-of-two-sorted-arrays-xun-zhao-li/

# 搜索

https://leetcode-cn.com/problems/er-cha-shu-de-jing-xiang-lcof/ 二叉树镜像

## 广度搜索BFS

## 深度搜索DFS

# 递归

想明白**终止条件**、**递归条件**

（递归函数的参数和返回值、终止条件、单层递归的逻辑）

# 动态规划

### 斐波那契数列

https://leetcode-cn.com/problems/qing-wa-tiao-tai-jie-wen-ti-lcof/solution/mian-shi-ti-10-ii-qing-wa-tiao-tai-jie-wen-ti-dong/

![](I:\Desktop\Work\AllAreMD\FromDay0\Total\leetcode.assets\image-20220208234808390.png)

![image-20220208234919706](I:\Desktop\Work\AllAreMD\FromDay0\Total\leetcode.assets\image-20220208234919706.png)

```c#
public class Solution
{
    private const int MOD = 1000000007;
    public int Fib(int n)
    {
        if (n <= 1)
        {
            return n;
        }
        int first = 0;
        int second = 1;
        int now = 1;
        for (int i = 1; i < n; ++i)
        {
            second = first;
            first = now;
            now = (first + second) % MOD;
        }
        return now;
    }
}

public class Solution {
    public int numWays(int n) {
        int a = 1, b = 1, sum;
        for(int i = 0; i < n; i++){
            sum = (a + b) % 1000000007;
            a = b;
            b = sum;
        }
        return a;
    }
}
```

# 字典树Trie()  ⭐

什么是字典树？（7.11~7.17)

什么样的结构？

如何运用？

676.实现一个魔法字典。

# FunFact

当 mid 是 偶数时， mid & 1 = 0

当 mid 是 奇数时， mid & 1 = 1          

https://leetcode-cn.com/problems/single-element-in-a-sorted-array/solution/you-xu-shu-zu-zhong-de-dan-yi-yuan-su-by-y8gh/

# Linq

```c#
给你一个仅由整数组成的有序数组，其中每个元素都会出现两次，唯有一个数只会出现一次。

nums.GroupBy(t => t).First(t => t.Count() == 1).Key;


var listbu = list.GroupBy(g => g);
var model = listbu.Where(t => t.Count() == 1);
var num = model.FirstOrDefault()?.Key;
return num ?? 0;
```

```c#
LINQ 交集

假设 Andy 和 Doris 想在晚餐时选择一家餐厅，并且他们都有一个表示最喜爱餐厅的列表，每个餐厅的名字用字符串表示。

你需要帮助他们用最少的索引和找出他们共同喜爱的餐厅。 如果答案不止一个，则输出所有答案并且不考虑顺序。 你可以假设答案总是存在。

输入:list1 = ["Shogun", "Tapioca Express", "Burger King", "KFC"]，list2 = ["KFC", "Shogun", "Burger King"]
输出: ["Shogun"]
解释: 他们共同喜爱且具有最小索引和的餐厅是“Shogun”，它有最小的索引和1(0+1)。


public class Solution {
    public string[] FindRestaurant(string[] list1, string[] list2) {
            var dic = list1.Intersect(list2).ToDictionary(x => x, x => Array.IndexOf(list1, x) + Array.IndexOf(list2, x));
            return dic.Where(x => x.Value == dic.Values.Min()).Select(x => x.Key).ToArray();
    }
}

链接：https://leetcode-cn.com/problems/minimum-index-sum-of-two-lists/  
```

#PriorityQueue<int, int> queue = new PriorityQueue<int, int>(Comparer<int>.Create((a, b) => b.CompareTo(a))); C#

## ReverseString

https://www.cnblogs.com/tudas/archive/2012/06/07/string-reverse.html

String targetStr =  "abc";

var cArray = targetStr.ToCharArray();

Array.Reverse(cArray);

return new string (cArray);

## GCD 最大公约数

```cs
public long GCD(long a, long b)

    {

        long indexer = a % b;

        while (indexer != 0)

        {

            a = b;

            b = indexer;

            indexer = a % b;

        }

        return b;

    }


辗转相除法
public int gcd(int n, int m)
{
    return m == 0 ? n : gcd(m, n % m);
}

作者：chusep
链接：https://leetcode.cn/problems/fraction-addition-and-subtraction/solution/by-chusep-g5ef/
来源：力扣（LeetCode）
著作权归作者所有。商业转载请联系作者获得授权，非商业转载请注明出处。
```

## 

## .NET 6 优先队列

[.NET 6 优先队列 PriorityQueue 实现分析 - SpringLeee - 博客园](https://www.cnblogs.com/myshowtime/p/15725897.html)

```cs
PriorityQueue<int, int> queue = new PriorityQueue<int, int>
(Comparer<int>.Create((a, b) => b.CompareTo(a)));
```

```cs
Dictionary<string, int> map = new Dictionary<string, int>();

PriorityQueue<string, string> pq = new PriorityQueue<string, string>
(Comparer<string>.Create((a, b) => 
map[a] == map[b] ? a.CompareTo(b) : map[b] - map[a]));
```
