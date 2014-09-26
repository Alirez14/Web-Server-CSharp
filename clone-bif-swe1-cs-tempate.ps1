param (
	[Parameter(Mandatory=$true, HelpMessage="Name des Repositories: BIF-WS??-SWE1")][string] $repoName,
	[Parameter(Mandatory=$true, HelpMessage="Hauptrepository: if00x000")][string] $mainRepoUserName,
	[Parameter(Mandatory=$true, HelpMessage="Zweites Repository: if00x000")][string] $secondaryRepoUserName,
	[Parameter(Mandatory=$true, HelpMessage="Ihr Benutzername: if00x000")][string] $myUserName
)

"Cloning template" | out-host
"================" | out-host

git clone https://inf-swe-git.technikum-wien.at/r/BIF/SWE1-CS.git "$repoName"
cd "$repoName"
.\setup-remotes $repoName $mainRepoUserName $secondaryRepoUserName $myUserName