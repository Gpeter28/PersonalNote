SortingClass sortingClass = new SortingClass();


var array = new int[] { 3, 4, 5, 7, 1, 2, 6, 8, 9 };

var m = SortingClass.TMergeSort(array, 0, array.Length - 1);

foreach(var j in m)
{
    Console.Write(j + " ");
}
Console.WriteLine();

// https://blog.csdn.net/weixin_41190227/article/details/86600821

public class SortingClass
{
    public int[] BubbleSort(int[] array)
    {
        if (array.Length == 0) return array;

        for(int i = 0; i < array.Length; ++i)
        {
            for(int j = 0; j < array.Length - 1 - i; ++j)
            {
                if (array[j + 1] < array[j])
                {
                    // (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                }
            }
        }
        return array;
    }


    public int[] SelectionSort(int[] array)
    {
        if (array.Length == 0) return array;

        for(int i = 0; i < array.Length; ++i)
        {
            int minIndex = i;
            for(int j = i; j < array.Length; ++j)
            {
                if (array[j] < array[minIndex])
                {
                    minIndex = j;
                }
            }
            (array[i], array[minIndex]) = (array[minIndex], array[i]);
        }
        return array;
    }

    public int[] InsertionSort(int[] array)
    {
        if (array.Length == 0) return array;

        int current;
        for (int i = 0; i < array.Length - 1; ++i)
        {
            current = array[i + 1];
            int preIndex = i;
            while(preIndex >= 0 && array[preIndex] > current)
            {
                array[preIndex + 1] = array[preIndex];
                preIndex--;
            }
            array[preIndex + 1] = current;
        }
        return array;
    }


    public int[] _InsertSort(int[] array)
    {
        if (array.Length == 0) return array;

        int current;
        for(int i = 0; i < array.Length - 1; ++i)
        {
            current = array[i + 1];
            int preIndex = i;
            while(preIndex >= 0 && array[preIndex] > current)
            {
                array[preIndex + 1] = array[preIndex];
                preIndex--;
            }
            array[preIndex + 1] = current;
        }
    }

    public int[] ShellSort(int[] array)
    {
        int len = array.Length;

        int temp, gap = len / 2;

        while(gap > 0)
        {
            for(int i = gap; i < len; ++i)
            {
                temp = array[i];
                int preIndex = i - gap;
                while(preIndex >= 0 && array[preIndex] > temp)
                {
                    array[preIndex + gap] = array[preIndex];
                    preIndex -= gap;
                }
                array[preIndex + gap] = temp;
            }
            gap /= 2;
        }
        return array;
    }

    public static int[] MergeSort(int[] arr, int lowIndex, int highIndex)
    {
        // 子表的长度大于1，则进入下面的递归处理
        if (lowIndex < highIndex)
        {
            // 分割位点midIndex          
            int midIndex = (lowIndex + highIndex) / 2;

            // 递归划分二部分  (arr[lowIndex].....arr[midIndex])  、 (arr[midIndex+1].....arr[high])
            MergeSort(arr, lowIndex, midIndex);
            MergeSort(arr, midIndex + 1, highIndex);
            //归并
            Merge(arr, lowIndex, midIndex, highIndex);
        }
        return arr;

    }
    //归并排序的核心部分：将两个有序的左右子表（以midIndex区分），合并成一个有序的表
    private static int[] Merge(int[] arr, int lowIndex, int midIndex, int highIndex)
    {
        //左侧A子表 lowIndex....midIndex  右侧B子表 midIndex+1....highIndex
        int[] tempArr = new int[arr.Length];
        int tempIndex = 0;
        int indexA = lowIndex, indexB = midIndex + 1;
        //左右表同时遍历比较  ，如果其中有一个子表遍历完，则跳出循环
        while (indexA <= midIndex && indexB <= highIndex)
        {
            tempArr[tempIndex++] = (arr[indexA] <= arr[indexB] ? arr[indexA++] : arr[indexB++]);

        }
        //左表遍历完，右表还有数据，将右表剩余数，放入tempArr中
        while (indexB <= highIndex)
        {
            tempArr[tempIndex++] = arr[indexB++];
        }
        //右表遍历完，左表还有数据，将左表剩余数，放入tempArr中
        while (indexA <= midIndex)
        {
            tempArr[tempIndex++] = arr[indexA++];
        }

        //将暂存数组中有序的数列写入目标数组的制定位置，使进行归并的数组段有序
        tempIndex = 0;
        for (int i = lowIndex; i <= highIndex; i++)
        {
            arr[i] = tempArr[tempIndex++];
        }
        return arr;
    }


    public static int[] TMergeSort(int[] arr, int left, int right)
    {
        if(left < right)
        {
            int mid = (left + right) / 2;
            TMergeSort(arr, left, mid);
            TMergeSort(arr, mid + 1, right);
            TMerge(arr, left, mid, right);
        }
        return arr;
    }


    private static int[] TMerge(int[] arr, int left, int mid, int right)
    {
        int[] tempArr = new int[arr.Length];
        int tempIndex = 0;
        int indexL = left, indexR = mid + 1;
        while(indexL <= mid && indexR <= right)
        {
            tempArr[tempIndex++] = arr[indexL] <= arr[indexR] ? arr[indexL++] : arr[indexR++];
        }

        while(indexR <= right)
        {
            tempArr[tempIndex++] = arr[indexR++];
        }

        while (indexL <= mid)
        {
            tempArr[tempIndex++] = arr[indexL++];
        }

        tempIndex = 0;
        for(int i = left; i <= right; ++i)
        {
            arr[i] = tempArr[tempIndex++];
        }
        return arr;
    }
} 