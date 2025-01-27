using Microsoft.Data.SqlClient;
using System.Data;

namespace ProjetoFinalAtletasTreinadores.Models
{
    public enum StatusAtividade
    {
        Reformado = 0,
        Ativo = 1
    }
    public abstract class Contacto
    {
        private string _conexao = "";

        protected int _idContacto;
        private string _nome;
        private int _idade;
        private int _modalidade;
        private int _ematividade;
        private string _descricao;

        public int IdContacto
        {
            get { return _idContacto; }
            set
            {
                if (value != 0)
                    _idContacto = value;
            }
        }

        public string Nome
        {
            get { return _nome; }
            set
            {
                _nome = value;
                if (_nome.Length > 50) _nome = _nome.Substring(0, 50);
            }
        }

        public int Idade
        {
            get { return _idade; }
            set
            {
                if (value != 0)
                    _idade = value;
            }
        }
        public int Modalidade
        {
            get { return _modalidade; }
            set
            {
                if (value != 0)
                    _modalidade = value;
            }
        }
        public int emAtividade
        {
            get { return _ematividade; }
            set
            {
                if (value != 0)
                    _ematividade = value;
            }
        }
        public string Descricao
        {
            get { return _descricao; }
            set
            {
                if (value != string.Empty)
                    _descricao = value;
            }
        }

        public Contacto(string conexao)
        {
            _conexao = conexao;
            Nome = "";
            Idade = 0;
            Modalidade = 0;
            emAtividade = 0;
            Descricao = "";
        }

        public int Criar()
        {
            int retID = 0;
            try
            {
                SqlCommand sqlC = new SqlCommand();
                SqlParameter parmOut = new SqlParameter();
                parmOut.Direction = ParameterDirection.Output;
                parmOut.ParameterName = "@idContactoOut";
                parmOut.SqlDbType = SqlDbType.Int;
                parmOut.Value = 0;
                //----
                sqlC.Connection = new SqlConnection(_conexao);
                sqlC.Connection.Open();
                sqlC.CommandType = CommandType.StoredProcedure;
                sqlC.CommandText = "QContactoCriar";
                sqlC.Parameters.Add("@Nome", SqlDbType.VarChar, 50).Value = Nome;
                sqlC.Parameters.Add("@Idade", SqlDbType.Int).Value = Idade;
                sqlC.Parameters.Add("@Modalidade", SqlDbType.Int).Value = Modalidade;
                sqlC.Parameters.Add("@emAtividade", SqlDbType.Int).Value = emAtividade;
                sqlC.Parameters.Add("@Descricao", SqlDbType.Text).Value = Descricao;
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
            return retID;
        }

    }
}
