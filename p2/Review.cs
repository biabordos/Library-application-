namespace ProiectPOO;
/// recenzie lasata de un membru pt o carte
public class Review
{
    // usernameul utilizatorului care a scris recenzia
    // are doar get pt a preveni modificarea ulterioara
    public string Username { get; }
    
    // titlul cartii pt care a fost lasata recenzia
    // folosim titlul pt identificarea cartii
    public string TitluCarte { get; }
    
    // nota acordata cartii
    // valori permise: intre 1 si 5
    public int Rating { get; }   // 1 - 5
    
    // comentariul text scris de utilizator
    public string Comentariu { get; } 
    
    // constructor
    public Review(string username, string titluCarte, int rating, string comentariu)
    {
        Username = username;
        TitluCarte = titluCarte;
        Rating = rating;
        Comentariu = comentariu;
    }
    
    // suprascriem metoda ToString pt a afisa recenzia intr-un format usor de citit
    // aceasta metoda este folosita automat de Console.WriteLine(review)
    public override string ToString()
    {
        return $"{TitluCarte} | Rating: {Rating}/5 | {Comentariu}";
    }
}