using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;
using System;

namespace AppDesktop {
    public partial class Paginas : ContentPage {
        public Paginas() {
            InitializeComponent();
        }

        private void OnHomeButtonClicked(object sender, EventArgs e) {
            if (MenuLateral.IsVisible) {
                // Oculta o menu lateral
                MenuLateral.IsVisible = false;
                // Expande o MainContent para preencher a tela inteira
                AbsoluteLayout.SetLayoutBounds(MainContent, new Rect(0, 0, 1, 1));
                AbsoluteLayout.SetLayoutFlags(MainContent, AbsoluteLayoutFlags.All);
            } else {
                // Exibe o menu lateral
                MenuLateral.IsVisible = true;
                // Define o layout do MainContent para a posi��o original
                AbsoluteLayout.SetLayoutBounds(MainContent, new Rect(250, 0, 1, 1));
                AbsoluteLayout.SetLayoutFlags(MainContent, AbsoluteLayoutFlags.HeightProportional | AbsoluteLayoutFlags.WidthProportional);
            }
        }

        private void ImageButton_Clicked_1(object sender, EventArgs e) {
            // L�gica para o bot�o de configura��o
        }

        private void Botao_cadastro_clicavel(object sender, EventArgs e) {
            // L�gica para o bot�o de cadastrar cliente
        }

        private void Botao_Vendas_clicavel(object sender, EventArgs e) {
            // L�gica para o bot�o de vendas
        }

        private void Botao_CadastarPI_clicavel(object sender, EventArgs e) {
            // L�gica para o bot�o de insumos e produtos
        }

        private void Botao_Relatorio_Clicavel(object sender, EventArgs e) {
            // L�gica para o bot�o de relat�rios
        }
    }
}
