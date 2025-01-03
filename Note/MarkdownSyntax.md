# Markdown Syntax

[Markdown官网](https://markdown.com.cn/basic-syntax/)

## <center>基本语法

这些是 John Gruber 的原始设计文档中列出的元素。所有 Markdown 应用程序都支持这些元素。

|元素	                   |Markdown 语法    |
|:-------------------------|:-----------------|
|标题（Heading）	       |# H1 ## H2 ### H3|
|粗体（Bold）              |**bold text**    |
|斜体（Italic）	           |*italicized text*|
|引用块（Blockquote）      |	> blockquote  |
|有序列表（Ordered List）  |	1. First item<br> 1. Second item<br> 2. Third item|
|无序列表（Unordered List）| - First item - Second item - Third item|
|代码（Code）	           |`code`|
|分隔线（Horizontal Rule） |---|
|链接（Link）	           | [title](https://www.example.com)|
|图片（Image）	           | ![alt text](image.jpg)|

## 扩展语法

这些元素通过添加额外的功能扩展了基本语法。但是，并非所有 Markdown 应用程序都支持这些元素。

|元素	                    |Markdown 语法     |
|:-------------------------|:-----------------|
|表格（Table）	            |\| Syntax \| Description \|<br>\|----------- \| ----------- \|<br>\| Header      \| Title       \|<br>\| Paragraph   \| Text        \|<br>|
|代码块（Fenced Code Block）|	```{"firstName": "John","lastName": "Smith","age": 25}```|
|脚注（Footnote）	        |Here's a sentence with a footnote. [^1]<br> [^1]: This is the footnote.|
|标题编号（Heading ID）	|### My Great Heading {#custom-id}|
|定义列表（Definition List）|	term : definition|
|删除线（Strikethrough）	|~~The world is flat.~~|
|任务列表（Task List）	|- [x] Write the press release<br> - [ ] Update the website<br> - [ ] Contact the media|

## Other

- `<link rel = "stylesheet" type = "text/css" href = "Notes.css" />`
- [markdown系列blog](https://www.cnblogs.com/hanzongze/category/1475469.html)
- 在文件加入css方式
  - `<link rel = "" type = "" href = "" />`
  - 在标签添加*style*属性：`<span style = "color:blue" >`