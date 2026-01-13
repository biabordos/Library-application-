using ProiectPOO;

// INITIALIZARE SERVICII
// cream serviciul care se ocupa de salvarea si incarcarea datelor din fisier
FileService fileService = new FileService();

// cream coordonatorul aplicatiei (LibraryService)
// acesta gestioneaza toate operatiile pe carti
// si primeste FileService pt a putea salva/incarca date
LibraryService libraryService = new LibraryService(fileService);

// AUTENTIFICARE
// cerem utilizatorului rolul
Console.Write("Rol (bibliotecar/membru): ");
string rol = Console.ReadLine();
Console.Write("Username: ");
string username = Console.ReadLine();

// cream variabile separate pt cele doua roluri
// la inceput sunt null (nu exista niciun utilizator logat)
Bibliotecar bibliotecar = null;
Membru membru = null;

// verificam ce rol a introdus utilizatorul
if (rol == "bibliotecar")
{
    bibliotecar = new Bibliotecar(username);
}
else
{
    membru = new Membru(username);
}

// bucla principala a aplicatiei

// variabila care controleaza rularea aplicatiei
bool ruleaza = true;

// cat timp ruleaza este true, aplicatia continua sa ruleze
while (ruleaza)
{
    // afișăm meniul în funcție de rol
    if (bibliotecar != null)
        bibliotecar.AfiseazaMeniu();
    else
        membru.AfiseazaMeniu();
    
    // citim optiunea aleasa de utilizator
    Console.Write("Optiune: ");
    string opt = Console.ReadLine();
    
    // iesire din aplicatie
    if (opt == "0")
    {
        ruleaza = false;  // oprim bucla principala
        libraryService.Salveaza();  // salvam datele in fisier inainte de iesire
        break;
    }

    // MENIU BIBLIOTECAR
    if (bibliotecar != null)
    {
        if (opt == "1") // optiunea 1: adaugare carte
        {
            Console.Write("Titlu: ");
            string titlu = Console.ReadLine();

            Console.Write("Autor: ");
            string autor = Console.ReadLine();

            Console.Write("Gen: ");
            string gen = Console.ReadLine();

            Console.Write("Copii: ");
            int copii = int.Parse(Console.ReadLine());

            libraryService.AdaugaCarte(titlu, autor, gen, copii); // apelam LibraryService pt adaugare carte
            Console.WriteLine("Carte adaugata!");
        }
        if (opt == "2")  // optiunea 2: afisare toate cartile
        {
            foreach (Carte c in libraryService.GetCarti()) // parcurgem lista de carti si le afisam
                Console.WriteLine(c);
        }
        if (opt == "3") // optiunea 3: stergere carte dupa titlu
        {
            Console.Write("Titlu carte: ");
            string titlu = Console.ReadLine();

            libraryService.StergeCarte(titlu); // apelam LibraryService pt a sterge cartea
            Console.WriteLine("Carte stearsa!");
        }
    }
    // MENIU MEMBRU
    if (membru != null)
    {
        if (opt == "1") // optiunea 1: cautare carte dupa titlu
        {
            Console.Write("Titlu cautat: ");
            string cauta = Console.ReadLine();

            foreach (Carte c in libraryService.CautaCarte(cauta)) // afisam cartile care contin textul cautat
                Console.WriteLine(c);
        }

        if (opt == "2")  // optiunea 2: afisare toate cartile
        {
            foreach (Carte c in libraryService.GetCarti())
                Console.WriteLine(c);
        }
        
        if (opt == "3") //optiunea 3: imprumutare carte
        {
            Console.Write("Titlu carte: ");
            string titlu = Console.ReadLine();

            Console.Write("Durata imprumut (zile): ");
            int zile = int.Parse(Console.ReadLine());

            libraryService.ImprumutaCarte(username, titlu, zile);
        }

        if (opt == "4") //optiunea 4: review
        {
            Console.Write("Titlu carte: ");
            string titlu = Console.ReadLine();

            Console.Write("Rating (1-5): ");
            int rating = int.Parse(Console.ReadLine());

            Console.Write("Comentariu: ");
            string comentariu = Console.ReadLine();

            libraryService.AdaugaReview(username, titlu, rating, comentariu);
        }
        
        if (opt == "5") // optiunea 5: afisare reviewuri
        {
            Console.Write("Titlul cartii pentru care vrei sa vezi reviewuri: ");
            string titlu = Console.ReadLine();

            libraryService.AfiseazaReviewuri(titlu);
        }
    }
}