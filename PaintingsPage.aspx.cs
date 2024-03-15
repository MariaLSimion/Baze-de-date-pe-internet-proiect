 using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Drawing;

namespace SimionMariaBDIProiectTablou
{
    public partial class PaintingsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                DropDownListartisti.Items.Add(new ListItem("- artist - ", "-1"));
                PopulateArtistDropDownList();
            }
        }
        //populare drop down list cu numele artistilor ale caror lucrari sunt disponibile
        private void PopulateArtistDropDownList()
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Paintings.mdf;Integrated Security=True;Connect Timeout=30";
            string query = "SELECT DISTINCT Artist FROM Paintings WHERE Disponibilitate = 'Da'";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command= new SqlCommand(query, connection))
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        string artist = reader["Artist"].ToString();
                        DropDownListartisti.Items.Add(artist);
                    }
                }
            }

        }
        //filtrare cu ajutorul Radio buttons dupa disponibilitate: Available Unavailable All
        protected void RadioButtonAvailable_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonAvailable.Checked)
            {
                RadioButtonUnavailable.Checked = false;
                RadioButtonAll.Checked = false;
                FilterGridViewByAvailability("Da");
            }

        }

        protected void RadioButtonAll_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioButtonAll.Checked)
            {
                RadioButtonAvailable.Checked = false;
                RadioButtonUnavailable.Checked = false;
                SqlDataSource_Painting.SelectCommand = "SELECT * FROM Paintings";
                SqlDataSource_Painting.SelectParameters.Clear();
                

                GridView1.DataBind();
            }
        }

        protected void RadioButtonUnavailable_CheckedChanged(object sender, EventArgs e)
        {
            if(RadioButtonUnavailable.Checked)
            {
                RadioButtonAvailable.Checked = false;
                RadioButtonAll.Checked=false;
                FilterGridViewByAvailability("Nu");
            }
        }
        private void FilterGridViewByAvailability(string availability)
        {
            SqlDataSource_Painting.SelectCommand = "SELECT * FROM Paintings where Disponibilitate = @Disponibilitate";
            SqlDataSource_Painting.SelectParameters.Clear();
            SqlDataSource_Painting.SelectParameters.Add("Disponibilitate", availability);

            GridView1.DataBind();
        }
        //afisare picturi in gridview + functionalitate de afisare imagine in imagge view pe select + afisare detalii in label-uri dedicate la select
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            if (GridView1.SelectedIndex >= 0)
            {
                
                GridViewRow selectedRow = GridView1.Rows[GridView1.SelectedIndex];

               
                LabelIDLucrare.Text = selectedRow.Cells[1].Text; 
                LabelTitle.Text = selectedRow.Cells[2].Text;
                LabelArtist.Text = selectedRow.Cells[3].Text;
                LabelYear.Text = selectedRow.Cells[4].Text;
                LabelTechique.Text= selectedRow.Cells[5].Text;
                LabelDimensions.Text= selectedRow.Cells[6].Text;
                LabelMinValue.Text= selectedRow.Cells[7].Text;
                LabelDisponibilitate.Text= selectedRow.Cells[8].Text;


                TextBoxIdUpdt.Text = selectedRow.Cells[1].Text;
                TextBoxTitleUpdt.Text = selectedRow.Cells[2].Text;
                TextBoxArtistUpdt.Text = selectedRow.Cells[3].Text;
                TextBoxYearUpdt.Text = selectedRow.Cells[4].Text;
                TextBoxTechniqueUpdt.Text = selectedRow.Cells[5].Text;
                TextBoxDimensionsUpdt.Text = selectedRow.Cells[6].Text;
                TextBoxMinValUpdt.Text = selectedRow.Cells[7].Text;
                TextBoxAvailabilityUpdt.Text = selectedRow.Cells[8].Text;

                //imaginea

                int selectedId = Convert.ToInt32(selectedRow.Cells[1].Text);
                string imageUrl = GetImageUrlFromDataBase(selectedId);

                if (IsUrlImageValid(imageUrl))
                {

                    ImageSelectedPainting.ImageUrl = imageUrl;
                }
                else
                {
                    ImageSelectedPainting.ImageUrl = "https://cdn.dribbble.com/users/5895703/screenshots/19041386/media/97a091cda53d5cf2b4f4a401168b600a.png?resize=400x300&vertical=center";
                }
            }
        }
        //verificare valididate url
        private bool IsUrlImageValid(string url)
        {
            return !string.IsNullOrEmpty(url) && (url.StartsWith("http://") || url.StartsWith("https://"));
        }

        private string GetImageUrlFromDataBase(int id)
        {
            string query = "SELECT URL from Paintings WHERE IdLucrare = @IdLucrare";
            string imageUrl = "";
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Paintings.mdf;Integrated Security=True;Connect Timeout=30";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@IdLucrare", id);
                connection.Open();
                imageUrl = (string)cmd.ExecuteScalar();
                

            }
            return imageUrl;
        }

        //filtrare gridview in functie de tehnica cu ajutorul check box-urilor, daca mai multe checkbox-uri sunt bifate sunt afisate
        //picturile pentru ambele variante bifate, daca sunt toate cele 3 debifate se va afisa inapoi tot continutul tabelei 

        private void FilterGridViewByTechniques(List<string> techniques)
        {
            string query = "SELECT * FROM Paintings";

            if (techniques.Count > 0)
            {
                query += " WHERE Technique IN (";
                for (int i = 0; i < techniques.Count; i++)
                {
                    query += "'" + techniques[i] + "'";
                    if (i < techniques.Count - 1)
                    {
                        query += ",";
                    }
                }
                query += ")";
            }

            SqlDataSource_Painting.SelectCommand = query;
            GridView1.DataBind();
        }

        protected void CheckBoxOil_CheckedChanged(object sender, EventArgs e)
        {
            List<string> selectedTechniques = new List<string>();

            if (CheckBoxOil.Checked)
            {
                selectedTechniques.Add("Oil on canvas");
            }
            if (CheckBoxPastel.Checked)
            {
                selectedTechniques.Add("Pastel on cardboard");
            }
            if (CheckBoxTempera.Checked)
            {
                selectedTechniques.Add("Tempera on wood");
            }

            FilterGridViewByTechniques(selectedTechniques);
        }

        protected void CheckBoxPastel_CheckedChanged(object sender, EventArgs e)
        {
            List<string> selectedTechniques = new List<string>();

            if (CheckBoxOil.Checked)
            {
                selectedTechniques.Add("Oil on canvas");
            }
            if (CheckBoxPastel.Checked)
            {
                selectedTechniques.Add("Pastel on cardboard");
            }
            if (CheckBoxTempera.Checked)
            {
                selectedTechniques.Add("Tempera on wood");
            }

            FilterGridViewByTechniques(selectedTechniques);
        }

        protected void CheckBoxTempera_CheckedChanged(object sender, EventArgs e)
        {
            List<string> selectedTechniques = new List<string>();

            if (CheckBoxOil.Checked)
            {
                selectedTechniques.Add("Oil on canvas");
            }
            if (CheckBoxPastel.Checked)
            {
                selectedTechniques.Add("Pastel on cardboard");
            }
            if (CheckBoxTempera.Checked)
            {
                selectedTechniques.Add("Tempera on wood");
            }

            FilterGridViewByTechniques(selectedTechniques);
        }

        //navigare pe pagina licitatii 
        protected void ButtonToTheAuctions_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Path/To/AuctionsPage.aspx");
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //int idToDelete = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
            //DeleteRowFromDatabase(idToDelete);

            ////bind gridview
            //GridView1.DataBind();
        }


        //inserare de tablou in baza de date
        protected void ButtonInsertPainting_Click(object sender, EventArgs e)
        {
            string titleStr = TextBoxTitle.Text.Trim();
            string artistStr = TextBoxArtist.Text.Trim();
            string yearStr = TextBoxYear.Text.Trim();
            string techniqueStr = TextBoxTechnique.Text.Trim();
            string dimensionsStr = TextBoxDimensions.Text.Trim();
            string minValueStr = TextBoxMinVal.Text.Trim();
            string urlStr = TextBoxURL.Text.Trim();
            string disponibilitateStr = TextBoxAvailability.Text.Trim();
            

            //validari
            if (titleStr.Length < 3)
            {
                LabelValidation.Text = "Title must be at least 3 characters long.";
                return;
            }
            if (artistStr.Length < 3)
            {
                LabelValidation.Text = "Artist must be at least 3 characters long.";
                return;
            }

            int year;
            if (!int.TryParse(yearStr, out year) || year < 1000 || year > DateTime.Now.Year)
            {
                LabelValidation.Text = "Invalid year.";
                return;
            }


            Uri uriResult;
            bool isValidUrl = Uri.TryCreate(urlStr, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            if (!isValidUrl)
            {
                LabelValidation.Text = "Invalid URL.";
                return;
            }


            if (!dimensionsStr.EndsWith("cm") || !dimensionsStr.Contains("x"))
            {
                LabelValidation.Text = "Invalid dimensions format. Use '0 cm x 0 cm' format.";
                return;
            }


            decimal minValue;
            if (!decimal.TryParse(minValueStr, out minValue) || minValue <= 0)
            {
                LabelValidation.Text = "Minimum value must be a number greater than 0.";
                return;
            }

            if (disponibilitateStr.Trim() != "Da" && disponibilitateStr.Trim() != "Nu" && disponibilitateStr.Trim() != "da" && disponibilitateStr.Trim() != "nu")
            {
                LabelValidation.Text = "Availability value must be Da or Nu";
                
                return;
            }

            if (techniqueStr.Trim() != "Oil on canvas" && techniqueStr.Trim() != "Pastel on cardboard" && techniqueStr.Trim() != "Tempera on wood")
            {
                LabelValidation.Text = "Availability value must be Oil on canvas or Pastel on cardboard or Tempera on wood";
                
                return;
            }

            try
            {
                string title = TextBoxTitle.Text;
                string artist = TextBoxArtist.Text;
                int yeaR = Convert.ToInt32(TextBoxYear.Text);
                string technique = TextBoxTechnique.Text;
                string dimensions = TextBoxDimensions.Text;
                int valmin = Convert.ToInt32(TextBoxMinVal.Text);
                string availability = TextBoxAvailability.Text;
                string url = TextBoxURL.Text;

                InsertPaintingIntoDatabase(title, artist, yeaR, technique, dimensions, valmin, availability, url);

                //golire text box-uri
                TextBoxTitle.Text = " ";
                TextBoxArtist.Text = "";
                TextBoxYear.Text = " ";
                TextBoxTechnique.Text = "";
                TextBoxDimensions.Text = "";
                TextBoxMinVal.Text = " ";
                TextBoxAvailability.Text = " ";
                TextBoxURL.Text = "";
                LabelError.Text = "Painting added succesfully";

                GridView1.DataBind();


            }
            catch (Exception ex)
            {
                LabelError.Text = "An error occurred: " + ex.Message;
            }




        }

        private void InsertPaintingIntoDatabase(string title, string artist, int year, string technique, string dimensions, int valmin, string availability, string url)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Paintings.mdf;Integrated Security=True;Connect Timeout=30";
            string query = "INSERT INTO Paintings (Title, Artist, Year, Technique, Dimensions, ValoareMinima, Disponibilitate, URL) VALUES (@Title, @Artist, @Year, @Technique, @Dimensions, @ValoareMinima, @Disponibilitate, @URL)";

            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Artist", artist);
                    command.Parameters.AddWithValue("@Year", year);
                    command.Parameters.AddWithValue("@Technique", technique);
                    command.Parameters.AddWithValue("@Dimensions", dimensions);
                    command.Parameters.AddWithValue("@ValoareMinima", valmin);
                    command.Parameters.AddWithValue("@Disponibilitate", availability);
                    command.Parameters.AddWithValue("@URL", url);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }

        protected void DropDownListartisti_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(DropDownListartisti.SelectedValue != "-1")
            {
                string selectedArtist = DropDownListartisti.SelectedItem.Text;
                DataTable artworksTable = GetAvailableArtworksByArtist(selectedArtist);
                GridViewPaintingsSelectedByartist.DataSource = artworksTable;
                GridViewPaintingsSelectedByartist.DataBind();

            }
            else
            {
                GridViewPaintingsSelectedByartist.DataSource = null;
                GridViewPaintingsSelectedByartist.DataBind();
            }
           
        }
        private DataTable GetAvailableArtworksByArtist(string artist)
        {
            DataTable artworksTable = new DataTable();
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Paintings.mdf;Integrated Security=True;Connect Timeout=30";
            string query = "SELECT * FROM dbo.GetAvailableArtworksByArtist(@Artist)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Artist", artist);
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(artworksTable);
                }
            }

            return artworksTable;
        }

        private void UpdatePaintingInDatabase(int paintingId, string title, string artist, int year, string technique, string dimensions, int valmin, string availability)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Paintings.mdf;Integrated Security=True;Connect Timeout=30";
            string query = "UPDATE Paintings SET Title = @Title, Artist = @Artist, Year = @Year, Technique = @Technique, Dimensions = @Dimensions, ValoareMinima = @ValoareMinima, Disponibilitate = @Disponibilitate  WHERE IdLucrare = @PaintingId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PaintingId", paintingId);
                    command.Parameters.AddWithValue("@Title", title);
                    command.Parameters.AddWithValue("@Artist", artist);
                    command.Parameters.AddWithValue("@Year", year);
                    command.Parameters.AddWithValue("@Technique", technique);
                    command.Parameters.AddWithValue("@Dimensions", dimensions);
                    command.Parameters.AddWithValue("@ValoareMinima", valmin);
                    command.Parameters.AddWithValue("@Disponibilitate", availability);
                    

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            GridViewRow row = GridView1.Rows[e.RowIndex];
            int paintingId = Convert.ToInt32(row.Cells[1].Text.Trim());

            // Obținem valorile vechi ale rândului pentru a le folosi în caz de eroare
            string oldTitle = row.Cells[2].Text.Trim(); // Titlul este în a doua celulă (index 2)
            string oldArtist = row.Cells[3].Text.Trim(); // Artistul este în a treia celulă (index 3)
            int oldYear = Convert.ToInt32(row.Cells[4].Text.Trim());
            string oldTechnique = row.Cells[5].Text.Trim();
            string oldDimensions = row.Cells[6].Text.Trim();
            int oldValoareMin = Convert.ToInt32(row.Cells[7].Text.Trim());
            string oldAvailability = row.Cells[8].Text.Trim();


            // Obținem noile valori introduse de utilizator
            string newTitle = ((TextBox)row.Cells[2].Controls[0]).Text.Trim(); // Titlul este în a doua celulă (index 2)
            string newArtist = ((TextBox)row.Cells[3].Controls[0]).Text.Trim(); // Artistul este în a treia celulă (index 3)
            int newYear = Convert.ToInt32(((TextBox)row.Cells[4].Controls[0]).Text.Trim()); // Anul este în a patra celulă (index 4)

            string newTechnique = ((TextBox)row.Cells[5].Controls[0]).Text.Trim();
            string newDimensions = ((TextBox)row.Cells[6].Controls[0]).Text.Trim();
            int newValoareMin = Convert.ToInt32(((TextBox)row.Cells[7].Controls[0]).Text.Trim());
            string newAvailability = ((TextBox)row.Cells[8].Controls[0]).Text.Trim();

            // Verificări pentru noile valori introduse
            if (newTitle.Length < 3)
            {
                LabelValidation.Text = "Title must be at least 3 characters long.";
                UpdatePaintingInDatabase(paintingId, oldTitle, oldArtist, oldYear, oldTechnique, oldDimensions, oldValoareMin, oldAvailability);
                // Nu este nevoie să revenim la valoarea veche, deoarece nu am modificat nimic în gridview
                return;
            }
            if (newArtist.Length < 3)
            {
                LabelValidation.Text = "Artist must be at least 3 characters long.";
                // Nu este nevoie să revenim la valoarea veche, deoarece nu am modificat nimic în gridview
                return;
            }


            UpdatePaintingInDatabase(paintingId, newTitle, newArtist, newYear, newTechnique, newDimensions, newValoareMin, newAvailability);

        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {

            if (GridView1.SelectedIndex < 0)
            {
                LabelValidation.Text = "Please select a painting first.";
                return;
            } 
            string oldTitle = LabelTitle.Text;
            string oldArtist = LabelArtist.Text;
            int oldYear = Convert.ToInt32(LabelYear.Text);
            string oldTechnique = LabelTechique.Text;
            string oldDimensions = LabelDimensions.Text;
            int oldMinValue = Convert.ToInt32(LabelMinValue.Text);
            string oldDisponibilitate = LabelDisponibilitate.Text;



            int id = Convert.ToInt32(TextBoxIdUpdt.Text);
            string title = TextBoxTitleUpdt.Text;
            string artist = TextBoxArtistUpdt.Text;
            string yearStr = TextBoxYearUpdt.Text;
            string technique = TextBoxTechniqueUpdt.Text;
            string dimensions = TextBoxDimensionsUpdt.Text;
            string minValueStr = TextBoxMinValUpdt.Text;
            string disponibilitate = TextBoxAvailabilityUpdt.Text;


            if (title.Length < 3)
            {
                LabelValidation.Text = "Title must be at least 3 characters long.";
                title = oldTitle;
                return;
            }
            if (artist.Length < 3)
            {
                LabelValidation.Text = "Artist must be at least 3 characters long.";
                artist = oldArtist;
                return;
            }

            int year;
            if (!int.TryParse(yearStr, out year) || year < 1000 || year > DateTime.Now.Year)
            {
                LabelValidation.Text = "Invalid year.";
                oldYear = year;
                return;
            }

            if (!dimensions.Trim().EndsWith("cm") || !dimensions.Contains("x"))
            {
                LabelValidation.Text = "Invalid dimensions format. Use '0 cm x 0 cm' format.";
                oldDimensions = dimensions;
                return;
            }


            int minValue;
            if (!int.TryParse(minValueStr, out minValue) || minValue <= 0)
            {
                LabelValidation.Text = "Minimum value must be a number greater than 0.";
                oldMinValue = minValue;
                return;
            }

            if (disponibilitate.Trim() != "Da" && disponibilitate.Trim() != "Nu" && disponibilitate.Trim() != "da" && disponibilitate.Trim() != "nu")
            {
                LabelValidation.Text = "Availability value must be Da or Nu";
                oldDisponibilitate = disponibilitate;
                return;
            }

            if (technique.Trim() != "Oil on canvas" && technique.Trim() != "Pastel on cardboard" && technique.Trim() != "Tempera on wood" )
            {
                LabelValidation.Text = "Availability value must be Oil on canvas or Pastel on cardboard or Tempera on wood";
                oldDisponibilitate = disponibilitate;
                return;
            }




            UpdatePaintingInDatabase(id, title, artist, year, technique, dimensions, minValue, disponibilitate);

            TextBoxIdUpdt.Text = " ";
            TextBoxTitleUpdt.Text = " ";
            TextBoxArtistUpdt.Text = "";
            TextBoxYearUpdt.Text = " ";
            TextBoxTechniqueUpdt.Text = "";
            TextBoxDimensionsUpdt.Text = "";
            TextBoxMinValUpdt.Text = " ";
            TextBoxAvailabilityUpdt.Text = " ";
            

            GridView1.DataBind();

        }



        //private void DeleteRowFromDatabase(int idToDelete)
        //{
        //    string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Paintings.mdf;Integrated Security=True;Connect Timeout=30";
        //    string query = "DELETE FROM Paintings WHERE IdLucrare= @IdLucrare";
        //    using(SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@IdLucrare", idToDelete);
        //            connection.Open();
        //            command.ExecuteNonQuery();  
        //        }
        //    }
        //}



        //private void FilterGridViewByTechnique(string technique)
        //{
        //    //technique = technique.Trim();

        //    SqlDataSource_Painting.SelectCommand = "SELECT * FROM Paintings where Technique = @Technique";
        //    SqlDataSource_Painting.SelectParameters.Clear();
        //    SqlDataSource_Painting.SelectParameters.Add("Technique", technique);

        //    GridView1.DataBind();
        //}

        //protected void CheckBoxOil_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (CheckBoxOil.Checked)
        //    {
        //        string technique = "Oil on canvas";

        //        FilterGridViewByTechnique(technique);
        //    } 
        //}


        //protected void CheckBoxPastel_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (CheckBoxPastel.Checked)
        //    {
        //        string technique = CheckBoxPastel.Text;
        //        FilterGridViewByTechnique(technique);
        //    }
        //}

        //protected void CheckBoxTempera_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (CheckBoxTempera.Checked)
        //    {
        //        string technique = CheckBoxTempera.Text;
        //        FilterGridViewByTechnique(technique);
        //    }
        //}
    }


}