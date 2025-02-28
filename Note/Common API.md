# Untitled H1

## Untitled H2

### Categories

#### [Linq.IEnumerable](https://learn.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable?view=net-8.0)

> **`IEnumerable<T>.Aggregate<TSource>(IEnumerable<TSource>, Func<TSource,TSource,TSource>)`**
>> Sample
>>
>> ```cs
>> string sentence = "the quick brown fox jumps over the lazy dog";
>> string[] words = sentence.Split(' ');
>> string reversed = words.Aggregate((workingSentence, next) => next + " " + workingSentence);
>> ```
>
> **`OrderBy<TSource,TKey>(IEnumerable<TSource>, Func<TSource,TKey>)`**
>> Sample
>>
>> ```cs
>> // order by param1, then order by param2; t in T
>> List<T>.OrderBy(t => t.param1).ThenBy(t => t.param2);
>> ```

---

#### Lambda

> Lambda expression
>> Sample
>>
>> ```cs
>> // specify the type explicityly
>> Func<int, int> square = object (int x, int y) => x * y;
>> Console.WriteLine(square(5, 3));
>> ```
>
> Lambda to write LINQ
>> Sample
>>
>> ```CS
>> // n in numbers
>> int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
>> int oddNumbers = numbers.Count(n => n % 2 == 1);
>> ```

---

#### Set Text Component alignment
>
> **`GetComponent<Text>().alignment = CS.UnityEngine.TextAnchor.MiddleCenter;`**

---

#### DOTween
>
> SetEase have three special enum *Flash*, *InFlash*, *InOutFlash*. This jogs apply a blinking effect to the animation.
>> Sample
>>
>> ```CS
>> SetEase(Ease.Flash, overshoot, period)
>> SetEase(Ease.Flash, 15, 0)
>> ```
>
> Three LoopType enum *Restart*, *Yoyo*, *Incremental*, set loops to -1 will make loop infinity
>> Sample
>>
>> ```CS
>> SetLoops(int loops, LoopType.Restart)
>> ```
>
> Sequence
>
> **`Append(Tween tween)`** `//在Sequence的最后添加一个tween`
>
> **`AppendCallback(TweenCallback callback)`** `//在Sequence的最后添加一个回调函数`
>
> **`AppendInterval(float interval)`** `//在Sequence的最后添加一段时间间隔`
>
> **`Insert(float atPosition,Tween tween)`** `//在给定的时间位置上放置一个tween，可以实现同时播放多个tween的效果，而不是一个接一个播放`
>
> **`InsertCallback(float atPosition, TweenCallback callback)`** `//在给定的时间位置上放置一个回调函数`
>
> **`Join(Tween tween)`** `//在Sequence的最后一个tween的开始处放置一个tween。可以实现同时播放多个tween的效果，而不是一个接一个播放`
>
> **`Prepend(Tween tween)`** `//在Sequence开始处插入一个tween，原先的内容根据时间往后移`
>
> **`PrependCallback(TweenCallback callback)`** `//在Sequence开始处插入一个回调函数`
>
> **`PrependInterval(float interval)`** `//在Sequence开始处插入一段时间间隔，原先的内容根据时间往后移`
>> Sample
>>
>> ```CS
>> public Text mText;
>> public float duration = 1;
>> public Vector3 scaleEnd = Vector3.one;
>> public Color colorEnd = Color.red;
>> 
>> private Sequence mSequence;
>> private void Awake()
>> {
>>     mText = GetComponent<Text>();
>>     mSequence = DOTween.Sequence();
>>     mSequence.Append(transform.DOScale(scaleEnd, duration));
>>     mSequence.Insert(0, mText.DOScale(scaleEnd, duration))
>>     mSequence.Join(mText.DOColor(colorEnd, duration));
>>     mSequence.SetAutoKill(false);
>>     mSequence.Pause();
>> }
>> private void Update()
>> {
>>     if (Input.GetKeyUp(KeyCode.O))
>>     {
>>         PlayForward();
>>     }
>> }
>> public void PlayForward()
>> {
>>     mSequence.PlayForward();
>> }
>> ```
>
> **`DOTween.To(getter, setter, to, float duration)`**
>
> variable change to target value / **delay execution**
>> Sample
>>
>> ```CS
>> Vector3 myvalue = new Vector3(0, 0, 0);
>> DOTween.To(() => myvalue, x => myvalue = x, new Vector3(10, 10, 10), 2);
>> 
>> DOTween.To(()=> myvalue, x => myvalue = x, 1, 3).OnComplete(() => "execution");
>> DOTween.To(()=> myvalue, x => myvalue = x, 1, 3).OnStepComplete(() => "execution"); // executed every three seconds
>> ```

