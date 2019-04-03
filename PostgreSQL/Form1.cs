using System;
using System.Collections.Generic;
using System.Windows.Forms;
using _excel = Microsoft.Office.Interop.Excel;
using Npgsql;
using System.Data;
using System.Data.OleDb;//nese duam te importojme data nga excel to gridview
using System.IO;
using System.Drawing;

namespace PgSql
{
   public partial class Form1 : Form
   {
        
      List<string> dataItems = new List<string>();
      data_server_connection DtServer = new data_server_connection();

        //ketu
        private NpgsqlCommand command;
        private NpgsqlDataReader dataReader;
        private string NOvalueChange,valueChange;
        //mbaron


        public Form1()
      {
         InitializeComponent();
      }
        //public struct ValidFileCheck
        //{
        //    public bool ValidFile;
        //    public int row;
        //}
        //perpara se te dhenat te dergohen ne database shikojme nese File permban te dhena te sakta. funksioni me poshte.
        public bool Validate_File()
        {
            // ValidFileCheck vlfchek = new ValidFileCheck();
            //  vlfchek.row = 0;
            bool Value = false;
            int row_check = 0;
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
               
                if (dataGridView1.Rows[i].Cells[0].Value.ToString() != "" && dataGridView1.Rows[i].Cells[1].Value.ToString() != "" && dataGridView1.Rows[i].Cells[2].Value.ToString() != "" && dataGridView1.Rows[i].Cells[3].Value.ToString() != "" )
                {
                    row_check += 1;
                }
                else
                {
                   // vlfchek.row = row_check + 1;
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }

               if (row_check == (dataGridView1.Rows.Count-1))
                {
                    // vlfchek.ValidFile = true;
                    Value = true;
                }
                else
                {
                    // vlfchek.ValidFile = false;
                    Value = false;
                  
                }
            }
            return Value;//return true/or false ne varsi te te dhenave ne file te shfaqura ne gridview.
        }

      private void button1_Click(object sender, EventArgs e)
        {
            
            PostGreSQL pg_Connect = new PostGreSQL();
                DtServer = pg_Connect.connect_database();
                string connstring = DtServer.dt_connection;
                bool conn_True = DtServer.fileExist;
            if (conn_True)
            {
                try
                {
                    
                    NpgsqlConnection connection = new NpgsqlConnection(connstring);
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM public.student", connection);
                    NpgsqlDataReader dataReader = command.ExecuteReader();
                    for (int i = 0; dataReader.Read(); i++)
                    {
                        dataItems.Add(dataReader[4].ToString() + "   " + dataReader[0].ToString() + "   " + dataReader[2].ToString() + "    " + dataReader[1].ToString() + "    " + dataReader[3].ToString() + "\r\n");
                    }
                    connection.Close();

                    //  return dataItems;

                }
                catch (Exception msg)
                {
                    MessageBox.Show(msg.ToString());
                    throw;
                }
            }
            else
            {
                MessageBox.Show("Connection to dataBase has Failed Because File with data connections not Exist or name of the file has changed!");
            }

            tbDataItems.Clear();
         
            for (int i = 0; i < dataItems.Count; i++)
         {
            tbDataItems.Text += dataItems[i];
            tbDataItems.ScrollToCaret();
         }
            dataItems.Clear();
      }

