using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;
using System;

namespace AppDesktop {
    public partial class Paginas : ContentPage {
        public Paginas() {
            InitializeComponent();

            paymentMethodPicker.SelectedIndexChanged += PaymentMethodPicker_SelectedIndexChanged;
        }

        private void OnHomeButtonClicked(object sender, EventArgs e) {
            // L�gica para o bot�o inicial
        }

        private void ImageButton_Clicked_1(object sender, EventArgs e) {
            // L�gica para o bot�o de configura��o
        }

        private void Botao_cadastro_clicavel(object sender, EventArgs e) {
            // L�gica para o bot�o de cadastrar cliente
        }

        private void Botao_Vendas_clicavel(object sender, EventArgs e) {
            // L�gica para o bot�o de vendas
            ShowVendasPage();
        }

        private void Botao_CadastarPI_clicavel(object sender, EventArgs e) {
            // L�gica para o bot�o de insumos e produtos
        }

        private void Botao_Relatorio_Clicavel(object sender, EventArgs e) {
            // L�gica para o bot�o de relat�rios
        }

        private void ShowVendasPage() {
            VendasPage.IsVisible = true;
            // Define o layout do MainContent para n�o cobrir a p�gina de vendas
            AbsoluteLayout.SetLayoutBounds(MainContent, new Rect(250, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(MainContent, AbsoluteLayoutFlags.HeightProportional | AbsoluteLayoutFlags.WidthProportional);
        }

        private void Botao_Informacoes_Cliente(object sender, EventArgs e) {
            // L�gica para o bot�o de informa��es do cliente
        }

        private void PaymentMethodPicker_SelectedIndexChanged(object sender, EventArgs e) {
            var selectedPaymentMethod = paymentMethodPicker.SelectedItem?.ToString();
            if (selectedPaymentMethod == "� Vista" || selectedPaymentMethod == "Cart�o de D�bito") {
                parcelasPicker.IsEnabled = false;
                parcelasPicker.SelectedIndex = -1; // Clear selection
            } else {
                parcelasPicker.IsEnabled = true;
            }
        }

        private void Button_Nova_venda(object sender, EventArgs e) {
            // Habilita o campo de texto de desconto
            DescontoEntry.IsEnabled = true;
            ClienteEntry.IsEnabled = true;
            ProdutoEntry.IsEnabled = true;
            QuantidadeEntry.IsEnabled = true;
            
        }

        private void Button_Confirmacao(object sender, EventArgs e) {

            String ProdutoDigitado = ProdutoEntry.Text;

            // Atualiza o Label com o nome digitado
            Produtonacaixa.Text = ProdutoDigitado;
        }
    }
}
