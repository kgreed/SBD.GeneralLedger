# SBD.GeneralLedger
XAF, Entity Framework Code First LocalDB General Ledger
https://discourse.softwarebydesign.com.au/t/about-the-sbd-general-ledger-category/1612


## MG

Added .gitignore from [https://github.com/github/gitignore/blob/master/VisualStudio.gitignore](https://github.com/github/gitignore/blob/master/VisualStudio.gitignore)

## kgreed
Note the first time you build (or if you clean and build)  you will get some errors. For example
"The process cannot access the file 'bin\x64\Release\Scissors.Xaf.CacheWarmup.Generators.MsBuild.dll' because it is being used by another process.	SBD.GL.Win"

To resolve this 
1) Delete All the bin and obj folders in all the projects/
2) Run Sbd.GL.Win in Debug ( i.e Press F5) and exit.
3) Switch back to release mode and build.
4) Right Click the Packages Solution -> Store -> Create Packages. This will display an error 
5) Repeat step 4 , this time you can create the packages.
6) Right Click the packages Solution and Launch the Windows App Certification Kit.
Chances are that this will fail but that the upload to the store will work anyway.
7) Upload to the store



