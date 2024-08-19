using System.Threading.Tasks;
using MySql.Data.MySqlClient;

public class DataAccess {
    private readonly string connStr = "server=localhost;user=root;database=agrotech;port=3306;password=AGROtech78@%24";

    public async Task<bool> InserirUsuarioAsync(string nome, string cpf, string nomeUsuario, string senha) {
        try {
            using (var conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();

                string sql = "INSERT INTO cadastro_usuario (nome, cpf, nome_usuario, senha_usuario) VALUES (@nome, @cpf, @nome_usuario, @senha)";
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

    public async Task<bool> InserirClienteAsync(string cl_nome, string cl_cpf_cnpj, string cl_rg, string cl_data_nascimento, string genero) {
        try {
            using (var conn = new MySqlConnection(connStr)) {
                await conn.OpenAsync();

                string sql = "INSERT INTO cliente (nome, cpf_cnpj, rg, data_nascimento, genero) VALUES (@nome, @cpf_cnpj, @rg, @data_nascimento, @genero)";
                using (var cmd = new MySqlCommand(sql, conn)) {
                    cmd.Parameters.AddWithValue("@nome", cl_nome);
                    cmd.Parameters.AddWithValue("@cpf_cnpj", cl_cpf_cnpj);
                    cmd.Parameters.AddWithValue("@rg", cl_rg);
                    cmd.Parameters.AddWithValue("@data_nascimento", cl_data_nascimento);
                    cmd.Parameters.AddWithValue("@genero", genero);  // Passar o gênero

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

                string sql = "SELECT COUNT(*) FROM cadastro_usuario WHERE nome_usuario = @nome_usuario AND senha_usuario = @senha";
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