---

#### 获取XR手柄射线接触到物体（射线有距离限制）
>
> `XRRayInteractor.GetCurrentRaycastHit(out rayInfor)`

---

#### InputSystem

> BehaviorType中我们有四种不同的调用方式
> 使用Send Message时，每次的触发会调用一个对应的函数（就是在对应的Actions名前面加个On-）
>
>> Sample
>>
>> ```cs
>> public class PlayerController : MonoBehaviour
>> {
>>     void OnAction1(InputValue value)
>>     {
>>         bool isAction1Pressd = value.isPressed;
>>         Debug.Log(isAction1Pressd);
>>     }
>>     void OnMove(InputValue value)
>>     {
>>         Vector2 moveVal = value.Get<Vector2>();
>>         Debug.Log(moveVal);
>>     }
>> }
>> ```
>
> ```cs
> InputActionProperty.EnableDirectAction() // 启动InputSystem中的Actions
> ```
>
> Invoke C Sharp Events
> 与Invoke Unity Events方式其实大致相同，需要自己先写好一个带有InputAction.CallbackContext类型入参的动作方法，不同的是挂载方式变成了脚本事件加载而不是在Unity界面上的可视化挂载
>> Sample
>>
>> ```CS
>>  public PlayerInput playerInput;
>>  void OnEnable()
>>  {
>>      playerInput.onActionTriggered += MyEventFunction;
>>  }
>>  void OnDisable()
>>  {
>>      playerInput.onActionTriggered -= MyEventFunction;
>>  }
>>  void MyEventFunction(InputAction.CallbackContext value)
>>  {
>>      Debug.Log(value.action.name + (" was triggered"));
>>      if (value.action.name == "Move")
>>      {
>>          move = value.ReadValue<Vector2>();
>>      }
>>  }
>> ```
>
> 生成C#脚本，在自己写的PlayerController 类中调用该脚本了
>> Sample
>>
>> ```cs
>> // 输入控制类的实例  
>> private TestInputControls InputControls;
>> //将对应的ActionMaps中对应的Action进行传址引用
>> private Vector2 keyboardMoveAxes = InputControls.Keyboard.moveControl.ReadValue<Vector2>();  
>> 
>> void OnEnable()  
>> {  
>>     InputControls = new TestInputControls(); // 创建输入控制实例  
>>     InputControls.Keyboard.Fire.started += OnFireDown; // 注册开火开始动作的回调  
>>     InputControls.Keyboard.Fire.performed  += OnLongPress; // 注册长按动作的回调  
>>     InputControls.Keyboard.Fire.canceled += OnFireUp; // 注册开火结束动作的回调  
>>     InputControls.Enable(); // 启用InputActions下的所有ActionMap
>> }  
>> 
>> //当开火动作被触发时调用此方法。  
>> private void OnFireDown(InputAction.CallbackContext Obj)  
>> {  
>>     Debug.Log($"Fire Down | KeyName:{Obj.control.name}"); // 输出"Fire Down"到控制台 
>> }  
>> 
>> //当开火动作持续时调用的方法。  
>> private void OnLongPress(InputAction.CallbackContext Obj)  
>> {  
>>     Debug.Log($"Fire Long Press | KeyName:{Obj.control.name},持续时间{Obj.duration}"); // 输出动作持续时间  
>> }  
>> 
>> //当开火动作释放时调用此方法。  
>> private void OnFireUp(InputAction.CallbackContext Obj)  
>> {  
>>     Debug.Log($"Fire Up | KeyName:{Obj.control.name}"); // 输出"Fire Up"到控制台  
>> }  
>> //主要用于移除输入动作的回调函数，并禁用输入控制。  
>> private void OnDisable()  
>> {  
>>     InputControls.Keyboard.Fire.started -= OnFireDown; // 移除开火开始事件的监听  
>>     InputControls.Keyboard.Fire.performed  -= OnLongPress; // 移除长按事件的监听 
>>     InputControls.Keyboard.Fire.canceled -= OnFireUp; // 移除开火结束事件的监听 
>>     InputControls.Disable(); // 禁用输入控制  
>> }
>> private void Update()
>> {
>>     if (keyboardMoveAxes != Vector2.zero)
>>     {
>>         Debug.Log(keyboardMoveAxes);
>>     }
>> }
>> ```

