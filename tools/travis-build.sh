#!/bin/sh

if [ "$TRAVIS_PULL_REQUEST" = "false" ]; then
	mono ../tools/sonar/SonarQube.Scanner.MSBuild.exe begin /n:link-reader /k:link-reader /d:sonar.login=${SONAR_TOKEN} /d:sonar.host.url="https://sonarcloud.io" /d:sonar.cs.opencover.reportsPaths="./test/code-coverage/coverage.opencover.xml"
fi

dotnet restore ./src
dotnet build ./src
dotnet restore ./test
dotnet test ./test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover /p:CoverletOutput="./code-coverage/"

if [ "$TRAVIS_PULL_REQUEST" = "false" ]; then
	mono ../tools/sonar/SonarQube.Scanner.MSBuild.exe end /d:sonar.login=${SONAR_TOKEN}
fi