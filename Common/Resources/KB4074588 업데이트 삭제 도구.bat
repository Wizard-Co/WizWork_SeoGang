@echo off
title KB4074588 업데이트 삭제 도구
bcdedit /enum {bootmgr} > nul || goto _admin

wusa /uninstall /kb:4074588 /quiet /norestart

pause


:_admin
color 4f
cls
echo.
echo.
echo.
echo          관리자 권한으로 실행되지 않았습니다.
echo.
echo          우클릭 해서 관리자 권한으로 실행해주세요.
echo.
echo.
echo.
pause
exit