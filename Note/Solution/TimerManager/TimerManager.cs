using UnityEngine;
using System.Collections;
using System;
using System.Diagnostics;
using System.Collections.Generic;

/// <summary>
/// 计时器管理类 Create by yuanan 2015-11-4
/// </summary>
public class TimerManager : Manager 
{
	// 单例
	private static TimerManager instance;
	public static TimerManager ins
	{
		get
		{
			if (instance == null)
			{
				CCLog.LogError("TimeManager not create");
			}
			return instance;
		}

	}
	void Awake()
	{
		instance = this;
	}

	// 计时器起始id
	private static long TIMER_ID = 1000; // 不可外部随意改动,确保唯一性

	/// <summary>
	/// 计时器容器
	/// </summary>
	private Dictionary<long, Timer> _dicRunTimer = new Dictionary<long, Timer>();
    // 当前计时器个数
    public int timercount
    {
        get
        {
            return _dicRunTimer.Count;
        }
    }

	/// <summary>
	/// 帧数计时器容器
	/// </summary>
	private Dictionary<long, TimerFrame> _dicRunFrame = new Dictionary<long, TimerFrame>();

	/// <summary>
	/// 存储对应类名的的计时器id
	/// </summary>
	private Dictionary<string, List<long>> _dicClassName = new Dictionary<string, List<long>>();

	// Use this for initialization
    public override void Start()
    {
        base.Start();
    }

	/// <summary>
	/// 功能：延迟一定时间调用给定的函数（仅执行一次）
	/// </summary>
	/// <param name="delaytime"> 延迟的时间 </param>
	/// <param name="callback"> 回调方法</param>
	/// <returns> 返回任务id </returns>
	public static long runTimeOnce(float delaytime, Action callback)
	{
		long taskId = RandomTaskId();
		StoreTaskIdByClassName(taskId);
		Timer timer = new Timer(delaytime, 0.0f, callback, null, null, 1, RemoveTimer, taskId);
		timer.Start();
		instance._dicRunTimer.Add(taskId, timer);
        return taskId;
	}

	// @callParam 可传递一个参数
	public static long runTimeOnce(float delaytime, Action<object> callback, object callParam)
	{
		long taskId = RandomTaskId();
		StoreTaskIdByClassName(taskId);
		Timer timer = new Timer(delaytime, 0.0f, null, callback, callParam, 1, RemoveTimer, taskId);
		timer.Start();
		instance._dicRunTimer.Add(taskId, timer);
		return taskId;
	}


	/// <summary>
	/// 功能：每隔一定时间回调函数
	/// </summary>
	/// <param name="timeInterval"> 时间间隔 </param>
	/// <param name="callback"> 回调方法</param>
	/// <param name="loop"> 循环调用次数，-1为无限循环，0和1一样仅循环1次</param>
	/// <returns> 返回任务id </returns>
	public static long runTimeUpdate(float timeInterval, Action callback, int loop = -1)
	{
		long taskId = RandomTaskId();
		StoreTaskIdByClassName(taskId);
		Timer timer = new Timer(float.MaxValue, timeInterval, callback, null, null, loop, RemoveTimer, taskId);
		timer.Start();
		instance._dicRunTimer.Add(taskId, timer);
		return taskId;
	}
	// @callParam 可传递一个参数
	public static long runTimeUpdateWithParam(float timeInterval, Action<object> callback, object callParam, int loop = -1)
	{

		long taskId = RandomTaskId();
		StoreTaskIdByClassName(taskId);
		Timer timer = new Timer(float.MaxValue, timeInterval, null, callback, callParam, loop, RemoveTimer, taskId);
		timer.Start();
		instance._dicRunTimer.Add(taskId, timer);
		return taskId;
	}



	//1.3 TODO：每隔一定的时间调用某个函数（可设定执行的次数）


	//2. 按照帧数执行
	//2.1 TODO：每隔一定帧数调用某个函数（仅执行一次）

	//2.2 每隔一定帧数调用某个函数（永远执行，直到人为停止）
	/// <summary>
	/// 每隔一定帧数调用某个函数（永远执行）
	/// </summary>
	/// <param name="interval_frames">间隔帧数</param>
	/// <returns></returns>
	public static long runFrameUpdate( Action callback, int interval_frames = 1)
	{
		long taskId = RandomTaskId();
		StoreTaskIdByClassName(taskId);
		TimerFrame timerFrame = new TimerFrame(interval_frames, callback, null, null);
		timerFrame.Start();
		instance._dicRunFrame.Add(taskId, timerFrame);
		return taskId;
	}
	public static long runFrameUpdate(Action<object> callback, object callParam, int interval_frames = 1)
	{
		long taskId = RandomTaskId();
		StoreTaskIdByClassName(taskId);
		TimerFrame timerFrame = new TimerFrame(interval_frames, null, callback, callParam);
		timerFrame.Start();
		instance._dicRunFrame.Add(taskId, timerFrame);
		return taskId;
	}

	//2.3 TODO：每隔一定帧数调用某个函数（可设定执行的次数）

