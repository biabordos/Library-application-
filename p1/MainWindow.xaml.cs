using System;
using System.Linq;
using System.Windows;

namespace ProiectPOO
{
    public partial class MainWindow : Window
    {
        private LibraryService _libService;
        private string _username;
        private string _rol;

        public MainWindow(string username, string rol)
        {
            InitializeComponent();
            _username = username;
            _rol = rol;
            _libService = new LibraryService(new FileService());

            ConfigurareAcces();
            RefreshGrid();
        }

        private void ConfigurareAcces()
        {
            if (_rol == "membru") tabAdmin.Visibility = Visibility.Collapsed;
            else tabMembru.Visibility = Visibility.Collapsed;
        }

        private void RefreshGrid()
        {
            dgCarti.ItemsSource = null;
            dgCarti.ItemsSource = _libService.GetCarti();

            if (_rol == "bibliotecar")
            {
                if (dgToateImprumuturile != null) dgToateImprumuturile.ItemsSource = _libService.GetImprumuturiActive();
                if (dgRecenziiBibliotecar != null) dgRecenziiBibliotecar.ItemsSource = _libService.GetToateReviewurile();
            }
            else
            {
                if (dgIstoricPersonal != null) dgIstoricPersonal.ItemsSource = _libService.GetIstoricUtilizator(_username);
            }
        }

        private void BtnCauta_Click(object sender, RoutedEventArgs e)
        {
            string f = txtSearch.Text.ToLower();
            dgCarti.ItemsSource = _libService.GetCarti().Where(c => 
                c.Titlu.ToLower().Contains(f) || c.Autor.ToLower().Contains(f) || c.Gen.ToLower().Contains(f)).ToList();
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e) => RefreshGrid();

        private void BtnAdauga_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(addTitlu.Text))
            {
                _libService.AdaugaCarte(addTitlu.Text, addAutor.Text, addGen.Text, int.Parse(addCopii.Text));
                _libService.Salveaza();
                RefreshGrid();
            }
        }

        private void BtnSterge_Click(object sender, RoutedEventArgs e)
        {
            if (dgCarti.SelectedItem is Carte c) { _libService.StergeCarte(c.Titlu); _libService.Salveaza(); RefreshGrid(); }
        }

        private void BtnImprumuta_Click(object sender, RoutedEventArgs e)
        {
            if (dgCarti.SelectedItem is Carte c)
            {
                if (!_libService.PoateImprumuta(_username)) { MessageBox.Show("Limită de 3 cărți atinsă!"); return; }
                int zile = int.TryParse(txtZileImprumut.Text, out int z) ? z : 14;
                _libService.ImprumutaCarte(_username, c.Titlu, zile);
                RefreshGrid();
            }
        }

        private void BtnPrelungeste_Click(object sender, RoutedEventArgs e)
        {
            if (dgIstoricPersonal.SelectedItem is Loan l) { _libService.PrelungesteImprumut(_username, l.TitluCarte); RefreshGrid(); }
        }

        private void BtnReview_Click(object sender, RoutedEventArgs e)
        {
            if (dgCarti.SelectedItem is Carte c) { _libService.AdaugaReview(_username, c.Titlu, (int)sldRating.Value, txtComentariu.Text); RefreshGrid(); }
        }

        private void BtnNotifica_Click(object sender, RoutedEventArgs e)
        {
            if (dgToateImprumuturile.SelectedItem is Loan l)
            {
                double pen = _libService.CalculeazaPenalizare(l);
                MessageBox.Show($"User: {l.Username}\nPenalizare: {pen} RON", "Notificare");
            }
        }
    }
}