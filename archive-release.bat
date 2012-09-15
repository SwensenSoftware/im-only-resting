set /p versionNumber=

REM clean up
del builds\im-only-resting-%versionNumber%.zip
rd /q /s builds\im-only-resting-%versionNumber%

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

REM zip staging files
cd staging
7z a -tzip ..\builds\im-only-resting-%versionNumber%.zip *
cd ..

REM clean up
rd /q /s staging

pause
