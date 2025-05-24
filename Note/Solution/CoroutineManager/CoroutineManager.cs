using System.Collections;
using System;
using UnityEngine;

namespace Solution.CoroutineManager
{
    public class CoroutineManager : MonoBehaviour 
{
    private WaitForEndOfFrame wfs = new WaitForEndOfFrame();

    private static CoroutineManager instance;
    public static CoroutineManager ins
    {
        get
        {
            if (instance == null)
            {
                Debug.LogError("CoroutineManager not Init");
            }
            return instance;
        }
    }

    public void Awake()
    {
        instance = this;
    }
    
    //下面Unity自带协程。。
    /// <summary>
    /// 开启协程
    /// </summary>
    /// <param name="iaction"></param>
    public Coroutine StartLoader(IEnumerator iaction)
    {
        Coroutine coroutine = StartCoroutine(iaction);
        return coroutine;
    }

    /// <summary>
    /// 移除协程
    /// </summary>
    /// <param name="coroutine"></param>
    public void RemoveCoroutine(Coroutine coroutine)
    {
        StopCoroutine(coroutine);
    }


    /// <summary>
    /// 多少帧后执行某个函数
    /// </summary>
    /// <param name="fcount">划分的帧数</param>
    /// <param name="func">回调函数</param>
    public Coroutine RunFrameOnce(int fcount, Action func)
    {
        return StartLoader(IRFrame(fcount, func));
    }
    private IEnumerator IRFrame(int fcount, Action func)
    {
        for (int i = 0; i < fcount; i++)
        {
            yield return wfs;
        }
        func();
    }

    /// <summary>
    /// 每格一定帧数更新
    /// </summary>
    /// <param name="fcount">帧间隔</param>
    /// <param name="func"></param>
    /// <param name="fcount">循环次数 -1无限循环</param>
    /// <returns></returns>
    public Coroutine RunFrameUpdate(int fcount, Action func, int runcount)
    {
        return StartLoader(IRFrameUpdate(fcount, func, runcount));
    }
    private IEnumerator IRFrameUpdate(int fcount, Action func, int runcount)
    {
        int cur_runcount = 0; // 当前循环次数
        int count = 0; // 累计帧数
        while (true)
        {
            yield return null;
            //Debug.LogError("回调了==============");
            count++;
            if (count >= fcount)
            {
                func();
                count = 0;
                cur_runcount++;
            }
            if(runcount != -1 && cur_runcount >= runcount)
            {
                break;
            }
            else
            {
                // 无限循环
            }
        }
    }

    public void OnDestroy()
    {
        instance = null;
    }
}
}