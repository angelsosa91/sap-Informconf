using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using System.ServiceModel;
using Informconf.ServiceReferencePeople;
using Informconf.ServiceReferenceBusiness;

namespace Informconf
{
    public partial class FmStart : Form
    {
        public FmStart()
        {
            InitializeComponent();
        }

        private void lblTxt_Click(object sender, EventArgs e)
        {

        }

        private void bntStart_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Inicio Aplicacion => " + DateTime.Now);
            this.bntStart.Text = "EJECUTANDO";
            this.bntStart.Enabled = false;
            this.btnStop.Enabled = true;
            //call all methods and services first time
            ExecuteAll();
            //start timer
            timerStart.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Fin Aplicacion => " + DateTime.Now);
            this.bntStart.Text = "INICIAR";
            this.bntStart.Enabled = true;
            this.btnStop.Enabled = false;
            //stop timer
            timerStart.Stop();
        }

        private void FmStart_Load(object sender, EventArgs e)
        {
            //dtg.DataSource = GetClientList();
        }

        private static DataTable GetClientList()
        {
            DataTable dt = new DataTable();
            //database
            string conn = ConfigurationManager.ConnectionStrings["timbo"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM (SELECT TOP 20 idInclusion AS Id, Empresa, Documento, PrimerNombre, NumeroOperacion, FechaOperacion, Moneda, Monto, Plazo, FechaVtoPendiente, Saldo, DiasAtraso, Registro, FechaEnvio, NroRetornoEquifax FROM INCLUSION WHERE Enviado = 'SI' AND Empresa = 'CATHAY' UNION ALL SELECT TOP 20 idExclusion AS Id, Empresa, Documento, PrimerNombre, NumeroOperacion, FechaOperacion, Moneda, Monto, Plazo, FechaVtoPendiente, Saldo, DiasAtraso, Registro, FechaEnvio, NroRetornoEquifax FROM EXCLUSION WHERE Enviado = 'SI' AND Empresa = 'CATHAY' ORDER BY NroRetornoEquifax DESC) AS T ORDER BY NroRetornoEquifax DESC", con))
                {
                    con.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    dt.Load(sqlDataReader);
                    con.Close();
                }
            }
            return dt;
            //throw new NotImplementedException();
        }

        private void ExecuteMorosidadDataFill()
        {
            //database
            string conn = ConfigurationManager.ConnectionStrings["timbo"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                SqlCommand sql_cmnd = new SqlCommand("SP_CargarTablasEquifax", con);
                sql_cmnd.CommandType = CommandType.StoredProcedure;
                //sql_cmnd.Parameters.AddWithValue("@FIRST_NAME", SqlDbType.NVarChar).Value = firstName;
                sql_cmnd.ExecuteNonQuery();
                con.Close();
            }
        }

        private void ExecuteMorosidadInclusion()
        {
            //database
            string conn = ConfigurationManager.ConnectionStrings["timbo"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT idInclusion, Empresa, Documento, PrimerNombre, FechaOperacion, NumeroOperacion, TipoOperacion, CONVERT(float, Monto) as Monto, Moneda, Plazo, FechaVtoPendiente, case FechaUltimoPago when '00/00/0000' then '' else FechaUltimoPago end as FechaUltimoPago, CONVERT(float, Saldo) as Saldo, Registro, DiasAtraso, Enviado, FechaEnvio, TipoPersona FROM INCLUSION WHERE Enviado = 'NO' AND ExcluidoInterno = 'NO' AND Empresa = 'CATHAY'", con))
                {
                    con.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    //dt.Load(sqlDataReader);
                    while (sqlDataReader.Read())
                    {
                        People people = new People();
                        people.idInclusion = sqlDataReader.GetInt32(0);
                        people.Documento = sqlDataReader.GetString(2);
                        people.Persona = 0;
                        people.PrimerNombre = sqlDataReader.GetString(3);
                        people.SegundoNombre = "";
                        people.PrimerApellido = "";
                        people.SegundoApellido = "";
                        people.ApellidoCasada = "";
                        people.FechaOperacion = sqlDataReader.GetString(4);
                        people.NumeroOperacion = sqlDataReader.GetString(5);
                        people.TipoOperacion = sqlDataReader.GetInt32(6);
                        people.Monto = sqlDataReader.GetDouble(7);
                        people.Moneda = sqlDataReader.GetString(8);
                        people.Plazo = sqlDataReader.GetInt32(9);
                        people.FechaVencimiento = sqlDataReader.GetString(10);
                        people.FechaUltimoPago = sqlDataReader.GetString(11);
                        people.Saldo = sqlDataReader.GetDouble(12);
                        people.Registro = sqlDataReader.GetString(13);
                        int TipoPersona = sqlDataReader.GetInt32(17);
                        //condition
                        if (TipoPersona.Equals(1))
                        {
                            //send ws people
                            sendPeopleData(people);
                        } /*else if (TipoPersona.Equals(1))
                        {
                            //send ws business
                            sendBusinessData(people);
                        }*/
                        //fill data
                        dtg.DataSource = GetClientList();
                    }
                    con.Close();
                }
            }
        }

