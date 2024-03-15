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
    public partial class AuctionsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Afisare Licitatii deschise
        protected void ButtonOpenAuc_Click(object sender, EventArgs e)
        {
           
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Paintings.mdf;Integrated Security=True;Connect Timeout=30";

            string query = @"SELECT A.IdAuction AS IdAuction, 
       P.IdLucrare, 
       P.Title, 
       P.Artist, 
       P.Year, 
       P.Technique, 
       P.Dimensions, 
       P.ValoareMinima, 
       P.Disponibilitate 
       
FROM Auctions A 
INNER JOIN PaintingsAuctions PA ON A.IdAuction = PA.IdAuction
INNER JOIN Paintings P ON PA.IdLucrare = P.IdLucrare
WHERE A.Status = 'Open'";

            SqlDataAdapter adapter = new SqlDataAdapter(query, connectionString);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            GridViewOpenAuc.DataSource = dt;
            GridViewOpenAuc.DataBind();
        }
        //afisare licitatii viitoare
        protected void ButtonCommingSoonAuc_Click(object sender, EventArgs e)
        {
            
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Paintings.mdf;Integrated Security=True;Connect Timeout=30";

            string query = @"SELECT A.IdAuction AS IdAuction, 
       P.IdLucrare, 
       P.Title, 
       P.Artist, 
       P.Year, 
       P.Technique, 
       P.Dimensions, 
       P.ValoareMinima 
       
FROM Auctions A 
INNER JOIN PaintingsAuctions PA ON A.IdAuction = PA.IdAuction
INNER JOIN Paintings P ON PA.IdLucrare = P.IdLucrare
WHERE A.Status = 'Comming soon'";

            SqlDataAdapter adapter = new SqlDataAdapter(query, connectionString);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            GridViewCommingSoonAuc.DataSource = dt;
            GridViewCommingSoonAuc.DataBind();
        }
        //afisare detalii licitatie cu id introdus
        protected void ButtonDisplayWantedAuction_Click(object sender, EventArgs e)
        {
            
            int auctionId;
            if (int.TryParse(TextBoxIdWantedAuction.Text, out auctionId))
            {
                
                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Paintings.mdf;Integrated Security=True;Connect Timeout=30";
                string query = @"SELECT A.IdAuction AS AuctionId, 
                               P.IdLucrare, 
                               P.Title, 
                               P.Artist, 
                               P.Year, 
                               P.Technique, 
                               P.Dimensions, 
                               P.ValoareMinima, 
                               P.Disponibilitate 
                                
                        FROM Auctions A 
                        INNER JOIN PaintingsAuctions PA ON A.IdAuction = PA.IdAuction
                        INNER JOIN Paintings P ON PA.IdLucrare = P.IdLucrare
                        WHERE A.IdAuction = @AuctionId";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        
                        command.Parameters.AddWithValue("@AuctionId", auctionId);

                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {

                            GridViewWantedAuction.DataSource = reader;
                            GridViewWantedAuction.DataBind();
                        }
                        else
                        {
                            LabelError.Text = "!Auction not found.!";
                          
                            GridViewWantedAuction.DataSource = null;
                            GridViewWantedAuction.DataBind();
                        }


                        reader.Close();
                        connection.Close();
                    }
                }
            }
            else
            {
            
                LabelError.Text = "Please enter a valid auction ID.";
            }
        }
        //schimbare status disponibilitate a unei lucrari
        protected void ButtonChangeStatus_Click(object sender, EventArgs e)
        {
            
            if (int.TryParse(TextBoxIdPainting.Text, out int IdLucrare))
            {

                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Paintings.mdf;Integrated Security=True;Connect Timeout=30";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    
                    SqlCommand command = new SqlCommand("ChangePaintingAvailability", connection);
                    command.CommandType = CommandType.StoredProcedure;

                    
                    command.Parameters.AddWithValue("@IdLucrare", IdLucrare);

                    try
                    {
                        
                        connection.Open();
                        
                       
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            LabelStatusError.Text = "Painitng id not found";
                        }
                        else
                        {
                            LabelStatusError.Text = "Availability toggled successfully!";
                        }
                        
                        
                    }
                    catch (Exception ex)
                    {
                        
                        LabelStatusError.Text = "Error: " + ex.Message;
                    }
                }
            }
            else
            {
                
                LabelStatusError.Text = "Please enter a valid painting ID.";
            }
        }

        //TO DO: Nu merge cum trebuie review this! 
        //Cand toate lucrarile dintr-o licitatie sunt cu disponibilitate "Nu" statusul licitatiei sa se schimbe in Closed
        //de asemenea daca pe o licitatie inchisa se schimba statusul unei lucrari din Nu in Da sa se redeschida licitatia
        protected void ButtonRefreshAuctionStatus_Click(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Paintings.mdf;Integrated Security=True;Connect Timeout=30";
            string commandText = "UpdateAuctionStatusBasedOnPaintingAvailability";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using(SqlCommand command= new SqlCommand(commandText, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.ExecuteNonQuery();
                        connection.Close();

                    }
                }
                LabelChangeStatus.Text = "Auction statuses refreshed successfully!";
            }catch(Exception ex)
            {
                LabelChangeStatus.Text = "Error when refreshing auction statuses: " + ex.Message;
            }

        }
        //calculul sumei totale oferite pe o anumita pictura intr-o anumita licitatie (licitatie si pictura specificate de la tastatura)
        protected void ButtonCalculateAmountOffered_Click(object sender, EventArgs e)
        {
            int IdLucrare, IdAuction;

            if (int.TryParse(TextBoxPainting.Text, out IdLucrare) && int.TryParse(TextBoxAuction.Text, out IdAuction)){

                string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Paintings.mdf;Integrated Security=True;Connect Timeout=30";
                string proceduraOfferedAmount = "CalculateTotalOferedAmountForAPaintingAtAnAuction";
                IdLucrare = Convert.ToInt32(TextBoxPainting.Text);
                IdAuction = Convert.ToInt32(TextBoxAuction.Text);
                int TotalAmount = 0;
                
                try
                {
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand(proceduraOfferedAmount, connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            command.Parameters.AddWithValue("@IdLucrare", IdLucrare);
                            command.Parameters.AddWithValue("@IdAuction", IdAuction);

                            command.Parameters.Add("@TotalAmount", SqlDbType.Int);
                            command.Parameters["@TotalAmount"].Direction = ParameterDirection.Output;


                            command.ExecuteNonQuery();


                            TotalAmount = Convert.ToInt32(command.Parameters["@TotalAmount"].Value);
                        }
                    }
                    LabelAmount.Text = "Suma totală a ofertelor: " + TotalAmount.ToString();
                }
                catch (Exception ex)
                {
                    
                    LabelAmount.Text = "Eroare: " + ex.Message;
                }
            }
            else
            {
                LabelAmount.Text = "Please enter a valid id!";
            }
            

        }

        //insert new auction
        protected void ButtonInsertAuction_Click(object sender, EventArgs e)
        {
            string dateStr = TextBoxDate.Text.Trim();
            string hourStr = TextBoxHour.Text.Trim();
            string locationStr = TextBoxLocation.Text.Trim();
            string minimAmountStr = TextBoxMinAmount.Text.Trim();
            string statusStr = TextBoxStatus.Text.Trim();

            
            DateTime date;
            if (!DateTime.TryParse(dateStr, out date))
            {
                LabelErrorAddAuction.Text = "Invalid date format.";
                return;
            }

            
            TimeSpan hour;
            if (!TimeSpan.TryParse(hourStr, out hour))
            {
                LabelErrorAddAuction.Text = "Invalid hour format.";
                return;
            }

            
            decimal minimAmount;
            if (!decimal.TryParse(minimAmountStr, out minimAmount) || minimAmount <= 0)
            {
                LabelErrorAddAuction.Text = "Minimum amount must be a positive number.";
                return;
            }

            
            string[] validStatuses = { "Open", "Closed", "Coming soon" };
            if (!validStatuses.Contains(statusStr))
            {
                LabelErrorAddAuction.Text = "Invalid auction status.";
                return;
            }

            try
            {
                
                InsertAuctionIntoDatabase(date, hour, locationStr, minimAmount, statusStr);

                
                TextBoxDate.Text = "";
                TextBoxHour.Text = "";
                TextBoxLocation.Text = "";
                TextBoxMinAmount.Text = "";
                TextBoxStatus.Text = "";
                LabelErrorAddAuction.Text = "Auction added successfully";

                
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                LabelErrorAddAuction.Text = "An error occurred: " + ex.Message;
            }
        }
        private void InsertAuctionIntoDatabase(DateTime date, TimeSpan hour, string location, decimal minimAmount, string status)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Paintings.mdf;Integrated Security=True;Connect Timeout=30";
            string query = "INSERT INTO Auctions (Date, Hour, Location, MinimAmount, Status) VALUES (@Date, @Hour, @Location, @MinimAmount, @Status)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Date", date);
                    command.Parameters.AddWithValue("@Hour", hour);
                    command.Parameters.AddWithValue("@Location", location);
                    command.Parameters.AddWithValue("@MinimAmount", minimAmount);
                    command.Parameters.AddWithValue("@Status", status);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }


        private void UpdateAuctionInDataBase(int IdAuction, DateTime date, TimeSpan hour, string location, decimal minimAmount, string status)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Paintings.mdf;Integrated Security=True;Connect Timeout=30";
            string query = "UPDATE Auctions SET Date = @Date, Hour = @Hour, Location = @Location, MinimAmount = @MinimAmount, Status = @Status  WHERE IdAuction = @IdAuction";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdAuction", IdAuction);
                    command.Parameters.AddWithValue("@Date", date);
                    command.Parameters.AddWithValue("@Hour", hour);
                    command.Parameters.AddWithValue("@Location", location);
                    command.Parameters.AddWithValue("@MinimAmount", minimAmount);
                    command.Parameters.AddWithValue("@Status", status);


                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        protected void ButtonUpdateAuction_Click(object sender, EventArgs e)
        {

            if (GridView1.SelectedIndex < 0)
            {
                LabelUpdateAuctionErr.Text = "Auction updated successfully";
                return;
            }

            int id = Convert.ToInt32(TextBoxIdAucUpdt);
            string dateStr = TextBoxDateAucUpdt.Text.Trim();
            string hourStr = TextBoxHour.Text.Trim();
            string locationStr = TextBoxLocation.Text.Trim();
            string minimAmountStr = TextBoxMinAmount.Text.Trim();
            string statusStr = TextBoxStatus.Text.Trim();


            DateTime date;
            if (!DateTime.TryParse(dateStr, out date))
            {
                LabelUpdateAuctionErr.Text = "Invalid date format.";
                return;
            }


            TimeSpan hour;
            if (!TimeSpan.TryParse(hourStr, out hour))
            {
                LabelUpdateAuctionErr.Text = "Invalid hour format.";
                return;
            }


            decimal minimAmount;
            if (!decimal.TryParse(minimAmountStr, out minimAmount) || minimAmount <= 0)
            {
                LabelUpdateAuctionErr.Text = "Minimum amount must be a positive number.";
                return;
            }


            string[] validStatuses = { "Open", "Closed", "Coming soon" };
            if (!validStatuses.Contains(statusStr))
            {
                LabelUpdateAuctionErr.Text = "Invalid auction status.";
                return;
            }

            try
            {

                UpdateAuctionInDataBase(id, date, hour, locationStr, minimAmount, statusStr);


                TextBoxDate.Text = "";
                TextBoxHour.Text = "";
                TextBoxLocation.Text = "";
                TextBoxMinAmount.Text = "";
                TextBoxStatus.Text = "";
                LabelUpdateAuctionErr.Text = "Auction updated  successfully";


                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                LabelErrorAddAuction.Text = "An error occurred: " + ex.Message;
            }

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow selectedRow = GridView1.Rows[GridView1.SelectedIndex];

            TextBoxIdAucUpdt.Text = selectedRow.Cells[1].Text;
            TextBoxDateAucUpdt.Text = selectedRow.Cells[2].Text;
            TextBoxHourAucUpdt.Text = selectedRow.Cells[3].Text;
            TextBoxLocationAucUpdt.Text = selectedRow.Cells[4].Text;
            TextBoxMinAmountAucUpdt.Text = selectedRow.Cells[5].Text;
            TextBoxStatusAucUpdt.Text = selectedRow.Cells[6].Text;
        }


    }
}