using Microsoft.Data.SqlClient;
using System.Data;

namespace ProjetoFinalAtletasTreinadores.Models
{
    public class Treinador : Contacto
    {

        private string _conexao = "";
        protected int _idTreinador;
        private int _anos_de_experiencia;
        private int _nivel_de_certificao;

        public int IdTreinador
        {
            get { return _idTreinador; }
            set
            {
                if (value != 0)
                {
                    _idTreinador = value;
                }
            }
        }

        public int Anos_de_experiencia
        {
            get { return _anos_de_experiencia; }
            set
            {
                if (value != 0)
                    _anos_de_experiencia = value;
            }
        }

        public int Nivel_de_certificao
        {
            get { return _nivel_de_certificao; }
            set
            {
                if (value != 0)
                    _nivel_de_certificao = value;
            }
        }
        public Treinador(string con) : base(con)
        {
            _conexao = con;
            Anos_de_experiencia = 0;
            Nivel_de_certificao = 0;
        }

        public int Criar()
        {
            int retID = 0;
            int retIdContacto = base.Criar();
            if (retIdContacto > 0)
            {
                try
                {
                    SqlCommand sqlC = new SqlCommand();
                    SqlParameter parmOut = new SqlParameter();
                    parmOut.Direction = ParameterDirection.Output;
                    parmOut.ParameterName = "@idTreinadorOut";
                    parmOut.SqlDbType = SqlDbType.Int;
                    parmOut.Value = 0;
                    //----
                    sqlC.Connection = new SqlConnection(_conexao);
                    sqlC.Connection.Open();
                    sqlC.CommandType = CommandType.StoredProcedure;
                    sqlC.CommandText = "QTreinadorCriar";
                    sqlC.Parameters.Add("@idContacto", SqlDbType.Int).Value = retIdContacto;
                    sqlC.Parameters.Add("@Anos_de_experiencia", SqlDbType.Int).Value = Anos_de_experiencia;
                    sqlC.Parameters.Add("@Nivel_de_certificao", SqlDbType.Int).Value = Nivel_de_certificao;
                    sqlC.Parameters.Add(parmOut);

                    sqlC.ExecuteNonQuery();

                    retID = Convert.ToInt32(parmOut.Value);
                    sqlC.Connection.Close();
                    sqlC.Connection.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERRO: " + ex.Message);   //Nota--> Escrever em log de erros e não na console
                    retID = 0;
                }
            }
            return retID;
        }

        public int Atualizar()
        {        
            int retID = 0;

            try
            {
                SqlCommand sqlC = new SqlCommand();
                SqlParameter parmOut = new SqlParameter();
                parmOut.Direction = ParameterDirection.Output;
                parmOut.ParameterName = "@idTreinadorOut";
                parmOut.SqlDbType = SqlDbType.Int;
                parmOut.Value = 0;
                //----
                sqlC.Connection = new SqlConnection(_conexao);
                sqlC.Connection.Open();
                sqlC.CommandType = CommandType.StoredProcedure;
                sqlC.CommandText = "QTreinadorUpdate";
                sqlC.Parameters.Add("@idContacto", SqlDbType.Int).Value = base.IdContacto;
                sqlC.Parameters.Add("@idTreinador", SqlDbType.Int).Value = IdTreinador;
                sqlC.Parameters.Add("@Nome", SqlDbType.VarChar, 50).Value = base.Nome;
                sqlC.Parameters.Add("@Idade", SqlDbType.Int).Value = base.Idade;
                sqlC.Parameters.Add("@Modalidade", SqlDbType.Int).Value = base.Modalidade;
                sqlC.Parameters.Add("@emAtividade", SqlDbType.Int).Value = base.emAtividade;
                sqlC.Parameters.Add("@Descricao", SqlDbType.Text).Value = base.Descricao;
                sqlC.Parameters.Add("@Anos_de_experiencia", SqlDbType.Int).Value = Anos_de_experiencia;
                sqlC.Parameters.Add("@Nivel_de_certificao", SqlDbType.Int).Value = Nivel_de_certificao;
                sqlC.Parameters.Add(parmOut);
                sqlC.ExecuteNonQuery();

                retID = Convert.ToInt32(parmOut.Value);
                sqlC.Connection.Close();
                sqlC.Connection.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERRO: " + ex.Message);
                retID = 0;
            }
            return retID;
        }
    }
}
