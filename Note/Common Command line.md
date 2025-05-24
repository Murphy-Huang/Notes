# Common Command Line

## Git

1. git clone -b \<branch name> \<url>
2. git add . / git add '...'
3. git commit -m '...' / git commit -am '...' (-am equal to -a and -m)
4. git status
5. git checkout -[changed file in working directory]
6. git checkout '...' (switch to assigned branch) / git checkout -b [local branch name] origin/[remote branch name]
7. git reset HEAD [commited file]
8. git mv -v [oldfolder] [newfolder]
9. git pull [remote host name] [branch name]
10. git push [remote host branch name] [local branch name]
11. git push --delete origin [branch name]
12. git branch -a (check out all remote and local branch)
13. git branch -r (check out remote branch)
14. git branch --set-upstream-to=[alias]/[remote branch name] [local branch name] (establish a tracking relationship, alias default name origin)
15. git branch -d [branch name]
16. git branch -m [branch name] [new branch name]
17. git fetch [alias] (Synchronize the local branch with remote)
18. git fetch [repo] [remote_branch_name] : [local_branch_name]
19. git switch [branch name]
20. git remote -v
21. git remote add [alias] [remote repository url]
22. git remote show origin
23. git remote prune origin
24. git stash save "message"
25. git stash pop
26. git stash list
27. git log -10
28. git reset --soft [commit ID]
29. git revert [commit ID]
30. git diff (working & stash)
31. git diff head (working & head)
32. git diff -cached (stash & head)

### [Git Submodules](https://cloud.tencent.com/developer/article/2136829)

1. git submodule add [GitURL/倉庫地址]
2. git submodule add [倉庫地址] [希望submodule位於的文件路徑]
3. git submodule update --remote [submodule文件夹相对路径]
4. git submodule update [submodule文件夹相对路径]
5. git submodule init [submodule的文件夹的相对路径]
6. git submodule update [submodule的文件夹的相对路径]

### Git Commit Message

#### Format

```json
<type>(<scope>): <subject>
// blank line
<body>
// blank line
<footer>
```

#### Partical Realization

- Header
  - type：必须
     1. feat
     2. fix
     3. docs
     4. style
     5. refactor
     6. test：增加测试
     7. chore：构建过程和辅助工具变动
  - scope：可选
    - 影响层级和范围
  - subject：必须
- Body
  - 第一人称现在时
  - 变动的动机以及与之前的对比
- Footer
  1. 不兼容变动：BREAKING CHANGE开头
  2. 关闭issue：Closes #123，#456

## PowerShell

- 对比目录文件差异：Compare-Object -ReferenceObject (Get-ChildItem -File -Path "<源文件夹路径>") -DifferenceObject (Get-ChildItem -File -Path "<目标文件夹路径>") -Property Name, Length, LastWriteTime | FT -AutoSize

## 批处理文件

.bat批处理文件，将由命令行组成的文本文件后缀改为.bat，成为可运行的程序

1. mklink：创建文件夹的链接：可用于减少同一个项目不同版本的大小问题，公用Assets|ProjectSettings目录，不同版本独立的Unity Setting
    - /D 目录的符号链接（相对路径）；/H 文件硬链接；/J 目录的链接点（绝对路径）；
2. pause： 暂停批处理文件执行的关键字 = Ctrl + S
3. rem：注释命令，该命令后的内容不被执行，但是能回显（echo）
4. if： if[not] var1 compare-op var2 (do action1) else (do action2)
