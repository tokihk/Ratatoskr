@echo off
pushd %~dp0

set DIR_TARGET=src
set DIR_OUTPUT=output

set APP_NAME=Ratatoskr
set APP_VERSION=0.5.0.0
set OUTPUT_NAME=%APP_NAME%_%APP_VERSION%

set BIN_INNOSETUP=C:\Program Files (x86)\Inno Setup 5\ISCC.exe
set BIN_7ZIP=C:\Program Files\7-Zip\7z.exe

rem ターゲットディレクトリクリア
if exist "%DIR_TARGET%" (
	del /Q "%DIR_TARGET%\*"
) else (
	mkdir "%DIR_TARGET%"
)

rem 必要なファイルをビルドディレクトリから収集
copy /Y ReleaseNote.txt .\src

copy /Y ..\build\release\*.exe .\src
copy /Y ..\build\release\*.exe.config .\src
copy /Y ..\build\release\*.dll .\src

xcopy /Y /E /R /I /K ..\docs .\src\docs

rem インストーラー作成
"%BIN_INNOSETUP%" "InstallerConfig.iss"

rem Zipファイル作成
rem "%BIN_7ZIP%" a -tzip "%OUTPUT_NAME%.zip" "DIR_TARGET"

popd
