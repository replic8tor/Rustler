@echo off

del "%1..\FarmVille\bin\Debug\Resources\Scripts\*.cs"
xcopy /Y "%1*.cs" "%1..\FarmVille\bin\%2\Resources\Scripts\" 

exit /B 0
