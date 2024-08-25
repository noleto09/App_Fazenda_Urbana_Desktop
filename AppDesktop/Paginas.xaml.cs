using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;
using Microsoft.Maui.Layouts;
using System.Globalization;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Text;

namespace AppDesktop {
    public partial class Paginas : ContentPage {

       

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

        private async void Button_Novo_Cliente(object sender, EventArgs e) {
            // Variável para armazenar o ID gerado
            int randomId = 0;

            // Verifica se o idEntry já tem um valor (se o cadastro já foi iniciado)
            if (string.IsNullOrEmpty(idEntry.Text)) {
                // Cria uma nova instância de Random
                Random random = new Random();

                // Cria uma instância de DataAccess
                var dataAccess = new DataAccess();

                // Gera um número aleatório e verifica se ele já existe no banco
                bool idExists = true;
                while (idExists) {
                    // Gera um número aleatório de 6 dígitos
                    randomId = random.Next(100000, 999999); // Gera um número entre 100000 e 999999

                    // Verifica no banco de dados se o ID já existe
                    idExists = await dataAccess.VerificarIdClienteExistenteAsync(randomId.ToString());
                }

                // Define o valor gerado no Entry idEntry
                idEntry.Text = randomId.ToString();

                // Armazena temporariamente o ID gerado (pode ser numa variável ou lista temporária)
                // Isso pode ser necessário para garantir que outros usuários não peguem o mesmo ID
                ArmazenarIdTemporariamente(randomId);
            }

            // Habilita os campos para o novo cliente
            gerenciadorCampos.HabilitarCamposCliente(
                Cl_nome_Entry, CpfCnpjEntry,
                RuaEntry, NumeroEntry, ComplementarEntry, CepEntry,
                BairroEntry, EstadoEntry, CidadeEntry,
                RadioButtonPF, RadioButtonPJ);
        }

        // Método para armazenar temporariamente o ID gerado
        private void ArmazenarIdTemporariamente(int id) {
            // Implemente aqui a lógica para armazenar o ID temporariamente
            // Pode ser uma lista estática, cache, ou uma tabela temporária no banco de dados
        }


        

        // Evento TextChanged no Entry
        private void Cl_dn_Entry_TextChanged(object sender, TextChangedEventArgs e) {
            var entry = sender as Entry;

            // Remove caracteres não numéricos
            string text = new string(entry.Text.Where(char.IsDigit).ToArray());

            // Limita o comprimento a no máximo 8 dígitos (DDMMAAAA)
            if (text.Length > 8) {
                text = text.Substring(0, 8); // Corta o texto para 8 dígitos
            }

            // Formata o texto para o formato DD/MM/AAAA
            if (text.Length >= 2 && text.Length <= 4) {
                text = text.Insert(2, "/");
            } else if (text.Length > 4) {
                text = text.Insert(2, "/");
                text = text.Insert(5, "/");
            }

            // Evita loop infinito ao atualizar o texto no Entry
            if (entry.Text != text) {
                entry.Text = text;
            }

            // Converte o texto formatado para o formato yyyy-MM-dd
            if (text.Length == 10) {
                try {
                    DateTime parsedDate = DateTime.ParseExact(text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string dbDate = parsedDate.ToString("yyyy-MM-dd"); // Formato para banco de dados
                                                                       // Aqui você pode armazenar ou usar a data formatada para banco de dados
                                                                       // Exemplo: SaveDateToDatabase(dbDate);
                } catch (FormatException) {
                    // Trate o erro de formato, se necessário
                }
            }
        }


        // Contém a  lógica da classe Gereciador Tipo Cliente ou seja o sexo do cliente pessoa física
        private readonly GerenciadorTipoCliente gerenciadorTipoCliente = new GerenciadorTipoCliente();

        private void OnClientTypeChanged(object sender, CheckedChangedEventArgs e) {
            gerenciadorTipoCliente.OnClientTypeChanged(RadioButtonPF, RadioButtonPJ, CpfCnpjEntry, CheckBoxMale, CheckBoxFemale, CheckBoxOtherSex);

            if (RadioButtonPF.IsChecked) {
                // Habilitar os campos Entry
                Cl_Rg_Entry.IsEnabled = true;
                Cl_dn_Entry.IsEnabled = true;

                // Ajustar a altura dos Entry para deixá-los visíveis
                Cl_Rg_Entry.HeightRequest = -1; // -1 define a altura automática com base no conteúdo
                Cl_dn_Entry.HeightRequest = -1;

                // Desabilitar os campos de PJ (se houver)
                // Exemplo:
                // Cl_PJ_Field.IsEnabled = false;
            } else {
                // Desabilitar os campos Entry
                Cl_Rg_Entry.IsEnabled = false;
                Cl_dn_Entry.IsEnabled = false;

                // Redefinir a altura para ocultá-los
                Cl_Rg_Entry.HeightRequest = 0;
                Cl_dn_Entry.HeightRequest = 0;
            }
        }

        private async void Button_Salvar_Cliente(object sender, EventArgs e) {

            // LÓGICA PARA QUANDO O USUÁRIO SALVAR O CADASTRO OS ENTRY FICA FALSE NOVAMENTE PARA DIGITAR ATÉ QUE O USUÁRIO CLIQUE NO BOTÃO NOVO
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


            // Obter o tipo de cliente usando o GerenciadorTipoCliente
            string tipoCliente = RadioButtonPF.IsChecked ? "Pessoa Física" : "Pessoa Jurídica";
            // Obter os dados dos campos
            string cl_Id = idEntry.Text;
            string cl_nome = Cl_nome_Entry.Text;
            string cl_cpf_cnpj = CpfCnpjEntry.Text;
            string cl_rg = Cl_Rg_Entry.Text;
            string cl_data_nascimento = Cl_dn_Entry.Text;
            string genero = selectedGender;
            string Ed_cliente_rua = RuaEntry.Text;
            string Ed_cliente_numero = NumeroEntry.Text;
            string Ed_cliente_complemento = ComplementarEntry.Text;
            string Ed_cliente_cep = CepEntry.Text;
            string Ed_cliente_bairro = BairroEntry.Text;
            string Ed_cliente_estado = EstadoEntry.Text;
            string Ed_cliente_cidade = CidadeEntry.Text;


            // Criar uma instância da classe DataAccess
            var dataAccess = new DataAccess();

            // Inserir dados no banco de dados, incluindo o gênero
            bool success = await dataAccess.InserirClienteAsync(cl_Id, tipoCliente, cl_nome, cl_cpf_cnpj, cl_rg, cl_data_nascimento, genero, Ed_cliente_rua, Ed_cliente_numero,
                Ed_cliente_complemento, Ed_cliente_cep, Ed_cliente_bairro, Ed_cliente_estado, Ed_cliente_cidade);

            if (success) {
                await DisplayAlert("Sucesso", "Dados inseridos com sucesso.", "OK");
                // Limpar os campos
                idEntry.Text = "";
                Cl_nome_Entry.Text = "";
                CpfCnpjEntry.Text = "";
                Cl_Rg_Entry.Text = "";
                Cl_dn_Entry.Text = "";
                RuaEntry.Text = "";
                NumeroEntry.Text = "";
                ComplementarEntry.Text = "";
                CepEntry.Text = "";
                BairroEntry.Text = "";
                EstadoEntry.Text = "";
                CidadeEntry.Text = "";


            } else {
                await DisplayAlert("Erro", "Não foi possível inserir os dados. Por favor, tente novamente.", "OK");
            }
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


