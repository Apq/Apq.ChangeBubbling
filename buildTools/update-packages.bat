@echo off
chcp 65001 >nul
title Apq.ChangeBubbling 依赖包升级工具
powershell -ExecutionPolicy Bypass -NoProfile -Command "[Console]::OutputEncoding = [System.Text.Encoding]::UTF8; & '%~dp0update-packages.ps1'"
pause
