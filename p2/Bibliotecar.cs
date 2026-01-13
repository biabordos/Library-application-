namespace ProiectPOO;
/// utilizator de tip bibliotecar
public class Bibliotecar : Utilizator
{
    public Bibliotecar(string username) : base(username) //apelam clasa parinte sa setam usernameul
    {
    }
    public override void AfiseazaMeniu() // suprascriem metoda AfiseazaMeniu, fiecare rol are meniul lui
    {
        Console.WriteLine();
        Console.WriteLine("=== MENIU BIBLIOTECAR ==="); //meniul bibliotecarului
        Console.WriteLine("1 - Adauga carte");
        Console.WriteLine("2 - Afiseaza carti");
        Console.WriteLine("3 - Sterge carte");
        Console.WriteLine("0 - Iesire");
    }
}