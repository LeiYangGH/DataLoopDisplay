echo 'commit message?:'
set /p msg=�������ύ��Ϣ
if not defined msg (echo "msg" not defined ) else (
git add . -A
git commit -m "%msg%"
echo 'pushing...'
echo 'end'
)
pause