# <p style = "text-align:center"> TODO&LOG </p>

<!--- [toc] --->

---
## <span style = "color:Purple"> Aladdin Slot </span>

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
31. ~~改动ComFunc.FormatKMBT~~


### CS Script

1. ~~MonsterPath.cs未进行xlua生成，导致XLua_Gen_Initer_Register__.cs报错~~
   - ~~存在多个脚本未xlua生成~~
2. ~~Image.DOFade函数定义在那~~
    1. ~~DOTween插件函数~~

---

## <span style = "color:orange"> Mammon Slot </span>

### Unity Editor

1. ~~tip02目录图片，代替神灯smoke~~
2. ~~icon_16（lamp）暂时缺少~~
3. ~~等美术资源添加筛子游戏下注时的动画~~s
4. ~~筛子游戏的新结算筛子图案~~
   1. 动态修改image.rectTransform.rect.size适配大小
5. ~~筛子游戏的按钮颜色修改~~
   
### Lua Script

1. ~~添加的lamp动画时的说明(sprite)，module/res/lamp目录下~~
2. ~~SlotAniMgr:PlayAni报错cannot set sprite, no such field~~
3. ~~完善按钮左右选择~~
4. ~~元宝的位置重定位（ef_bet重定位）~~
5. ~~模糊图像和结果筛子图像大小不一致~~
6. ~~result自动格式~~
7. ~~button颜色设定无效~~
   1. ~~!GetButton函数可能对组件做出了修改~~
   2. ~~UIBase:OnClick()修改了组件颜色~~
      1. Button.colors参数修改无效，需要将整个结构重新赋予
      2. BlockColor结构需要注意colorMultipier设置
8. ~~image获取rectTransform失败~~
   1. 不同组件的rectTransform的变量不能直接赋予
9. ~~button的image闪烁越来越快，在多次以后几乎不能发现闪烁~~
   1. 在脚本关闭时杀死动画
10. self.node_wheel在RunTimeOnce之前已经处于活动状态，重新设置其他组件活动开始时间
11. 左上角的奖励金额似乎提前更新了
12. ~~slotRoller.update(dt)是什么~~
    1. 对应TimeMgr的update，在Game.Update统一调用
13. ~~元宝渐显~~
14. ~~freetime的mask位置错位~~
15. 在每次回调完成后删除回调函数
16. ~~为什么在这个条件下生成end_data~~
    1. 只有一个函数可以写入grid_icons，因此在一开始将最终呈现的结果确定下来

### CS
1. ~~sizeDelta anchoredPosition offset~~
2. ~~AppMain.cs:320:HotDownLoadManager.InitLoadFile.LoadFileDatList()（加载文件）调用HotDirsUtil.LoadFileText时issuccess为false,因UnityWebRequest遇到错误HTTP响应代码和系统错误，导致不能运行（服务端配置文件获取失败）。在AppConst.curConfigUrl在对应地址获取文件失败，在初次运行需要线打包上传项目。~~
3. ~~luaPanel是什么~~
   1. luaPanel是lua脚本自身，在调用lua函数的时候充当self

---

## About The Project

### <span style = "color:blue" name = "Tips"> Tips </span>

1. **TODO使用(- [ ])标记**
2. **项目改动和增加使用了注释 '101' 标记**
3. **缺少XLua项，删掉Gen文件夹重新生成**
4. **Document更新后要（工具）重新生成，并修改Config.json文件（重定向）**
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
13. SlotAniMgr:PlayAni(sp, ...)调用的Transform.Find()只能在子物体查找,不能找物体的组件，且sp需要spirit作为参数
14. UIBase:OnClick()修改了组件颜色

### <font color = red name = "Unknow"> Unknow </font>

1. lua在vscode运行run code报错，但run file可行？
2. 如何加载不放在BuildSetting场景？
3. scene.sceneass spiritass是否是资源文件？
4. NetManager.Connect:59 为何id存在反而是不正确的？
5. 为何要将序列帧文件转json格式？
6. 预加载缺少了生成路径导致报错，需要指定config路径生成PLABconfig.lua文件，并对应生成Assets\Editor\ABPack\PreLoad\config\preload_mammon.conf。preload_mammon.conf是自用的文件，不需要上传。两个文件之间的联系？
7. 为何Coroutine需要专门用一个单例类来管理？
8. RunTimeOnce与handler的运作原理,self在其中的作用

---
