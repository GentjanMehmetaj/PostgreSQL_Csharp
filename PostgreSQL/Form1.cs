using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Npgsql;


namespace PgSql
{
   public partial class Form1 : Form
   {
      List<string> dataItems = new List<string>();

      public Form1()
      {
         InitializeComponent();
      }

      private void button1_Click(object sender, EventArgs e)
      {
         PostGreSQL pgTest = new PostGreSQL();
         dataItems = pgTest.PostgreSQLtest1();
         tbDataItems.Clear();
         for (int i = 0; i < dataItems.Count; i++)
         {
            tbDataItems.Text += dataItems[i];
            tbDataItems.ScrollToCaret();
         }
      }

      private void button2_Click(object sender, EventArgs e)
      {
         PostGreSQL pgTest = new PostGreSQL();
         dataItems = pgTest.PostgreSQLtest2();
         tbDataItems.Clear();
         for (int i = 0; i < dataItems.Count; i++)
         {
            tbDataItems.Text += dataItems[i];
            tbDataItems.ScrollToCaret();
         }
      }

        private void button3_Click(object sender, EventArgs e)
        {
            string connstring = "Server=127.0.0.1; Port=5432; User Id=postgres; " +
                "Password=b2b4cc1b2; Database=DataStudent;";
            NpgsqlConnection connection = new NpgsqlConnection(connstring);
            
            string Query= "insert into public.student (id,first_name,last_name,meadle_name,studying) values('"+ this.id_txt.Text+"','"+this.firstname_txt.Text+"','"+this.secondname_txt.Text+"','"+this.meadlename_txt.Text+"','"+this.studying_txt.Text+"');";

            NpgsqlCommand command = new NpgsqlCommand(Query, connection);
            NpgsqlDataReader dataReader;
            try
            {
                connection.Open();
                dataReader = command.ExecuteReader();
                MessageBox.Show("Saved");
                while(dataReader.Read())
                {

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
