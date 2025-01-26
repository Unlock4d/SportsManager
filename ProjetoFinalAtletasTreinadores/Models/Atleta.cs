using Microsoft.Data.SqlClient;
using Swashbuckle.AspNetCore.Annotations;
using System.Data;

namespace ProjetoFinalAtletasTreinadores.Models
{
    public class AtletaDto
    {
        public string Nome { get; set; }
        public int Idade { get; set; }
        public int Modalidade { get; set; }
        public int emAtividade { get; set; }
        public string Descricao { get; set; }
        public float Peso { get; set; }
        public float Altura { get; set; }
    }
    public class Atleta : Contacto
    {

        private string _conexao = "";
        protected int _idAtleta;
        private float _peso;
        private float _altura;

        public int IdAtleta
        {
            get { return _idAtleta; }
            set
            {
                if (value != 0)
                {
                    _idAtleta = value;
                }
            }
        }

        public float Peso
        {
            get { return _peso; }
            set
            {
                if (value != 0)
                    _peso = value;
            }
        }

        public float Altura
        {
            get { return _altura; }
            set
            {
                if (value != 0)
                    _altura = value;
            }
        }

        public Atleta(string con) : base(con)
        {
            _conexao = con;
            Peso = 0;
            Altura = 0;
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
                    parmOut.ParameterName = "@idAtletaOut";
                    parmOut.SqlDbType = SqlDbType.Int;
                    parmOut.Value = 0;
                    //----
                    sqlC.Connection = new SqlConnection(_conexao);
                    sqlC.Connection.Open();
                    sqlC.CommandType = CommandType.StoredProcedure;
                    sqlC.CommandText = "QAtletaCriar";
                    sqlC.Parameters.Add("@idContacto", SqlDbType.Int).Value = retIdContacto;
                    sqlC.Parameters.Add("@Peso", SqlDbType.Float).Value = Peso;
                    sqlC.Parameters.Add("@Altura", SqlDbType.Int).Value = Altura;
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
            }
            return retID;
        }

        public int Buscar(int id)
        {
            DataTable dtC = new DataTable();
            SqlDataAdapter SqlA = new SqlDataAdapter();
            int retID = 0;
            try
            {
                SqlA.SelectCommand = new SqlCommand();
                SqlA.SelectCommand.Connection = new SqlConnection(_conexao);
                SqlA.SelectCommand.Connection.Open();
                SqlA.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlA.SelectCommand.CommandText = "QAtletaBuscarPorId";
                SqlA.SelectCommand.Parameters.AddWithValue("@idAtleta", id);
                //---
                SqlA.Fill(dtC);
                //--- 
                SqlA.SelectCommand.Connection.Close();
                SqlA.SelectCommand.Connection.Dispose();
            }
            catch (Exception ex)
            {
                dtC = null;
            }
            if (dtC != null)
            {
                foreach (DataRow r in dtC.Rows)
                {
                    Nome = r["nome"].ToString();
                    Idade = Convert.ToInt32(r["Idade"]);
                    Modalidade = Convert.ToInt32(r["Modalidade"]);
                    emAtividade = Convert.ToInt32(r["emAtividade"]);
                    Descricao = r["Descricao"].ToString();
                    Peso = (float)Convert.ToDouble(r["Peso"]);
                    Altura = (float)Convert.ToDouble(r["Altura"]);

                }
                retID = id;
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
                parmOut.ParameterName = "@idAtletaOut";
                parmOut.SqlDbType = SqlDbType.Int;
                parmOut.Value = 0;
                //----
                sqlC.Connection = new SqlConnection(_conexao);
                sqlC.Connection.Open();
                sqlC.CommandType = CommandType.StoredProcedure;
                sqlC.CommandText = "QAtletaUpdate";
                sqlC.Parameters.Add("@idContacto", SqlDbType.Int).Value = base.IdContacto;
                sqlC.Parameters.Add("@idAtleta", SqlDbType.Int).Value = IdAtleta;
                sqlC.Parameters.Add("@Nome", SqlDbType.VarChar, 50).Value = base.Nome;
                sqlC.Parameters.Add("@Idade", SqlDbType.Int).Value = base.Idade;
                sqlC.Parameters.Add("@Modalidade", SqlDbType.Int).Value = base.Modalidade;
                sqlC.Parameters.Add("@emAtividade", SqlDbType.Int).Value = base.emAtividade;
                sqlC.Parameters.Add("@Descricao", SqlDbType.Text).Value = base.Descricao;
                sqlC.Parameters.Add("@Peso", SqlDbType.Float).Value = Peso;
                sqlC.Parameters.Add("@Altura", SqlDbType.Float).Value = Altura;
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

        public int GetContactIdByAtleteID()
        {
            DataTable dtC = new DataTable();
            SqlDataAdapter SqlA = new SqlDataAdapter();
            int retID = 0;
            try
            {
                SqlA.SelectCommand = new SqlCommand();
                SqlA.SelectCommand.Connection = new SqlConnection(_conexao);
                SqlA.SelectCommand.Connection.Open();
                SqlA.SelectCommand.CommandType = CommandType.StoredProcedure;
                SqlA.SelectCommand.CommandText = "QContactIdByAtleteID";
                SqlA.SelectCommand.Parameters.AddWithValue("@idAtleta", IdAtleta);
                //---
                SqlA.Fill(dtC);
                //--- 
                SqlA.SelectCommand.Connection.Close();
                SqlA.SelectCommand.Connection.Dispose();
            }
            catch (Exception ex)
            {
                dtC = null;
            }
            if (dtC != null)
            {
                foreach (DataRow r in dtC.Rows)
                {
                    base.IdContacto = Convert.ToInt32(r["idContacto"]);
                }
                retID = base.IdContacto;
            }
            return retID;
        }
    }
}
