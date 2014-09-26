param (
	[Parameter(Mandatory=$true, HelpMessage="Name des Repositories: BIF-WS??-SWE1")][string] $repoName,
	[Parameter(Mandatory=$true, HelpMessage="Hauptrepository: if00x000")][string] $mainRepoUserName,
	[Parameter(Mandatory=$true, HelpMessage="Zweites Repository: if00x000")][string] $secondaryRepoUserName,
	[Parameter(Mandatory=$true, HelpMessage="Ihr Benutzername: if00x000")][string] $myUserName
)

"Setting up remotes" | out-host
"==================" | out-host

"Repo Name: $repoName" | out-host
"Main:      $mainRepoUserName" | out-host
"Secondary: $secondaryRepoUserName" | out-host
"Username:  $myUserName" | out-host

git remote set-url origin "https://$myUserName@inf-swe-git.technikum-wien.at/r/~$mainRepoUserName/$repoName.git"

git remote remove $secondaryRepoUserName
git remote remove all

git remote add $secondaryRepoUserName https://$myUserName@inf-swe-git.technikum-wien.at/r/~$secondaryRepoUserName/$repoName.git
git remote add all "https://$myUserName@inf-swe-git.technikum-wien.at/r/~$mainRepoUserName/$repoName.git"
git remote set-url --add all "https://$myUserName@inf-swe-git.technikum-wien.at/r/~$secondaryRepoUserName/$repoName.git"

"Result:" | out-host
git remote -v