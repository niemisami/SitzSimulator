# IGDT 2018 project course - SitzSimulator

> Unity project for the Introduction to Game Development Tools course at the University of Turku

Usefull links and most used `git` commands can be read for example from here [git-good](https://github.com/DigitKoodit/git-good)
## Getting started 

_Run highlighted git commands on terminal/command line_

Clone the repository: `git clone git@github.com:niemisami/SitzSimulator.git`

Open the `SitzSimulator` folder on Unity

## Developing 

> To make sure the project stays manageble use `branches` when developing new features etc. Let's try to keep the `master` branch always "clean" and working so anyone who clones the repository from GitHub always get's working code. 

### Beginning a new feature
1. Checkout `master` branch 
2. Fetch latest changes (if any): `git pull`
3. Check the branch you're on with: `git branch`
    * If branch is what you want move to the next step
    * If the terminal says `* master`, then create a new branch: `git checkout -b name-of-the-new-branch` (e.g. `git checkout -b 'wobbling-controls`) This command will create a new branch and move into that.
4. Start developing

### Committing changes
1. Check changed files: `git status`
2. Add all or select files to be committed: all files `git add .` or single file with it's name `git add file.txt`
3. Check that correct files are added: `git status`
4. Make a meaningful commit message: `git commit -m "Improve avatar movement`
  
### Merging with master 
1. Checkout master branch: `git checkout master`
2. Fetch latest changes (if any): `git pull`
3. Checkout feature branch: `git checkout name-of-the-new-branch`
4. Merge master to feature: `git merge master`
5. Check that everything works and/or fix merge conflicts and follow instructions on terminal
6. When everything is ok move to master: `git checkout master`
7. Merge new features to master: `git merge name-of-the-new-branch`
8. Push master to GitHub: `git push origin master`

Same branches can be used again and again but after merge with master branches could be removed if one wants with `git branch -d name-of-the-new-branch` and then create a new branch following the instructions in "Beginning a new feature" 

## Notes

If using Visual Studio Code as a main code editor note that some Unity generated files and folders are hidden on the editor e.g. `ProjectSettings`. These files are neither auto generated or managed by Unity therefore not neccessary to display on the editor. 

Many files are also ignored from version control due the same reason. See `.gitignore` file. 


