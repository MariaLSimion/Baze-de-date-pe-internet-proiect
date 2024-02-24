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

namespace SimionMariaBDIProiectTablou
{
    public partial class PaintingsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

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

    }


}