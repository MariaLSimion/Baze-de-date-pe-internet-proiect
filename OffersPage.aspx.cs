using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SimionMariaBDIProiectTablou
{
    public partial class OffersPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownListSelectClient.Items.Add(new ListItem("- client - ", "-1"));
                PopulateDropDownList();
            }
        }

        protected void PopulateDropDownList()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Paintings.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GetClients", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Iterăm prin fiecare rând citit din baza de date
                        while (reader.Read())
                        {
                            // Obținem id-ul clientului și numele complet concatenat din rândul curent
                            int idClient = reader.GetInt32(reader.GetOrdinal("IdClient"));
                            string fullName = reader.GetString(reader.GetOrdinal("FullName"));

                            // Adăugăm un nou element în dropdown list cu id-ul și numele clientului
                            DropDownListSelectClient.Items.Add(new ListItem(fullName, idClient.ToString()));
                        }
                    }
                }
            }


        }

        protected void DropDownListSelectClient_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (DropDownListSelectClient.SelectedValue != "-1")
            {


                string clientFullName = DropDownListSelectClient.SelectedItem.Text;
                Console.WriteLine(clientFullName);

                clientFullName.Trim();
                int spaceIndex = clientFullName.IndexOf(' ');

                string clientSurname = clientFullName.Substring(0, spaceIndex);
                string clientGivenName = clientFullName.Substring(spaceIndex + 1);

                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Paintings.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("GetOffersByClientName", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ClientSurname", clientSurname);
                        command.Parameters.AddWithValue("@ClientGivenName", clientGivenName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(reader);
                            GridViewOffersByClientName.DataSource = dt;
                            GridViewOffersByClientName.DataBind();
                        }
                    }
                }

            }
            else
            {
                GridViewOffersByClientName.DataSource = null;
                GridViewOffersByClientName.DataBind();
            }
        }

        protected void ButtonDisplayMaxOffersWinner_Click(object sender, EventArgs e)
        {
            LoadClientWithMaxWinningOffers();


        }
    private void LoadClientWithMaxWinningOffers()
    {
     
            //daca exista in cache 
        if (Cache["ClientWithMaxWinningOffers"] != null)
        {
            LabelMaxOffersWinner.Text = Cache["ClientWithMaxWinningOffers"].ToString();
        }
        else
        {
            // dacă nu exista in cache
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Paintings.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("GetClientWithMaxWinningOffers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        string clientName = result.ToString();
                        LabelMaxOffersWinner.Text = clientName;

                        // Salvare rezultat în cache pentru utilizari ulterioare
                        Cache["ClientWithMaxWinningOffers"] = clientName;
                    }
                }
            }
        } }
    }
}


