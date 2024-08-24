## Git
1. git add . / git add '...'
2. git commit -m '...' / git commit -am '...' (-am equal to -a and -m)
3. git status
4. git checkout -[changed file in working directory]
5. git checkout '...' (switch to assigned branch) / git checkout -b [local branch name] origin/[remote branch name]
6. git reset HEAD [commited file]
7. git mv -v [oldfolder] [newfolder]
8. git push [remote host branch name] [local branch name]
9. git push --delete origin [branch name]
10. git branch -a (check out all remote and local branch)
11. git branch -r (check out remote branch)
12. git branch --set-upstream-to=[alias]/[remote branch name] [local branch name] (establish a tracking relationship, alias default name origin)
13. git branch -d [branch name]
14. git branch -m [branch name] [new branch name]
15. git fetch [alias] (Synchronize the local branch with remote)
16. git remote -v
17. git remote add [alias] [remote repository url]
18. git remote show origin
19. git remote prune origin
20. git stash save "..."
21. git stash pop
22. git stash list

## PowerShell
- 对比目录文件差异：Compare-Object -ReferenceObject (Get-ChildItem -File -Path "<源文件夹路径>") -DifferenceObject (Get-ChildItem -File -Path "<目标文件夹路径>") -Property Name, Length, LastWriteTime | FT -AutoSize