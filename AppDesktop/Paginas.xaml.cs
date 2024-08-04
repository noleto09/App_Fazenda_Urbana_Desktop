using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using System;

namespace AppDesktop {
    public partial class Paginas : ContentPage {

        private string selectedSexo;

        public Paginas() {
            InitializeComponent();
            paymentMethodPicker.SelectedIndexChanged += PaymentMethodPicker_SelectedIndexChanged;
            this.SizeChanged += OnPageSizeChanged;
        }


        private void Botao_Configuracao(object sender, EventArgs e) {
            // Lógica para o botão de configuração
            ShowconfiguracaoPage();
        }

        private async void Button_Salvar_Clicked(object sender, EventArgs e) {
            // Obter os dados dos campos
            string nome = nomeEntry.Text; // Ajuste os nomes dos Entry conforme sua implementação
            string cpf = cpfEntry.Text;
            string nomeUsuario = nomeUsuarioEntry.Text;
            string senha = senhaEntry.Text;

            // Criar uma instância da classe DataAccess
            var dataAccess = new DataAccess();

            // Inserir dados no banco de dados
            bool success = await dataAccess.InserirUsuarioAsync(nome, cpf, nomeUsuario, senha);

            if (success) {
                await DisplayAlert("Sucesso", "Dados inseridos com sucesso.", "OK");
                // Limpar os campos ou realizar outras ações conforme necessário
                nomeEntry.Text = "";
                cpfEntry.Text = "";
                nomeUsuarioEntry.Text = "";
                senhaEntry.Text = "";
            } else {
                await DisplayAlert("Erro", "Não foi possível inserir os dados. Por favor, tente novamente.", "OK");
            }
        }


        private void Button_Cadastrar_Usuario(object sender, EventArgs e) {
            ShowCadastrar_Usuario();

        }

        private void Botao_cadastro_clicavel(object sender, EventArgs e) {
            // Lógica para o botão de cadastrar cliente
            ShowCadastrar_Cliente();
        }

        private void Botao_Vendas_clicavel(object sender, EventArgs e) {
            // Lógica para o botão de vendas
            ShowVendasPage();
        }

        private void Botao_CadastarPI_clicavel(object sender, EventArgs e) {
            // Lógica para o botão de insumos e produtos
            ShowInsumos_Produtos();
        }

        private void Botao_Relatorio_Clicavel(object sender, EventArgs e) {
            // Lógica para o botão de relatórios
        }