---

#### 通过Select Event，物体获取当前射线来源的对象（设备）
>
> ```cs
> void AttachTransform(SelectEnterEvenetArgs arg){
>     Transform interactor = arg.interactorObject.transform;
> }
> ```

---

#### 脚本中常用属性

- [ContextMenu("Delete save file")]
- [MenuItem("Window/Dialogue Editor")]
- [OnOpenAssetAttribute(num)]
- EditorGUI/EditorGUILayout

---

#### 編輯器扩展功能

> ```CS
> //一般这段代码写在EditorWindow的OnGUI()的某个button下，实现“点击保存”功能
> EditorUtility.SetDirty(simpleScriptableObject); //标记脏数据
> AssetDatabase.SaveAssets(); //对所有未写入硬盘保存的脏数据信息保存
> ```
>
> ```CS
> //值得注意的是，这种创建方式没有创建资产实例，仅仅是在内存中创建，不会保存在硬盘上。
> var simpleScriptableObject = ScriptableObject.CreateInstance<SimpleScriptableObject>();
> //如果想保存在硬盘上，需要使用如下方式
> AssetDatabase.CreateAsset(simpleScriptableObject, "Assets/SomePath/simpleSO.asset"); //记得路径必须合法，也要加上文件后缀
> ```
>
> ```CS
> //这方法有许多重载，请自行查看
> var flag = EditorUtility.DisplayDialog("标题", "显示消息", "确认键");
> ```
>
> ```CS
> private float value; //定义进度条填充值
> private void OnGUI() //EditorWindow.OnGUI
> {
>     if (GUILayout.Button("增加进度")) //手动增加进度
>     {
>         value += 0.1f;
>         value = Mathf.Clamp01(value); //约束value值到0~1
>     }
>     EditorUtility.DisplayProgressBar("进度条", "显示信息", value); //显示进度条
>     if (value == 1)
>     {
>         EditorUtility.ClearProgressBar(); //关闭进度条
>     }
> }
> ```
>
> ```CS
> private Texture tex;
> ...
> if (GUILayout.Button("查找法线贴图"))
> {
>     //参数释义
>     //1. 查找对象的引用
>     //2. 是否允许查找场景对象
>     //3. 查找对象名称过滤（比如这里的normal是指文件名称中有normal的会被搜索到）
>     //4. controlID, 默认写0
>     EditorGUIUtility.ShowObjectPicker<Texture>(tex, false, "normal", 0);
> }
> ```

#### float到int/限制位数
>
> ```cs
> Mathf.Approximate/RoundToInt/MoveTowards()
> (float)Math.Round(double, int)
> string.Format("{0,N2}", float)
> ```
  
---

#### UnityEvent\<T>有可能不能正常序列化可加上下面这段
>
> ```cs
> [System.Serializable]
> public class TakeDamageEvent : UnityEvent<float>{}
> ```

---

#### Unity 获取鼠标在世界的射线
>
> `Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);`

---

#### UI坐标、世界坐标、屏幕坐标转换

