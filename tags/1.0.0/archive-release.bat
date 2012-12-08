set /p versionNumber=

REM clean up
del builds\im-only-resting-%versionNumber%.zip
rd /q /s builds\im-only-resting-%versionNumber%
rd /q /s builds\im-only-resting

REM preparing staging dir
mkdir staging
copy LICENSE staging
copy NOTICE staging
copy Ior\bin\Release\im-only-resting.exe staging
copy Ior\bin\Release\libtidy.dll staging
copy Ior\bin\Release\Newtonsoft.Json.dll staging
copy Ior\bin\Release\PropertyGridEx.dll staging
copy Ior\bin\Release\RestSharp.dll staging
copy Ior\bin\Release\TidyManaged.dll staging
copy Ior\bin\Release\Utils.dll staging
copy Ior\bin\Release\NLog.config staging
copy Ior\bin\Release\NLog.dll staging
copy Ior\bin\Release\System.Net.Http.dll staging
copy Ior\bin\Release\System.Net.Http.WebRequest.dll staging

REM zip staging files
cd staging
"..\tools\7z\7za.exe" a -tzip "..\builds\im-only-resting-%versionNumber%.zip" *
cd ..

REM extract build
"tools\7z\7za.exe" e "builds\im-only-resting-%versionNumber%.zip" -o"builds\im-only-resting-%versionNumber%"
"tools\7z\7za.exe" e "builds\im-only-resting-%versionNumber%.zip" -o"builds\im-only-resting"

REM clean up
rd /q /s staging

pause
