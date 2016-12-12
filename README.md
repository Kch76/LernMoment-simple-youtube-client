# simple-youtube-client

Anhand dieses Beispiels zeige ich im [C# Tutorial Deutsch - Konfiguration laden & speichern](http://www.lernmoment.de/csharp-tutorial-deutsch/anwendungseinstellungen-visual-studio/) wie du Anwendungseinstellungen in .NET und Visual Studio erstellst und verwendest.

Es ist natürlich auch ein Beispiel wie du mit der [YouTube Data API für .NET](https://developers.google.com/api-client-library/dotnet/apis/youtube/v3) Informationen über Videos von YouTube abrufen kannst.

Ganz konkret lädt dieser Client die wichtigsten Meta-Daten aller Videos von einem YouTube-Kanal und gibt sie auf der Konsole aus. Unglücklicherweise ist die YouTube API an dieser Stelle etwas sperrig. D.h. es ist nicht direkt möglich alle Videos eines Kanals zu holen (bzw. deren Daten), sondern es gibt einen mehrstufigen Prozess:

 1. Die *Upload-Playlist* eines Kanals holen. Dabei interessiert uns an dieser Stelle lediglich die `Id` der *Upload-Playlist*.
 2. Alle Video-IDs aus der *Upload-Playlist* extrahieren. Die *Upload-Playlist* enthält nicht direkt `Video`-Objekte, sondern `PlaylistItem`-Objekte. Diese enthalten wiederum dann die Video-IDs.
 3. Nun kann mit jeder Video-IDs die entsprechende Information zum Video abgerufen werden. Die API erlaubt es mehrere Video-IDs in einem `VideoListRequest` abzufragen. Das ist sehr hilfreich, weil es wesentlich schneller geht. Allerdings gibt es in der API eine Grenze für die Anzahl der Video-IDs in dieser Anfrage. YouTube lässt hier maximal 50 Video-IDs zu. Möchtest du also Daten zu mehr Videos abrufen, dann musst du das in mehreren `Requests` nacheinander machen.
 
