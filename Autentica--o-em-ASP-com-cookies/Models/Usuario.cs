using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace AppAutenticacao.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; } 
        public string UsuNome { get; set; }
        
        public string Login { get; set; }
        
        public string Senha { get; set; }

        MySqlConnection conexao = new MySqlConnection(ConfigurationManager.ConnectionStrings["conexao"].ConnectionString);
        MySqlCommand comand = new MySqlCommand();

        public void InsertUsuario (Usuario usuario)
        {
            conexao.Open();
            comand.CommandText = "call InsertUsuario(@UsuNome, @Login, @Senha);" ;
            comand.Parameters.Add("@UsuNome", MySqlDbType.VarChar).Value = usuario.UsuNome; 
            comand.Parameters.Add("@Login", MySqlDbType.VarChar).Value = usuario.Login;
            comand.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = usuario.Senha;
            comand.Connection = conexao;
            comand.ExecuteNonQuery();
            conexao.Close();
        }

        public string SelectUsuLogin (string Login)
        {
            conexao.Open();
            comand.CommandText = "call SelectLogin(@Login)";
            comand.Parameters.Add("@Login", MySqlDbType.VarChar).Value = Login;
            comand.Connection = conexao;
            string login = (string)comand.ExecuteScalar();
            conexao.Close();
            if (login == null)
            {
                login = "";
            }
            return login;
        }
        
        public Usuario SelectUsuario(string vLogin)
        {
            conexao.Open();
            comand.CommandText = "call SelectUsuario(@Login);";
            comand.Parameters.Add("@Login", MySqlDbType.VarChar).Value= vLogin;
            comand.Connection = conexao;
            var readUsuario = comand.ExecuteReader();
            var TempUsuario = new Usuario();

            if (readUsuario.Read())
            {
                TempUsuario.UsuarioID = int.Parse(readUsuario["UsuarioID"].ToString());
                TempUsuario.UsuNome = readUsuario["UsuNome"].ToString();
                TempUsuario.Login = readUsuario["Login"].ToString();
                TempUsuario.Senha = readUsuario["Senha"].ToString();

            };
            readUsuario.Close();
            conexao.Close();
            return TempUsuario;
        }

        public void UpdateSenha(Usuario usuario)
        {
            conexao.Open();
            comand.CommandText = "call UpdateSenha(@Login, @Senha);";
            comand.Parameters.Add("@Login", MySqlDbType.VarChar).Value = usuario.Login;
            comand.Parameters.Add("@Senha", MySqlDbType.VarChar).Value = usuario.Senha;
            comand.Connection = conexao;
            comand.ExecuteNonQuery();
            conexao.Close();
        }
    }
}