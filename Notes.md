- ![git流程图](gitlct.webp)
- [git常用命令](https://blog.csdn.net/qtiao/article/details/97783243)
- 闭包可以从类变量与类函数来作用域来理解，函数的闭包如同类中的函数调用类的变量（自由变量）。在当前作用域之外将自由变量的状态保存下来，保持对词法作用域的引用。
- ![热更新流程图](rgxlct.png)
- lua调用CS存在多种方式xlua,tolua,slua
- setnativesize令image自适应大小
- redis与monogo配合mysql实现冷热数据存储
- lua调用cs脚本的绑定函数（事件实现），将对应函数绑定在cs脚本（生命周期）上，实现lua的逻辑
- 使用protobuf-net or protobuf
  - [官方实现版本过高，需要注意unity版本](https://www.cnblogs.com/caiger-blog/p/14040130.html)
- pip不是命令
  - pip未注册环境变量
  - [pip安装第三方报错](https://www.cnblogs.com/yinhaiping/p/13375375.html)
    - 添加--trusted-host有效: -i http://pypi.tuna.tsinghua.edu.cn/simple/ --trusted-host pypi.tuna.tsinghua.edu.cn
- 缺少XLua项
  - 删掉Gen文件夹重新生成
- 避免使用foreach，因为会释放内存
- 使用Mathf.PI时注意有效输入范围和结果精度（角度*PI/180 = 弧度）
- 注意组件默认设置的Color
- 设置text的对齐方式 `GetComponent<Text>().alignment = CS.UnityEngine.TextAnchor.MiddleCenter`
- image动态遮罩全屏幕
```C#
    self.GetComponent<Image>().SetNativeSize();
    olSize = self.sizeDelta;
    al = olSize.x / olSize.y;
    size = new Vector2(parentHeight * al, parentHeight);
    self.sizeDelta = size;
```