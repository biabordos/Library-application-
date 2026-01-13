namespace ProiectPOO;
// clasa care reprezinta un imprumut de carte
// un obiect Loan tine informatii despre CINE a imprumutat, CE carte,
// CAND a imprumutat si CAT timp este valabil imprumutul
public class Loan
{
    // usernameul utilizatorului care a imprumutat cartea
    // are doar get pt a nu putea fi modificat din exterior
    public string Username { get; }


// titlul cartii imprumutate
    public string TitluCarte { get; }
    
    // data la care s a facut imprumutul
    // este setata automat in momentul crearii obiectului Loan
    public DateTime DataImprumut { get; }
    
    // durata imprumutului in zile
    public int DurataZile { get; set; }
    // proprietate pt a calcula penalizarea
    public double Penalizare => EsteExpirat() ? (DateTime.Now - DataImprumut.AddDays(DurataZile)).Days * 2.5 : 0;
    
    // constructorul clasei Loan
    // e apelat atunci cand un utilizator imprumuta o carte
    public Loan(string username, string titluCarte, int durataZile)
    {
        // salvam username utilizatorului care a imprumutat cartea
        Username = username;
        TitluCarte = titluCarte;
        // salvam data curenta ca data a imprumutului
        // dateTime.Now reprezinta data si ora curenta
        DataImprumut = DateTime.Now;
        DurataZile = durataZile;
    }
    
    // metoda care verifica daca imprumutul a expirat
    // returneaza true daca perioada de imprumut a trecut
    // returneaza false daca imprumutul este inca valid
    public bool EsteExpirat()
    {
        return DateTime.Now > DataImprumut.AddDays(DurataZile);
    }
    public override string ToString() => $"{Username} - {TitluCarte} (Expiră: {DataImprumut.AddDays(DurataZile).ToShortDateString()})";
}


