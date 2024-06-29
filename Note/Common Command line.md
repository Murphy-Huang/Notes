## Git
1. git add . / git add '...'
2. git status
3. git checkout -\<changed file in working directory\>
4. git commit -m '...' / git commit -am '...' (-am equal to -a and -m)
5. git reset HEAD \<commited file\>
6. git mv -v \<oldfolder\> \<newfolder\>
7. git push \<remote host branch name\> \<local branch name\>
8. git branch -a (check out all remote and local branch)
9. git branch -r (check out remote branch)
10. git branch --set-upstream-to=\<alias\>/\<remote branch name\> \<local branch name\> (establish a tracking relationship, alias default name origin)
11. git fetch \<alias\> (Synchronize the local branch with remote)
12. git checkout '...' (switch to assigned branch) / git checkout -b \<local branch name\> origin/\<remote branch name\>
13. git remote -v
14. git remote add \<alias\> \<remote repository url\>