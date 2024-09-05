# Notes

## Untitled

### Markdown相关

- `<link rel = "stylesheet" type = "text/css" href = "Notes.css" />`
- [markdown系列blog](https://www.cnblogs.com/hanzongze/category/1475469.html)
- 在文件加入css方式
  - `<link rel = "" type = "" href = "" />`
  - 在标签添加*style*属性：`<span style = "color:blue" >`
- '/'forward slash在网络上使用，'\'backward slash在window文件系统和转义使用
- URL保留字符：space %20

### 其他

- redis与monogo配合mysql实现冷热数据存储
- 多层状态机中，上层状态机为下层状态机设置目标实现控制
- mvc和esc设计的类同和区别，避免在游戏开发使用mvc
- 欧拉角存在万向结死锁的情况，由于其旋转定义为先z、再x、再y的顺规定义，在某些情况存在某轴向自由度丢失的情况
- 四元素是一种人为定义的数学工具，让相对坐标轴的平滑的旋转移动可以通过一个数值的变化表现出来，摆脱以往需要多个数值变化的繁杂
- jps寻路，A*的改进算法，根据强迫邻居、jump point来确定路径，跳跃查找减少运算量
- smb共享：专用防火墙关闭，固定ip，开启网络发现，密码保护共享，smb文件共享功能，完成后重启

---

### 常用插件

1. Unity-Logs-Viewer插件
2. SimpleJson
3. rapidJson解析器
4. flutter_unity_widget[android打包](https://juejin.cn/post/7021063008143015950)

### 注释说明

1. TODO：如果代码中有该标识，说明在标识处有功能代码待编写，待实现的功能在说明中会简略说明。
2. FIXME：如果代码中有该标识，说明标识处代码需要修正，甚至代码是错误的，不能工作，需要修复，如何修正会在说明中简略说明。
3. XXX：如果代码中有该标识，说明标识处代码虽然实现了功能，但是实现的方法有待商榷，希望将来能改进，要改进的地方会在说明中简略说明。
4. HACK：如果代码中有该标识，说明标识处代码需要根据自己的需求去调整程序代码。

### 代码规范

- bool类型以is、has、can开头
- 接口以I开头
- 以基类作为派生类结尾
- 泛型类型以T开头
- 枚举类型使用单数
- 谓词命名方法和事件，不使用Before、After区分事件前后
- 属性和字段使用Pascalcase
- 参数和局部变量使用驼峰
- 私有以'_'开头+驼峰
- 变量名以@开头，使用关键词作为名称
- 特性以Attribute结尾
- 文件地址相关全lowercase
- 非公开方法可以以Internal、Implement开头
- 异步以Async结尾
- 单词不能产生歧义
  Order用于排序，不用于命令
  Apply用于应用，不用于申请
  Command用于名词，不用于动词
- 尽量不使用单复数不符合常见形式的（适当的违背语法）

---

### 部分报错处理

1. 使用protobuf-net or protobuf
   - [官方实现版本过高，需要注意unity版本](https://www.cnblogs.com/caiger-blog/p/14040130.html)
2. pip不是命令
   - pip未注册环境变量
   - [pip安装第三方报错](https://www.cnblogs.com/yinhaiping/p/13375375.html)
    > 添加--trusted-host有效: -i http://pypi.tuna.tsinghua.edu.cn/simple/ --trusted-host pypi.tuna.tsinghua.edu.cn
3. 缺少XLua项
   - 删掉Gen文件夹重新生成
4. Component GUI Layer in Main Camera is no longer available.
   - remove Component Flare Layer in Camera
5. `<a id="unitylinker"></a>`'could not produce class with id XXX', can find class id in [https://docs.unity3d.com/Manual/ClassIDReference.html](https://docs.unity3d.com/Manual/ClassIDReference.html), add link.xml in Assets document and fill in following information: `<assembly fullname="UnityEngine"> <type fullname="UnityEngine.SphereCollider" preserve="all"/> </assembly>`
6. attempt to index a boolean value：lua模块语句缺少return关键词
7. Android打包libmain.os不能加载，Configuration的il2cpp模式缺少ARM64平台作为目标

### 面向对象提示

1. 使用事件来进行跨模块的传递，不需要引用数据实例
2. 函数的参数合法性检测：一般只在用户的输入正确性，测试阶段调试或设计兼容性高的函数时需要检测合法性
3. 委托是一个类型，将具体实现交付出去；回调是函数指针，将执行时机交付出去
4. 整数ID比指针更容易指代无效的对象
5. 闭包可以从类变量与类函数来作用域来理解，函数的闭包如同类中的函数调用类的变量（自由变量）。在当前作用域之外将自由变量的状态保存下来，保持对词法作用域的引用
6. 类是引用传递，结构体默认是值传递；类可以有虚方法也可以继承其他类，结构体没有虚方法也不能继承
7. 在仅仅使用方法时静态工具类取代单例，单例只在需要面向对象特性时使用
8. 基于IEnumerable来隐藏替换List、Array等可迭代元素
9. enum 和 interface 搭配使用
10. 私有接口函数：处理接口函数之间重用部分而不在实现类实现，实现类不可访问
11. 避免使用foreach，因为会释放内存

### Lua

1. lua调用CS存在多种方式xlua,tolua,slua
2. lua调用CS脚本的绑定函数（事件实现），将对应函数绑定在cs脚本（生命周期）上，实现lua的逻辑
3. lua设置元表，不能使lua寻找父类的方法或属性，需要设置原方法：self.__index = self。因table的查找逻辑是先判断是否有元表再判断元表的__index方法，不会直接查找元表。当在当前table找不到属性的时候会一直回溯到元表table声明时就存在的属性，这些属性在面向对象中会像静态变量一样存在。

### C\#

1. C#通用委托EvenArgs、EventHandler构建事件管理中心：CustomEventArgs.cs(继承EventArgs)/EventManager.cs/EventName.cs/EventTriggerExt.cs。也可以用泛型匹配方法的参数，不用EventArgs创建参数包装类。
2. C#扩展方法
   - 必须在非泛型的靜態類中聲明，且是静态方法
   - 扩展方法必须有一个参数，且只有第一个参数使用this标记
   - 靜態類本身必须具有文件作用域
   - 编译要求导入扩展方法
   - 调用时无需传递第一个参数，默认调用this作为第一个参数
   - 是不是每个对象都加入了这个扩展方法？这个问题其实并未发生，因为C#使用的方式不是给每个对象加一个方法，而是另外提供了一个扩展方法的列表，在使用时通过列表找到被扩展的静态方法然后调用，也就是说方法还是只有那一个方法，并没有大范围的占据方法区。
3. Enum做为字典的key的时候，会有装箱的行为，因为Enum没有实现IEquatable,这是字典的key必要的接口。
4. ArrayList不是类型安全的，List时类型安全，而且使用时会有拆箱装箱操作，连着对比array优点在于动态长度
5. async/await：await不会开启新的线程；异步调用前的线程会在异步等待时放回线程池，异步等待结束后，会从线程池取一个空闲的线程，来运行异步等待调用结束后的后续代码。
6. Invoke，BeginInvoke区别：Invoke会阻塞当前线程，begininvoke则可以异步调用，不会等委托方法执行结束；invoke（同步）和begininvoke（异步）的概念，其实它们所说的意思是相对于子线程而言的，其实对于控件的调用总是由主线程来执行的。
7. 使用Mathf.PI时注意有效输入范围和结果精度（角度*PI/180 = 弧度）
8. 利用static作为全体类实体的存储库
9. const: 是静态的、编译期变量，只在聲明时赋值
   readonly: 运行时变量，在聲明/构造时赋值
   static readonly: 静态的、编译期时变量，只在静态构造时赋值
10. lock()应该锁定引用类型，推荐锁定私用的只读静态对象：private static readonly obj = new obj();
11. [optional]属性使方法参数可选：void func([optional] int num)
12. 字符串比较一般情况下，建议调用不依赖于默认设置的方法，因为这会明确代码的意图。这进而使代码更具可读性且更易于调试和维护。StringComparison.CurrentCulture/InvariantCulture/Ordinal。[https://blog.csdn.net/dark_tone/article/details/101808816](https://blog.csdn.net/dark_tone/article/details/101808816)
13. 适当使用弃元"_"减少内存的分配，例：swicth、out、元组、函数无用返回值
14. 使用partial将类分到不同的地方实现，逻辑清晰些；例：扩展方法、引用外部扩展
15. DllImport其功能是提供从非托管DLL导出的函数的必要调用信息。会按照顺序自动去寻找的地方
    - exe所在目录
    - System32目录
    - 环境变量目录
16. [MonoPInvokeCallbackAttribute()]，此属性在静态函数上有效，Mono 的提前编译器使用它来生成支持回调用托管代码的本机调用所需的代码。
17. DllImport & MonoPInvokeCallbackAttribute 配合使用，
18. 字符'$'作用：代替string.format()；格式：$"string{}"
19. 字符'@'作用：原意标识符，即除了("")不会按字面解释，简单转义、Unicode转义序列都将按字面解释；格式：@"string"

### Unity

1. ![热更新流程图](../Picture/hotRefresh%20flowChart.png)
2. 关于代码剥离的构建：Unity会使用一个专门用于托管代码剥离的工具UnityLinker来进行剥离处理,其默认将unity中用到的所有程序集合并程一个整体程序集，然后根据一定规则，比如场景中游戏对象继承Monobehavior的对象，标记根元素，再次有根元素进行依赖查询，并将其他依赖的程序集或类或命名空间进行打标记。最后没有被标记的，将会被裁剪剥离。UnityLinker在构建时，会检查Assets/link.xml文件[sample](#unitylinker)，将里面设置的忽略的程序集或者类型直接标记为根元素。或者我们可以为需要保留的程序集、类和方法加上[Preserve]特性，针对性的解决错误代码剥离。[https://blog.csdn.net/zhush_2005/article/details/125229154](https://blog.csdn.net/zhush_2005/article/details/125229154)
3. 利用config中prefab实例化（关于config的使用）
4. 将存储实现ISavable依赖于SavingEntity调用，避开存在ISavable实现的多处地方调用形成相同的副本
5. AppInfoBack是安卓和iOS写死的回调方式
6. 外部调用通过: void SendMessage(string methodName, object value = null, SendMessageOptions options = SendMessageOption.RequireReceiver);
7. UnityWebRequest和HttpWebRequest系列都可以用来进行通信，功能上是一样的
8. [插件默认下载位置](C:\Users\Administrator\AppData\Roaming\Unity)
9. 聲明 [DllImport("__Internal")]表示这个函数位于DLL中，DLL名字是 __Internal，这是固定语法，意思是这个函数是静态链接在 iOS 的 App 中的
10. [MonoPInvokeCallback()]，用来标记这个函数会被iOS反向调用<https://www.jianshu.com/p/f01c7e3f666c>
11. Burst Compiler的代码优化过程主要包括以下几个步骤：
    1. 将C#代码编译为中间语言IL代码。
    2. 将IL代码转换为C++代码。
    3. 使用C++编译器将C++代码编译为本地代码。
    4. 使用Burst Compiler的代码生成器生成多个版本的本地代码。
    5. 使用SIMD指令和多线程技术来优化代码的性能。
    6. 使用缓存优化技术来优化代码的性能。
12. Burst就是LLVM将C#代码转换成LLVM IR中间代码，通过LLVM的优化和代码生成功能生成目标平台的Native机器码。这个过程中，Burst利用了LLVM中内置的向量化指令优化技术，将一些常规的循环和算法转换成SIMD指令集，以实现对代码的高效优化。但Burst只支持值类型的数据编译，不支持引用类型数据编译。<https://zhuanlan.zhihu.com/p/623274986>
13. 多线程方式：TPL（task）、job system，TPL是.Net 5后基于ThreadPool设计的一组api，job system是unity提供配合brust使用的多线程解决方案
14. async 用在方法定义前面，await只能写在带有async标记的方法中；注意await异步等待的地方，await后面的代码和前面的代码执行的线程可能不一样；async关键字创建了一个状态机，类似yield return 语句；await会解除当前线程的阻塞，完成其他任务；处理本地IO和网络IO任务是尽量使用async/await来提高任务执行效率

#### Editor

1. where include a prefab often overrides the transform of root element of prefab
2. 处理好prefab的apply、reverse、copy的关系
3. ScriptableObject的非持久化
4. Editor文件夹 EditorWindow类
5. TextAsset文本资源存储txt/json/bytes格式文件，TextAsset不适用于运行时生成文本
6. vr项目需要添加InputActionManager组件
7. 直接使用prefab有的组件生成实体
8. 在角色周围生成粒子可以形成在整个场景粒子的错觉
9. 使用动画关键帧（add property）代替脚本

#### Script

1. 物体在SetActive隐藏后，脚本仍会运行
2. DOTween在脚本结束的时候要DOKill杀死动画，例如在SetLoops之后，不然可能有意料之外的状态
3. 利用unityevent，interface，unityaction解耦
   - 脚本不被勾选，虽然大部分生命周期函数不会执行，但是内置的事件监测的方法，譬如OnMouseDown()，OnTriggerEnter();都能运行
   - 可以考虑动态加载和卸载这个脚本
4. 调用this.transform实际上是一个调用intenal method的过程（这是用C/C++写的，不是MONO的）。值得注意的是这个调用方法略慢，因为你需要调用外部的CIL（aka interop），花费了额外的性能
5. yield语句就是这条分界线，想要代码“停住”，就不执行后面语句对应的代码块，想要代码恢复，就接着执行后面语句对应的代码块。而调度上下文的保存，是通过将需要保存的变量都定义成成员变量来实现的。[参考](https://www.cnblogs.com/iwiniwin/p/14878498.html)
6. update()中尽量不使用Find()
7. 继承Mono和不继承Mono的单例的写法不同
8. 运行时的公开数据用get、set避免在inspector面板上出现
9. 协程问题
   - Invoke受Time.timeScale影响，并且无法避免。Coroutine可以通过Time.unscaledDeltaTime，WaitForSecondsRealtime来执行不受Time.timeScale影响的代码。菜单、UI、HUD等可以考虑用Coroutine
   - 当类所属游戏对象active为false时，函数中的StartCoroutine无法执行，而函数中的Invoke仍可以执行。如果在SetActive(true)前需要进行一些与本体无关的额外处理而需要推迟SetActive(true)（如登场时的光效动画等），考虑使用Invoke。若用Coroutine，就需要很多额外代码来调整各部件的出现时间、Start中调用的函数何时开始执行等，或者就需要把动画和时间的处理函数写在其他类（如敌人或玩家角色的Manager类等）中
10. 协程无法返回值，可以利用回调函数、共享变量、事件来返回结果
11. 协程适用于处理Unity 对象、生命周期等与Unity API交互相关的任务，如延时、动画序列、协作动作；线程更适合计算密集型任务，如物理模拟、算法计算

#### 资源持久化

1. 在不同场景物体GUID相同时，加载问题，[冲突避免](https://blog.csdn.net/linjf520/article/details/127998024)
2. Resources文件夹 Resources.LoadAll
3. playerPrefs存储玩家简单的数据：string,int,float在注册表上，但可以使用JsonUtility工具将unity可序列化的类转换成json格式存储，间接存储更复杂数据
4. 调用打包函数BuildPipeline.BuildAssetBundles时，需要传进去一个Path，用于存放打包的AssetBundle，通常传进去的是Application.streamingAssets。然后在打包完成后，unity会默认生成一个存放AssetBundle的文件夹同名的assetbundle文件，用来存放所有AssetBundle的依赖关系，在这里，就会生成一个叫StreamingAssets的AssetBundle文件。因此，在加载某一个AssetBundle之前，我们都必须先加载这个名称叫做StreamingAssets的bundle文件，然后通过这个bundle文件寻找任意一个AssetBundle需要的依赖文件。<https://www.jianshu.com/p/95af464020c7>
5. 持久化路径：Application.dataPath跟apk同级，常用于访问Assets目录，可读写，但可能有权限问题，写入优先考虑persistentDataPath；Application.persistantDataPath改文件在安装完apk后，里面的数据持久存在，可读写，常在运行时使用；Application.StreamingAsset只可读，常在初始化阶段使用。[参考](https://zhuanlan.zhihu.com/p/141641436)

#### 打包

1. 关于android打包：unityhub 安装 android build support: OpenJDK & SDK；unity editor 设置 perferences 的external tools 的 JDK 和 SDK（可以不内置SDK，减少因安装不同版本Unity占用的硬盘空间）[https://developer.unity.cn/projects/5e6b6a78edbc2a00245cbbef](https://developer.unity.cn/projects/5e6b6a78edbc2a00245cbbef)
2. 打包的目标目录必须是可以被删除的，例如：被打包的项目目录、桌面目录

#### UGUI

1. Button在Selected状态，可以理解为按钮被按下之后，Selected的状态其实相当于一个”lock（锁定）“状态，需要执行一步”unlock（解锁）“的动作才能将按钮返回普通状态。
2. Button.colors参数修改无效，需要将整个BlockColor结构重新赋予；BlockColor需要注意colorMultipier的设置
3. 注意组件默认设置的Color
4. 不同组件的rectTransform不能直接赋值
5. EventSystem.current.IsPointerOverGameObject()检测UI
6. 改变position的时候需要注意缩放，特别是父物体的scale
7. width height：多少像素点来渲染，因此更改大小最好使用scale。将ui和canvas结合使用获得适当的大小和分辨率
8. UI设置
   - Aspect Ratio Fitter固定图片比例
   - Constant Size Fitter配合grid group使用限制范围
9. 动态设置RectTransform
   - rectTransform.anchorMin/anchorMax设置锚点
   - SetInsetAndSizeFromParentEdge() 设定 RectTransform 到父对象的某一边（参数：edge）的距离（参数：inset），以及在该轴向上的大小（参数：size）
   - SetSizeWithCurrentAnchors() 只设定 RectTransform 在某轴向（参数：axis）上的大小（参数：size），还需要 anchoredPosition 辅助设定其在该轴向上的位置
   - rectTransform.rect.size(rect.height, rect.width)返回矩形大小，sizeDelta = offsetMax - offsetMin（ui本身大小减去锚框大小）
   - rectTransform.anchoredPosition(从锚框的pivot位置，指向RectTransform的pivot的一个向量)，可以改变元素Pivot到锚框中心点的距离或返回pivot所处相对位置
   - pivot的位置就是RectTransform.localPosition
   - 当 Anchors 分散（即在某方向上存在 Stretch）时，需要使用 offsetMin 和 offsetMax 的对应分量来设定位置（即 RectTransform 到父对象边缘的距离(UI元素的右上角的坐标，减去AnchorMax的值)）
   - rectTransform.GetWorldCorners(corners)获取四个角的坐标,间接设置
   - 锚框(W,H) = (AnchorMax - AnchorMin) * 父物体(W,H)
   - 不同组件的rectTransform的变量不能直接赋予（存疑）

---

### 碰撞检测

- 多变体碰撞检测：分离轴定理（SAT）：依次再不同角度照射待检测物体，当存在一个角度两者影子没有重叠则分离轴存在
- 圆形碰撞检测：略
- Multi Box Pruning（SAP + 网格）
  - 网格：对预处理对象进行区域划分，只关注每个小格子内的遍历
    - 四叉树：维护一个四叉树数据结构，各个对象均匀分布在叶节点，当一个叶节点超出容量上限则新分出四个叶节点
  - 扫掠算法（SAP）：根据对应场景选择坐标轴，对待检测物体遍历，若不满足max1>min2&&max2>min1则不会发生碰撞
- 散弹的碰撞检测：根据项目而定，可能会生成多个碰撞体单独检测/生成单个碰撞体检测碰撞面积
- 根据上一帧和当前帧的位置做一个胶囊体来检测碰撞，避免飞行过快发生子弹越过物体的现象

### Dots（Data-Oriented-Tech-Stack）/ ECS

- 5 principal using Dots
  1. 组件没有函数（行为），只有状态。更严谨地讲，组件只允许有一些访问函数，用于访问状态。
  2. 系统没有状态，只有行为。
  3. 共享函数（被多个系统调用的函数）放入utility函数（辅助函数）中。
  4. （通过调整执行顺序的方式）将复杂的副作用函数延迟执行。（副作用：当调用函数时，函数在完成原本的计算任务同时还改变了外部数据），比如管理角色死亡的system，会在大部分system执行之后再执行。
  5. 系统不能调用其它系统的函数（解耦） 作者：MisakaNo10086 <https://www.bilibili.com/read/cv16047480/>出处：bilibili
- Entity它的意义在于生命期管理，Component 之间可以组合在一起作为 System 筛选的标准
- System 之间也不需要相互调用（减少耦合），是由游戏世界（外部框架）来驱动若干 System 的。
- Utility 函数的概念，行为涉及多个 Entity或者行为并不想修改 Component 的状态，共享给不同的 System 调用。为了降低系统复杂度，就要求要么这种函数是无副作用的，随便怎么调用都没问题。[https://blog.codingnow.com/2017/06/overwatch_ecs.html](https://blog.codingnow.com/2017/06/overwatch_ecs.html)

### Git

- ![git流程图](../Picture/git%20flowChart.webp)
- git常用命令 [https://blog.csdn.net/qtiao/article/details/97783243](https://blog.csdn.net/qtiao/article/details/97783243)
- git有多种工作流，fork flow、gitflow、GitHub flow等。其中github flow的重要部分在于PR（pull request），通过fork（上游仓库）/clone（远程仓库）、branch、merge命令工作，有一个长期分支main，branch一般只在PR过程中产生；gitlab flow的重要原则是上游优先（upstream first）。name和email旨在pull留名，用户需要账号密码登陆或pull，ssh（需要登陆设置且有时效）旨在本地机器加入认证、简化提交时需要输入账号密码、提高安全性。
  - [github分支保护 和 pr规则](https://docs.github.com/zh/repositories)
  - [合并commit](https://blog.csdn.net/Spade_/article/details/108698036)，git rebase -i HEAD~5
  - 个人认为应该尽量少使用force push，应该在本地分支确定好commit再谨慎推送
  - https方式使用账号和密码授权，简单易用，便于进行权限细分管理，而且防火墙一般会打开 http 和https协议的端口号80 和 443。可以进行匿名访问，对于开源项目，其他人即使没有任何权限也可以方便进行除提交之外的克隆和读取操作。但是可能需要每个项目成员都有一个代码托管平台的账号，而且缺乏凭证管理的话，可能要频繁的进行账号密码输入；`<br/>`ssh方式单独使用非对称的秘钥进行认证和加密传输，和账号密码分离开来，不需要账号也可以访问repo。生成和管理秘钥有点繁琐，需要管理员添加成员的public key。不能进行匿名访问，ssh不利于对权限进行细分，用户必须具有通过SSH协议访问你主机的权限，才能进行下一步操作，比较适合内部项目。
- git-crypt issue: git cannot checkout after git-crypt encrypt file, use 'git crypt lock' then checkout can solve this problem. cannot do git-crypt init in same repo even id in different branch, one-to-one correspondence between git-crypt-key and repository. [https://github.com/AGWA/git-crypt/issues/125](https://github.com/AGWA/git-crypt/issues/125)
- git-crypt add-gpg-user [gpgID]，会使用gpgID匹配的gpg公钥来加密由git-crypt init命令产生的对等密钥（.git/git-crypt/keys/default），并生成一个文件在根目录下来导出结果

---

### 参考BLOG

- [事件管理系统三种](https://blog.csdn.net/qq_46044366/article/details/122722948)
- [烟雨迷离blog](https://www.lfzxb.top/categories/%E6%B8%B8%E6%88%8F%E5%BC%95%E6%93%8E/)
- [动作游戏通用框架](https://github.com/ImYellowFish/ActionGameTips)
- [Lua的数据结构——Table](https://www.jianshu.com/p/56ca3d77c7de)
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
