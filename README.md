# Meet Me Game

Ein Projekt im Rahmen des Studiengangs **Hochschule für Wirtschaft und Technik Berlin**  
**Fachbereich 4 - Informatik, Kommunikation und Wirtschaft**  
Dokumentation zur **Computergraphik** für das Unity-Spielprojekt **Meet Me Game**

**Dozent:** Thomas Jung  

---

## Inhaltsverzeichnis

1. [Projektidee](#projektidee)
2. [Installation](#installation)
3. [Spielanleitung](#spielanleitung)
4. [Hauptfeatures](#hauptfeatures)
    - [Hauptseite](#hauptseite)
    - [Spielstart](#spielstart)
    - [Konferenzräume](#konferenzräume)
    - [Breakout Room](#breakout-room)
    - [Nachrichten Feature](#nachrichten-feature)
    - [Logout](#logout)
5. [Verwendete Technologien](#verwendete-technologien)
6. [Fazit](#fazit)
7. [Quellen](#quellen)

---

## Projektidee

**Meet Me Game** ist ein Multiplayer-Spiel, das die Interaktion und den Austausch von Teilnehmern in einer virtuellen Umgebung ermöglicht. Ziel des Spiels ist es, Teilnehmer zusammenzubringen, sie über verschiedene Konferenzräume und Breakout-Räume kommunizieren zu lassen und ihnen die Möglichkeit zu geben, durch Chats und Videokonferenzen zu interagieren. Das Spiel schafft eine immersive und interaktive Plattform, die soziale Verbindungen fördert und eine inklusive Community aufbaut.

## Installation

Um das Meet Me Game zu installieren und auszuführen, sind die folgenden Schritte erforderlich:

1. **Voraussetzungen**:
   - **Unity Hub** und **Unity Editor** (Version 2021 oder höher).
   - **Git** für das Klonen des Repositories.
   - Eine Internetverbindung für das Multiplayer-Feature.

2. **Repository klonen**:
   ```bash
   git clone https://github.com/Jujubash/meet-me-game.git
   
3. **Unity-Projekt öffnen**:

- Öffne **Unity Hub** und wähle die Option „Add Project“.
- Wähle den Ordner aus, in dem du das Repository geklont hast.

4. **Abhängigkeiten installieren**:

- Stelle sicher, dass die benötigten Unity-Pakete installiert sind:
  - **Photon PUN** für Multiplayer-Netzwerkfunktionen.
  - **Agora SDK** für Audio- und Video-Chat.
- Überprüfe die Projektkonfiguration und lade fehlende Abhängigkeiten im Package Manager.

5. **Build und Play**:

- Starte das Spiel über den Unity Editor mit der Schaltfläche **Play** oder erstelle einen Build über **File > Build Settings**.
  
**Hinweis**: Stelle sicher, dass die Internetverbindung aktiv ist, da einige Funktionen, wie Multiplayer und Videochat, darauf angewiesen sind.

## Spielanleitung

1. **Registrierung und Anmeldung**  
   - Bei erstmaliger Nutzung können Spieler ein Konto erstellen. Bestehende Nutzer loggen sich direkt ein.

2. **Steuerung**  
   - Der Avatar wird mit den Pfeiltasten oder WASD bewegt.
   - In den Konferenzräumen kann der Spieler durch Annäherung an Türen und andere Spieler interagieren.

3. **Interaktion mit anderen Teilnehmern**  
   - Beim Näherkommen an andere Avatare ist ein Chat oder Videocall möglich.
   - In Breakout Rooms stehen Audio- und Video-Chat-Optionen zur Verfügung.

---

## Hauptfeatures

### Hauptseite
- **Registrierung/Anmeldung**: Spieler können sich registrieren, einloggen und ihr Passwort ändern.
- **Informationen über das Spiel**: Die Hauptseite bietet allgemeine Informationen zum Spiel.

### Spielstart
Nach dem Login betreten die Spieler die Startwelt:
- Ein vordefinierter Avatar wird geladen, und der Benutzername des Teilnehmers wird angezeigt.
- Spieler können sich frei im Bereich bewegen und andere Teilnehmer treffen.

### Konferenzräume
Spieler können verschiedene Konferenzräume betreten, die jeweils thematisch und stilistisch unterschiedliche Umgebungen bieten:
- **Raumauswahl**: Über Türen kann der Spieler verschiedene Räume betreten (z. B. ein „Iceland“-Setting).
- **Zusammenführung von Teilnehmern**: Spieler können sich anderen Teilnehmern in den Konferenzräumen anschließen und interagieren.

### Breakout Room
In den Breakout Rooms stehen Video- und Audio-Chat-Optionen zur Verfügung:
- **Automatische Aktivierung**: Kamera und Mikrofon werden bei Betreten aktiviert (optional deaktivierbar).
- **Video-Chat-Funktionalität**: Spieler können live mit anderen Teilnehmern sprechen und sich unterhalten.

### Nachrichten Feature
- **Live-Chat**: Bei Annäherung an einen anderen Avatar wird ein Chat aktiviert, der über ein Popup gestartet werden kann.
- **Chat-Funktionen**: Nachrichten werden in Echtzeit versendet und verschwinden, wenn sich die Spieler voneinander entfernen.

### Logout
- **Ausloggen**: Über eine spezielle Tür auf der Startwelt-Karte können sich Spieler abmelden und zur Login-Seite zurückkehren.

## Spielanleitung

1. **Registrierung und Anmeldung**  
   - Bei erstmaliger Nutzung können Spieler ein Konto erstellen. Bestehende Nutzer loggen sich direkt ein.

2. **Steuerung**  
   - Der Avatar wird mit den Pfeiltasten oder WASD bewegt.
   - In den Konferenzräumen kann der Spieler durch Annäherung an Türen und andere Spieler interagieren.

3. **Interaktion mit anderen Teilnehmern**  
   - Beim Näherkommen an andere Avatare ist ein Chat oder Videocall möglich.
   - In Breakout Rooms stehen Audio- und Video-Chat-Optionen zur Verfügung.
   - 
## Verwendete Technologien

- **Photon Unity Networking (PUN)**  
  Photon PUN ermöglicht die Multiplayer-Funktionalität im Spiel. Es bietet eine schnelle und zuverlässige Übertragung von Daten und ermöglicht es den Spielern, miteinander zu interagieren und zu kommunizieren.

- **Agora SDK**  
  Das Agora SDK bietet Echtzeit-Video- und Audio-Interaktion für die Konferenz- und Breakout-Räume. Es unterstützt qualitativ hochwertige Videoanrufe und niedrige Latenz, um eine nahtlose Kommunikation zwischen den Spielern zu gewährleisten.

- **MongoDB**  
  MongoDB wird als Datenbank für Benutzerinformationen genutzt. Es handelt sich um eine NoSQL-Datenbank, die eine flexible und skalierbare Datenverwaltung ermöglicht und hohe Leistung bietet.

---

## Fazit

Das Unity-Projekt **Meet Me Game** wurde entwickelt, um Menschen in einer virtuellen Umgebung zu verbinden. Spieler können in diesem Multiplayer-Spiel Konferenzräume und Breakout-Räume betreten, mit anderen Teilnehmern chatten, Audio- und Videoanrufe tätigen und neue Kontakte knüpfen. Durch den Einsatz von Technologien wie Photon und Agora gelingt es dem Projekt, eine unterhaltsame und vernetzte Umgebung zu schaffen, die soziale Interaktionen fördert und eine inklusive Community aufbaut.

---

## Quellen

- Photon Unity Networking (PUN): [https://doc.photonengine.com/pun/current/getting-started/pun-intro](https://doc.photonengine.com/pun/current/getting-started/pun-intro)
- Agora SDK: [https://www.agora.io/en/products/video-call/](https://www.agora.io/en/products/video-call/)
- MongoDB: [https://www.mongodb.com/de-de](https://www.mongodb.com/de-de)



