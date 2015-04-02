::发布程序批处理文件
@ECHO OFF

SET DirectoryName=大山

SET TimeString1=%date%%time%
SET TimeString2=%TimeString1:-=%
SET TimeString3=%TimeString2: =0%
SET TimeString4=%TimeString3::=%
SET TimeString5=%TimeString4:~0,14%
:: ==============================

SET SoftwareName=SignalReceiver
IF EXIST .\%SoftwareName%\bin\Release\%SoftwareName%.exe (ECHO %SoftwareName%:> ..\..\Used\%DirectoryName%\%SoftwareName%\publish.txt
ECHO ==============================>> ..\..\Used\%DirectoryName%\%SoftwareName%\publish.txt)
IF EXIST .\%SoftwareName%\bin\Release\%SoftwareName%.exe (xcopy .\%SoftwareName%\bin\Release\*.dll ..\..\Used\%DirectoryName%\%SoftwareName%\ /s /h /d /y
xcopy .\%SoftwareName%\bin\Release\%SoftwareName%.exe ..\..\Used\%DirectoryName%\%SoftwareName%\ /s /h /d /y
ECHO F | xcopy .\%SoftwareName%\bin\Release\%SoftwareName%.exe.config ..\..\Used\%DirectoryName%\%SoftwareName%\%SoftwareName%.exe.%TimeString5%.config /s /h /d /y
START D:\"Program Files\Beyond Compare\BCompare.exe" ..\..\Used\%DirectoryName%\%SoftwareName%\%SoftwareName%.exe.%TimeString5%.config ..\..\Used\%DirectoryName%\%SoftwareName%\%SoftwareName%.exe.config
START explorer.exe ..\..\Used\%DirectoryName%\%SoftwareName%\
START notepad.exe ..\..\Used\%DirectoryName%\%SoftwareName%\publish.txt) >> ..\..\Used\%DirectoryName%\%SoftwareName%\publish.txt

SET SoftwareName=ParameterTool
IF EXIST .\%SoftwareName%\bin\Release\%SoftwareName%.exe (ECHO %SoftwareName%:> ..\..\Used\%DirectoryName%\%SoftwareName%\publish.txt
ECHO ==============================>> ..\..\Used\%DirectoryName%\%SoftwareName%\publish.txt)
IF EXIST .\%SoftwareName%\bin\Release\%SoftwareName%.exe (xcopy .\%SoftwareName%\bin\Release\*.dll ..\..\Used\%DirectoryName%\%SoftwareName%\ /s /h /d /y
xcopy .\%SoftwareName%\bin\Release\%SoftwareName%.exe ..\..\Used\%DirectoryName%\%SoftwareName%\ /s /h /d /y
ECHO F | xcopy .\%SoftwareName%\bin\Release\%SoftwareName%.exe.config ..\..\Used\%DirectoryName%\%SoftwareName%\%SoftwareName%.exe.%TimeString5%.config /s /h /d /y
START D:\"Program Files\Beyond Compare\BCompare.exe" ..\..\Used\%DirectoryName%\%SoftwareName%\%SoftwareName%.exe.%TimeString5%.config ..\..\Used\%DirectoryName%\%SoftwareName%\%SoftwareName%.exe.config
START explorer.exe ..\..\Used\%DirectoryName%\%SoftwareName%\
START notepad.exe ..\..\Used\%DirectoryName%\%SoftwareName%\publish.txt) >> ..\..\Used\%DirectoryName%\%SoftwareName%\publish.txt

SET SoftwareName=MonitoringTool
IF EXIST .\%SoftwareName%\bin\Release\%SoftwareName%.exe (ECHO %SoftwareName%:> ..\..\Used\%DirectoryName%\%SoftwareName%\publish.txt
ECHO ==============================>> ..\..\Used\%DirectoryName%\%SoftwareName%\publish.txt)
IF EXIST .\%SoftwareName%\bin\Release\%SoftwareName%.exe (xcopy .\%SoftwareName%\bin\Release\*.dll ..\..\Used\%DirectoryName%\%SoftwareName%\ /s /h /d /y
xcopy .\%SoftwareName%\bin\Release\%SoftwareName%.exe ..\..\Used\%DirectoryName%\%SoftwareName%\ /s /h /d /y
ECHO F | xcopy .\%SoftwareName%\bin\Release\%SoftwareName%.exe.config ..\..\Used\%DirectoryName%\%SoftwareName%\%SoftwareName%.exe.%TimeString5%.config /s /h /d /y
START D:\"Program Files\Beyond Compare\BCompare.exe" ..\..\Used\%DirectoryName%\%SoftwareName%\%SoftwareName%.exe.%TimeString5%.config ..\..\Used\%DirectoryName%\%SoftwareName%\%SoftwareName%.exe.config
START explorer.exe ..\..\Used\%DirectoryName%\%SoftwareName%\
START notepad.exe ..\..\Used\%DirectoryName%\%SoftwareName%\publish.txt) >> ..\..\Used\%DirectoryName%\%SoftwareName%\publish.txt

::ECHO.
::PAUSE