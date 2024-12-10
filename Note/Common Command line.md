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
9. git push [remote host branch name] [local branch name]
10. git push --delete origin [branch name]
11. git branch -a (check out all remote and local branch)
12. git branch -r (check out remote branch)
13. git branch --set-upstream-to=[alias]/[remote branch name] [local branch name] (establish a tracking relationship, alias default name origin)
14. git branch -d [branch name]
15. git branch -m [branch name] [new branch name]
16. git fetch [alias] (Synchronize the local branch with remote)
17. git fetch [repo] [remote_branch_name] : [local_branch_name]
18. git remote -v
19. git remote add [alias] [remote repository url]
20. git remote show origin
21. git remote prune origin
22. git stash save "..."
23. git stash pop
24. git stash list
25. git log -10
26. git reset --soft [commit ID]
27. git switch [branch name]

### [Git Submodules](https://cloud.tencent.com/developer/article/2136829)

1. git submodule add [GitURL/倉庫地址]
2. git submodule add [倉庫地址] [希望submodule位於的文件路徑]
3. git submodule update --remote [submodule文件夹相对路径]
4. git submodule update [submodule文件夹相对路径]
5. git submodule init [submodule的文件夹的相对路径]
6. git submodule update [submodule的文件夹的相对路径]

## PowerShell

- 对比目录文件差异：Compare-Object -ReferenceObject (Get-ChildItem -File -Path "<源文件夹路径>") -DifferenceObject (Get-ChildItem -File -Path "<目标文件夹路径>") -Property Name, Length, LastWriteTime | FT -AutoSize
