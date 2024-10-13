using System.Threading.Tasks;
using MySql.Data.MySqlClient;

public class DataAccess {
    private readonly string connStr = "server=localhost;user=root;database=sys_agrotech;port=3306;password=AGROtech78@%24";

    public async Task<bool> InserirUsuarioAsync(string nome, string cpf, string nomeUsuario, string senha) {
        try {
            using (var conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();

                string sql = "INSERT INTO usuario (nome, cpf, nome_usuario, senha_usuario) VALUES (@nome, @cpf, @nome_usuario, @senha)";
                using (var cmd = new MySqlCommand(sql, conn)) {
                    cmd.Parameters.AddWithValue("@nome", nome);
                    cmd.Parameters.AddWithValue("@cpf", cpf);
                    cmd.Parameters.AddWithValue("@nome_usuario", nomeUsuario);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    await cmd.ExecuteNonQueryAsync();
                }
            }
            return true;
        } catch (Exception ex) {
            // Logar o erro ou exibir uma mensagem de erro
            Console.WriteLine("Erro: " + ex.Message);
            return false;
        }
    }

    public async Task<bool> VerificarIdClienteExistenteAsync(string id) {
        try {
            using (var conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();

                string sql = "SELECT COUNT(*) FROM cliente WHERE Id_cliente = @id_cliente";
                using (var cmd = new MySqlCommand(sql, conn)) {
                    cmd.Parameters.AddWithValue("@id_cliente", id);

                    var result = await cmd.ExecuteScalarAsync();
                    return Convert.ToInt32(result) > 0;
                }
            }
        } catch (Exception ex) {
            // Logar o erro ou exibir uma mensagem de erro
            Console.WriteLine("Erro: " + ex.Message);
            return false;
        }
    }


    public async Task<bool> InserirClienteAsync(string cl_id, string tipoCliente, string cl_nome, string cl_cpf_cnpj, string cl_rg, string cl_data_nascimento, string genero, string Ed_cliente_rua, string Ed_cliente_numero,
    string Ed_cliente_complemento, string Ed_cliente_cep, string Ed_cliente_bairro, string Ed_cliente_estado, string Ed_cliente_cidade) {
        try {
            using (var conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();

                // Iniciar uma transação para garantir a consistência dos dados
                using (var transaction = await conn.BeginTransactionAsync()) {
                    try {
                        // Primeiro, inserir os dados na tabela `cliente`
                        string sqlCliente = "INSERT INTO cliente (tipo_cliente, nome_cliente, rua, numero, complemento, CEP, bairro, estado, cidade, nm_cadastro_cliente) " +
                                            "VALUES (@tipocliente, @nome, @ed_rua, @ed_numero, @ed_complementar, @ed_cep, @ed_bairro, @ed_estado, @ed_cidade, @nm_cadastro_cliente)";

                        long idCliente;
                        using (var cmdCliente = new MySqlCommand(sqlCliente, conn, transaction)) {
                            cmdCliente.Parameters.AddWithValue("@tipocliente", tipoCliente);
                            cmdCliente.Parameters.AddWithValue("@nome", cl_nome);
                            cmdCliente.Parameters.AddWithValue("@ed_rua", Ed_cliente_rua);
                            cmdCliente.Parameters.AddWithValue("@ed_numero", Ed_cliente_numero);
                            cmdCliente.Parameters.AddWithValue("@ed_complementar", Ed_cliente_complemento);
                            cmdCliente.Parameters.AddWithValue("@ed_cep", Ed_cliente_cep);
                            cmdCliente.Parameters.AddWithValue("@ed_bairro", Ed_cliente_bairro);
                            cmdCliente.Parameters.AddWithValue("@ed_estado", Ed_cliente_estado);
                            cmdCliente.Parameters.AddWithValue("@ed_cidade", Ed_cliente_cidade);
                            cmdCliente.Parameters.AddWithValue("@nm_cadastro_cliente", cl_id);

                            // Executar a inserção e obter o ID gerado
                            await cmdCliente.ExecuteNonQueryAsync();
                            idCliente = cmdCliente.LastInsertedId;
                        }

                        // Verificar o tipo de cliente e inserir na tabela correspondente
                        if (tipoCliente == "Pessoa Física") {
                            string sqlPF = "INSERT INTO cliente_pf (id_cliente, cpf, rg_cliente, data_nascimento, genero_cliente) " +
                                           "VALUES (@id_cliente, @cpf, @rg, @data_nascimento, @genero)";

                            using (var cmdPF = new MySqlCommand(sqlPF, conn, transaction)) {
                                cmdPF.Parameters.AddWithValue("@id_cliente", idCliente);
                                cmdPF.Parameters.AddWithValue("@cpf", cl_cpf_cnpj);
                                cmdPF.Parameters.AddWithValue("@rg", cl_rg);
                                cmdPF.Parameters.AddWithValue("@data_nascimento", cl_data_nascimento);
                                cmdPF.Parameters.AddWithValue("@genero", genero);

                                await cmdPF.ExecuteNonQueryAsync();
                            }
                        } else if (tipoCliente == "Pessoa Jurídica") {
                            string sqlPJ = "INSERT INTO cliente_pj (id_cliente, cnpj, razao_social) " +
                                           "VALUES (@id_cliente, @cnpj, @razao_social)";

                            using (var cmdPJ = new MySqlCommand(sqlPJ, conn, transaction)) {
                                cmdPJ.Parameters.AddWithValue("@id_cliente", idCliente);
                                cmdPJ.Parameters.AddWithValue("@cnpj", cl_cpf_cnpj);
                                cmdPJ.Parameters.AddWithValue("@razao_social", cl_nome);

                                await cmdPJ.ExecuteNonQueryAsync();
                            }
                        }

                        // Confirmar a transação
                        await transaction.CommitAsync();
                    } catch (Exception) {
                        // Em caso de erro, reverter a transação
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
            return true;
        } catch (Exception ex) {
            // Logar o erro ou exibir uma mensagem de erro
            Console.WriteLine("Erro: " + ex.Message);
            return false;
        }
    }






    public async Task<bool> VerificarUsuarioAsync(string nomeUsuario, string senha) {
        try {
            using (var conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();

                string sql = "SELECT COUNT(*) FROM usuario WHERE nome_usuario = @nome_usuario AND senha_usuario = @senha";
                using (var cmd = new MySqlCommand(sql, conn)) {
                    cmd.Parameters.AddWithValue("@nome_usuario", nomeUsuario);
                    cmd.Parameters.AddWithValue("@senha", senha);

                    var result = await cmd.ExecuteScalarAsync();
                    return Convert.ToInt32(result) > 0;
                }
            }
        } catch (Exception ex) {
            // Logar o erro ou exibir uma mensagem de erro
            Console.WriteLine("Erro: " + ex.Message);
            return false;
        }
    }
}