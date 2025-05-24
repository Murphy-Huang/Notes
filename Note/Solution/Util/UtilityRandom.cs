using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Solution.Util
{
    public class UtilityRandom: System.Random
    {
        public int RangeInt(int min, int max)
        {
            return Next(min, max);
        }

        public float RangeFloat(float min, float max)
        {
            float rand = (float)NextDouble();
            float lest = rand % (max - min);
            return min + lest;
        }

        // 返回随机数在指定数轴的所处的区域
        // list = [0.1, 1, 3, 4, 8]; random = 1.2; return 2;
        public int RangeList<T> (List<T> list)
        {
            if (!list.Count == 0) return -1;
            list.Sort();
            T index = Next(list[0], list[list.Count - 1]);
            for (int i = 1; i < list.Count; ++i)
            {
                if (index <= list[i])
                {
                    return index;
                }
            }
            return -1;
        }
    }
}