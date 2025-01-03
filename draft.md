# Draft

## draft

## Common Links

[Notes](./Note/Notes.md)

[Solution](./Note/Solution/Solution.md)

[Common API](./Note/Common%20API.md)

[Common Command line](./Note/Common%20Command%20line.md)

[Informal essays](./.secrets/Informal%20essays/Informal%20essays.md)

[Inspiration fragment](./.secrets/Informal%20essays/Inspiration%20fragment.md)

[Current Log](./LegacyLog/Tianan_LOG.md)

## Tips

### Resources that may be useful

- [插件](./Note/Notes.md)
- Search way: site:/filetype:/+/-/""/inurl/intitle/*
- github.dev on website
- 熊猫压缩
- gpg
- edge://flags/forcetodark
- renderdoc (shader调试)
- <https://github.com/mcmilk/7-Zip-zstd>
- Unity shader 源码地址：\Unity\Editor\Data\CGIncludes
- Unity 崩溃日志地址：\Users\Administrator\AppData\Loacl\Unity\Editor\Editor.log
- Unity 证书日志地址：\Users\Administrator\AppData\Loacl\Unity\Unity.Licensing.Client.log
- C:\Windows\System32目录下有一系列.cpl文件，分别对应着控制面板的项目（main.cpl控制鼠标属性）
- Beyond Compare

### Summary that may help

- 多看官方文档
- 重构
- 工作日志，有自己心里的工作日报
- 先用profiler，再优化
- 注释贵精不贵多
- 先实现再有框架，从原型中提炼出框架
- 重构/优化/修Bug，一次只能做一样
- 隔离是方法，起名时关键，测试时主角，调试是补充，版本控制
- 需求是一切的开头，之后才是编码
- 对小黄鸭说话，讲问题思路
- Debug从高层往底层找

### 规范建议

- 注释*TODO*：说明在标识处有功能代码待编写，待实现的功能在说明中会简略说明
- 注释*FIXME*：说明标识处代码需要修正，甚至代码是错误的，不能工作，需要修复，如何修正会在说明中简略说明
- 注释*XXX*：说明标识处代码虽然实现了功能，但是实现的方法有待商榷，希望将来能改进，要改进的地方会在说明中简略说明
- 注释*HACK*：说明标识处代码需要根据自己的需求去调整程序代码
- bool类型属性和方法以is、has、can开头
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
- toc == to client / tos == to server 为分发事件名称

![开源协议](./Picture/Open%20Resource%20License.png)