实机尽量不使用Camera.main
>
> ```CS
> Vector2 screenPoint = Camera.main.WorldToScreenPoint(worldPoint);
> Vector3 worldPoint = Camera.main.ScreenToWorldPoint(position);
> Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(uiCamera, worldPoint);
> 
> // 当 Canvas renderMode 为 RenderMode.ScreenSpaceCamera、RenderMode.WorldSpace 时 uiCamera 不能为空
> // 当 Canvas renderMode 为 RenderMode.ScreenSpaceOverlay 时 uiCamera 可以为空
> RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, screenPoint, uiCamera, out globalMousePos);
> RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRT, screenPoint, uiCamera, out localPos);
> ```
>
原文链接：<https://blog.csdn.net/LIQIANGEASTSUN/article/details/124413387>

---

#### 物体移动方式
>
> `Vector3.MoveTowards(transform.position, target.position, Time.deltaTime)`
> `Mathf.MoveTowards(float position, float target, float maxDelta)`
> `transform.Translate((target.positin - transform.position) * Time.deltaTime)`
> `this.transform.position = Vector3.Lerp(objectPos, targetPos, curve.Evaluate(x)); //AnimationCurve变速调节应用`
> `Rigidbody.MovePosition(transform.position + transform.forward * Time.deltaTime) //一般配合FixedUpdate使用，适用平滑移动同时避免穿透；如果在刚体上启用了刚体插值，则调用 Rigidbody.MovePosition 会导致在渲染的任意中间帧中的两个位置之间平滑过渡，如果将刚体的 isKinematic 设置为 false，则其以不同的方式工作。 它的工作方式类似于 transform.position=newPosition 并传送对象 （而不是平滑过渡）。`

---

#### NavMesh寻路
>
> NavMesh.Raycast()、NavMesh.SamplePosition()
> NavMesh.CalculatePath()智能寻路

---

#### .Net Object比较运算
>
> `Object.ReferenceEquals(object, object);  //对比的是所指向的地址`
> `Object.Equals(Object objA,Object objB);  //默认对比引用`
<https://www.cnblogs.com/weicanpeng/p/8073763.html>

---

#### .Net 添加实现ICollection接口的一个集合的所有元素
>
> **`List.AddRange()`**
>> Sample
>>
>> ```CS
>> ArrayList myAL = new ArrayList();
>> myAL.Add( "The" );
>> myAL.Add( "quick" );
>> myAL.Add( "brown" );
>> myAL.Add( "fox" );
>>  
>> Queue myQueue = new Queue();
>> myQueue.Enqueue( "jumped" );
>> myQueue.Enqueue( "over" );
>> myQueue.Enqueue( "the" );
>> myQueue.Enqueue( "lazy" );
>> myQueue.Enqueue( "dog" );
>>  
>> myAL.AddRange( myQueue );
>> ```

---

#### Unity游戏对象之间发送消息，在外部调用组件方法
>
> ```CS
> void GameObject.SendMessage(string ModName, object[] paramters, SendMessageOption options)           // 遍历自身所有组件调用对应函数，包括私有
> void GameObject.SendMessageUpwards(string ModName, object[] paramters, SendMessageOption options)    // 推送消息给当前类以及其父类
> void GameObject.BroadcastMessage(string ModName, object[] paramters, SendMessageOption options)      // 推送消息给所有子类
> ```

---

