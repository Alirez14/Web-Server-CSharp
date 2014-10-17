BIF/SWE 1 My WebServer
======================

C# Template für das Übungsbeispiel "WebServer". Damit die Übung erfolgreich abgegeben werden kann müssen folgende Kriterien erfüllt sein:

* Die Solution muss BIF-SWE1.sln heißen
* JEDE/R in der Gruppe muss in SEIN Repository hochladen (git push)

Benutzen Sie bitte die Vorlage. Sie ist so vorbereitet, dass sie am Jenkins verwendet werden kann.

Diese Vorlage wurde für Visual Studio 2013 erstellt. Sie können aber jede andere Entwicklungsumgebung benutzen, solange BIF-SWE1.sln am Jenkins kompilierbar bleibt.

Repository
----------
https://inf-swe-git.technikum-wien.at/

Das Repository ist selbst anzulegen: 

* my dashboard 
* owned 
* new repository 
* if00x000/BIF-WS??-SWE1

Das Repository hat die URL: https://if00x000@inf-swe-git.technikum-wien.at/?r=~if00x000/BIF-WS??-SWE1.git

* if00x000 ist durch Ihre if-Nummer zu ersetzen
* BIF-WS??-SWE1 durch das Jahr (WS 14/15 -> BIF-WS14-SWE1)

Sie sollten Ihr Repository Ihren KollegInnen freigeben. Mit "git add remote" (http://git-scm.com/docs/git-remote) können Sie mehrere Remotes angeben und die Abgabe somit vereinfachen.

Setup des Projektes
-------------------
https://inf-swe-git.technikum-wien.at/summary/?r=BIF/SWE1-CS.git

Laden Sie aus dem Template die Datei clone-bif-swe1-cs-tempate.sh herunter 

https://inf-swe-git.technikum-wien.at/raw/BIF/SWE1-CS.git/master/clone-bif-swe1-cs-tempate.sh

Starten Sie das Script mit git-Bash oder Bash und befolgen Sie die Anweisungen

    ./clone-bif-swe1-cs-tempate.sh

Mit den Anweisungen kopieren Sie das Template in Ihr lokales Projekt.

Achten Sie bitte darauf, dass immer BEIDE abgeben:

    git push all --all
	./git-push-all.sh

Implementierung
---------------
Im Verzeichnis "Übungen" finden Sie für jede Abgabe eine Implementierung des jeweiligen Übungsinterface. Die Dokumentation finden Sie an dem Interface selbst.

Es spielt keine Rolle, wie die Abgabe-Klassen heisen oder in welchem Namespace sie liegen. Die Unit-Tests suchen nach genau einer Klasse, die das jeweilige Übungsinterface implementiert.

Ihre eigenen Klassen, die Sie im Rahmen der Übung implementieren, müssen ebenfalls bestimmte Interfaces implementieren. Diese leiten sich von den Übungsinterfaces bzw. den Unit-Tests ab.

Unit-Tests
----------
https://inf-swe-git.technikum-wien.at/tree/?f=BIF-SWE1/CS&r=SYSTEM/unit-tests.git&h=master

Am Jenkins wird Ihre Abgabe mit diesen Unit-Tests getestet. Diese Tests stehen Ihnen zur Verfügung. Sie können daher lokal, vorab überprüfen, ob Sie die Unit-Tests bestehen oder nicht.

In diesem Projekt ist auch dokumentiert, welche Interfaces Ihre Klassen implementieren müssen um die Unit-Tests zu bestehen.

Jenkins
-------
https://inf-swe-jenkins.technikum-wien.at/view/BIF-SWE1/

Am Jenkins können Sie dann das Ergebnis Ihrer Abgabe sehen.

* Build aus dem Build-Verlauf auswählen
* Testergebnisse
* Konsolenausgabe

Sie dürfen so oft Sie möchten abgeben. Zu einem definierten Zeitpunkt (siehe Moodle) werden die Ergebnisse eingesammelt und ausgewertet. 
Am Ende der LV müssen alle Unit-Tests erfolgreich sein, mind. jedoch 50%. Zwischenergebnisse zählen nicht mehr. Die Anzahl der erfolgreich bestandenen Unit-Tests fließt in die Bewertung der Übung ein.