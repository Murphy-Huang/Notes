[Notes](./Note/Notes.md)
[Solution](./Note/Solution/Solution.md)
[Common API](./Note/Common%20API.md)
[Common Command line](./Note/Common%20Command%20line.md)
[Informal essays](./.secrets/Informal%20essays/Informal%20essays.md)
[Inspiration fragment](./.secrets/Informal%20essays/Inspiration%20fragment.md)
[Current Log](./LegacyLog/TODO&LOG.md)

Applications that may be used:
- github.dev on website
- 熊猫压缩
- gpg
- edge://flags/forcetodark
- renderdoc shader调试

规范建议：
- 注释*TODO*：说明在标识处有功能代码待编写，待实现的功能在说明中会简略说明
- 注释*FIXME*：说明标识处代码需要修正，甚至代码是错误的，不能工作，需要修复，如何修正会在说明中简略说明
- 注释*XXX*：说明标识处代码虽然实现了功能，但是实现的方法有待商榷，希望将来能改进，要改进的地方会在说明中简略说明
- 注释*HACK*：说明标识处代码需要根据自己的需求去调整程序代码
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