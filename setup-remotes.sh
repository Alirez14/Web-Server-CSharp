#!/bin/bash

if [ "$#" -ne 4 ]; then
  echo "use: ./setup-remotes.sh BIF-WS??-SWE1 <if-nummer hauptrepository> <if-nummer zweitrepository> <if-nummer ihres Benutzers>"
  exit 1;
fi

repoName=$1
mainRepoUserName=$2
secondaryRepoUserName=$3
myUserName=$4

echo "Setting up remotes"
echo "=================="

echo "Repo Name: $repoName"
echo "Main:      $mainRepoUserName"
echo "Secondary: $secondaryRepoUserName"
echo "Username:  $myUserName"

git remote set-url origin "https://$myUserName@inf-swe-git.technikum-wien.at/r/~$mainRepoUserName/$repoName.git"

git remote remove $secondaryRepoUserName
git remote remove all

git remote add $secondaryRepoUserName https://$myUserName@inf-swe-git.technikum-wien.at/r/~$secondaryRepoUserName/$repoName.git
git remote add all "https://$myUserName@inf-swe-git.technikum-wien.at/r/~$mainRepoUserName/$repoName.git"
git remote set-url --add all "https://$myUserName@inf-swe-git.technikum-wien.at/r/~$secondaryRepoUserName/$repoName.git"

echo "Result:"
git remote -v
