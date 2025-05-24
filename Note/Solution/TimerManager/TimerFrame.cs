using System;

public class TimerFrame
{
    //If the Timer is running 
    private bool b_Tricking;

    //If the Timer is Stop
    private bool b_end;

    private int _interval_frames; // 最小值为1

    //Use delegate to hold the methods
    private Action _callback;
    private Action<object> _callbackWithparam;
    private object _callParam;

    private Action<long> _removeTimer;

    // 构造函数
    public TimerFrame(int interval_frames, Action callback, Action<object> callbackWithparam, object callParam)
    {
        _interval_frames = interval_frames > 0 ? interval_frames : 1;
        _callback = callback;
        _callbackWithparam = callbackWithparam;
        _callParam = callParam;
    }

    // 当前帧数
    private int m_curFrame = 0; 
    public void Update()
    {
        if (b_Tricking)
        {
            m_curFrame++;
            if (m_curFrame >= _interval_frames)
            {
                if (_callback != null) _callback();
                if (_callbackWithparam != null) _callbackWithparam(_callParam);
                m_curFrame = 0;
            }
        }
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
        m_curFrame = 0;
    }
}