#### UnityEngine.Networking
>
> UnityWebRequest由三个元素组成：
>
> 1. UpLoadHandler处理数据将数据上传到服务器的对象；
> 2. DownLoadHandler从服务器下载数据的对象；
> 3. UnityWebRequest负责与HTTP通信并管理上面两个对象。还处理 HTTP 流量控制。此对象是定义自定义标头和 URL 的位置，也是存储错误和重定向信息的位置
>
> [构造创建的Request没有DownloadHandler和UploadHandler需要手动创建赋值](https://blog.csdn.net/qq_42345116/article/details/123413736)
>
> ```CS
> // 构造函数
> public UnityWebRequest（）; 
> public UnityWebRequest（Uri uri）;
> public UnityWebRequest（Uri uri，string method）;
> public UnityWebRequest（Uri uri，string method,Networking.DownloadHandler downloadHandler，Networking.UploadHandler uploadHandler）;
> SendWebRequest()     // 开始与远程服务器通信。在调用此方法之后，有必要的话UnityWebRequest将执行DNS解析，将HTTP请求发送到目标URL的远程服务器并处理服务器的响应。
> Get(url)             // 创建一个HTTP为传入URL的UnityWebRequest对象
> Post（url)         // 向Web服务器发送表单信息
> Put(url)             // 将数据上传到Web服务器
> Abort()             // 直接结束联网
> Head()             // 创建一个为传输HTTP头请求的UnityWebRequest对象
> GetResponseHeader() // 返回一个字典，内容为在最新的HTTP响应中收到的所有响应头
> ```

---

#### .Net Activator创造实例
>
> <https://learn.microsoft.com/zh-cn/dotnet/api/system.activator?view=net-8.0>
> 通过调用与指定参数最匹配的构造函数来创建程序集中定义的类型的实例。必须具有足够的权限才能搜索和调用构造函数;否则，将引发异常。
>
> ```CS
> System.Activator.CreateInstance(Type)
> System.Activator.CreateInstance(Type, Object[])
> System.Activator.CreateInstance(string, string)
> ```

---

#### C# String.Format
>
> ```CS
> string.Format("{0:C3}", 2)                  // $2.000
> string.Format("{0:D3}", 2)                  // 002
> string.Format("{0:N2}", 56789)              // 56,789.00
> string.Format("{0:X000}", 12)               // C
> string.Format("{0:P1}", 0.23456)            // 23.5%
> string.Format("{0:0000.00}", 195.038)       // 0195.04
> string.Format("{0:###.##}", 12568.039)      // 12568.04
> string.Format("{0:g}", System.DateTime.Now) // 2009-3-20 15:38
> string.Format("{0:d}",System.DateTime.Now)  // 2009-3-20
> string.Format("{0:T}",System.DateTime.Now)  // 15:41:50
> ```

---

#### 从非托管函数导出函数
>
> - DllImport是System.Runtime.InteropServices命名空间下的一个属性类
> - DllImport只能放置在方法声明上。
> - DllImport具有单个定位参数：指定包含被导入方法的 dll 名称的 dllName 参数。
> - DllImport具有6个命名参数：
>   1. CallingConvention 参数指示入口点的调用约定。如果未指定CallingConvention，则使用默认值CallingConvention.Winapi。
>   2. CharSet参数指定用在入口点的字符集。如果未指定CharSet，则使用默认值CharSet.Auto。
>   3. EntryPoint参数给出dll中入口点的名称。如果未指定EntryPoint，则使用方法本身的名称。
>   4. ExactSpelling参数指示EntryPoint是否必须与指示的入口点的拼写完全匹配。如果未指定ExactSpelling，则使用默认值false。
>   5. PreserveSig参数指示方法的签名被保留还是被转换。当签名被转换时，它被转换为一个具有HRESULT返回值和该返回值的一个名为retval的附加输出参数的签名。如果未指定PreserveSig，则使用默认值true。
>   6. SetLastError参数指示方法是否保留Win32“上一错误”。如果未指定SetLastError，则使用默认值false。
> - 返回类型变量、方法名称、参数列表一定要与DLL文件中的定义相一致
> - 它是一次性属性类。
> - 用DllImport属性修饰的方法必须具有extern修饰符。
>
>> Sample
>>
>> ```CS
>> namespace System.Runtime.InteropServices
>>
>> [DllImport(LUADLL, CallingConvention = CallingConvention.Cdecl)]
>> public static extern int luaopen_pb(IntPtr L);
>>
>> [DllImport("kernel32")]
>> private static extern long WritePrivateProfileString(string section,string key,string val,string filePath);
>>```

---

#### EventTrigger
>
> |公共函数|说明|
> |:---:|:---:|
> |OnBeginDrag|在拖动事件开始之前调用|
> |OnCancel|在取消事件发生时由 EventSystem 调用。|
> |OnDeselect|在选择新对象时由 EventSystem 调用。|
> |OnDrag|在拖动期间，每次移动指针时由 EventSystem 调用。|
> |OnDrop|当对象接受拖放时由 EventSystem 调用。|
> |OnEndDrag|拖动结束时由 EventSystem 调用。|
> |OnInitializePotentialDrag|找到了拖动事件，但在它变得有效以开始拖动之前由 EventSystem 调用。|
> |OnMove|发生移动事件时由 EventSystem 调用。|
> |OnPointerClick|发生单击事件时由 EventSystem 调用。|
> |OnPointerDown|发生 PointerDown 事件时由 EventSystem 调用。|
> |OnPointerEnter|当指针进入与此 EventTrigger 关联的对象时由 EventSystem 调用。|
> |OnPointerExit|当指针退出与此 EventTrigger 关联的对象时由 EventSystem 调用。|
> |OnPointerUp|发生 PointerUp 事件时由 EventSystem 调用。|
> |OnScroll|发生滚动事件时由 EventSystem 调用。|
> |OnSelect|发生选择事件时由 EventSystem 调用。|
> |OnSubmit|发生提交事件时由 EventSystem 调用。|
> |OnUpdateSelected|与此 EventTrigger 关联的对象更新时由 EventSystem 调用。|
>
>> Sample
>>
>> ```cs
>> public class LongEventTrigger : EventTrigger
>> {
>>     public Action clickDown;
>> 
>>     public override void OnPointerDown(PointerEventData eventData)
>>     {
>>         base.OnPointerDown(eventData);
>>         //Debug.Log("按下" + this.gameObject.name);
>>         if (clickDown != null)
>>         {
>>             clickDown();
>>         }
>> 
>>     }
>> }
>> ```

---

#### 反射判断对象是否包含某个属性

> 参考<https://blog.csdn.net/wang_xinyu/article/details/107464083>
>
> ```cs
> /// <summary>
> /// 利用反射来判断对象是否包含某个属性
> /// </summary>
> /// <param name="instance">object</param>
> /// <param name="propertyName">需要判断的属性</param>
> /// <returns>是否包含</returns>
> public static bool ContainProperty(this object instance, string propertyName)
> {
>   if (instance != null && !string.IsNullOrEmpty(propertyName))
>   {
>       PropertyInfo _findedPropertyInfo = instance.GetType().GetProperty(propertyName);
>       MethodInfo _findedMethodInfo = instance.GetType().GetMethod(methodName);
>       return (_findedPropertyInfo != null);
>   }
>   return false;
> }
> ```

---

### Uncategories

> 表示 Windows NT 性能计数器组件
> ` PerformanceCounter `
>> Sample
>>
>> ```CS
>> using (var p1 = new PerformanceCounter("Process", "Working Set - Private", "GCtest.vshost"))
>> {
>>     Console.WriteLine( (p1.NextValue()/1024/1024).ToString("0.0")+"MB");
>> }
>> ```
>
> 垃圾回收
> `System.GC.Collect();`
> `Collect(Int32) 强制对 0 代到指定代进行即时垃圾回收;`
>
> 创建Delegate后运行
> `Action mainFunc = (Action)Delegate.CreateDelegate(typeof(Action), MethodInfo method);`
> 反射创建出对象后调用接口，实例化某些以泛型参数指定类型，例如Dll加载场景
> `(Type)Activator.CreateInstance(Type type);`
>
> 将此属性添加至某个静态方法后，系统会在 Unity 即将打开资源时调用该方法。该方法应有以下签名之一：
> `static bool OnOpenAsset(int instanceID, int line)`

### Template
<!--
    <span></span>

    #### Title
    > Usage scenarios / Subject description
    > **` Function declarations `** `//exegesis`
    >> Sample
    >>
    >> ```CS
    >> Sample
    >> ```  
-->