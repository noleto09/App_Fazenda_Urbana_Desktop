using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics; // Certifique-se de que este namespace est� inclu�do
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
            // Habilita os campos de texto
            DescontoEntry.IsEnabled = true;
            ClienteEntry.IsEnabled = true;
            ProdutoEntry.IsEnabled = true;
            QuantidadeEntry.IsEnabled = true;
        }

        private void Button_Confirmacao(object sender, EventArgs e) {
            string produtoDigitado = ProdutoEntry.Text;
            string QuantidadeDigitado = QuantidadeEntry.Text;
            string PrecounDigitado = Preco_UnitarioEntry.Text;
            string DescontoDigitado = DescontoEntry.Text;
            string ValorTotalDigitado = ValorToTalEntry.Text;


            // Cria uma nova linha para o produto dentro de um Frame
            var productLayout = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label { Text = produtoDigitado, WidthRequest = 100 },
                    
                }
            };

            var QuantidadeLayout = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                Children =
               {
                    new Label { Text = QuantidadeDigitado, WidthRequest = 100 },

                }
            };

            var PrecoUnLayout = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                Children =
               {
                    new Label { Text =PrecounDigitado, WidthRequest = 100 },

                }
            };

            var DescontoLayout = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                Children =
               {
                    new Label { Text = DescontoDigitado, WidthRequest = 100 },

                }
            };

            var ValorTotalLayout = new StackLayout {
                Orientation = StackOrientation.Horizontal,
                Children =
               {
                    new Label { Text = ValorTotalDigitado, WidthRequest = 100 },

                }
            };


            var productFrame = new Frame {
                Content = productLayout,
                BackgroundColor = Colors.White,
                BorderColor = Colors.Gainsboro,
                CornerRadius = 0,
                Padding = new Thickness(5,2),
                Margin = new Thickness(0, 0)
            };

            var QuantidadeFrame = new Frame {
                Content = QuantidadeLayout,
                BackgroundColor = Colors.White,
                BorderColor = Colors.Gainsboro,
                CornerRadius = 0,
                Padding = new Thickness(5, 2),
                Margin = new Thickness(0, 0)
            };

            var PrecoUnFrame = new Frame {
                Content = PrecoUnLayout,
                BackgroundColor = Colors.White,
                BorderColor = Colors.Gainsboro,
                CornerRadius = 0,
                Padding = new Thickness(5, 2),
                Margin = new Thickness(0, 0)
            };

            var DescontoFrame = new Frame {
                Content = DescontoLayout,
                BackgroundColor = Colors.White,
                BorderColor = Colors.Gainsboro,
                CornerRadius = 0,
                Padding = new Thickness(5, 2),
                Margin = new Thickness(0, 0)
            };

            var valorTotalFrame = new Frame {
                Content = ValorTotalLayout,
                BackgroundColor = Colors.White,
                BorderColor = Colors.Gainsboro,
                CornerRadius = 0,
                Padding = new Thickness(5, 2),
                Margin = new Thickness(0, 0)
            };


            // Adiciona o produto ao StackLayout
            ProductStackLayout.Children.Add(productFrame);
            LinhaQuantidade.Children.Add(QuantidadeFrame);
            LinhaPrecoUn.Children.Add(PrecoUnFrame);
            LinhaDesconto.Children.Add(DescontoFrame);
            LinhaValorTotal.Children.Add(valorTotalFrame);




        }
    }
}