      private void button2_Click(object sender, EventArgs e)
        {
                PostGreSQL pg_Connect = new PostGreSQL();
                DtServer = pg_Connect.connect_database();
                string connstring = DtServer.dt_connection;
                bool conn_True = DtServer.fileExist;

            //   string connstring = "Server=127.0.0.1; Port=5432; User Id=postgres; Password=b2b4cc1b2; Database=DataStudent;";
            if (conn_True)
            {
              try
                {
                    
                    NpgsqlConnection connection = new NpgsqlConnection(connstring);
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM public.student WHERE id > '1' AND id < '4'", connection);
                    NpgsqlDataReader dataReader = command.ExecuteReader();
                    for (int i = 0; dataReader.Read(); i++)
                    {
                        dataItems.Add(dataReader[4].ToString() + "   " + dataReader[0].ToString() + "    " + dataReader[2].ToString() + "     " + dataReader[1].ToString() + "    " + dataReader[3].ToString() + "\r\n");
                    }
                    connection.Close();
                    // connection i sukseshem me serverin. ruajme te dhenat e ketij serveri ne file(text). 
                    //write_data_to_file(connstring);
                    //return dataItems;
                }
                catch (Exception msg)
                {
                    MessageBox.Show(msg.ToString());
                    throw;
                }
            }
            //ketu fillimi
            //   PostGreSQL pgTest = new PostGreSQL();
            //dataItems = pgTest.PostgreSQLtest2();            

            tbDataItems.Clear();
            for (int i = 0; i < dataItems.Count; i++)
         {
            tbDataItems.Text += dataItems[i];
            tbDataItems.ScrollToCaret();
         }
            dataItems.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        { // Krijohet instance e classes PostGreSQL sepse ketu ndodhet funksioni 'connect_database()'
          //i cili kthen nje  object te tipit 'data_server_connection' 
            PostGreSQL pg_Connect = new PostGreSQL();
           DtServer= pg_Connect.connect_database();
            string connstring = DtServer.dt_connection;
            bool conn_True = DtServer.fileExist;
            if (conn_True)
            {
                NpgsqlConnection connection = new NpgsqlConnection(connstring);
                if (firstname_txt.Text != "" && meadlename_txt.Text != "" && secondname_txt.Text != "" && studying_txt.Text != "")
                {
                    string Query = "insert into public.student (first_name,last_name,meadle_name,studying) values('" + this.firstname_txt.Text + "','" + this.secondname_txt.Text + "','" + this.meadlename_txt.Text + "','" + this.studying_txt.Text + "');";

                    NpgsqlCommand command = new NpgsqlCommand(Query, connection);
                    NpgsqlDataReader dataReader;
                    try
                    {
                        connection.Open();
                        dataReader = command.ExecuteReader();
                        MessageBox.Show("Data saved to the database!");
                        //fshirja e fushave pasi behet update ne database
                        firstname_txt.Clear(); meadlename_txt.Clear();
                        secondname_txt.Clear(); studying_txt.Clear();
                        
                        //Ruajtja e te dhenave te serverit ne file
                        //Rasti kur shtohet nje student ruhen te dhenat e ketij serveri tek i cili u be shtimi i studentit.
                        // PostGreSQL data_server = new PostGreSQL();
                       // data_server.write_data_to_file(connstring);

                        while (dataReader.Read())
                        {

                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Please fill all the fields in order to insert the data into the database");
                }
            }
            else
            {
                MessageBox.Show("Connection to dataBase has Failed Because File with data connections not Exist or name of the file has changed!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {    // Krijimi i Excel Application  
            _excel._Application app = new _excel.Application();
            // krijimi i new WorkBook Ne Excel application  
            _excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // krijimi i  new Excelsheet in workbook  
            _excel._Worksheet worksheet = null;
            // Shfaqja e excel sheet Sapo te ekzekutohet programi 
            app.Visible = true;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // Ndryshimi i emrit te active sheet  
            worksheet.Name = "Exported from gridview";
            // Ruajtja e header part in Excel  
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
            }
            // ruajtja e cdo rreshti dhe kolone (value) ne excel sheet  
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
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Krijohet instance e classes PostGreSQL sepse ketu ndodhet funksioni 'connect_database()'
            //i cili kthen nje  object te tipit 'data_server_connection' 
            PostGreSQL pg_Connect = new PostGreSQL();
            DtServer = pg_Connect.connect_database();
            string connstring = DtServer.dt_connection;
            bool conn_True = DtServer.fileExist;
            if (conn_True)
            {
               
                NpgsqlConnection connection = new NpgsqlConnection(connstring);
                  NpgsqlCommand command = new NpgsqlCommand("SELECT * from public.student", connection);
               
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
            else
            {
                MessageBox.Show("Connection to dataBase has Failed Because File with data connections not Exist or name of the file has changed!");
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog openfiledialog1 = new OpenFileDialog();
            openfiledialog1.Filter = "Excel File | *.xlsx; *.xls; *.xlsm;";
            if(openfiledialog1.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                this.textBox_path.Text = openfiledialog1.FileName;

            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            string pathconn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + textBox_path.Text + ";Extended Properties=\"Excel 12.0; HDR=YES;\" ; ";
            OleDbConnection conn = new OleDbConnection(pathconn);
            OleDbDataAdapter mydataadapter = new OleDbDataAdapter("Select * from [" + textBox_sheet.Text + "$]", conn);
            DataTable dt = new DataTable();
            mydataadapter.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        // update table in database from gridview
        private void button8_Click(object sender, EventArgs e)
        {
            // Krijohet instance e classes PostGreSQL sepse ketu ndodhet funksioni 'connect_database()'
            //i cili kthen nje  object te tipit 'data_server_connection' 
            PostGreSQL pg_Connect = new PostGreSQL();
            DtServer = pg_Connect.connect_database();
            string connstring = DtServer.dt_connection;
            bool conn_True = DtServer.fileExist;
            if (conn_True)
            {
                NpgsqlConnection connection = new NpgsqlConnection(connstring);
                // ValidFileCheck result = Validate_File(); 
                if (Validate_File())
                {
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        // if (dataGridView1.Rows[i].Cells[0].Value.ToString() != "" && dataGridView1.Rows[i].Cells[1].Value.ToString() != "" && dataGridView1.Rows[i].Cells[2].Value.ToString() != "" && dataGridView1.Rows[i].Cells[3].Value.ToString() != "" && dataGridView1.Rows[i].Cells[4].Value.ToString() != "")
                        //{
                        string Query = "insert into public.student (first_name,last_name,meadle_name,studying) values('" + dataGridView1.Rows[i].Cells[0].Value + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + dataGridView1.Rows[i].Cells[3].Value + "');";

                        NpgsqlCommand command = new NpgsqlCommand(Query, connection);

                        NpgsqlDataReader dataReader;
                        try
                        {
                            connection.Open();
                            dataReader = command.ExecuteReader();

                            connection.Close();
                            while (dataReader.Read())
                            {

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        //}
                        //else
                        //{
                        //    MessageBox.Show("In the row " + (i + 1).ToString() + " you have empty cell and the data can't insert to the data base");
                        //}

                        //  break;
                    }
                    MessageBox.Show("Data saved to the database!");
                    dataGridView1.DataSource = null;
                    dataGridView1.Refresh();
                }
                else
                {
                    MessageBox.Show("In the RED line to the DataGridView you have empty cell and the data can't insert to the data base");
                }

            }
            else
            {
                MessageBox.Show("Connection to dataBase has Failed Because File with data connections not Exist or name of the file has changed!");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {       // Krijohet instance e classes PostGreSQL sepse ketu ndodhet funksioni 'connect_database()'
                //i cili kthen nje  object te tipit 'data_server_connection' 
                PostGreSQL pg_Connect = new PostGreSQL();
                DtServer = pg_Connect.connect_database();
                string connstring = DtServer.dt_connection;
                bool conn_True = DtServer.fileExist;
            if (conn_True)
            {
                NpgsqlConnection connection = new NpgsqlConnection(connstring);
                connection.Open();
                NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM public.student", connection);
                try
                {
                    NpgsqlDataReader dataReader = command.ExecuteReader();
                    for (int i = 0; dataReader.Read(); i++)
                    {
                        StreamWriter file = new StreamWriter("database_exported.txt", true);
                        file.Write(dataReader[0].ToString() + "   " + dataReader[1].ToString() + "   " + dataReader[3].ToString() + "    " + dataReader[2].ToString() + "    " + dataReader[4].ToString() + "\r\n");
                        file.Close();

                    }
                    MessageBox.Show("Data successfully saved to the File text!");
                    connection.Close();
                }
                catch (Exception msg)
                {
                    MessageBox.Show(msg.ToString());
                    throw;
                }
            }
            else
            {
                MessageBox.Show("Connection to dataBase has Failed Because File with data connections not Exist or name of the file has changed!");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //string connstring = "Server=127.0.0.1; Port=5432; User Id=postgres; " +
            //  "Password=b2b4cc1b2; Database=DataStudent;";
            //NpgsqlConnection connection = new NpgsqlConnection(connstring);
            //string Query = "delete from public.student;";

            //NpgsqlCommand command = new NpgsqlCommand(Query, connection);
            //NpgsqlDataReader dataReader;
            //try
            //{
            //    connection.Open();
            //    dataReader = command.ExecuteReader();

            //    connection.Close();
            //    while (dataReader.Read())
            //    {

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
             MessageBox.Show("You must insert code and after that  you can make tests!");
          
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //PostGreSQL pg_Connect = new PostGreSQL();
            //DtServer = pg_Connect.connect_database();
            //string conn = DtServer.dt_connection;
            //bool File_exist_True = DtServer.fileExist;

            //try
            //{
            ////    //    using ( connection = new NpgsqlConnection(conn))
            //    //    {
            //    //        //npgsqlDA = new NpgsqlDataAdapter("select id,first_name,last_name,meadle_name,studying from student;", connection);
            //    //        //ds = new DataSet();
            //    //        //npgsqlDA.Fill(ds, "student_Details");
            //   NpgsqlDA = new NpgsqlDataAdapter("SELECT first_name,last_name,meadle_name,studying,id FROM public.student", conn);
            //    NpgsqlCommandBuilder cmdbl = new NpgsqlCommandBuilder(NpgsqlDA);
              
            //    //NpgsqlDA.Fill(ds, "Student_details");
            //    NpgsqlDA.Update(ds);
            //    MessageBox.Show("Information.updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //}
            ////        cmdbl = new NpgsqlCommandBuilder(npgsqlDA);
            ////    }
            
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            //}
           
        }

       

        

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null&&NOvalueChange!=valueChange)
            {
                 valueChange = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                DialogResult result = MessageBox.Show("Do you want to save changes?", "Confirmation", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    PostGreSQL pg_Connect = new PostGreSQL();
                    DtServer = pg_Connect.connect_database();
                    string connstring = DtServer.dt_connection;
                    bool conn_True = DtServer.fileExist;
                    if (conn_True)
                    {
                        NpgsqlConnection connection = new NpgsqlConnection(connstring);
                        int Column_index = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ColumnIndex.ToString());
                        int value_of_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                        switch (Column_index)
                        {
                            case 0:
                                //update student set first_name = 'Klesti', meadle_name = 'Elion' where id = 1;
                                //select* from student;
                                string Query = "update public.student set first_name='"+valueChange+"'where id = '"+value_of_id+"';";
                                NpgsqlCommand command = new NpgsqlCommand(Query, connection);

                                NpgsqlDataReader dataReader;
                                try
                                {
                                    connection.Open();
                                    dataReader = command.ExecuteReader();

                                    connection.Close();
                                    while (dataReader.Read())
                                    {

                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                dataGridView1.Refresh();
                                break;
                            case 1:
                                string Query1 = "update public.student set last_name='" + valueChange + "'where id = '" + value_of_id + "';";
                               // NpgsqlCommand 
                                    command = new NpgsqlCommand(Query1, connection);

                               // NpgsqlDataReader dataReader;
                                try
                                {
                                    connection.Open();
                                    dataReader = command.ExecuteReader();

                                    connection.Close();
                                    while (dataReader.Read())
                                    {

                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                dataGridView1.Refresh();
                                break;
                            case 2:
                                string Query2 = "update public.student set meadle_name='" + valueChange + "'where id = '" + value_of_id + "';";
                                       command = new NpgsqlCommand(Query2, connection);

                                try
                                {
                                    connection.Open();
                                    dataReader = command.ExecuteReader();

                                    connection.Close();
                                    while (dataReader.Read())
                                    {

                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                dataGridView1.Refresh();
                                break;
                            case 3:
                                string Query3 = "update public.student set studying='" + valueChange + "'where id = '" + value_of_id + "';";
                                
                                command = new NpgsqlCommand(Query3, connection);
                                
                                try
                                {
                                    connection.Open();
                                    dataReader = command.ExecuteReader();

                                    connection.Close();
                                    while (dataReader.Read())
                                    {

                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                break;
                            case 4:
                                MessageBox.Show("Id value can not changed");
                                valueChange = NOvalueChange;
                                this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = NOvalueChange;
                               
                                break;
                            default:
                                MessageBox.Show("You must try to change column 1 to 5 ");
                                dataGridView1.Refresh();
                                break;

                        }
                        
                    }
                    else
                    {
                        MessageBox.Show("Connection to dataBase has Failed Because File with data connections not Exist or name of the file has changed!");
                    }
                    
                }
                else if (result == DialogResult.No)
                {
                    valueChange = NOvalueChange;
                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value=NOvalueChange;
                }
               
            }
            
          
        }
        //nese nuk ndollndryshim ne cell
        private void cellclick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                NOvalueChange = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
        
        }
    }
}
