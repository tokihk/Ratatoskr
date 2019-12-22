@rem Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨Ñ¨
@rem Ñ¨Ñ¨Ñ¨
@rem Ñ¨Ñ¨       Installer Generate Script
@rem Ñ°             Copyright (C) 2019 Toki.H.K
@rem Ñ°
@rem Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°Ñ°
@echo off
pushd %~dp0

@rem ================================================================
@rem 	Project Config
@rem ================================================================

set APP_NAME=Ratatoskr
set APP_VERSION=0.7.0
set APP_VERSION_SUB=
set APP_AUTHOR=Toki.H.K
set APP_COPYRIGHT=Copyright (c) 2018-2019 Toki.H.K
set APP_HOMEPAGE=https://github.com/tokihk/Ratatoskr

set PACKAGE_DOCUMENT_DIR=..\docs
set PACKAGE_TARGET_DIR=..\build\Release
set PACKAGE_INPUT_DIR=.\output.in
set PACKAGE_OUTPUT_DIR=.\output

@rem ================================================================
@rem 	External Tool Config
@rem ================================================================

@rem Visual Studio
set VISUAL_STUDIO_DEVCMD_SCRIPT=C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\VsDevCmd.bat
set MSBUILD_TARGET_PROJECT_PATH=..\src\Ratatoskr.sln
set MSBUILD_OPTION=-t:rebuild -p:Configuration=Release

@rem Inno Setup
set INNO_SETUP_BIN=C:\Program Files (x86)\Inno Setup 6\ISCC.exe
set INNO_SETUP_SCRIPT_PATH=.\InstallerConfig.iss
set INNO_SETUP_OPTION=^
/DMyAppName="%APP_NAME%" ^
/DMyAppExeName="%APP_NAME%.exe" ^
/DMyAppVersion="%APP_VERSION%" ^
/DMyAppVersionSub="%APP_VERSION_SUB%" ^
/DMyAppPublisher="%APP_AUTHOR%" ^
/DMyAppCopyright="%APP_COPYRIGHT%" ^
/DMyAppURL="%APP_HOMEPAGE%" ^
/DMyInputDir="%PACKAGE_INPUT_DIR%" ^
/DMyOutputDir="%PACKAGE_OUTPUT_DIR%" ^


@rem ================================================================
@rem 	Process
@rem ================================================================

rem Clear Working Directory
if exist "%PACKAGE_INPUT_DIR%" (
	del /Q "%PACKAGE_INPUT_DIR%\*"
) else (
	mkdir "%PACKAGE_INPUT_DIR%"
)


@rem Load Visual Studio Config
@call "%VISUAL_STUDIO_DEVCMD_SCRIPT%"

@rem Build Visual Studio Solution
MSBuild.exe "%MSBUILD_TARGET_PROJECT_PATH%" %MSBUILD_OPTION%

rem Collect target item
copy /Y ReleaseNote.txt %PACKAGE_INPUT_DIR%
copy /Y %PACKAGE_TARGET_DIR%\*.exe %PACKAGE_INPUT_DIR%
copy /Y %PACKAGE_TARGET_DIR%\*.exe.config %PACKAGE_INPUT_DIR%
copy /Y %PACKAGE_TARGET_DIR%\*.dll %PACKAGE_INPUT_DIR%

xcopy /Y /E /R /I /K %PACKAGE_DOCUMENT_DIR% %PACKAGE_INPUT_DIR%\docs

rem Build Installer
"%INNO_SETUP_BIN%" "%INNO_SETUP_SCRIPT_PATH%" %INNO_SETUP_OPTION%

rem ZipÉtÉ@ÉCÉãçÏê¨
rem "%BIN_7ZIP%" a -tzip "%OUTPUT_NAME%.zip" "DIR_TARGET"

popd
