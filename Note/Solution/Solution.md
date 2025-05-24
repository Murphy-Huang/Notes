# Fragmentation Solutions

```cs
// shuffle the array order
public int[] Shuffle(int[] nums)
{
    Random random = new Random();
    for (int i = 0; i < nums.Length; ++i)
    {
        int j = random.Next(i, nums.Length);
        nums[i] ^= nums[j];
        nums[j] ^= nums[i];
        nums[i] ^= nums[j];
    }
    return nums;
}
```

---

```cs
// interval execution
float visualTime = 0;
float nextTime = 0;
// unseen interval Execution method
public void IntervalExecution1(float interval)
{
    visualTime += Time.deltaTime;
    if (visualTime > interval)
    {
        print("do something")
        visualTime -= interval; // maybe make visualTime = 0 is also feasible
    }
}
public void IntervalExecution2(float interval)
{
    if (Time.time >= nextTime)
    {
        print("do something");
        nextTime += interval;
    }
}
Invoke(() => print("do something"), 1f)
InvokeRepeating(() => print("do something"), 1f, 1f)
```

---

Set Random seed

```lua
-- 用毫秒级的最后几位设置
math.randomseed(tostring(os.time()):reverse():sub(1,7))
```

```cs
// use DataTime to set random seed
public int DataTimeRandom()
{
    long tick = DateTime.Now.Ticks; // return data type Int64
    Random ran = new Random((int)(tick & 0xffffffffL) | (int) (tick >> 32));
    return ran
}
// use guid to set random seed
public int GuidRandom()
{
    byte[] buffer = Guid.NewGuid().ToByteArray();
    int iSeed = BitCoverter.ToInt32(buffer, 0);
    Random rand = Random(iSeed)
    return rand.Next()
}
// 其他两种方法，效果更好但计算繁琐
string MemeberShip.GeneratePassword(int length, int numberOfNonAlphanumericCharacters);
RNGCrytoServiceProvider csp = RNGCrytoServiceProvider(); csp.GetBytes(byte[]);
```

---

Image与父级最长边保持比例缩放

```CS
void SetWidthHight(float widthBorder, float heightBorder, bool lockPos)
{
    RectTransform rectTransform = GetComponent<RectTransform>();
    Vector2 parentSize = rectTransform.parent.GetComponent<RectTransform>().rect.size;
    float referWidth = parentSize.width - widthBorder;
    float referHeight = parentSize.height - heightBorder;
    rectTransform.GetComponent<Image>().SetNativeSize();
    //原始尺寸宽高比
    float al = rectTransform.sizeDelta.x / rectTransform.sizeDelta.y;
    //缩放后的尺寸
    if (rectTransform.sizeDelta.x > rectTransform.sizeDelta.y)
    {
        rectTransform.sizeDelta = new Vector2(referWidth, referWidth / al);
    }
    else 
    {
        rectTransform.sizeDelta = new Vector2(referHeight * al, referHeight);
    }
    if (lockPos)
    {
        rectTransform.anchoredPosition = Vector2.zero;
    }
}
```

---

不等概率的随机数，[.Net.Random文档](https://learn.microsoft.com/zh-cn/dotnet/api/system.random?view=net-8.0)

```CS
// 通过不等长的区域，根据随机数落的区域作为索引
int UnequalProbability(int[] probability)
{
    Random rand = Random(DataTime.Now.Millisecond);
    float r = rand.Sample();
    for(int i = 1; i < probability.size(); ++i)
    {
        if (r > probability[i-1] && r < probability[i])
        {
            return i;
        }
    }
}
```

---

物体是否被相机看到

> ```CS
> // 转换为视口坐标，正式使用不要有Camera.main
> bool ObjectVisible(Camera camera, GameObject obj) {
>     vector3 viewPortPosition = camera.WorldToViewportPoint(obj.transform.position);
>     if (viewportPosition.x > 0 && viewportPosition.x < 1 && viewportPosition.y > 0 && viewportPosition.y < 1){
>         return true;
>     }
>     return false;
> }
> ```

---

代码生成GUI

> ```CS
> private void OnGUI()
> {
>     if (GUI.Button(new Rect(0, 0, 100, 30), "1"))
>     {
>         animation.Play("step1");
>     }
> }
> ```

---

生成GUID

> ```CS
> if (string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue))
> {
>     property.stringValue = System.Guid.NewGuid().ToString();
>     serializedObject.ApplyModifiedProperties();
> }
> ```
>
> ```CS
> string path = AssetDatabase.GetAssetPath(this);
> itemId = AssetDatabase.AssetPathToGUID(path);
> ```

---

lua的table.remove的不安全问题，数值型解决方法

> ```lua
> local index, r_index, length = 1, 1, #obj
> while index <= length do
>     local v = obj[index]
>     obj[index] = nil
>     if not rm_func(v) then
>         obj[r_index] = v
>         r_index = r_index + 1
>     end
>     index = index + 1
> end
> ```

---

避免每帧GC
字典的 GetEnumerator() 返回的是结构体：
在 C# 中，Dictionary<TKey, TValue>.GetEnumerator() 返回的是一个结构体类型的枚举器（而非类对象），例如 Dictionary<int, NetSocket>.Enumerator。结构体是值类型，分配在栈上，不会触发堆内存分配，因此不会产生 GC 压力。

避免 foreach 的隐式装箱：
如果使用 foreach 遍历字典，在某些旧版本的 Unity 或 C# 运行时中，结构体枚举器可能会被隐式装箱为 IEnumerator 接口（引用类型），导致堆分配和 GC。而代码中通过 while (enumerator.MoveNext()) 手动遍历枚举器，避免了装箱操作，确保整个过程无堆内存分配。

> ```CS
> void Update ()
> {
>     //此种遍历方式可以避免每帧GC
>     var enumerator = nets.GetEnumerator();
>     while (enumerator.MoveNext())
>     {
>         var element = enumerator.Current;
>         element.Value.Update();
>     }
> }
> ```

---

单帧检测碰撞

> ```CS
> public static Collider[] Physics.OverlapSphere (Vector3 position, float radius, int layerMask= AllLayers, QueryTriggerInteraction queryTriggerInteraction= QueryTriggerInteraction.UseGlobal);
> public static int Physics.OverlapCapsuleNonAlloc (Vector3 point0, Vector3 point1, float radius, Collider[] results, int layerMask= AllLayers, QueryTriggerInteraction queryTriggerInteraction= QueryTriggerInteraction.UseGlobal);    // 给定缓冲区 results，返回缓冲区长度
> ```

---

技能特效系统
Skill(Class)
SkillEntity(ScriptObject).Controller(Monobehaviour)
List<Effect(Class)>

```cs
void Skill:UseSkill(Prefab controller, Callback effect)
{

}
```
