namespace ProiectPOO;
/// utilizator de tip membru
public class Membru : Utilizator
{
    public Membru(string username) : base(username) // apelam clasa parinte pt a seta username
    {
    }
    public override void AfiseazaMeniu() ///suprascriem metoda AfiseazaMeniu
    {
        Console.WriteLine();
        Console.WriteLine("=== MENIU MEMBRU ===");                //meniu membru
        Console.WriteLine("1 - Cauta carte dupa titlu");
        Console.WriteLine("2 - Afiseaza toate cartile");
        Console.WriteLine("3 - Imprumuta carte");
        Console.WriteLine("4 - Adauga review");
        Console.WriteLine("5. Afiseaza review-uri");
        Console.WriteLine("0 - Iesire");
    }
}
