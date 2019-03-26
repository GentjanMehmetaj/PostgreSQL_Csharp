using System;
using System.Collections.Generic;
using System.Windows.Forms;

using _excel = Microsoft.Office.Interop.Excel;
using Npgsql;
using System.Data;

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
                MessageBox.Show("Data saved to the database!");
                //Ruajtja e te dhenave te serverit ne file
                //Rasti kur shtohet nje student ruhen te dhenat e ketij serveri tek i cili u be shtimi i studentit.

                PostGreSQL data_server = new PostGreSQL();
                data_server.write_data_to_file(connstring);
                while(dataReader.Read())
                {

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {    // creating Excel Application  
            _excel._Application app = new _excel.Application();
            // creating new WorkBook within Excel application  
            _excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            _excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "Exported from gridview";
            // storing header part in Excel  
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView1.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value.ToString();
                }
            }
            // save the application  
            workbook.SaveAs(@"C:\Users\albana\Desktop\output.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, _excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  
            app.Quit();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            string connstring = "Server=127.0.0.1; Port=5432; User Id=postgres; " +
                   "Password=b2b4cc1b2; Database=DataStudent;";
            NpgsqlConnection connection = new NpgsqlConnection(connstring);
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM public.student", connection);

            try
            {

                NpgsqlDataAdapter NpgsqlDA = new NpgsqlDataAdapter();

                NpgsqlDA.SelectCommand = command;
                DataTable dbdataset = new DataTable();
                NpgsqlDA.Fill(dbdataset);
                BindingSource bsource = new BindingSource();
                bsource.DataSource = dbdataset;
                dataGridView1.DataSource = bsource;
                NpgsqlDA.Update(dbdataset);
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.Message);

            }
        }
    }
}
