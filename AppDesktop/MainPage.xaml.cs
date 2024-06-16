using Microsoft.Maui.Controls;

namespace AppDesktop {
    public partial class MainPage : ContentPage {
        private bool isPasswordVisible = false;

        public MainPage() {
            InitializeComponent();
        }

        private void OnForgotPasswordTapped(object sender, EventArgs e) {
            // Lógica para o link "Esqueceu a senha?"
        }

        private void TogglePasswordVisibility(object sender, EventArgs e) {
            isPasswordVisible = !isPasswordVisible;
            Password.IsPassword = !isPasswordVisible;
            ((ImageButton)sender).Source = isPasswordVisible ? "eye_icon_open.png" : "eye_icon.png";
        }

        private async void Login_Clicked_1(object sender, EventArgs e) {
            await Navigation.PushAsync(new Paginas());
        }
    }
}
