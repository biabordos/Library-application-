using System.Windows;
using System.Windows.Controls;

namespace ProiectPOO {
    public partial class LoginWindow : Window {
        public LoginWindow() { InitializeComponent(); }

        private void BtnLogin_Click(object sender, RoutedEventArgs e) {
            string user = txtUsername.Text.Trim();
            if (string.IsNullOrEmpty(user)) {
                MessageBox.Show("Introdu un username!");
                return;
            }
            string rol = (cmbRol.SelectedItem as ComboBoxItem).Content.ToString();
            MainWindow main = new MainWindow(user, rol);
            main.Show();
            this.Close();
        }
    }
}