        private void ShowVendasPage() {
            VendasPage.IsVisible = true;
            // Define o layout do MainContent para não cobrir a página de vendas
            AbsoluteLayout.SetLayoutBounds(MainContent, new Rect(250, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(MainContent, AbsoluteLayoutFlags.HeightProportional | AbsoluteLayoutFlags.WidthProportional);
        }

        private void ShowconfiguracaoPage() {
            configuracaoPage.IsVisible = true;

            AbsoluteLayout.SetLayoutBounds(MainContent, new Rect(250, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(MainContent, AbsoluteLayoutFlags.HeightProportional | AbsoluteLayoutFlags.WidthProportional);
        }

        private void ShowCadastrar_Usuario() {
            Pagina_Cadastrar_Usuario.IsVisible = true;

            AbsoluteLayout.SetLayoutBounds(MainContent, new Rect(250, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(MainContent, AbsoluteLayoutFlags.HeightProportional | AbsoluteLayoutFlags.WidthProportional);
        }

        private void ShowCadastrar_Cliente() {
            Pagina_Cadastrar_Cliente.IsVisible = true;

            AbsoluteLayout.SetLayoutBounds(MainContent, new Rect(250, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(MainContent, AbsoluteLayoutFlags.HeightProportional | AbsoluteLayoutFlags.WidthProportional);
        }

        private void ShowInsumos_Produtos() {
            Insumos_Produtos.IsVisible = true;

            AbsoluteLayout.SetLayoutBounds(MainContent, new Rect(250, 0, 1, 1));
            AbsoluteLayout.SetLayoutFlags(MainContent, AbsoluteLayoutFlags.HeightProportional | AbsoluteLayoutFlags.WidthProportional);
        }

        private void Botao_Informacoes_Cliente(object sender, EventArgs e) {
            // Lógica para o botão de informações do cliente
        }

        private void PaymentMethodPicker_SelectedIndexChanged(object sender, EventArgs e) {
            var selectedPaymentMethod = paymentMethodPicker.SelectedItem?.ToString();
            if (selectedPaymentMethod == "À Vista" || selectedPaymentMethod == "Cartão de Débito") {
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

            // Função para criar um StackLayout com um Label dentro
            StackLayout CriarStackLayout(string texto) {
                return new StackLayout {
                    Orientation = StackOrientation.Horizontal,
                    Children = {
                new Label { Text = texto, WidthRequest = 100 }
            }
                };
            }

            // Função para criar um Frame que contém um StackLayout
            Frame CriarFrame(StackLayout layout) {
                return new Frame {
                    Content = layout,
                    BackgroundColor = Colors.White,
                    BorderColor = Colors.Gainsboro,
                    CornerRadius = 0,
                    Padding = new Thickness(5, 2),
                    Margin = new Thickness(0, 0)
                };
            }

            // Cria os layouts e frames
            var productFrame = CriarFrame(CriarStackLayout(produtoDigitado));
            var QuantidadeFrame = CriarFrame(CriarStackLayout(QuantidadeDigitado));
            var PrecoUnFrame = CriarFrame(CriarStackLayout(PrecounDigitado));
            var DescontoFrame = CriarFrame(CriarStackLayout(DescontoDigitado));
            var ValorTotalFrame = CriarFrame(CriarStackLayout(ValorTotalDigitado));

            // Adiciona os frames aos StackLayouts apropriados
            ProductStackLayout.Children.Add(productFrame);
            LinhaQuantidade.Children.Add(QuantidadeFrame);
            LinhaPrecoUn.Children.Add(PrecoUnFrame);
            LinhaDesconto.Children.Add(DescontoFrame);
            LinhaValorTotal.Children.Add(ValorTotalFrame);
        }


        private void OnPageSizeChanged(object sender, EventArgs e) {
            double width = this.Width;
            double height = this.Height;

            // Defina os tamanhos desejados para a tela menor
            double desiredWidth = 1100;
            double desiredHeight = 650;

            // Calcule os novos tamanhos baseados na proporção da tela maior
            double newWidth = width > desiredWidth ? width * 0.8 : desiredWidth;
            double newHeight = height > desiredHeight ? height * 0.9 : desiredHeight;

            // Ajuste os tamanhos e posicionamento do ContentView
            AdjustContentViewLayout(VendasPage, newWidth, newHeight);
            AdjustContentViewLayout(configuracaoPage, newWidth, newHeight);
            AdjustContentViewLayout(Pagina_Cadastrar_Usuario, newWidth, newHeight);
            AdjustContentViewLayout(Pagina_Cadastrar_Cliente, newWidth, newHeight);
            AdjustContentViewLayout(Insumos_Produtos, newWidth, newHeight);
        }

        private void AdjustContentViewLayout(View contentView, double width, double height) {
            AbsoluteLayout.SetLayoutBounds(contentView, new Rect(0.9, 0.5, width, height));
            AbsoluteLayout.SetLayoutFlags(contentView, AbsoluteLayoutFlags.PositionProportional);
           
        }

        private void Button_Salvar_Cliente(object sender, EventArgs e) {
            
        }

        private void OnClientTypeChanged(object sender, CheckedChangedEventArgs e) {
            if (RadioButtonPF.IsChecked) {
                // Se PF estiver selecionado, habilita os CheckBox para sexo
                CheckBoxMale.IsEnabled = true;
                CheckBoxFemale.IsEnabled = true;
                CheckBoxOtherSex.IsEnabled = true;
            } else if (RadioButtonPJ.IsChecked) {
                // Se PJ estiver selecionado, desabilita os CheckBox para sexo
                CheckBoxMale.IsEnabled = false;
                CheckBoxFemale.IsEnabled = false;
                CheckBoxOtherSex.IsEnabled = false;

                // Desmarca todos os CheckBox para evitar seleção acidental
                CheckBoxMale.IsChecked = false;
                CheckBoxFemale.IsChecked = false;
                CheckBoxOtherSex.IsChecked = false;
            }
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e) {
            if (RadioButtonPF.IsChecked) {
                if (sender is CheckBox selectedCheckBox && selectedCheckBox.IsChecked) {
                    // Desmarcar todos os outros CheckBox
                    foreach (var checkbox in new[] { CheckBoxMale, CheckBoxFemale, CheckBoxOtherSex }) {
                        if (checkbox != selectedCheckBox) {
                            checkbox.IsChecked = false;
                        }
                    }
                }
            }
        }





        private void OnAddressTypeChanged(object sender, CheckedChangedEventArgs e) {
           
        }

        private void CheckBoxMale_CheckedChanged(object sender, CheckedChangedEventArgs e) {

        }

        private void CheckBoxFemale_CheckedChanged(object sender, CheckedChangedEventArgs e) {

        }

        private void CheckBoxOtherSex_CheckedChanged(object sender, CheckedChangedEventArgs e) {

        }
    }
}
    

