namespace AppDesktop {
    public partial class MainPage : ContentPage {
        int count = 0;

        public MainPage() {
            InitializeComponent();
        }

        private void Login_Clicked(object sender, EventArgs e) {

            Navigation.PushAsync(new Paginas());


        }

    }

}



