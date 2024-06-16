# 物体移动方式
`Vector3.MoveTowards(transform.position, target.position, Time.deltaTime)`
`transform.Translate((target.positin - transform.position) * Time.deltaTime)`
`this.transform.position = Vector3.Lerp(objectPos, targetPos, curve.Evaluate(x));`*AnimationCurve变速调节应用*

# 获取鼠标在世界的射线
`Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);`

# this.transform
- 调用this.transform实际上是一个调用intenal method的过程（这是用C/C++写的，不是MONO的）。值得注意的是这个调用方法略慢，因为你需要调用外部的CIL（aka interop），花费了额外的性能。

# Find()
- update()中尽量不使用Find()

# NavMesh寻路
1. NavMesh.Raycast()、NavMesh.SamplePosition()、2. NavMesh.CalculatePath()智能寻路
  
# 关于GUID：
```C#
if (string.IsNullOrEmpty(property.stringValue) || !IsUnique(property.stringValue))
{
    property.stringValue = System.Guid.NewGuid().ToString();
    serializedObject.ApplyModifiedProperties();
}
```
```C#
string path = AssetDatabase.GetAssetPath(this);
itemId = AssetDatabase.AssetPathToGUID(path);
```

# UnityEvent<T>有可能不能正常序列化可加上下面这段
`[System.Serializable]`
`public class TakeDamageEvent : UnityEvent<float>{}`

# float到int
- Mathf.Approximate/RoundToInt/MoveTowards()

# UI设置
1. Aspect Ratio Fitter固定图片比例
2. Constant Size Fitter配合grid group使用限制范围

# 关于数据存储：两种方式
```C#
public void Save(GameData _data)
{
    string fullPath = Path.Combine(dataDirPath, dataFileName);

    try
    {
        Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
        string dataToStore = JsonUtility.ToJson(_data, true);
        using (FileStream stream = new FileStream(fullPath, FileMode.Create))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                if(encryptData) 
                    dataToStore = EncryptDecrypt(dataToStore);
                writer.Write(dataToStore);
            }
        }
    }   
    catch(Exception e)
    {
        Debug.LogError("Errror on trying to save data to file: " + fullPath + "\n" + e.ToString());
    }
}
public GameData Load()
{
    string fullPath = Path.Combine(dataDirPath, dataFileName);
    GameData loadData = null;
    if (File.Exists(fullPath))
    {
        try
        {
            string dataToLoad = "";
            using (FileStream stream = new FileStream(fullPath, FileMode.Open))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    dataToLoad = reader.ReadToEnd();
                    if (encryptData)
                        dataToLoad = EncryptDecrypt(dataToLoad);
                }
            }
            loadData = JsonUtility.FromJson<GameData>(dataToLoad);
        }
        catch (Exception e)
        {
            Debug.LogError("Errror on trying to load data from file: " + fullPath + "\n" + e.ToString());
        }
    }
    return loadData;
}
```
```C#
private Dictionary<string, object> LoadFile(string saveFile)
{
    string path = GetPathFromSaveFile(saveFile);
    if (!File.Exists(path))
    {
        return new Dictionary<string, object>();
    }
    using (FileStream stream = File.Open(path, FileMode.Open))
    {
        BinaryFormatter formatter = new BinaryFormatter();
        return (Dictionary<string, object>)formatter.Deserialize(stream);
    }
}

private void SaveFile(string saveFile, object state)
{
    string path = GetPathFromSaveFile(saveFile);
    print("Saving to " + path);
    using (FileStream stream = File.Open(path, FileMode.Create))
    {
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, state);
    }
}
```

# 脚本中常用属性
- [ContextMenu("Delete save file")]
- [MenuItem("Window/Dialogue Editor")]
- [OnOpenAssetAttribute(num)]
- EditorGUI/EditorGUILayout

# 通过Select Event，物体获取当前射线来源的对象（设备）
`void AttachTransform(SelectEnterEvenetArgs arg){
    Transform interactor = arg.interactorObject.transform;
}`

# 启动InputSystem中的Actions
`InputActionProperty.EnableDirectAction()`

