namespace ProiectPOO;
/// clasa de baza pt utilizatori
public class Utilizator
{
    public string Username { get; } // fiecare utilizator are un nume; folosim get pt ca nu se schimba dupa creare

    public Utilizator(string username) //constructor
    {
        Username = username;
    }
    // metoda virtuala de afisare
    public virtual void AfiseazaMeniu() // virtual = metoda poate fi suprascrisa
    {
        Console.WriteLine("Meniu utilizator");
    }
}
