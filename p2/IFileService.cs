namespace ProiectPOO;
/// interfata pt serviciul de fisiere
// "orice serviciu de fisiere trebuie sa faca asta"
public interface IFileService
{
    // salveaza lista de carti
    void SalveazaCarti(List<Carte> carti); //o metoda care salveaza datele aplicatiei; primeste o lista de obiecte Carte
// metoda SalveazaCarti ia toate cartile din memorie ,le scrie intr un fisier pt a nu se pierde cand se inchide aplicatia
// metoda nu returneaza nimic doar salveaza date.

    List<Carte> IncarcaCarti(); 
}