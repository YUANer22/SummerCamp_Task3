@echo off
setlocal

set "folder=."  REM 将此路径替换为您要删除 .meta 文件的文件夹路径

for /r "%folder%" %%a in (*.meta) do (
    del "%%a"
    echo "%%a" 已删除
)

echo .meta 文件已成功删除
pause
