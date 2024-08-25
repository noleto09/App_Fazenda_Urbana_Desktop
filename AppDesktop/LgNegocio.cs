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


    public async Task<bool> InserirClienteAsync(string cl_Id, string tipoCliente, string cl_nome, string cl_cpf_cnpj, string cl_rg, string cl_data_nascimento, string genero, string Ed_cliente_rua, string Ed_cliente_numero,
         string Ed_cliente_complemento, string Ed_cliente_cep, string Ed_cliente_bairro, string Ed_cliente_estado, string Ed_cliente_cidade) {
        try {
            using (var conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();

                string sql = "INSERT INTO cliente (Id_cliente ,tipo_cliente, nomeCliente, cpf_cnpj, rg_cliente, data_nascimento, genero_cliente, rua, numero, complementar, CEP, bairro, Estado, cidade) " +
                    "VALUES (@id_cliente, @tipocliente, @nome, @cpf_cnpj, @cl_rg, @data_nascimento, @genero, @ed_rua, @ed_numero, @ed_complementar, @ed_cep, @ed_bairro, @ed_estado, @ed_cidade)";
                
                using (var cmd = new MySqlCommand(sql, conn)) {
                    cmd.Parameters.AddWithValue("@id_cliente", cl_Id);
                    cmd.Parameters.AddWithValue("@tipocliente", tipoCliente);
                    cmd.Parameters.AddWithValue("@nome", cl_nome);
                    cmd.Parameters.AddWithValue("@cpf_cnpj", cl_cpf_cnpj);
                    cmd.Parameters.AddWithValue("@cl_rg", cl_rg);
                    cmd.Parameters.AddWithValue("@data_nascimento", cl_data_nascimento);
                    cmd.Parameters.AddWithValue("@genero", genero);
                    cmd.Parameters.AddWithValue("@ed_rua", Ed_cliente_rua);
                    cmd.Parameters.AddWithValue("@ed_numero", Ed_cliente_numero);
                    cmd.Parameters.AddWithValue("@ed_complementar", Ed_cliente_complemento);
                    cmd.Parameters.AddWithValue("@ed_cep", Ed_cliente_cep);
                    cmd.Parameters.AddWithValue("@ed_bairro", Ed_cliente_bairro);
                    cmd.Parameters.AddWithValue("@ed_estado", Ed_cliente_estado);
                    cmd.Parameters.AddWithValue("@ed_cidade", Ed_cliente_cidade);

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