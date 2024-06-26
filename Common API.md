#### [Linq.IEnumerable](https://learn.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable?view=net-8.0)
> **`IEnumerable<T>.Aggregate<TSource>(IEnumerable<TSource>, Func<TSource,TSource,TSource>)`**
>> Sample
>> ```C#
>> string sentence = "the quick brown fox jumps over the lazy dog";
>> string[] words = sentence.Split(' ');
>> string reversed = words.Aggregate((workingSentence, next) => next + " " + workingSentence);
>> ```

> **`OrderBy<TSource,TKey>(IEnumerable<TSource>, Func<TSource,TKey>)`**
>> Sample
>> ```C#
>> // order by param1, then order by param2; t in T
>> List<T>.OrderBy(t => t.param1).ThenBy(t => t.param2);
>> ```

---

#### Lambda
> Lambda expression
>> Sample
>> ``` C#
>> // specify the type explicityly
>> Func<int, int> square = object (int x, int y) => x * y;
>> Console.WriteLine(square(5, 3));
>> ```

> Lambda to write LINQ
>> Sample
>> ```C#
>> // n in numbers
>> int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
>> int oddNumbers = numbers.Count(n => n % 2 == 1);
>> ```

---

#### Set Text Component alignment
> **`GetComponent<Text>().alignment = CS.UnityEngine.TextAnchor.MiddleCenter;`**

---

<span id = "guid" style = "display:none;"></span>

#### Set Random seed with GUID 
> ```C#
> byte[] buffer = Guid.NewGuid().ToByteArray();
> int seed = BitCoverter.ToInt32(buffer, 0);
> Random rand = Random(seed);
> rand.Next();
> ```

---

#### DOTween
> SetEase have three special enum *Flash*, *InFlash*, *InOutFlash*. This jogs apply a blinking effect to the animation.
>> Sample 
>> ```C#
>> SetEase(Ease.Flash, overshoot, period)
>> SetEase(Ease.Flash, 15, 0)
>> ```

> Three LoopType enum *Restart*, *Yoyo*, *Incremental*, set loops to -1 will make loop infinity 
>> Sample 
>> ```C#
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
>> ```C#
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
>> ```C#
>> Vector3 myvalue = new Vector3(0, 0, 0);
>> DOTween.To(() => myvalue, x => myvalue = x, new Vector3(10, 10, 10), 2);
>> 
>> DOTween.To(()=> myvalue, x => myvalue = x, 1, 3).OnComplete(() => "execution");
>> DOTween.To(()=> myvalue, x => myvalue = x, 1, 3).OnStepComplete(() => "execution"); // executed every three seconds
>> ```

---

TEMPLATE:
<span></span>

#### Title
> Usage scenarios / Subject description
> **` Function declarations `** `//exegesis`
>> Sample
>> ```C#
>> Sample
>> ```  