# 获取XR手柄射线接触到物体（射线有距离限制）
`XRRayInteractor.GetCurrentRaycastHit(out rayInfor)`

# 代码生成GUI
```C#
private void OnGUI()
{
    if (GUI.Button(new Rect(0, 0, 100, 30), "1"))
    {
        animation.Play("step1");
    }
}
```

# 物体是否被相机看到：
-转换为视口坐标，正式使用不要有Camera.main
```C#
bool ObjectVisible(Camera camera, GameObject obj) {
    vector3 viewPortPosition = camera.WorldToViewportPoint(obj.transform.position);
    if (viewportPosition.x > 0 && viewportPosition.x < 1 && viewportPosition.y > 0 && viewportPosition.y < 1){
        return true;
    }
    return false;
}
```

# VR
- 需要添加InputActionManager组件

# 其他
- enum 和 interface 搭配使用
- 使用动画关键帧（add property）代替脚本
- EventSystem.current.IsPointerOverGameObject()检测UI
- width height：多少像素点来渲染，因此更改大小最好使用scale。将ui和canvas结合使用获得适当的大小和分辨率
  - 协程问题
  1. Invoke受Time.timeScale影响，并且无法避免（若可以还请指正）Coroutine可以通过Time.unscaledDeltaTime，WaitForSecondsRealtime来执行不受Time.timeScale影响的代码。菜单、UI、HUD等可以考虑用Coroutine
  2. 当类所属游戏对象active为false时，函数中的StartCoroutine无法执行，而函数中的Invoke仍可以执行。如果在SetActive(true)前需要进行一些与本体无关的额外处理而需要推迟SetActive(true)（如登场时的光效动画等），考虑使用Invoke。若用Coroutine，就需要很多额外代码来调整各部件的出现时间、Start中调用的函数何时开始执行等，或者就需要把动画和时间的处理函数写在其他类（如敌人或玩家角色的Manager类等）中
- 在角色周围生成粒子可以形成在整个场景粒子的错觉
- 利用unityevent，interface，unityaction解耦
- where include a prefab often overrides the transform of root element of prefab
- 私有接口函数：处理接口函数之间重用部分而不在实现类实现，实现类不可访问
- Resources文件夹 Resources.LoadAll
- Editor文件夹 EditorWindow类
- 利用static作为全体类实体的存储库
- 直接使用prefab有的组件生成实体
- 利用config中prefab实例化（关于config的使用）
- object.ReferenceEquals比较两个对象
- 将存储实现ISavable依赖于SavingEntity调用，避开存在ISavable实现的多处地方调用形成相同的副本
- 在不同场景物体GUID相同时，加载问题
- List.AddRange()实现ICollection接口的一个泛型集合，所有指定元素到末尾
- ScriptableObject的非持久化
- 基于IEnumerable来隐藏替换List、Array等可迭代元素

# 参考BLOG
- [游戏中近战攻击判定检测——射线检测](https://blog.csdn.net/wch3351028/article/details/122326021)
- [实现物体围绕某一点进行旋转](https://blog.csdn.net/qiaoquan3/article/details/51306514)
- [四种迷宫生成算法的实现和可视化](https://blog.csdn.net/imred/article/details/105329806)
- [正则](https://refrf.dev/)
- [UGUI相关文章](https://www.zhihu.com/column/c_1440746540318650368)
- [Unity 射线检测的原理分析](https://zhuanlan.zhihu.com/p/585528006)
- [操作系统](https://www.bilibili.com/list/watchlater?oid=857492315&bvid=BV1jV4y1H7Gj&spm_id_from=333.1007.top_right_bar_window_view_later.content.click)
- [刷题中输入处理c++篇](https://blog.csdn.net/HappyHeavyRain/article/details/106750601)
- [Unity-UGUI](https://juejin.cn/user/4143405263232440/posts)
- [Unity-Scene功能](https://blog.csdn.net/weixin_44013533?type=blog)
- [unity手册Graphic的API](https://docs.unity.cn/cn/2018.4/ScriptReference/UI.GraphicRaycaster.html)
- [Cinemachine Camera详细讲解和使用](https://zhuanlan.zhihu.com/p/516625841)