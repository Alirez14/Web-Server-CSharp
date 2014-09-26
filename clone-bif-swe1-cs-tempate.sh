#!/bin/bash

if [ "$#" -ne 4 ]; then
  echo "use: ./clone-bif-swe1-cs-tempate.sh BIF-WS??-SWE1 <if-nummer hauptrepository> <if-nummer zweitrepository> <if-nummer ihres Benutzers>"
  exit 1;
fi

repoName=$1

echo "Cloning template"
echo "================"

git clone https://inf-swe-git.technikum-wien.at/r/BIF/SWE1-CS.git "$repoName"
cd "$repoName"
chmod +x *.sh
./setup-remotes.sh $@
