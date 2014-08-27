BIF/SWE 1 My WebServer
======================

C# Template für das Übungsbeispiel "WebServer". Damit die Übung erfolgreich abgegeben werden kann müssen folgende Kriterien erfüllt sein:

* Die Solution muss BIF-SWE1.sln heißen
* JEDE/R in der Gruppe muss in SEIN Repository hochladen (git push)

Benutzen Sie bitte die Vorlage. Sie ist so vorbereitet, dass sie am Jenkins verwendet werden kann.

Diese Vorlage wurde für Visual Studio 2013 erstellt. Sie können aber jede andere Entwicklungsumgebung benutzen, solange BIF-SWE1.sln am Jenkins kompilierbar bleibt.

Repository
----------
Das Repository is selbst anzulegen: 
* https://inf-swe.technikum-wien.at/
* -> my dashboard 
* -> owned 
* -> new repository 
* -> if00x000/BIF-WS??-SWE1

Das Repository hat die URL: https://if00x000@inf-swe.technikum-wien.at/r/if00x000/BIF-WS??-SWE1.git
* if00x000 ist durch Ihre if-Nummer zu ersetzen
* BIF-WS??-SWE1 durch das Jahr (WS 14/15 -> BIF-WS14-SWE1)

Sie sollten Ihr Repository Ihren KollegInnen freigeben. Mit "git add remote" (http://git-scm.com/docs/git-remote) können Sie mehrere Remotes angeben und die Abgabe somit vereinfachen.

Setup des Projektes
-------------------

Nachdem das/die Repositories eingerichtet sind, können Sie das Projekt lokal einrichten

cd <Mein Projekte Basis Verzeichnis>
git clone https://inf-swe.technikum-wien.at/r/BIF/SWE1-CS.git BIF-WS??-SWE1
cd BIF-WS??-SWE1
git remote set-url origin https://if00x000@inf-swe.technikum-wien.at/r/if00x000/BIF-WS??-SWE1.git
git remote add if00x000-Ihres-Kollegen/inn https://if00x000@inf-swe.technikum-wien.at/r/if00x000-Ihres-Kollegen/inn/BIF-WS??-SWE1.git
git push all --all

Jenkins
-------

Unter http://inf-swe.technikum-wien.at:8080/view/SWE1/ können Sie dann das Ergebnis Ihrer Abgabe sehen.