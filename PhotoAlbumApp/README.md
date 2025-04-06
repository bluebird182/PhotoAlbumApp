# Felhőalapú elosztott rendszerek laboratórium - 4.házi

>Készítették: Erőss Helga Enikő (ZCA6AF), Gódor Márton (UDRF04)

## PhotoAlbumApp
A PhotoAlbumApp egy ASP.NET Core MVC alapú webalkalmazás, amely lehetővé teszi a felhasználók számára a fotók feltöltését, megtekintését, valamint törlését. A fényképek modal ablakban jelennek meg, és lehetőség van közöttük lépkedni. Az alkalmazás SQLite adatbázist használ, Dockerrel konténerizálva, és OpenShift-en is futtatható.

## Felhasznált technológiák
- **.NET 8:** A projekt ASP.NET Core MVC architektúrára épül, amely elválasztja az adatkezelést, megjelenítést és vezérlést.
- **Razor, ccs, Bootstrap:** A felhasználói felület megjelenítését és reszponzivitását biztosítják. Bootstrap biztosítja a modern dizájnt és interakciókat.
- **SQLite:** Fájl alapú relációs adatbázis a fotók és felhasználók tárolására.
- **Docker:** A projekt konténerizálása, az egyszerű fejlesztési és üzemeltetési környezet érdekében.
- **OpenShift:** Felhőalapú konténerkezelő platform, amely biztosítja az alkalmazás skálázható és megbízható üzemeltetését.

## Modellek
### Photo:
    - Id: a fotó azonosítója.
    - Name: a fotó neve.
    - UploadDate: a fotó feltöltésének ideje.
    - FilePath: a fotó elérési útvonala, ahonnan képes betölteni.
    - UderId: a fotót feltöltő felhasználó azonisítója.
### Profile:
    - Id: a felhasználó azonosítója.
    - Username: a felhasználó neve.
    - Email: a felhasználó e-mail címe.
    - Photos: a felhasználó általt feltöltött képek listája.
    - Password: a felhasználó bejelntkezéséhez szükséges jelszó.

## Funkciók
### Fotók:
- Új feltöltése
- Listázása kártyás nézetben
- Rendezés név vagy dátum szerint gomb segítségével
- Modal ablakos képnézegetés
- Nyilakkal lépkedés a képek között
- Fénykép törlése szemetes ikon segítségével, megerősítéssel