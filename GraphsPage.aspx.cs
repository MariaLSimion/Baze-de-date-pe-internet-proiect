using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimionMariaBDIProiectTablou
{
    public partial class GraphsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Obțineți datele din sursa de date și populați graficul
                PopulateChartWithArtTechniquesData();
                PopulateChartWithTotalOffersByTechnique();
                PopulateChartWithTotalWinningOffersByClosedAuction();
            }
        }

        //grafic numar lucrari de arta pe fiecare tehnica

        private void PopulateChartWithArtTechniquesData()
        {
            
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Paintings.mdf;Integrated Security=True;Connect Timeout=30";

            
            string query = @"
        SELECT Technique, COUNT(*) AS NumberOfArtworks
        FROM Paintings
        GROUP BY Technique
    ";

          
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                
                connection.Open();

                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                   
                    Chart1.DataSource = dt;

                   
                    Chart1.Series["ArtTechniques"].XValueMember = "Technique";
                    Chart1.Series["ArtTechniques"].YValueMembers = "NumberOfArtworks";

                    
                    Chart1.DataBind();
                }
            }
        }

        //grafic in functie de numar de oferte pe tehnica lucrarii
        private void PopulateChartWithTotalOffersByTechnique()
        {
            
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Paintings.mdf;Integrated Security=True;Connect Timeout=30";

            
            string query = @"
                SELECT Technique, COUNT(*) AS TotalOffers
                FROM Paintings
                INNER JOIN Oferte ON Paintings.IdLucrare = Oferte.IdLucrare
                GROUP BY Technique
            ";

           
            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                
                connection.Open();

                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    
                    Chart2.DataSource = dt;

                    
                    Chart2.Series["TotalOffers"].XValueMember = "Technique";
                    Chart2.Series["TotalOffers"].YValueMembers = "TotalOffers";

                   
                    Chart2.DataBind();
                }
            }
        }

        //avem 3 licitatii inchise. avem lucrari vandute. grafic pe licitatie in functie de suma totala castigata de galerie 
        //pe licitatiile inchise

        private void PopulateChartWithTotalWinningOffersByClosedAuction()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Paintings.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand("GetTotalWinningOffersByClosedAuction", connection))
            {
                command.CommandType = CommandType.StoredProcedure;

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);

                    Chart3.DataSource = dt;
                    Chart3.Series["TotalWinningOffers"].XValueMember = "IdAuction"; // ID-ul licitației
                    Chart3.Series["TotalWinningOffers"].YValueMembers = "TotalWinningAmount"; // Suma totală a ofertelor câștigătoare
                    Chart3.DataBind();
                }
            }
        }
    }
}