        private void ExecuteMorosidadExclusion()
        {
            //database
            string conn = ConfigurationManager.ConnectionStrings["timbo"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conn))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT idExclusion, Empresa, Documento, PrimerNombre, FechaOperacion, NumeroOperacion, TipoOperacion, CONVERT(float, Monto) as Monto, Moneda, Plazo, FechaVtoPendiente, case FechaUltimoPago when '00/00/0000' then '' else FechaUltimoPago end as FechaUltimoPago, CONVERT(float, Saldo) as Saldo, Registro, DiasAtraso, Enviado, FechaEnvio, TipoPersona FROM EXCLUSION WHERE Enviado = 'NO' AND Empresa = 'CATHAY'", con))
                {
                    con.Open();
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    //dt.Load(sqlDataReader);
                    while (sqlDataReader.Read())
                    {
                        People people = new People();
                        people.idInclusion = sqlDataReader.GetInt32(0);
                        people.Documento = sqlDataReader.GetString(2);
                        people.Persona = 0;
                        people.PrimerNombre = sqlDataReader.GetString(3);
                        people.SegundoNombre = "";
                        people.PrimerApellido = "";
                        people.SegundoApellido = "";
                        people.ApellidoCasada = "";
                        people.FechaOperacion = sqlDataReader.GetString(4);
                        people.NumeroOperacion = sqlDataReader.GetString(5);
                        people.TipoOperacion = sqlDataReader.GetInt32(6);
                        people.Monto = sqlDataReader.GetDouble(7);
                        people.Moneda = sqlDataReader.GetString(8);
                        people.Plazo = sqlDataReader.GetInt32(9);
                        people.FechaVencimiento = sqlDataReader.GetString(10);
                        people.FechaUltimoPago = sqlDataReader.GetString(11);
                        people.Saldo = sqlDataReader.GetDouble(12);
                        people.Registro = sqlDataReader.GetString(13);
                        int TipoPersona = sqlDataReader.GetInt32(17);
                        //condition
                        if (TipoPersona.Equals(1))
                        {
                            //send ws people
                            sendPeopleData(people);
                        } /*else if (TipoPersona.Equals(1))
                        {
                            //send ws business
                            sendBusinessData(people);
                        }*/
                        //fill data
                        dtg.DataSource = GetClientList();
                    }
                    con.Close();
                }
            }
        }

        private static void UpdateInclusion(int numero, string retorno, int idInclusion, string enviado)
        {
            string conn = ConfigurationManager.ConnectionStrings["timbo"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                string sql = @"UPDATE INCLUSION SET [Enviado] = @Enviado, [FechaEnvio] = GETDATE(), [NroRetornoEquifax] = @NroRetornoEquifax, [ComentarioEquifax] = @ComentarioEquifax WHERE [idInclusion] = @idInclusion";

                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("Enviado", enviado);
                command.Parameters.AddWithValue("NroRetornoEquifax", numero);
                command.Parameters.AddWithValue("ComentarioEquifax", retorno);
                command.Parameters.AddWithValue("idInclusion", idInclusion);
                command.ExecuteNonQuery();
                //MessageBox.Show("Datos Actualizados Satisfactoriamente", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                con.Close();
            }
        }

        private static void UpdateExclusion(int numero, string retorno, int idInclusion, string enviado)
        {
            string conn = ConfigurationManager.ConnectionStrings["timbo"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                string sql = @"UPDATE EXCLUSION SET [Enviado] = @Enviado, [FechaEnvio] = GETDATE(), [NroRetornoEquifax] = @NroRetornoEquifax, [ComentarioEquifax] = @ComentarioEquifax WHERE [idExclusion] = @idExclusion";

                SqlCommand command = new SqlCommand(sql, con);
                command.Parameters.AddWithValue("Enviado", enviado);
                command.Parameters.AddWithValue("NroRetornoEquifax", numero);
                command.Parameters.AddWithValue("ComentarioEquifax", retorno);
                command.Parameters.AddWithValue("idExclusion", idInclusion);
                command.ExecuteNonQuery();
                //MessageBox.Show("Datos Actualizados Satisfactoriamente", "Sistema", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                con.Close();
            }
        }

        private static void sendPeopleData(People people)
        {
            //instance http cliente
            PersonasClient client = new PersonasClient();
            //instace MorosidadAltaPersona
            MorosidadAltaPersona morosidadAltaPersona = new MorosidadAltaPersona();
            //set morosidadAltaPersona
            morosidadAltaPersona.Documento = people.Documento;
            morosidadAltaPersona.Persona = people.Persona;
            morosidadAltaPersona.PrimerNombre = people.PrimerNombre;
            morosidadAltaPersona.SegundoNombre = people.SegundoNombre;
            morosidadAltaPersona.PrimerApellido = people.PrimerApellido;            
            morosidadAltaPersona.SegundoApellido = people.SegundoApellido;
            morosidadAltaPersona.ApellidoCasada = people.ApellidoCasada;
            morosidadAltaPersona.FechaOperacion = people.FechaOperacion;
            morosidadAltaPersona.NumeroOperacion = people.NumeroOperacion;
            morosidadAltaPersona.TipoOperacion = people.TipoOperacion;
            morosidadAltaPersona.Monto = people.Monto;
            morosidadAltaPersona.Moneda = people.Moneda;
            morosidadAltaPersona.Plazo = people.Plazo;
            morosidadAltaPersona.FechaVencimiento = people.FechaVencimiento;
            morosidadAltaPersona.FechaUltimoPago = people.FechaUltimoPago;
            morosidadAltaPersona.Saldo = people.Saldo;
            morosidadAltaPersona.Registro = people.Registro;
            //set alta morosidad request
            ServiceReferencePeople.AltaMorosidadesRequest altaMorosidadesRequest = new ServiceReferencePeople.AltaMorosidadesRequest();
            altaMorosidadesRequest.usuario = "CATHAY_WS_24309";
            altaMorosidadesRequest.contrasenia = "N4#aP6_c";
            altaMorosidadesRequest.morosidadAlta = morosidadAltaPersona;
            try
            {
                //set alta morosidad response
                ServiceReferencePeople.AltaMorosidadesResponse altaMorosidadesResponse = client.AltaMorosidades(altaMorosidadesRequest);
                Console.WriteLine("WS Send - ID SAP => " + people.idInclusion);
                Console.WriteLine("Numero Informconf => " + altaMorosidadesResponse.AltaMorosidadesResult.Numero);
                Console.WriteLine("Retorno Informconf => " + altaMorosidadesResponse.AltaMorosidadesResult.Retorno);
                //fill data
                int numero = altaMorosidadesResponse.AltaMorosidadesResult.Numero;
                string retorno = altaMorosidadesResponse.AltaMorosidadesResult.Retorno;
                int idInclusion = people.idInclusion;
                //update table inclusion
                UpdateInclusion(numero, retorno, idInclusion, "SI");
            }
            catch (FaultException<ServiceReferencePeople.ErrorData> e)
            {
                //fill data
                int numero = e.Detail.CodigoError;
                string retorno = e.Detail.DescripcionError;
                int idInclusion = people.idInclusion;
                //update table inclusion
                UpdateInclusion(numero, retorno, idInclusion, "NO");
                //error show
                throw new Exception(e.Detail.DescripcionError);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            //throw new NotImplementedException();
        }

        private static void sendBusinessData(People people)
        {
            //instance http cliente
            EmpresasClient client = new EmpresasClient();
            //instace MorosidadAltaPersona
            MorosidadAltaEmpresa morosidadAltaEmpresa = new MorosidadAltaEmpresa();
            //set morosidadAltaPersona
            morosidadAltaEmpresa.Ruc = people.Documento;
            morosidadAltaEmpresa.Empresa = people.Persona;
            morosidadAltaEmpresa.RazonSocial = people.PrimerNombre;
            morosidadAltaEmpresa.FechaOperacion = people.FechaOperacion;
            morosidadAltaEmpresa.NumeroOperacion = people.NumeroOperacion;
            morosidadAltaEmpresa.TipoOperacion = people.TipoOperacion;
            morosidadAltaEmpresa.Monto = people.Monto;
            morosidadAltaEmpresa.Moneda = people.Moneda;
            morosidadAltaEmpresa.Plazo = people.Plazo;
            morosidadAltaEmpresa.FechaVencimiento = people.FechaVencimiento;
            morosidadAltaEmpresa.FechaUltimoPago = people.FechaUltimoPago;
            morosidadAltaEmpresa.Saldo = people.Saldo;
            morosidadAltaEmpresa.Registro = people.Registro;
            //set alta morosidad request
            ServiceReferenceBusiness.AltaMorosidadesRequest altaMorosidadesRequest = new ServiceReferenceBusiness.AltaMorosidadesRequest();
            altaMorosidadesRequest.usuario = "CATHAY_WS_24309";
            altaMorosidadesRequest.contrasenia = "N4#aP6_c";
            altaMorosidadesRequest.xoMorosidadAlta = morosidadAltaEmpresa;
            try
            {
                //set alta morosidad response
                ServiceReferenceBusiness.AltaMorosidadesResponse altaMorosidadesResponse = client.AltaMorosidades(altaMorosidadesRequest);
                Console.WriteLine("WS Send - ID SAP => " + people.idInclusion);
                Console.WriteLine("Numero Informconf => " + altaMorosidadesResponse.AltaMorosidadesResult.Numero);
                Console.WriteLine("Retorno Informconf => " + altaMorosidadesResponse.AltaMorosidadesResult.Retorno);
                //fill data
                int numero = altaMorosidadesResponse.AltaMorosidadesResult.Numero;
                string retorno = altaMorosidadesResponse.AltaMorosidadesResult.Retorno;
                int idInclusion = people.idInclusion;
                //update table exclusion
                UpdateExclusion(numero, retorno, idInclusion, "SI");
            }
            catch (FaultException<ServiceReferenceBusiness.ErrorData> e)
            {
                //fill data
                int numero = e.Detail.CodigoError;
                string retorno = e.Detail.DescripcionError;
                int idInclusion = people.idInclusion;
                //update table exclusion
                UpdateExclusion(numero, retorno, idInclusion, "NO");
                //error show
                throw new Exception(e.Detail.DescripcionError);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            //throw new NotImplementedException();
        }

        public class People
        {
            public int idInclusion { get; set; }
            public string Documento { get; set; }
            public int Persona { get; set; }
            public string PrimerNombre { get; set; }
            public string SegundoNombre { get; set; }
            public string PrimerApellido { get; set; }
            public string SegundoApellido { get; set; }
            public string ApellidoCasada { get; set; }
            public string FechaOperacion { get; set; }
            public string NumeroOperacion { get; set; }
            public int TipoOperacion { get; set; }
            public double Monto { get; set; }
            public string Moneda { get; set; }
            public int Plazo { get; set; }
            public string FechaVencimiento { get; set; }
            public string FechaUltimoPago { get; set; }
            public double Saldo { get; set; }
            public string Registro { get; set; }
        }

        private void ExecuteAll()
        {
            //ejecutamos procedimiento para carga de registros
            //en tablas de inclusio y exclusion
            Console.WriteLine("Inicio procedimiento => " + DateTime.Now);
            ExecuteMorosidadDataFill();
            Console.WriteLine("Fin procedimiento => " + DateTime.Now);
            //ejecutamos la inclusion a registros de informconf
            Console.WriteLine("Inicio ws inclusion => " + DateTime.Now);
            ExecuteMorosidadInclusion();
            Console.WriteLine("Fin ws inclusion => " + DateTime.Now);
            //ejecutamos la exclusion a registros de informconf
            Console.WriteLine("Inicio ws exclusion => " + DateTime.Now);
            ExecuteMorosidadExclusion();
            Console.WriteLine("Fin ws exclusion => " + DateTime.Now);
        }

        private void timerStart_Tick(object sender, EventArgs e)
        {
            //call all methods and services
            ExecuteAll();
        }
    }
}