//Too simple, so I do not need to explain, just see the code. Help yourself.
using System;

public class Timer
{
    //If the Timer is running 
    private bool b_Tricking;

    //If the Timer is Stop
    private bool b_end;

    //Current time
    private float f_CurTime;

    //Time to reach
    private float f_TriggerTime;

    private float f_timeInterval;

    //loop 
    private int _loop;
    private int _curloop;

    private object _callParam;

    private long _taskId;

    //Use delegate to hold the methods
    private Action _callback;
    private Action<object> _callbackWithparam;

    private Action<long> _removeTimer;


    // Ŀǰ�ۼƵ�ʱ��
    public float curTime
    {
        get
        {
            return f_CurTime;
        }
    }

    // ����ʱ��
    public float lastTime
    {
        get
        {
            return f_TriggerTime < f_CurTime ? f_CurTime - f_TriggerTime : 0.0f;
        }
    }

    /// <summary>
    /// Init ����ʱ����������ʱ��
    /// </summary>
    /// <param name="second">Trigger Time</param>
    public Timer(float second, float timeInterval, Action callback, Action<object> callbackWithparam, object callParam, int loop, Action<long> removeTimer, long taskId)
    {
        f_CurTime = 0.0f;
        f_TriggerTime = second;
        f_timeInterval = timeInterval;
        _callback = callback;
        _callbackWithparam = callbackWithparam;
        _callParam = callParam;
        _curloop = 0;
        _loop = loop == -1? int.MaxValue : loop;
        _removeTimer = removeTimer;
        _taskId = taskId;

        b_Tricking = false;
    }
    
    /// <summary>
    /// Start Timer
    /// </summary>
	public void Start()
    {
        b_Tricking = true;
        b_end = false;
    }
    
    /// <summary>
    /// Update Time
    /// </summary>
    public void Update(float deltaTime)
    {
        if (b_Tricking)
        {
            f_CurTime += deltaTime;
            if (f_CurTime >= float.MaxValue) f_CurTime=0.0f;

            if (f_CurTime >= f_TriggerTime)
            {
                //b_Tricking must set false before tick() , cause if u want to restart in the tick() , b_Tricking would be reset to fasle .
                b_Tricking = false;
                b_end = true;
                if (_callback != null) _callback();
                if (_callbackWithparam != null) _callbackWithparam(_callParam);
                if (_removeTimer!=null) _removeTimer(_taskId);
            }
            else if ( f_timeInterval>0.0f && ((int)(f_CurTime / f_timeInterval)) > _curloop)
            {
                if (_callback != null) _callback();
                if (_callbackWithparam != null) _callbackWithparam(_callParam);

                _curloop++;
                if (_curloop == int.MaxValue) _curloop = 0;

                if (_curloop >= _loop) // �Ѵﵽ�ض�������ɾ����
                {
                    b_Tricking = false;
                    b_end = true;
                    if (_removeTimer!=null) _removeTimer(_taskId);
                }
            }
        }
    }
	
    /// <summary>
    /// Pause the Timer
    /// </summary>
    public void Pause()
    {
        b_Tricking = false;
    }

    /// <summary>
    /// Stop the Timer
    /// </summary>
    public void Stop()
    {
        b_Tricking = false;
        f_CurTime = f_TriggerTime;
        b_end = true;
    }

    /// <summary>
    /// Continue the Timer
    /// </summary>
    public void Continue()
    {
        b_Tricking = true;
    }

    /// <summary>
    /// Restart the this Timer
    /// </summary>
    public void Restart()
    {
        b_Tricking = true;
        f_CurTime = 0.0f;
    }

    /// <summary>
    /// Change the trigger time in runtime
    /// </summary>
    /// <param name="second">Trigger Time</param>
    public void ResetTriggerTime(float second)
    {
        f_TriggerTime = second;
    }
}
