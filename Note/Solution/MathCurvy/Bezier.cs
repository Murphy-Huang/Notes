using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MathCurvy
{
    public class Bezier
    {
        /// <summary>
        /// 根据T值，计算返回贝塞尔曲线上面相对应的点
        /// </summary>
        /// <param name="t"></param>T值[0~1]插值
        /// <param name="p0"></param>起始点
        /// <param name="p1"></param>控制点
        /// <param name="p2"></param>目标点
        /// <returns></returns>根据T值计算出来的贝赛尔曲线点
        private static Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
        {
            float u = 1 - t;
            float tt = t * t;
            float uu = u * u;

            Vector3 p = uu * p0;
            p += 2 * u * t * p1;
            p += tt * p2;

            return p;
        }

        /// <summary>
        /// 获取存储贝塞尔曲线点的数组
        /// </summary>
        /// <param name="startPoint"></param>起始点
        /// <param name="controlPoint"></param>控制点
        /// <param name="endPoint"></param>目标点
        /// <param name="segmentNum"></param>采样点的数量
        /// <returns></returns>存储贝塞尔曲线点的数组
        public static Vector3[] GetBeizerList(Vector3 startPoint, Vector3 controlPoint, Vector3 endPoint, int segmentNum)
        {
            Vector3[] path = new Vector3[segmentNum];
            for (int i = 1; i <= segmentNum; i++)
            {
                float t = i / (float)segmentNum;
                Vector3 pixel = CalculateCubicBezierPoint(t, startPoint, controlPoint, endPoint);
                path[i - 1] = pixel;
            }
            return path;
        }

        /// <summary>
        /// 传入顶点集合，得到高阶的贝塞尔曲线，顶点数量不限
        /// </summary>
        /// <param name="vertex"></param>
        /// <param name="vertexCount">为构建曲线的顶点数，此数值越大曲线越平滑</param>
        /// <returns></returns>
        public static Vector3[] GetBezierCurveWithUnlimitPoints(Vector3[] vertex, int vertexCount)
        {
            List<Vector3> pointList = new List<Vector3>();
            pointList.Clear();
            for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
            {
                pointList.Add(UnlimitBezierCurve(vertex, ratio));
            }
            pointList.Add(vertex[vertex.Length - 1]);

            return pointList.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="vecs"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Vector3 UnlimitBezierCurve(Vector3[] vecs, float t)
        {
            Vector3[] temp = new Vector3[vecs.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = vecs[i];
            }
            //顶点集合有多长，曲线的每一个点就需要计算多少次。

            int n = temp.Length - 1;
            for (int i = 0; i < n; i++)
            {
                //依次计算各两个相邻的顶点的插值，并保存，每次计算都会进行降阶。剩余多少阶计算多少次。直到得到最后一条线性曲线。
                for (int j = 0; j < n - i; j++)
                {
                    temp[j] = Vector3.Lerp(temp[j], temp[j + 1], t);
                }
            }
            //返回当前比例下曲线的点
            return temp[0];
        }

    }
}