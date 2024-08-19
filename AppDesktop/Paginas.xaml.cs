using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Text;

namespace AppDesktop {
    public partial class Paginas : ContentPage {

        private string selectedSexo;

        public Paginas() {
            InitializeComponent();
            paymentMethodPicker.SelectedIndexChanged += PaymentMethodPicker_SelectedIndexChanged;
            this.SizeChanged += OnPageSizeChanged;
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


        private readonly Ajuste_Paginas ajustePaginas = new Ajuste_Paginas();

        private void Button_Cadastrar_Usuario(object sender, EventArgs e) {
            // Garante que a página de configuração esteja visível antes de exibir a de cadastro de usuário
            ShowconfiguracaoPage();
            ajustePaginas.MostrarPagina(Pagina_Cadastrar_Usuario, MainContent);
        }

        private void Botao_cadastro_clicavel(object sender, EventArgs e) {
            ajustePaginas.MostrarPagina(Pagina_Cadastrar_Cliente, MainContent);
        }

        private void Botao_Vendas_clicavel(object sender, EventArgs e) {
            ajustePaginas.MostrarPagina(VendasPage, MainContent);
        }

        private void Botao_CadastarPI_clicavel(object sender, EventArgs e) {
            ajustePaginas.MostrarPagina(Insumos_Produtos, MainContent);
        }

        private void Botao_Relatorio_Clicavel(object sender, EventArgs e) {
           
        }

        private void Botao_Configuracao(object sender, EventArgs e) {
            // Exibe a página de configuração
            ShowconfiguracaoPage();
        }

        private void ShowconfiguracaoPage() {
            // Exibe a página de configuração
            ajustePaginas.MostrarPagina(configuracaoPage, MainContent);
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



        private readonly FramesPaginaVendas framesPaginaVendas = new FramesPaginaVendas();

        private void Button_Confirmacao(object sender, EventArgs e) {
            string produtoDigitado = ProdutoEntry.Text;
            string quantidadeDigitado = QuantidadeEntry.Text;
            string precoUnDigitado = Preco_UnitarioEntry.Text;
            string descontoDigitado = DescontoEntry.Text;
            string valorTotalDigitado = ValorToTalEntry.Text;

            framesPaginaVendas.AdicionarFramesPaginaVendas(
                ProductStackLayout, LinhaQuantidade, LinhaPrecoUn, LinhaDesconto, LinhaValorTotal,
                produtoDigitado, quantidadeDigitado, precoUnDigitado, descontoDigitado, valorTotalDigitado);
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
            AdjustContentViewLayout(LogoAgrotech, newWidth, newHeight);
        }

        private void AdjustContentViewLayout(View contentView, double width, double height) {
            AbsoluteLayout.SetLayoutBounds(contentView, new Rect(0.9, 0.5, width, height));
            AbsoluteLayout.SetLayoutFlags(contentView, AbsoluteLayoutFlags.PositionProportional);

        }

        private string selectedGender;

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e) {
            if (RadioButtonPF.IsChecked) {
                if (sender is CheckBox selectedCheckBox && selectedCheckBox.IsChecked) {
                    // Desmarcar todos os outros CheckBox
                    foreach (var checkbox in new[] { CheckBoxMale, CheckBoxFemale, CheckBoxOtherSex }) {
                        if (checkbox != selectedCheckBox) {
                            checkbox.IsChecked = false;
                        }
                    }

                    // Armazenar a escolha do usuário
                    if (selectedCheckBox == CheckBoxMale) {
                        selectedGender = "Masculino";
                    } else if (selectedCheckBox == CheckBoxFemale) {
                        selectedGender = "Feminino";
                    } else if (selectedCheckBox == CheckBoxOtherSex) {
                        selectedGender = "Outros";
                    }
                }
            }
        }

        private readonly GerenciadorCamposFormulario gerenciadorCampos = new GerenciadorCamposFormulario();

        private void Button_Nova_venda(object sender, EventArgs e) {
            gerenciadorCampos.HabilitarCamposVenda(DescontoEntry, ClienteEntry, ProdutoEntry, QuantidadeEntry);
        }

        private void Button_Novo_Cliente(object sender, EventArgs e) {
            gerenciadorCampos.HabilitarCamposCliente(
                idEntry, Cl_nome_Entry, CpfCnpjEntry, Cl_Rg_Entry, Cl_dn_Entry,
                RuaEntry, NumeroEntry, ComplementarEntry, CepEntry,
                BairroEntry, EstadoEntry, CidadeEntry,
                RadioButtonPF, RadioButtonPJ);
        }


        private async void Button_Salvar_Cliente(object sender, EventArgs e) {

            idEntry.IsEnabled = false;
            Cl_nome_Entry.IsEnabled = false;
            CpfCnpjEntry.IsEnabled = false;
            Cl_Rg_Entry.IsEnabled = false;
            Cl_dn_Entry.IsEnabled = false;
            RuaEntry.IsEnabled = false;
            NumeroEntry.IsEnabled = false;
            ComplementarEntry.IsEnabled = false;
            CepEntry.IsEnabled = false;
            BairroEntry.IsEnabled = false;
            EstadoEntry.IsEnabled = false;
            CidadeEntry.IsEnabled = false;
            RadioButtonPF.IsEnabled = false;
            RadioButtonPJ.IsEnabled = false;


            // Obter os dados dos campos
            string cl_nome = Cl_nome_Entry.Text;
            string cl_cpf_cnpj = CpfCnpjEntry.Text;
            string cl_rg = Cl_Rg_Entry.Text;
            string cl_data_nascimento = Cl_dn_Entry.Text;

            // Obter o gênero selecionado
            string genero = selectedGender;

            // Criar uma instância da classe DataAccess
            var dataAccess = new DataAccess();

            // Inserir dados no banco de dados, incluindo o gênero
            bool success = await dataAccess.InserirClienteAsync(cl_nome, cl_cpf_cnpj, cl_rg, cl_data_nascimento, genero);

            if (success) {
                await DisplayAlert("Sucesso", "Dados inseridos com sucesso.", "OK");
                // Limpar os campos
                Cl_nome_Entry.Text = "";
                CpfCnpjEntry.Text = "";
                Cl_Rg_Entry.Text = "";
                Cl_dn_Entry.Text = "";

            } else {
                await DisplayAlert("Erro", "Não foi possível inserir os dados. Por favor, tente novamente.", "OK");
            }
        }


        // Contém a  lógica da classe Gereciador Tipo Cliente ou seja o sexo do cliente pessoa física
        private readonly GerenciadorTipoCliente gerenciadorTipoCliente = new GerenciadorTipoCliente();

        private void OnClientTypeChanged(object sender, CheckedChangedEventArgs e) {
            gerenciadorTipoCliente.OnClientTypeChanged(RadioButtonPF, RadioButtonPJ, CpfCnpjEntry, CheckBoxMale, CheckBoxFemale, CheckBoxOtherSex);
        }

        // Contém a lógica da Classe Lg_Formatacao_cpnf_cpf
        private Lg_Formatacao_cpnf_cpf formatador = new Lg_Formatacao_cpnf_cpf();

        private void CpfCnpjEntry_TextChanged(object sender, TextChangedEventArgs e) {
            var entry = sender as Entry;

            if (entry == null) return;

            string formattedText = string.Empty;

            if (RadioButtonPF.IsChecked) {
                // Formata como CPF
                formattedText = formatador.FormatarCpf(e.NewTextValue);
            } else if (RadioButtonPJ.IsChecked) {
                // Formata como CNPJ
                formattedText = formatador.FormatarCnpj(e.NewTextValue);
            }

            // Define o texto formatado
            if (entry.Text != formattedText) {
                entry.Text = formattedText;
            }

            // Move o cursor para o final do texto
            entry.CursorPosition = entry.Text.Length;
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


