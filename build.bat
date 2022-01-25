@echo off

echo "Windows Docker build"

cd ../LintSense.Exam.HttpApi.Host

dotnet publish -c Release -o ../publish

cd ../publish

echo "发布成功"

docker build -t lintense.exam.httpapi.host .