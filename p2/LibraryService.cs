using System;
using System.Collections.Generic;
using System.Linq;

namespace ProiectPOO;

/// creierul aplicatiei: tine lista de carti, controleaza modificarile asupra ei,
// NU permite acces direct din exterior
public class LibraryService
{
    // lista interna de carti din biblioteca
    // este private pentru a preveni modificari directe din exterior
    private List<Carte> carti;

    // serviciul care se ocupa de salvarea si incarcarea cartilor din fisier
    private FileService fileService; // un obiect care stie sa salveze si sa incarce fisiere

    // lista de imprumuturi (Loan)
    private List<Loan> imprumuturi = new List<Loan>();

    // lista de recenzii (Review)
    private List<Review> recenzii = new List<Review>();
    
    // --- NOI VARIABLE PENTRU REGULILE BIBLIOTECARULUI ---
    public int DurataMaximaZile { get; set; } = 14;
    public int MaxCartiPerUtilizator { get; set; } = 3;
    public double PenalizarePerZi { get; set; } = 0.5;

    public LibraryService(FileService fileService) // constructorul clasei
    {
        this.fileService = fileService; // salveaza referinta catre FileService

        // incarca lista de carti din fisier folosind serviciul de fisiere
        // aceasta este lista initiala de carti la pornirea aplicatiei
        carti = fileService.IncarcaCarti();

        // Incarcam si datele suplimentare pentru interfata
        this.imprumuturi = fileService.IncarcaImprumuturi();
        this.recenzii = fileService.IncarcaReviewuri();
    }
    
    // metoda publica care returneaza o lista de carti
    public List<Carte> GetCarti()
    {
        // returneaza o copie a listei de carti
        // copia protejeaza lista interna de modificari accidentale din exterior
        return new List<Carte>(carti);
    }

    // metoda pentru a adauga o carte noua in biblioteca
    public void AdaugaCarte(string titlu, string autor, string gen, int copii)
    {
        // creeaza un obiect Carte cu datele primite si il adauga in lista interna
        carti.Add(new Carte(titlu, autor, gen, copii));
    }

    // metoda pentru a sterge o carte din biblioteca dupa titlu
    public void StergeCarte(string titlu)
    {
        // parcurge lista si sterge toate cartile al caror titlu este egal cu cel primit
        carti.RemoveAll(c => c.Titlu == titlu);
    }

    // metoda pentru a cauta carti dupa titlu
    public List<Carte> CautaCarte(string titlu)
    {
        return carti
            // selecteaza cartile care contin textul cautat
            .Where(c => c.Titlu.Contains(titlu))
            .ToList();
    }

    // metoda pentru a salva lista de carti in fisier
    public void Salveaza()
    {
        // delegam salvarea catre FileService
        fileService.SalveazaTot(carti, imprumuturi, recenzii); 
    }
    
    public void ImprumutaCarte(string username, string titluCarte, int durataZile)
    {
        // cautam cartea dupa titlu
        Carte carte = carti.FirstOrDefault(c => c.Titlu == titluCarte);

        // verificam daca exista cartea
        if (carte == null)
        {
            Console.WriteLine("Cartea nu exista.");
            return;
        }

        // verificam daca mai exista copii disponibile
        if (carte.CopiiDisponibile <= 0)
        {
            Console.WriteLine("Nu mai sunt copii disponibile.");
            return;
        }

        // scadem o copie (controlat, prin metoda Cartii)
        carte.ScadeCopie();

        // cream un obiect Loan si il adaugam in lista de imprumuturi
        imprumuturi.Add(new Loan(username, titluCarte, durataZile));

        Console.WriteLine("Imprumut realizat cu succes!");
        
        // Salvam automat orice modificare
        Salveaza();
    }
    
    public void AdaugaReview(string username, string titluCarte, int rating, string comentariu)
    {
        // cream un obiect Review si il adaugam in lista
        recenzii.Add(new Review(username, titluCarte, rating, comentariu));

        Console.WriteLine("Review adaugat!");
        
        // Salvam automat orice modificare
        Salveaza();
    }

    public List<Review> GetReviewuri(string titluCarte) 
    {
        // returnam toate review-urile pentru o carte data
        return recenzii
            .Where(r => r.TitluCarte == titluCarte)
            .ToList();
    }
    
    public void AfiseazaReviewuri(string titluCarte) //pt afisarea reviewurilor
    {
        List<Review> reviewuri = GetReviewuri(titluCarte);

        if (reviewuri.Count == 0)
        {
            Console.WriteLine("Nu exista reviewuri pentru aceasta carte.");
            return;
        }

        Console.WriteLine($"Review-uri pentru '{titluCarte}':");
        foreach (Review r in reviewuri)
        {
            Console.WriteLine(r); // apeleaza automat ToString() din Review
        }
    }

    // --- METODE NOI PENTRU FUNCTIONALITATILE SOLICITATE ---

    // Repara search bar-ul: cauta dupa titlu, autor sau gen (ignore case)
    public List<Carte> CautaAvansat(string filtru)
    {
        if (string.IsNullOrWhiteSpace(filtru)) return GetCarti();
        
        return carti.Where(c => 
            c.Titlu.ToLower().Contains(filtru.ToLower()) || 
            c.Autor.ToLower().Contains(filtru.ToLower()) || 
            c.Gen.ToLower().Contains(filtru.ToLower())
        ).ToList();
    }

    // Permite membrului sa prelungeasca perioada de imprumut
    public bool PrelungesteImprumut(string username, string titlu)
    {
        var loan = imprumuturi.FirstOrDefault(l => l.Username == username && l.TitluCarte == titlu);
        if (loan != null)
        {
            loan.DurataZile += 7; // Prelungim cu o saptamana
            Salveaza();
            return true;
        }
        return false;
    }

    public List<Loan> GetImprumuturiActive()
    {
        return imprumuturi;
    }

    public List<Loan> GetIstoricUtilizator(string username)
    {
        return imprumuturi.Where(l => l.Username == username).ToList();
    }

    public List<Review> GetToateReviewurile()
    {
        return recenzii;
    }
    
    // calculează penalizarea: 0.5 RON pe zi / fiecare zi care depaseste DurataZile
    public double CalculeazaPenalizare(Loan loan)
    {
        var zileTrecute = (DateTime.Now - loan.DataImprumut).TotalDays;
        if (zileTrecute > loan.DurataZile)
        {
            return Math.Round((zileTrecute - loan.DurataZile) * 0.5, 2); 
        }
        return 0;
    }

    // verif daca membrul are voie sa mai împrumute 
    public bool PoateImprumuta(string username)
    {
        int nrCartiActive = imprumuturi.Count(l => l.Username == username);
        return nrCartiActive < 3; // limita stabilita de mine
    }
    
}