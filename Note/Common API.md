#### [Linq.IEnumerable](https://learn.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable?view=net-8.0)
> **`IEnumerable<T>.Aggregate<TSource>(IEnumerable<TSource>, Func<TSource,TSource,TSource>)`**
>> Sample
>> ```cs
>> string sentence = "the quick brown fox jumps over the lazy dog";
>> string[] words = sentence.Split(' ');
>> string reversed = words.Aggregate((workingSentence, next) => next + " " + workingSentence);
>> ```

> **`OrderBy<TSource,TKey>(IEnumerable<TSource>, Func<TSource,TKey>)`**
>> Sample
>> ```cs
>> // order by param1, then order by param2; t in T
>> List<T>.OrderBy(t => t.param1).ThenBy(t => t.param2);
>> ```

---

#### Lambda
> Lambda expression
>> Sample
>> ```cs
>> // specify the type explicityly
>> Func<int, int> square = object (int x, int y) => x * y;
>> Console.WriteLine(square(5, 3));
>> ```

> Lambda to write LINQ
>> Sample
>> ```CS
>> // n in numbers
>> int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
>> int oddNumbers = numbers.Count(n => n % 2 == 1);
>> ```

---

#### Set Text Component alignment
> **`GetComponent<Text>().alignment = CS.UnityEngine.TextAnchor.MiddleCenter;`**

---

#### DOTween
> SetEase have three special enum *Flash*, *InFlash*, *InOutFlash*. This jogs apply a blinking effect to the animation.
>> Sample 
>> ```CS
>> SetEase(Ease.Flash, overshoot, period)
>> SetEase(Ease.Flash, 15, 0)
>> ```

> Three LoopType enum *Restart*, *Yoyo*, *Incremental*, set loops to -1 will make loop infinity 
>> Sample 
>> ```CS
>> SetLoops(int loops, LoopType.Restart)
>> ```

> Sequence
> **`Append(Tween tween)`** `//在Sequence的最后添加一个tween`
> **`AppendCallback(TweenCallback callback)`** `//在Sequence的最后添加一个回调函数`
> **`AppendInterval(float interval)`** `//在Sequence的最后添加一段时间间隔`
> **`Insert(float atPosition,Tween tween)`** `//在给定的时间位置上放置一个tween，可以实现同时播放多个tween的效果，而不是一个接一个播放`
> **`InsertCallback(float atPosition, TweenCallback callback)`** `//在给定的时间位置上放置一个回调函数`
> **`Join(Tween tween)`** `//在Sequence的最后一个tween的开始处放置一个tween。可以实现同时播放多个tween的效果，而不是一个接一个播放`
> **`Prepend(Tween tween)`** `//在Sequence开始处插入一个tween，原先的内容根据时间往后移`
> **`PrependCallback(TweenCallback callback)`** `//在Sequence开始处插入一个回调函数`
> **`PrependInterval(float interval)`** `//在Sequence开始处插入一段时间间隔，原先的内容根据时间往后移`
>> Sample
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

> **`DOTween.To(getter, setter, to, float duration)`**
> variable change to target value / **delay execution**
>> Sample
>> ```CS
>> Vector3 myvalue = new Vector3(0, 0, 0);
>> DOTween.To(() => myvalue, x => myvalue = x, new Vector3(10, 10, 10), 2);
>> 
>> DOTween.To(()=> myvalue, x => myvalue = x, 1, 3).OnComplete(() => "execution");
>> DOTween.To(()=> myvalue, x => myvalue = x, 1, 3).OnStepComplete(() => "execution"); // executed every three seconds
>> ```

---

#### 获取XR手柄射线接触到物体（射线有距离限制）
> `XRRayInteractor.GetCurrentRaycastHit(out rayInfor)`

---

#### 启动InputSystem中的Actions
> `InputActionProperty.EnableDirectAction()`

---

#### 通过Select Event，物体获取当前射线来源的对象（设备）
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

#### float到int
> `Mathf.Approximate/RoundToInt/MoveTowards()`
  
---

#### UnityEvent<T>有可能不能正常序列化可加上下面这段
> `[System.Serializable]`
> `public class TakeDamageEvent : UnityEvent<float>{}`

---

#### 获取鼠标在世界的射线
> `Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);`

---

#### UI坐标、世界坐标、屏幕坐标转换
实机尽量不使用Camera.main
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
原文链接：<https://blog.csdn.net/LIQIANGEASTSUN/article/details/124413387>

---

#### 物体移动方式
> `Vector3.MoveTowards(transform.position, target.position, Time.deltaTime)`
> `transform.Translate((target.positin - transform.position) * Time.deltaTime)`
> `this.transform.position = Vector3.Lerp(objectPos, targetPos, curve.Evaluate(x)); //AnimationCurve变速调节应用`

---

#### NavMesh寻路
> NavMesh.Raycast()、NavMesh.SamplePosition()
> NavMesh.CalculatePath()智能寻路

---

#### Object比较运算
> `Object.ReferenceEquals(object, object);  //对比的是所指向的地址 `
> `Object.Equals(Object objA,Object objB);  //默认对比引用`
<https://www.cnblogs.com/weicanpeng/p/8073763.html>

---

#### 添加实现ICollection接口的一个集合的所有元素
> **`List.AddRange()`**
>> Sample
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

<!--
    TEMPLATE:
    <span></span>

    #### Title
    > Usage scenarios / Subject description
    > **` Function declarations `** `//exegesis`
    >> Sample
    >> ```CS
    >> Sample
    >> ```  
-->