	//3 自定义计时器
	//3.1 可用于
	/// <summary>
	/// 功能：创建一个计时器，需手动开启计时。如果计时未达到目标计时点，请注意手动删除该计时器
	/// </summary>
	/// <param name="remainTime"> 总计时 </param>
	/// <returns> 任务id </returns>
	public static long CreateTimer(float remainTime = float.MaxValue)
	{
		long taskId = RandomTaskId();
		StoreTaskIdByClassName(taskId);
		Timer timer = new Timer(remainTime, 0.0f, null, null, null, 1, RemoveTimer, taskId);
		instance._dicRunTimer.Add(taskId, timer);
		return taskId;
	}


	// 根据任务id获取对应的计时器
	public static Timer GetTimer(long taskId)
	{
		if (instance._dicRunTimer.ContainsKey(taskId))
		{
			return instance._dicRunTimer[taskId];
		}
		else
		{
			CCLog.LogWarning("Timer not exist which taskId = " + taskId);
			return null;
		}
	}

    // 根据键值移除Timer
    public static void RemoveLuaTimer(string staskId)
    {
        long taskId = long.Parse(staskId);
        RemoveTimer(taskId);
    }

    // 根据键值移除Timer
    public static void RemoveTimer(long taskId)
	{
		if (instance._dicRunTimer.ContainsKey(taskId))
		{
			instance._dicRunTimer.Remove(taskId);
		}
		if (instance._dicRunFrame.ContainsKey(taskId))
		{
			instance._dicRunFrame.Remove(taskId);
		}
	}

	// 根据类名移除Timer
	public static void RemoveTimer(string className)
	{
		if (instance._dicClassName.ContainsKey(className))
		{
			for (int i = 0; i < instance._dicClassName[className].Count; i++)
			{
				long taskId = instance._dicClassName[className][i];
                //CCLog.LogError("移除计时器:"+ className + " 任务id:" + taskId);
				RemoveTimer(taskId);
			}
			instance._dicClassName.Remove(className);
		}
	}

	// 清除所有计时器
	public static void Clear()
	{
		instance._dicRunTimer.Clear();
		instance._dicRunFrame.Clear();
	}

	// 暂停所有计时器
	public static void PauseAllTimers()
	{
		List<long> keys = new List<long>(instance._dicRunTimer.Keys);
		for (int i = 0; i < keys.Count; i++)
		{
			long key = keys[i];
			instance._dicRunTimer[key].Pause();
		}
	}

	// 继续之前所有暂停的计时器
	public static void ContinueAllTimers()
	{
		List<long> keys = new List<long>(instance._dicRunTimer.Keys);
		for (int i = 0; i < keys.Count; i++)
		{
			long key = keys[i];
			instance._dicRunTimer[key].Continue();
		}
	}

	// 暂停某个计时器
	public static void PauseTimer(long taskId)
	{
		if (instance._dicRunTimer.ContainsKey(taskId))
		{
			instance._dicRunTimer[taskId].Pause();
		}
		if (instance._dicRunFrame.ContainsKey(taskId))
		{
			instance._dicRunFrame[taskId].Pause();
		}
	}

	// 继续之前所暂停的某个计时器
	public static void ContinueTimer(long taskId)
	{
		if (instance._dicRunTimer.ContainsKey(taskId))
		{
			instance._dicRunTimer[taskId].Continue();
		}
		if (instance._dicRunFrame.ContainsKey(taskId))
		{
			instance._dicRunFrame[taskId].Continue();
		}
	}


	/// <summary>
	/// 生成一个不重复的随机任务id
	/// </summary>
	public static long RandomTaskId()
	{
		long taskId = TIMER_ID++;
		while (instance._dicRunTimer.ContainsKey(taskId))
		{
			taskId = TIMER_ID++;
		}
		return taskId;
	}

	/// <summary>
	/// 存储对应类名的计时器id
	/// </summary>
	/// <param name="taskId"></param>
	public static void StoreTaskIdByClassName(long taskId)
	{
        return; // 暂时不用
        string name = (new StackTrace()).GetFrame(3).GetMethod().ReflectedType.GUID.ToString();
        //CCLog.LogError("类名:"+ (new StackTrace()).GetFrame(3).GetMethod().Name + "添加计时器:"+ name + " 任务id:" + taskId);
		if (instance._dicClassName.ContainsKey(name))
		{
			instance._dicClassName[name].Add(taskId);
		}
		else
		{
			List<long> list = new List<long>();
			list.Add(taskId);
			instance._dicClassName[name] = list;
		}
	}

	// Update is called once per frame
	void Update()
	{
		if(_dicRunFrame.Count > 0)
		{
			List<TimerFrame> timers = new List<TimerFrame>(_dicRunFrame.Values);
			for (int i = 0; i < timers.Count; i++)
			{
				timers[i].Update();
			}
		}
	}

    void FixedUpdate()
    {
        if (_dicRunTimer.Count > 0)
        {
            List<Timer> timers = new List<Timer>(_dicRunTimer.Values);
            for (int i = 0; i < timers.Count; i++)
            {
                timers[i].Update(Time.deltaTime);
            }
        }
    }

    public void OnDestroy()
    {
        instance = null;
    }
}