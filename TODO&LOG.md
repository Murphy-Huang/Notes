# <p style = "text-align:center"> TODO&LOG </p>

<!--- [toc] --->

---
## <font color=Purple> Aladdin Slot </font>

### Unity Editor
nothing left but all move to [Tips](#Tips) or [Unkonw](#Unknow) page

### LuaScript

1. ~~local函数是怎么成为全局方法的，作用域是怎么样的~~
   - ~~local函數中，沒有local標記的變量默認作用全局~~
2. ~~TimerMgr.Update沒看懂~~
3. ~~startInter.lua SlotLoginCtrl.lua 的isInit可能存在问题、ctor()函数似乎没有被使用~~
4. ~~SlotRoller.lua.Init() 84行~~
5. ~~.proto文件位于Document对应项目的data文件夹中 ->（編譯）-> .lua文件位于Asset对应项目的ScriptLua/Data/Protocal文件夹~~
6. ~~SlotRoller：83注释~~
7. ~~btn_lamp测试按钮 OnLamp测试函数 添加node_lamp_circle变量 LampCircle脚本生成神灯绑定Lamp脚本 Lamp脚本做动画~~
8. ~~值爲空可能是脚本最后缺少return~~
9.  ~~DoLocalMove函数~~
    1. ~~來自DOTween文件~~
10. ~~PlayAni在Awake时为空，SetActive在脚本中使用报错：物体为空~~
    1. ~~与加载有关~~
11. ~~audio脚本沒加載新的声音资源~~
    1. ~~配置document文件夹xml，生成配置文件~~
12. ~~dump(self)查看self表内容~~
13. ~~SlotNet.game_id:19 game_id是否更改这个~~
14. ~~AppConfig加载rapidJson模块无引用~~
15. ~~让动画延时执行，有一点停顿~~
16. ~~mask动态遮罩全屏幕~~
17. ~~icon_16.icon_blur在spin之后出现~~
18. ~~icon_14在slot之外成型~~
19. ~~slotmainPanel.runtimeupdate~~
20. ~~缺少i16音频（i15替代）~~
21. ~~字体变大后不清晰~~
22. ~~结果在游戏结束之前在左上角金币中已经得出~~
23. ~~lampnode将值返回slotmainpanel~~
24. 灯在烟雾出来时已经开始
25. ~~获奖多停留~~
26. ~~初次的字体与最终字体格式化~~
27. ~~全部神灯出现后才可点击~~
28. ~~标题出来停顿~~
29. ~~多个神灯多次运行时最后得分重新出现~~
30. ~~奖励数字過大导致文本重叠~~
31. **改动ComFunc.FormatKMBT**


### CS Script

1. ~~MonsterPath.cs未进行xlua生成，导致XLua_Gen_Initer_Register__.cs报错~~
   - ~~存在多个脚本未xlua生成~~
2. ~~Image.DOFade函数定义在那~~
    1. ~~DOTween插件函数~~

---

## <font color = orange> Mammon Slot </font>

### Unity Editor

1. ~~tip02目录图片，代替神灯smoke~~
2. **icon_17（lamp）暂时缺少**
   
### Lua Script

1. **添加的lamp动画时的说明(sprite)，module/res/lamp目录下**

### CS
1. ~~sizeDelta anchoredPosition offset~~
2. ~~AppMain.cs:320:HotDownLoadManager.InitLoadFile.LoadFileDatList()（加载文件）调用HotDirsUtil.LoadFileText时issuccess为false,因UnityWebRequest遇到错误HTTP响应代码和系统错误，导致不能运行（服务端配置文件获取失败）。在AppConst.curConfigUrl在对应地址获取文件失败，在初次运行需要线打包上传项目。~~

---

## About The Project

### <font color = blue name = "Tips"> Tips </font>

1. **TODO使用(- [ ])标记**
2. **项目改动和增加使用了注释 '101' 标记**
3. **缺少XLua项，删掉Gen文件夹重新生成**
4. **Document更新后要（工具）重新生成**
5. **在初次运行需要线打包上传项目。**
6. AppConfig.config加载
    1. AppConfig.LoadConfig:62 ParseConfig(configjson,...)
       - AppConfig.LoadingConfig:120 => PlayerPrefabs.HasKey()
       - System.Net.Sockets.TcpClient UnityEngine.PlayerPrefabs
7. 没有自动同步到streamingassets
   1. streamingassets会打包到apk文件，让应用开始时可能减少下载更新
8.  DataUnpack在哪里调用了
   1. lua脚本通过NetManager.cs/PortManager.cs使用LuaMsgParse.cs,调用ProtoParse.lua进行通讯
9. System.Net.IPAddress.TryParse 根据 IPv4 的点四表示法和 IPv6 的冒号十六进制表示法表示的 IP 地址创建 IPAddress 实例
10. Invoke，BeginInvoke区别：Invoke会阻塞当前线程，begininvoke则可以异步调用，不会等委托方法执行结束
11. TcpClient.BeginConnect TcpClient.EndConnect组合使用，EndConnect会调用BeginConnect返回的IAsyncResult对象
12. AppMain.cs:316:AppConfig.LoadConfig.ParseConfig.LoadVersions.LoadFileText()确认各个信息。

### <font color = red name = "Unknow"> Unknow </font>

1. lua在vscode运行run code报错，但run file可行？
2. 如何加载不放在BuildSetting场景？
3. scene.sceneass spiritass是否是资源文件？
4. NetManager.Connect:59 为何id存在反而是不正确的？
5. 为何要将序列帧文件转json格式？
6. 预加载缺少了生成路径导致报错，需要指定config路径生成PLABconfig.lua文件，并对应生成Assets\Editor\ABPack\PreLoad\config\preload_mammon.conf。preload_mammon.conf是自用的文件，不需要上传。两个文件之间的联系？
7. 为何Coroutine需要专门用一个单例类来管理？

---
