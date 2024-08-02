<link rel = "stylesheet" type = "text/css" href = "Notes.css" />
- [markdown系列blog](https://www.cnblogs.com/hanzongze/category/1475469.html)

- 利用config中prefab实例化（关于config的使用）
- 将存储实现ISavable依赖于SavingEntity调用，避开存在ISavable实现的多处地方调用形成相同的副本
- PowerShell对比目录文件差异：Compare-Object -ReferenceObject (Get-ChildItem -File -Path "<源文件夹路径>") -DifferenceObject (Get-ChildItem -File -Path "<目标文件夹路径>") -Property Name, Length, LastWriteTime | FT -AutoSize

- 在文件加入css方式
  - `<link rel = "" type = "" href = "" />`
  - 在标签添加*style*属性：<span style = "color:blue" ></span>
- '/'forward slash在网络上使用，'\'backward slash在window文件系统和转义使用
- URL保留字符：space %20
- [optional]属性使方法参数可选：void func([optional] int num)

- 基于IEnumerable来隐藏替换List、Array等可迭代元素
- vr项目需要添加InputActionManager组件
- 利用static作为全体类实体的存储库
- enum 和 interface 搭配使用
- 私有接口函数：处理接口函数之间重用部分而不在实现类实现，实现类不可访问
- lua调用CS存在多种方式xlua,tolua,slua
- lua调用cs脚本的绑定函数（事件实现），将对应函数绑定在cs脚本（生命周期）上，实现lua的逻辑
- redis与monogo配合mysql实现冷热数据存储
- 闭包可以从类变量与类函数来作用域来理解，函数的闭包如同类中的函数调用类的变量（自由变量）。在当前作用域之外将自由变量的状态保存下来，保持对词法作用域的引用。
- 避免使用foreach，因为会释放内存
- 使用Mathf.PI时注意有效输入范围和结果精度（角度*PI/180 = 弧度）
- 函数的参数合法性检测：一般只在用户的输入正确性，测试阶段调试或设计兼容性高的函数时需要检测合法性
- 委托是一个类型，将具体实现交付出去；回调是函数指针，将执行时机交付出去
- async/await：await不会开启新的线程；异步调用前的线程会在异步等待时放回线程池，异步等待结束后，会从线程池取一个空闲的线程，来运行异步等待调用结束后的后续代码。
- Invoke，BeginInvoke区别：Invoke会阻塞当前线程，begininvoke则可以异步调用，不会等委托方法执行结束；invoke（同步）和begininvoke（异步）的概念，其实它们所说的意思是相对于子线程而言的，其实对于控件的调用总是由主线程来执行的。
- 整数ID比指针更容易指代无效的对象
- 多层状态机中，上层状态机为下层状态机设置目标实现控制
- mvc和esc设计的类同和区别，避免在游戏开发使用mvc
- 欧拉角存在万向结死锁的情况，由于其旋转定义为先z、再x、再y的顺规定义，在某些情况存在某轴向自由度丢失的情况
- 类是引用传递，结构体默认是值传递；类可以有虚方法也可以继承其他类，结构体没有虚方法也不能继承
- 在仅仅使用方法时静态工具类取代单例，单例只在需要面向对象特性时使用
- 使用事件来进行跨模块的传递，不需要引用数据实例
- C#扩展方法
  - 必须在非泛型的靜態類中聲明，且是静态方法
  - 扩展方法必须有一个参数，且只有第一个参数使用this标记
  - 靜態類本身必须具有文件作用域
  - 编译要求导入扩展方法
  - 调用时无需传递第一个参数，默认调用this作为第一个参数
  - 是不是每个对象都加入了这个扩展方法？这个问题其实并未发生，因为C#使用的方式不是给每个对象加一个方法，而是另外提供了一个扩展方法的列表，在使用时通过列表找到被扩展的静态方法然后调用，也就是说方法还是只有那一个方法，并没有大范围的占据方法区。
- Enum做为字典的key的时候，会有装箱的行为，因为Enum没有实现IEquatable,这是字典的key必要的接口。

---

#### 插件
1. Unity-Logs-Viewer插件
2. SimpleJson
3. rapidJson解析器

#### 报错处理
1. 使用protobuf-net or protobuf
  - [官方实现版本过高，需要注意unity版本](https://www.cnblogs.com/caiger-blog/p/14040130.html)
2. pip不是命令
  - pip未注册环境变量
  - [pip安装第三方报错](https://www.cnblogs.com/yinhaiping/p/13375375.html)
    - 添加--trusted-host有效: -i http://pypi.tuna.tsinghua.edu.cn/simple/ --trusted-host pypi.tuna.tsinghua.edu.cn
3. 缺少XLua项
  - 删掉Gen文件夹重新生成
4. Component GUI Layer in Main Camera is no longer available.
  - remove Component Flare Layer in Camera
5. <a id="unitylinker"></a>'could not produce class with id XXX', can find class id in <https://docs.unity3d.com/Manual/ClassIDReference.html>, add link.xml in Assets document and fill in following information: `<assembly fullname="UnityEngine"> <type fullname="UnityEngine.SphereCollider" preserve="all"/> </assembly>`
6. attempt to index a boolean value：lua模块语句缺少return关键词

#### Unity
1. 在不同场景物体GUID相同时，加载问题，[冲突避免](https://blog.csdn.net/linjf520/article/details/127998024)
2. ![热更新流程图](../Picture\hotRefresh%20flowChart.png)
3. where include a prefab often overrides the transform of root element of prefab
4. ScriptableObject的非持久化
5. Editor文件夹 EditorWindow类
6. Resources文件夹 Resources.LoadAll
7.  playerPrefs存储玩家简单的数据：string,int,float在注册表上，但可以使用JsonUtility工具将unity可序列化的类转换成json格式存储，间接存储更复杂数据
8.  TextAsset文本资源存储txt/json/bytes格式文件，TextAsset不适用于运行时生成文本
9.  Unity3d调用打包函数BuildPipeline.BuildAssetBundles时，需要传进去一个Path,用于存放打包的AssetBundle，通常传进去的是Application.streamingAssets。然后在打包完成后，unity会默认生成一个存放AssetBundle的文件夹同名的assetbundle文件，用来存放所有AssetBundle的依赖关系，在这里，就会生成一个叫StreamingAssets的AssetBundle文件。因此，在加载某一个AssetBundle之前，我们都必须先加载这个名称叫做StreamingAssets的bundle文件，然后通过这个bundle文件寻找任意一个AssetBundle需要的依赖文件。<https://www.jianshu.com/p/95af464020c7>
10. 直接使用prefab有的组件生成实体
11. 在角色周围生成粒子可以形成在整个场景粒子的错觉
12. 利用unityevent，interface，unityaction解耦
13. 物体在SetActive隐藏后，脚本仍会运行
    - 脚本不被勾选，虽然大部分生命周期函数不会执行，但是内置的事件监测的方法，譬如OnMouseDown()，OnTriggerEnter();都能运行
    - 可以考虑动态加载和卸载这个脚本
14. UI设置
  - Aspect Ratio Fitter固定图片比例
  - Constant Size Fitter配合grid group使用限制范围
15. 动态设置RectTransform
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
16. Button在Selected状态，可以理解为按钮被按下之后，Selected的状态其实相当于一个”lock（锁定）“状态，需要执行一步”unlock（解锁）“的动作才能将按钮返回普通状态。
17. DOTween在脚本结束的时候要DOKill杀死动画，例如在SetLoops之后，不然可能有意料之外的状态
18. 不同组件的rectTransform不能直接赋值
19. 注意组件默认设置的Color
20. Button.colors参数修改无效，需要将整个BlockColor结构重新赋予；BlockColor需要注意colorMultipier的设置
21. 使用动画关键帧（add property）代替脚本
22. EventSystem.current.IsPointerOverGameObject()检测UI
23. width height：多少像素点来渲染，因此更改大小最好使用scale。将ui和canvas结合使用获得适当的大小和分辨率
24. 协程问题
  - Invoke受Time.timeScale影响，并且无法避免。Coroutine可以通过Time.unscaledDeltaTime，WaitForSecondsRealtime来执行不受Time.timeScale影响的代码。菜单、UI、HUD等可以考虑用Coroutine
  - 当类所属游戏对象active为false时，函数中的StartCoroutine无法执行，而函数中的Invoke仍可以执行。如果在SetActive(true)前需要进行一些与本体无关的额外处理而需要推迟SetActive(true)（如登场时的光效动画等），考虑使用Invoke。若用Coroutine，就需要很多额外代码来调整各部件的出现时间、Start中调用的函数何时开始执行等，或者就需要把动画和时间的处理函数写在其他类（如敌人或玩家角色的Manager类等）中
25. 协程无法返回值，可以利用回调函数、共享变量、事件来返回结果
26. 协程适用于处理Unity 对象、生命周期等与Unity API交互相关的任务，如延时、动画序列、协作动作；线程更适合计算密集型任务，如物理模拟、算法计算
27. yield语句就是这条分界线，想要代码“停住”，就不执行后面语句对应的代码块，想要代码恢复，就接着执行后面语句对应的代码块。而调度上下文的保存，是通过将需要保存的变量都定义成成员变量来实现的。[参考](https://www.cnblogs.com/iwiniwin/p/14878498.html)
28. update()中尽量不使用Find()
29. 调用this.transform实际上是一个调用intenal method的过程（这是用C/C++写的，不是MONO的）。值得注意的是这个调用方法略慢，因为你需要调用外部的CIL（aka interop），花费了额外的性能
30. 改变position的时候需要注意缩放，特别是父物体的scale
31. 处理好prefab的apply、reverse、copy的关系
32. 关于代码剥离的构建：Unity会使用一个专门用于托管代码剥离的工具UnityLinker来进行剥离处理,其默认将unity中用到的所有程序集合并程一个整体程序集，然后根据一定规则，比如场景中游戏对象继承Monobehavior的对象，标记根元素，再次有根元素进行依赖查询，并将其他依赖的程序集或类或命名空间进行打标记。最后没有被标记的，将会被裁剪剥离。UnityLinker在构建时，会检查Assets/link.xml文件[sample](#unitylinker)，将里面设置的忽略的程序集或者类型直接标记为根元素。或者我们可以为需要保留的程序集、类和方法加上[Preserve]特性，针对性的解决错误代码剥离。<https://blog.csdn.net/zhush_2005/article/details/125229154>
33. 继承Mono和不继承Mono的单例的写法不同

#### 碰撞检测
- 多变体碰撞检测：分离轴定理（SAT）：依次再不同角度照射待检测物体，当存在一个角度两者影子没有重叠则分离轴存在
- 圆形碰撞检测：略
- Multi Box Pruning（SAP + 网格）
  - 网格：对预处理对象进行区域划分，只关注每个小格子内的遍历
    - 四叉树：维护一个四叉树数据结构，各个对象均匀分布在叶节点，当一个叶节点超出容量上限则新分出四个叶节点
  - 扫掠算法（SAP）：根据对应场景选择坐标轴，对待检测物体遍历，若不满足max1>min2&&max2>min1则不会发生碰撞
- 散弹的碰撞检测：根据项目而定，可能会生成多个碰撞体单独检测/生成单个碰撞体检测碰撞面积
  
#### Dots（Data-Oriented-Tech-Stack）/ ECS
- 5 principal using Dots
  1. 组件没有函数（行为），只有状态。更严谨地讲，组件只允许有一些访问函数，用于访问状态。
  2. 系统没有状态，只有行为。
  3. 共享函数（被多个系统调用的函数）放入utility函数（辅助函数）中。
  4. （通过调整执行顺序的方式）将复杂的副作用函数延迟执行。（副作用：当调用函数时，函数在完成原本的计算任务同时还改变了外部数据），比如管理角色死亡的system，会在大部分system执行之后再执行。
  5. 系统不能调用其它系统的函数（解耦） 作者：MisakaNo10086 https://www.bilibili.com/read/cv16047480/ 出处：bilibili
- Entity它的意义在于生命期管理，Component 之间可以组合在一起作为 System 筛选的标准
- System 之间也不需要相互调用（减少耦合），是由游戏世界（外部框架）来驱动若干 System 的。
-  Utility 函数的概念，行为涉及多个 Entity或者行为并不想修改 Component 的状态，共享给不同的 System 调用。为了降低系统复杂度，就要求要么这种函数是无副作用的，随便怎么调用都没问题。<https://blog.codingnow.com/2017/06/overwatch_ecs.html>

#### git:
- ![git流程图](../Picture/git%20flowChart.webp)
- git常用命令 <https://blog.csdn.net/qtiao/article/details/97783243>
- git有多种工作流，fork flow、gitflow、GitHub flow等。其中github flow的重要部分在于PR（pull request），通过fork（上游仓库）/clone（远程仓库）、branch、merge命令工作，有一个长期分支main，branch一般只在PR过程中产生；gitlab flow的重要原则是上游优先（upstream first）。name和email旨在pull留名，用户需要账号密码登陆或pull，ssh（需要登陆设置且有时效）旨在本地机器加入认证、简化提交时需要输入账号密码、提高安全性。
  - [github分支保护 和 pr规则](https://docs.github.com/zh/repositories)
  - [合并commit](https://blog.csdn.net/Spade_/article/details/108698036)，git rebase -i HEAD~5
  - 个人认为应该尽量少使用force push，应该在本地分支确定好commit再谨慎推送
  - https方式使用账号和密码授权，简单易用，便于进行权限细分管理，而且防火墙一般会打开 http 和https协议的端口号80 和 443。可以进行匿名访问，对于开源项目，其他人即使没有任何权限也可以方便进行除提交之外的克隆和读取操作。但是可能需要每个项目成员都有一个代码托管平台的账号，而且缺乏凭证管理的话，可能要频繁的进行账号密码输入；<br/>ssh方式单独使用非对称的秘钥进行认证和加密传输，和账号密码分离开来，不需要账号也可以访问repo。生成和管理秘钥有点繁琐，需要管理员添加成员的public key。不能进行匿名访问，ssh不利于对权限进行细分，用户必须具有通过SSH协议访问你主机的权限，才能进行下一步操作，比较适合内部项目。
- git-crypt issue: git cannot checkout after git-crypt encrypt file, use 'git crypt lock' then checkout can solve this problem. cannot do git-crypt init in same repo even id in different branch, one-to-one correspondence between git-crypt-key and repository. <https://github.com/AGWA/git-crypt/issues/125>
- git-crypt add-gpg-user [gpgID]，会使用gpgID匹配的gpg公钥来加密由git-crypt init命令产生的对等密钥（.git/git-crypt/keys/default），并生成一个文件在根目录下来导出结果

#### 注释说明：
  1. TODO：如果代码中有该标识，说明在标识处有功能代码待编写，待实现的功能在说明中会简略说明。
  2. FIXME：如果代码中有该标识，说明标识处代码需要修正，甚至代码是错误的，不能工作，需要修复，如何修正会在说明中简略说明。
  3. XXX：如果代码中有该标识，说明标识处代码虽然实现了功能，但是实现的方法有待商榷，希望将来能改进，要改进的地方会在说明中简略说明。
  4. HACK：如果代码中有该标识，说明标识处代码我们需要根据自己的需求去调整程序代码。

---
#### 设计模式总结
1. 单例模式过于简单，而且属于全局对象，以及考虑释放对象等因素，能不用最好不用。遇到多线程访问时候，互斥也是要考虑的因素。
2. 原型模式，本质是个COPY构造过程。
3. 其它创建类型模式，工厂方法、抽象工厂、简单工厂、生成器等，都是用一个生产者来生产产品对象，有根据不同类型或者配置参数来生成不同对象的，有根据不同工厂类来生成不同对象的，有根据不同对象类，同时再由这个对象类的不同方法来生产出来最终产品的。还有就是把生产产品对象的这个过程分步进行的封装。大概就是这些不同的创建模式，用UML关系图表达就是Factory与Product之间有一层的关系，这个关系可能是依赖或者关联或者组合等。同时还可以把这层“关系”通过继承继承下来，这样除了继承父类信息外，也继承了这一层“关系”，同时子类的多态性，又可以进行不同的一些变化。其它模式或者说小的程序结构也是类似如此，除了“关系”和“特殊的接口”，就是继承这种“关系”和“接口”等信息，再来个多态。
4. 结构性模式如下：适配、桥接、组成、装饰、外观、享元、代理等几个。
    - 适配比较好理解，当你有两个类接口和交互不匹配的时候，而且这两个类又都是成熟的类，不能随意修改，那中间创建一个适配的类出来，把接口等转换一下就好了。
    - 其实像组成、装饰、代理，都基本是把其它的类再包装一下，有些用组成关系，有些用关联关系，区别是组成是大量同类对象进行容器化管理；装饰的话，装饰与被装饰的类，是来自同一个父类，同时保持实现的父亲抽象接口一致；而代理的话，主要是要保持与被包装的类的接口完全一致。
    - 像外观和享元就简单点了，外观只是把一系列动作过程包装成一个，享元只是保持创建的对象列表里面，如果已经创建了对应的对象，就用创建的，如果没有，就创建一个新的，并放入列表，这些好像谈不到模式之说。
5. 最后就是行为类型模式：责任链、命令、迭代器、中介者、备忘录、观察者、访问者、策略、状态、模板方法等。
    - 像备忘录、观察者相对比较简单明了，见到的也比较多，备忘录有点像本地序列化的实现一样，能够保存对象的部分信息，并可以恢复出来。观察者就是个通知回调给观察类。
    - 迭代器这跟STL里面的一个概念。
    - 模板方法，跟上面的外观模式处理类似的策略，父类封装许多动作集合为一个动作，然后不同子类实现这些不同的小的动作方法，但总的动作方法不变，使用者统一使用。
    - 责任链主要是所有同类对象组成一个链，然后把一个动作消息，向链向里传递处理，直到处理完成。
    - 命令模式和中介者模式，命令模式就是把把个消息动作封装为对象，那这样动作就有了状态和其它信息，能够UNDO/REDO等，消息传递就是选择对应的命令对象就好。中介者模式呢，就是一系统对象之前不直接通信，都关联到一个中间对象来处理，所有信息和处理逻辑就有机会汇集在中介者对象上来了，其它对象信息传递就解耦。
    - 策略模式和状态模式，策略就是替换一个策略对象或者接口来改变策略动作行为，状态模式类似也是注入一个状态对象，来改变对象的行为表现。

#### 不使用设计模式要考虑些什么
1. 要区分实现者和使用者，即使这些局部性代码全部是写给自己用的，你都要尽量假设这些代码和框架是写给别人用的。这样你就会考虑和注意的多一点，能够区分机制和策略是不同的等。
2. 要区别变与不变的部分，基础功能可能不变，接口可能不变，协议可能不变，框架关系可能不变，其它实现过程基本是会随着需求和其它的因素会变化，同时其实接口和协议，以及框架关系也可能会变，能够抽象到什么合适的层次和粒度，就要看情况了，如果不清楚和不可预见，就适度就好。
3. 高内聚低耦合，这是基本的要求，一定的解耦是必要的，即使短期没有这种需求和好处，但意识到耦合严重，是要随时抽空整理一下的。
4. 面向对象的话，最好每个东西，包括对象和关系是能够解释的，也就是能够用语言表达给别人听，说逻辑是没有意义的。
5. 组件化或者服务化，让部分功能可以单独开发和测试，能够独立运行也可以。虽然运行效率会低一点，即使是这样，我觉得后期稳定了再完整的集成一体，提高运行效率，也算是个好的策略，毕竟开发前期分工多了，集成和测试是个难事，能够先组件服务化，开发稳定了，去掉这层壳，也是可以的。
6. 层次性
7. 迭代，重构，再迭代！许多时候并非你一开始知道所有的模块和业务，而是你慢慢的才知道的。一开始就知道的，那是领域专家，行业专家，比如图像编辑，别人细节清楚，代码工具链丰富，从提出需求，他就知道如何实现了，即使全部用逻辑堆，也能够堆的很完美。但你不是！
8. 要重用别人的封装代码，而不可随意改变它的时候怎么办？即使自己封装的代码和类等，封装的很完整纯粹，也是不可随意改变这种纯粹的，要进行一定的不纯粹改变使用，一样也是要考虑办法解决。
9. 代码量大了，要分工协作怎么拆分，后面能够及时的组装和测试，要如何？
10. 考虑调试
11. 插件化
12. 了解和区分动态和静态，比如动态对象和静态对象。
尽量精通开发语言、数据结构算法、操作系统、网络通信，以及其它相关领域知识和理论，比如图像、音频、机器学习，学习这些比设计模式实用多了，在学的过程中，接触别人的设计和实现，以及体系，迟早你就潜移默化的
[原文链接](https://blog.csdn.net/zxb452000/article/details/87166050)

---
# 参考BLOG
- [烟雨迷离blog](https://www.lfzxb.top/categories/%E6%B8%B8%E6%88%8F%E5%BC%95%E6%93%8E/)
- [动作游戏通用框架](https://github.com/ImYellowFish/ActionGameTips)
- [硬派游戏AI，FSM（状态机）、HFSM（分层状态机）、BT（行为树）的区别。](https://blog.csdn.net/qq_39885372/article/details/103950973)
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