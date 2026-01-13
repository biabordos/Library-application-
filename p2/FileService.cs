 namespace ProiectPOO;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System;

//gestioneaza salvarea si incarcarea din fisier
public class FileService : IFileService
{
    private string fileName = "carti.txt"; //fisierul unde salvam datele
    // FISIERE NOI PENTRU PERSISTENTA TOTALA (Imprumuturi si Review-uri)
    private string fileLoans = "imprumuturi.txt";
    private string fileReviews = "reviewuri.txt";

    public void SalveazaCarti(List<Carte> carti)
    {
        try
        {
            using StreamWriter writer = new StreamWriter(fileName); //deschidem fisierul pentru scriere;folosim using pt inchidere automata
            //creem un obiect StreamWriter;
            
            foreach (Carte c in carti) // parcurgem toata lista de carti
            {
                writer.WriteLine($"{c.Titlu};{c.Autor};{c.Gen};{c.CopiiDisponibile}"); //scriem o linie pt fiecare carte
            }
        }
        catch (Exception e) //// tratam eventualele erori aparute la scrierea in fisier
        {
            Console.WriteLine("Eroare la salvare: " + e.Message);
        }
    }

    // --- METODA NOUA: Salveaza tot ce se intampla in aplicatie ---
    public void SalveazaTot(List<Carte> carti, List<Loan> imprumuturi, List<Review> recenzii)
    {
        // Salvezi cartile folosind metoda ta originala
        SalveazaCarti(carti);

        // Salvezi imprumuturile
        try 
        {
            using StreamWriter writer = new StreamWriter(fileLoans);
            foreach (var l in imprumuturi)
                writer.WriteLine($"{l.Username};{l.TitluCarte};{l.DataImprumut};{l.DurataZile}");
        } 
        catch { }

        // Salvezi recenziile
        try 
        {
            using StreamWriter writer = new StreamWriter(fileReviews);
            foreach (var r in recenzii)
                writer.WriteLine($"{r.Username};{r.TitluCarte};{r.Rating};{r.Comentariu}");
        } 
        catch { }
    }

    // incarcare din fisier (Metoda ta originala)
    public List<Carte> IncarcaCarti()
    {
        List<Carte> carti = new List<Carte>(); // facem o lista goala unde o sa punem cartile din fisier

        try
        {
            if (!File.Exists(fileName)) //daca fisierul nu exista returnam lista goala
                return carti;

            foreach (string linie in File.ReadAllLines(fileName)) //citeste toate liniiile din fisier;o linie = o carte
            {
                string[] p = linie.Split(';'); //separam linia cu ";"

                carti.Add(new Carte( //creem un obiect Carte folosind datele citite din fisier si il adaugam in lista
                    p[0],
                    p[1],
                    p[2],              //vom avea un array de stringuri
                    int.Parse(p[3])
                ));
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Eroare la incarcare: " + e.Message);
        }

        return carti; //returnam lista finala aplicatia o va folosi mai departe
    }

    // METODE NOI: Incarca datele salvate anterior la pornirea programului
    public List<Loan> IncarcaImprumuturi() 
    {
        List<Loan> lista = new List<Loan>();
        if (!File.Exists(fileLoans)) return lista;
        
        foreach (var linie in File.ReadAllLines(fileLoans)) 
        {
            var p = linie.Split(';');
            if(p.Length >= 4)
                lista.Add(new Loan(p[0], p[1], int.Parse(p[3])));
        }
        return lista;
    }

    public List<Review> IncarcaReviewuri() 
    {
        List<Review> lista = new List<Review>();
        if (!File.Exists(fileReviews)) return lista;
        
        foreach (var linie in File.ReadAllLines(fileReviews)) 
        {
            var p = linie.Split(';');
            if(p.Length >= 4)
                lista.Add(new Review(p[0], p[1], int.Parse(p[2]), p[3]));
        }
        return lista;
    }
}