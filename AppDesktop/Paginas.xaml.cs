namespace AppDesktop;

public partial class Paginas : ContentPage {
    public Paginas() {
        InitializeComponent();
    }

    private void OnHomeButtonClicked(object sender, EventArgs e) {
        MenuLateral.IsVisible = !MenuLateral.IsVisible;
    }

}