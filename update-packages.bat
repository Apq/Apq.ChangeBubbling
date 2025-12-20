@echo off
chcp 65001 >nul
title Apq.ChangeBubbling 依赖包升级工具
powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0update-packages.ps1"
pause
