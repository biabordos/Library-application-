namespace ProiectPOO;
/// reprezinta o carte din biblioteca
public class Carte
{
    // proprietați doar cu get (imutabilitate)
    public string Titlu { get; } // proprietati carte
    public string Autor { get; }
    public string Gen { get; }
    public int CopiiDisponibile { get; private set; } // pot citi valoarea dar doar clasa carte o poate modifica
    // ADAUGARI PENTRU FUNCTIONALITATI:
    public string Format { get; set; } = "Fizic"; 
    public List<string> Rezervari { get; set; } = new List<string>();
    
    public Carte(string titlu, string autor, string gen, int copii) //constructor
    {
        Titlu = titlu;
        Autor = autor;
        Gen = gen;
        CopiiDisponibile = copii;
    }
    // metoda controlata pt modificarea copiilor
    public void ScadeCopie() // folosim o metoda publica ca sa poata fi apelata din exterior dar codul din interior e controlat de clasa
    {
        if (CopiiDisponibile > 0) //nu permitem valori negative
            CopiiDisponibile--;
    }
    
    public override string ToString() //metoda care spune cum se afiseaza obiectul 
    {
        return $"{Titlu} | {Autor} | {Gen} | {CopiiDisponibile} | {{Format}}";
    }
}