using MVC_1.Models;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace MVC_1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            List<Dipendenti> dipendenti = new List<Dipendenti>();

            try
            {
                conn.Open();
                string query = "SELECT * FROM DatiDipendenti";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    Dipendenti dipendente = new Dipendenti();
                    dipendente.IdDipendente = reader.GetInt32(0);
                    dipendente.Nome = reader.GetString(1);
                    dipendente.Cognome = reader.GetString(2);
                    dipendente.CF = reader.GetString(3);
                    dipendente.Indirizzo = reader.GetString(4);
                    dipendente.Coniugato = reader.GetBoolean(5);
                    dipendente.NumeroFigli = reader.GetInt32(6);
                    dipendente.Mansione = reader.GetString(7);
                    //dipendente.IDPagamento = reader.GetInt32(8);
                    dipendenti.Add(dipendente);

                }

            }
            catch (SqlException e)
            {
                ViewBag.Message = e.Message;
            }
            finally
            {
                conn.Close();
            }

            return View(dipendenti);
        }
        public ActionResult CreateDipendenti() { return View(); }

        [HttpPost]
        public ActionResult CreateDipendenti(Dipendenti dipendente)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "INSERT INTO DatiDipendenti (Nome, Cognome, CF, Indirizzo, Coniugato, NumeroFigli, Mansione) VALUES (@Nome, @Cognome, @CF, @Indirizzo, @Coniugato, @NumeroFigli, @Mansione)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Nome", dipendente.Nome);
                cmd.Parameters.AddWithValue("@Cognome", dipendente.Cognome);
                cmd.Parameters.AddWithValue("@CF", dipendente.CF);
                cmd.Parameters.AddWithValue("@Indirizzo", dipendente.Indirizzo);
                cmd.Parameters.AddWithValue("@Coniugato", dipendente.Coniugato);
                cmd.Parameters.AddWithValue("@NumeroFigli", dipendente.NumeroFigli);
                cmd.Parameters.AddWithValue("@Mansione", dipendente.Mansione);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                ViewBag.Message = e.Message;
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Index");
        }

        public ActionResult CreatePagamenti() { return View(); }

        [HttpPost]
        public ActionResult CreatePagamenti(Pagamenti pagamento)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
                string query = "INSERT INTO Pagamenti (PeriodoPagamento, Ammontare, Stipendio, Acconto) VALUES (@PeriodoPagamento, @Ammontare, @Stipendio, @Acconto)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@PeriodoPagamento", pagamento.PeriodoPagamento);
                cmd.Parameters.AddWithValue("@Ammontare", pagamento.Ammontare);
                cmd.Parameters.AddWithValue("@Stipendio", pagamento.Stipendio);
                cmd.Parameters.AddWithValue("@Acconto", pagamento.Acconto);
                cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                ViewBag.Message = e.Message;
            }
            finally
            {
                conn.Close();
            }
            return RedirectToAction("Index");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}