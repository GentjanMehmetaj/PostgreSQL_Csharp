using System;
using System.Collections.Generic;
using System.Windows.Forms;
using _excel = Microsoft.Office.Interop.Excel;
using Npgsql;
using System.Data;
using System.Data.OleDb;//nese duam te importojme data nga excel to gridview
using System.IO;
using System.Drawing;
using System.Linq;

namespace PgSql
{
    public partial class Form1 : Form
    {

        List<string> dataItems = new List<string>();
        data_server_connection DtServer = new data_server_connection();
        PostGreSQL pg_Connect = new PostGreSQL();

        private NpgsqlConnection connection;
        private NpgsqlCommand command;
        private NpgsqlDataReader dataReader;

        private string NOvalueChange, valueChange;
        private bool data_load_from_excel_file = false;
       private bool file_excel_formated_ok = false;
        
        private int row_selected;
        private int excelcopy = 0;
        
        public Form1()
        {
            InitializeComponent();
        }
        public bool IsOpened(string wbook)
        {
            bool isOpened = true;
            _excel.Application exApp;

            try
            {
                // place the following line here :
                exApp = (_excel.Application)System.Runtime.InteropServices.Marshal.GetActiveObject("excel.Application");
                // because it throws an exception if Excel is not running.
                exApp.Workbooks.get_Item(wbook);
            }
            catch (Exception)
            {
                isOpened = false;
            }
            return isOpened;
        }
        //private bool file_formated_ok()
        //{
        //    bool file_excel_formated_ok = false;


        //        if (dataGridView1.Rows[0].Cells[0].Value.ToString() == "first_name" && dataGridView1.Rows[0].Cells[1].Value.ToString() == "last_name" && dataGridView1.Rows[0].Cells[2].Value.ToString() == "meadle_name" && dataGridView1.Rows[0].Cells[3].Value.ToString() == "studying")
        //        {
        //            file_excel_formated_ok = true;
        //        }
        //        else { file_excel_formated_ok = false; }
                
            
        //    return file_excel_formated_ok;
        //}
        //perpara se te dhenat te dergohen ne database shikojme nese File permban te dhena te sakta. funksioni me poshte.
        private bool Validate_File()
        {
            
            // ValidFileCheck vlfchek = new ValidFileCheck();
            //  vlfchek.row = 0;
            bool Value = false;
            int row_check = 0;
            if (dataGridView1.Columns.Count == 5)
            {
              // MessageBox.Show(dataGridView1.Columns[0].Name.ToString());
                if (dataGridView1.Columns[0].Name.ToString() == "first_name" && dataGridView1.Columns[1].Name.ToString() == "last_name" && dataGridView1.Columns[2].Name.ToString() == "meadle_name" && dataGridView1.Columns[3].Name.ToString() == "studying")
                {
                    file_excel_formated_ok = true;
                    for (int i = 0; i <dataGridView1.Rows.Count - 1; i++)
                    {
                        //string str_cell = "";
                        //bool allLetters = str_cell.All(c => Char.IsLetter(c));
                        // bool checknr = dataGridView1.Rows[i].Cells[0].Value.ToString().All(char.IsDigit);
                        if (dataGridView1.Rows[i].Cells[0].Value.ToString() != ""&&dataGridView1.Rows[i].Cells[0].Value.ToString().All(c=>char.IsLetter(c)) && dataGridView1.Rows[i].Cells[1].Value.ToString() != "" && dataGridView1.Rows[i].Cells[1].Value.ToString().All(c => char.IsLetter(c)) && dataGridView1.Rows[i].Cells[2].Value.ToString() != "" && dataGridView1.Rows[i].Cells[2].Value.ToString().All(c => char.IsLetter(c)) && dataGridView1.Rows[i].Cells[3].Value.ToString() != "" && dataGridView1.Rows[i].Cells[3].Value.ToString().All(c => char.IsLetter(c)))
                        {
                            row_check += 1;
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Aqua;
                        }
                        else
                        {
                            // vlfchek.row = row_check + 1;
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }

                        if (row_check == (dataGridView1.Rows.Count - 1))
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
                }
                else
                {
                    file_excel_formated_ok = false;
                    MessageBox.Show("File Excel must have colums: first_name,last_name,meadle_name,Studying in this range!Yuo have select wrong file(or colums name are wrong");
                }
            }


            return Value;//return true/or false ne varsi te te dhenave ne file te shfaqura ne gridview.
        }

      private void button1_Click(object sender, EventArgs e)
        {
                DtServer = pg_Connect.connect_database();
                string connstring = DtServer.dt_connection;
                bool conn_True = DtServer.fileExist;
            if (conn_True)
            {
                try
                {
                    
                    connection = new NpgsqlConnection(connstring);
                    connection.Open();
                     command = new NpgsqlCommand("SELECT * FROM public.student", connection);
                     dataReader = command.ExecuteReader();
                    for (int i = 0; dataReader.Read(); i++)
                    {
                        dataItems.Add(dataReader[4].ToString() + "   " + dataReader[0].ToString() + "   " + dataReader[2].ToString() + "    " + dataReader[1].ToString() + "    " + dataReader[3].ToString() + "\r\n");
                    }
                    connection.Close();

                  

                }
                catch (Exception msg)
                {
                    MessageBox.Show("You can't connect with database!Please chek data connections saved in the file and try again!"+ "Server=127.0.0.1; Port=5432; User Id=postgres; Password=b2b4cc1b2; Database=DataStudent;");
                    //MessageBox.Show(msg.ToString());
                    //throw;
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
            if (studying_txt.Text.ToString() != "")
            {
                DtServer = pg_Connect.connect_database();
                string connstring = DtServer.dt_connection;
                bool conn_True = DtServer.fileExist;

                if (conn_True)
                {
                    try
                    {

                        connection = new NpgsqlConnection(connstring);
                        connection.Open();
                        command = new NpgsqlCommand("SELECT * FROM public.student WHERE studying='" + studying_txt.Text.ToString() + "'", connection);
                        dataReader = command.ExecuteReader();
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
                        MessageBox.Show("You can't connect with database! Please chek data connections saved in the file and try again");
                        //MessageBox.Show(msg.ToString());
                        //throw;
                    }
                }


                tbDataItems.Clear();
                for (int i = 0; i < dataItems.Count; i++)
                {
                    tbDataItems.Text += dataItems[i];
                    tbDataItems.ScrollToCaret();
                }
                dataItems.Clear();
            }
            else { MessageBox.Show("Please fill the field studying in or to make search!"); }
        }

        private void button3_Click(object sender, EventArgs e)
        { // Krijohet instance e classes PostGreSQL sepse ketu ndodhet funksioni 'connect_database()'
          //i cili kthen nje  object te tipit 'data_server_connection' 
          
           DtServer= pg_Connect.connect_database();
            string connstring = DtServer.dt_connection;
            bool conn_True = DtServer.fileExist;
            if (conn_True)
            {
               
                if (firstname_txt.Text != "" && meadlename_txt.Text != "" && secondname_txt.Text != "" && studying_txt.Text != "")
                {
                    if (id_text.Text.ToString()=="")
                    {
                        string Query = "insert into public.student (first_name,last_name,meadle_name,studying) values('" + this.firstname_txt.Text + "','" + this.secondname_txt.Text + "','" + this.meadlename_txt.Text + "','" + this.studying_txt.Text + "');";

                       
                        // NpgsqlDataReader dataReader;
                        try
                        {
                            connection = new NpgsqlConnection(connstring);
                            command = new NpgsqlCommand(Query, connection);
                            connection.Open();
                            dataReader = command.ExecuteReader();
                            MessageBox.Show("Data saved to the database!");
                            //fshirja e fushave pasi behet update ne database
                            firstname_txt.Clear(); meadlename_txt.Clear();
                            secondname_txt.Clear(); studying_txt.Clear();
                            id_text.Clear();

                            //Ruajtja e te dhenave te serverit ne file
                            //Rasti kur shtohet nje student ruhen te dhenat e ketij serveri tek i cili u be shtimi i studentit.
                            // PostGreSQL data_server = new Post=GreSQL();
                            // data_server.write_data_to_file(connstring);

                            while (dataReader.Read())
                            {

                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("You can't connect with database! And for this reason you can not add data to the databas.Please chek data connections saved in the file and try again");
                            // MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("This data can,t bee add to the database becouse are not put from the user by writing them! Are came from Gridview...");
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
        {   if (dataGridView1.DataSource != null)
            {
                // Krijimi i Excel Application  
                _excel._Application app = new _excel.Application();
                // krijimi i new WorkBook Ne Excel application  
                _excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
                // krijimi i  new Excelsheet in workbook  
                _excel._Worksheet worksheet = null;
                // Shfaqja e excel sheet Sapo te ekzekutohet programi 
                //app.Visible = true;
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
                if (File.Exists(@"C:\Users\albana\Desktop\output.xlsx"))
                {

                    DialogResult result = MessageBox.Show("File exist! Do you want to owerite it?", "Please Confirm", MessageBoxButtons.YesNoCancel);
                    if (result == DialogResult.Yes)
                    {// if (app.ActiveWorkbook = true) { }
                     //    xlApp.IgnoreRemoteRequests = true;
                        bool isopen = IsOpened("output.xlsx");
                        if (isopen)
                        {
                            MessageBox.Show("Please close Excel file in order to replace it with the file generated!");
                            app.DisplayAlerts = false;
                        }

                        else
                        {

                            File.Delete(@"C:\Users\albana\Desktop\output.xlsx");
                            workbook.SaveAs(@"C:\Users\albana\Desktop\output.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, _excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                            app.DisplayAlerts = false;
                            dataGridView1.DataSource = null;
                            dataGridView1.Refresh();
                        }

                    }
                    else if (result == DialogResult.No)
                    {
                        excelcopy += 1;

                        while ((File.Exists(@"C:\Users\albana\Desktop\output" + excelcopy + ".xlsx") == true))
                        {
                            excelcopy += 1;
                        }
                        //nese dumam te dime emrin e filit qe ka te njejtin emer dhe qe duam ti bejme kopje
                        //perjashtojme rastin kur eshte output1 dhe ne rastet tjera bejme 'show ["output"+"excelcopy-1+]'
                        workbook.SaveAs(@"C:\Users\albana\Desktop\output" + excelcopy + ".xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, _excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                        MessageBox.Show("File is saved in the same directori with existing file name and the name of file created is output" + excelcopy + ".xlsx");
                        app.DisplayAlerts = false;
                        dataGridView1.DataSource = null;
                        dataGridView1.Refresh();
                    }
                    else if(result==DialogResult.Cancel)
                    {
                        app.DisplayAlerts = false;
                        dataGridView1.Refresh();

                    }
                }
                else
                {
                    workbook.SaveAs(@"C:\Users\albana\Desktop\output.xlsx", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, _excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    app.DisplayAlerts = false;
                    dataGridView1.DataSource = null;
                    dataGridView1.Refresh();
                }


                // Exit from the application  
                app.Quit();
                
            }
        else
            {
                MessageBox.Show("Datagridview is empty! Nothing to save");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DtServer = pg_Connect.connect_database();
            string connstring = DtServer.dt_connection;
            bool conn_True = DtServer.fileExist;
            if (conn_True)
            {
                try
                {

                    connection = new NpgsqlConnection(connstring);
                
                    command = new NpgsqlCommand("SELECT * from public.student", connection);
                   NpgsqlDataAdapter NpgsqlDA = new NpgsqlDataAdapter();
                    NpgsqlDA.SelectCommand = command;
                    DataTable dbdataset = new DataTable();
                    NpgsqlDA.Fill(dbdataset);
                    BindingSource bsource = new BindingSource();
                    bsource.DataSource = dbdataset;
                    dataGridView1.DataSource = bsource;
                    NpgsqlDA.Update(dbdataset);
                    dataGridView1.AllowUserToAddRows = false;
                    data_load_from_excel_file = false;

                }
                catch (Exception msg)
                {
                    MessageBox.Show("You can't connect with database!Please chek data connections saved in the file and try again! "+ "Server=127.0.0.1; Port=5432; User Id=postgres; Password=b2b4cc1b2; Database=DataStudent;");
                    // MessageBox.Show(msg.Message);

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
            dataGridView1.DataSource = null;
            string pathconn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + textBox_path.Text + ";Extended Properties=\"Excel 12.0; HDR=YES;\" ; ";
            OleDbConnection conn = new OleDbConnection(pathconn);
            //foreach ( sheet in workbook.Sheets)
            //{
            //    if (sheet.Name.equals("sheetName"))
            //    {
            //        //do something
            //    }
            //}
            try
            {
                OleDbDataAdapter mydataadapter = new OleDbDataAdapter("Select * from [" + textBox_sheet.Text + "$]", conn);
                DataTable dt = new DataTable();
                mydataadapter.Fill(dt);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Name of the sheet is incorect!Please write correct name of excel sheet like it is in your workbook!" );
            }
            data_load_from_excel_file = true;

        }
        // update table in database from gridview
        private void button8_Click(object sender, EventArgs e)
        {
            if (dataGridView1.DataSource != null)
            {
                DtServer = pg_Connect.connect_database();
                string connstring = DtServer.dt_connection;
                bool conn_True = DtServer.fileExist;
                if (conn_True)
                {
                    if (data_load_from_excel_file == true)
                    {
                     
                        // ValidFileCheck result = Validate_File(); ketu fillon
                        if (Validate_File())
                        {
                            bool dt_saved_ok = true;
                            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                            {
                                // if (dataGridView1.Rows[i].Cells[0].Value.ToString() != "" && dataGridView1.Rows[i].Cells[1].Value.ToString() != "" && dataGridView1.Rows[i].Cells[2].Value.ToString() != "" && dataGridView1.Rows[i].Cells[3].Value.ToString() != "" && dataGridView1.Rows[i].Cells[4].Value.ToString() != "")
                                //{
                                string Query = "insert into public.student (first_name,last_name,meadle_name,studying) values('" + dataGridView1.Rows[i].Cells[0].Value + "','" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + dataGridView1.Rows[i].Cells[3].Value + "');";
                                try
                                {
                                    connection = new NpgsqlConnection(connstring);
                                    command = new NpgsqlCommand(Query, connection);

                                // NpgsqlDataReader dataReader;
                               
                                    connection.Open();
                                    dataReader = command.ExecuteReader();

                                    connection.Close();
                                  //  MessageBox.Show("Data saved to the database!");
                                    while (dataReader.Read())
                                    {

                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("You can't connect with database and for this reason you can not save this data!Please chek data connections saved in the file and try again");
                                    dt_saved_ok = false;
                                   // MessageBox.Show(ex.Message);
                                }

                                
                            }
                            if (dt_saved_ok)
                            {
                                MessageBox.Show("Data saved to the database!");
                                dataGridView1.DataSource = null;
                                dataGridView1.Refresh();
                            }
                        }
                        else
                        {
                            if (file_excel_formated_ok == true)
                            {
                                MessageBox.Show("Cell must contain only letter and must be not empty!In the RED line to the DataGridView you have wrong data to one or more cell! The data can't insert to the data base");
                            }
                            else
                            {
                                MessageBox.Show("As you can see in the gridview : Sheet File Excel that you select has wrong Data");
                            }
                        }
                        // catch (Exception ex)
                        //{
                        //    MessageBox.Show(ex.Message);
                        //}
                        //ketu mbaron
                    }
                    else
                    {
                        if (data_load_from_excel_file == false)
                        {
                            MessageBox.Show("This date are imported from database and can not be ADD again! You can do Update by cklick on cell and edit Value!");

                        }
                    }
                }
                else
                {
                    MessageBox.Show("Connection to dataBase has Failed Because File with data connections not Exist or name of the file has changed!");
                }
            }
            else
            {
                MessageBox.Show("Datagridview is empty! Nothing to add to the database");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {      
                DtServer = pg_Connect.connect_database();
            string connstring = DtServer.dt_connection;
            bool conn_True = DtServer.fileExist;
            if (conn_True)
            {
                try
                {
                    connection = new NpgsqlConnection(connstring);
               
                    connection.Open();
                     command = new NpgsqlCommand("SELECT * FROM public.student", connection);
               
                     dataReader = command.ExecuteReader();
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
                    MessageBox.Show("You can't connect with database and you can not save data to the text File!Please chek data connections saved in the file and try again! "+ "Server=127.0.0.1; Port=5432; User Id=postgres; Password=b2b4cc1b2; Database=DataStudent;");
                    //MessageBox.Show(msg.ToString());
                    //throw;
                }
            }
            else
            {
                MessageBox.Show("Connection to dataBase has Failed Because File with data connections not Exist or name of the file has changed!");
            }
        }

        private void button10_Click(object sender, EventArgs e)
        { if (!data_load_from_excel_file)
            {
                DtServer = pg_Connect.connect_database();
                string connstring = DtServer.dt_connection;
                bool conn_True = DtServer.fileExist;
                if (dataGridView1.DataSource != null || row_selected == 1)
                {
                    if (conn_True)
                    {
                        if (id_text.Text.ToString() != "")
                        {
                            if (firstname_txt.Text != "" && meadlename_txt.Text != "" && secondname_txt.Text != "" && studying_txt.Text != "")
                            {
                                //string s1 = dataGridView1.Rows[row_selected].Cells[0].Value.ToString();
                                //string s2 = dataGridView1.Rows[row_selected].Cells[1].Value.ToString();
                                //string s3 = dataGridView1.Rows[row_selected].Cells[2].Value.ToString();
                                //string s4 = dataGridView1.Rows[row_selected].Cells[3].Value.ToString();
                                if (firstname_txt.Text != dataGridView1.Rows[row_selected].Cells[0].Value.ToString() || secondname_txt.Text != dataGridView1.Rows[row_selected].Cells[1].Value.ToString() || meadlename_txt.Text != dataGridView1.Rows[row_selected].Cells[2].Value.ToString() || studying_txt.Text != dataGridView1.Rows[row_selected].Cells[3].Value.ToString())
                                {
                                    string Query = "update public.student set first_name='" + this.firstname_txt.Text + "',last_name='" + this.secondname_txt.Text + "',meadle_name='" + this.meadlename_txt.Text + "',studying='" + this.studying_txt.Text + "'where id = '" + Convert.ToInt32(this.id_text.Text.ToString()) + "';";
                                    try
                                    {
                                        connection = new NpgsqlConnection(connstring);
                                        command = new NpgsqlCommand(Query, connection);
                                        // NpgsqlDataReader dataReader;

                                        connection.Open();
                                        dataReader = command.ExecuteReader();
                                        // dataGridView1.Rows[row_selected].Cells[0].Value= firstname_txt.Text;
                                        MessageBox.Show("Data saved to the database!");
                                        //fshirja e fushave pasi behet update ne database
                                        dataGridView1.Rows.RemoveAt(row_selected);
                                        firstname_txt.Clear(); meadlename_txt.Clear();
                                        secondname_txt.Clear(); studying_txt.Clear();
                                        id_text.Clear();//dt_put_user = false;

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
                                        MessageBox.Show("You can't connect with database! And for this reason you can not add data to the databas.Please chek data connections saved in the file and try again");
                                        // MessageBox.Show(ex.Message);
                                    }

                                }
                                else
                                {
                                    MessageBox.Show("Nothing to update!No change are made in the selected row!");
                                    firstname_txt.Clear();
                                    secondname_txt.Clear();
                                    meadlename_txt.Clear();
                                    studying_txt.Clear();
                                    id_text.Clear();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Please fill all the fields in order to insert the data into the database");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Nothing to update! You can make update after you select on row in the gridview");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Connection to dataBase has Failed Because File with data connections not Exist or name of the file has changed!");
                    }
                }

            }
            else { MessageBox.Show("Nothing to update! Data are not came from data base and can't do update!"); }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //  int id= int.Parse(id_text.Text.ToString());             
            //   string Query = "delete from public.student where id =" + id + ";";
            if (!data_load_from_excel_file)
            {
                DtServer = pg_Connect.connect_database();
                string connstring = DtServer.dt_connection;
                bool conn_True = DtServer.fileExist;
                if (dataGridView1.DataSource != null || row_selected == 1)
                {
                    if (conn_True)
                    {
                        if (id_text.Text.ToString() != "")
                        {
                            //if (firstname_txt.Text != "" && meadlename_txt.Text != "" && secondname_txt.Text != "" && studying_txt.Text != "")
                            //{
                            string Query = "delete from public.student where id = '" + int.Parse(this.id_text.Text.ToString()) + "';";
                            try
                            {
                                connection = new NpgsqlConnection(connstring);
                                command = new NpgsqlCommand(Query, connection);
                                // NpgsqlDataReader dataReader;

                                connection.Open();
                                dataReader = command.ExecuteReader();
                                MessageBox.Show("Data deleted to the database!");
                                // int row_index = dataGridView1.CurrentCell.RowIndex;
                                // row selected cleared
                                dataGridView1.Rows.RemoveAt(row_selected);
                                //fshirja e fushave pasi behet delete ne database
                                firstname_txt.Clear(); meadlename_txt.Clear();
                                secondname_txt.Clear(); studying_txt.Clear();
                                id_text.Clear();//dt_put_user = false;

                                while (dataReader.Read())
                                {

                                }
                            }
                            catch (Exception ex)
                            {
                                //  MessageBox.Show("You can't connect with database! And for this reason you can not add data to the databas.Please chek data connections saved in the file and try again");
                                MessageBox.Show(ex.Message);
                            }


                        }
                        else
                        {
                            MessageBox.Show("Nothing to delete! You can delete after you select on row in the gridview!");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Connection to dataBase has Failed Because File with data connections not Exist or name of the file has changed!");
                    }
                }
            }
            else { MessageBox.Show("Nothing to Delete! Data are not came from data base and can't do delete!"); }


        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        { //vlera e cell e ndryshuar
            valueChange = this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null&&NOvalueChange!=valueChange&&data_load_from_excel_file==false)
            {
                DialogResult result = MessageBox.Show("Do you want to save changes?", "Confirmation", MessageBoxButtons.YesNo);
                  if (result == DialogResult.Yes&&valueChange!="" && dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().All(c => char.IsLetter(c)))
                {
                   
                    DtServer = pg_Connect.connect_database();
                    string connstring = DtServer.dt_connection;
                     bool conn_True = DtServer.fileExist;

                    if (conn_True)
                    {  
                            connection = new NpgsqlConnection(connstring);
                            int Column_index = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].ColumnIndex.ToString());
                            int value_of_id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                      
                            switch (Column_index)
                            {
                                case 0:
                                   
                                    string Query = "update public.student set first_name='" + valueChange + "'where id = '" + value_of_id + "';";
                                    command = new NpgsqlCommand(Query, connection);
                                
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
                                    dataGridView1.Refresh();
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
                   //cell-it ne datagridview i jepet vlera e qe kishte perpara setetentohej modifikimi
                   this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value=NOvalueChange;
                    dataGridView1.Refresh();
                }
                else if (valueChange=="")
                {
                    MessageBox.Show("You can not add empty field to the database!");
                   // valueChange = valuetemp;
                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = NOvalueChange;
                    dataGridView1.Refresh();
                }
                else if (!dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().All(c => char.IsLetter(c)))
                { MessageBox.Show("Is not valid update cell!Please use letter");
                    this.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = NOvalueChange;
                    dataGridView1.Refresh();
                }

            }
                       
          
        }

      

        private void firstname_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if(e.KeyChar>='0'&&e.KeyChar<='9')
            {
                e.Handled = true; 
            }
            else
            {
                e.Handled = false; 
            }
        }

        private void secondname_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = true; 
            }
            else
            {
                e.Handled = false;
            }
        }

        private void meadlename_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
        }

        private void studying_txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar >= '0' && e.KeyChar <= '9')
            {
                e.Handled = true; 
            }
            else
            {
                e.Handled = false;
            }
        }

        private void id_text_KeyPress(object sender, KeyPressEventArgs e)
        {
            id_text.ReadOnly = true; id_text.Enabled = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
           // tbDataItems.Clear();
            //textBox_path.Clear();
           // textBox_sheet.Clear();

        }

        private void button13_Click(object sender, EventArgs e)
        {
            firstname_txt.Clear();
            secondname_txt.Clear();
            meadlename_txt.Clear();
            studying_txt.Clear();
            id_text.Clear();
        }

        



        //nese nuk ndodh ndryshimi ne cell
        private void cellclick(object sender, DataGridViewCellEventArgs e)
        {   row_selected = e.RowIndex;
            dataGridView1.Columns[4].ReadOnly = true;
            if (e.ColumnIndex == 4)
            {
                MessageBox.Show("This column is read only and can not be changed!");
            }
            else if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {//ruajtja e vleres ne momentin e cklikimit ne cell
                NOvalueChange = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            }//zgjedhja e nje rreshti ne datagrid view ne rast se duam te bejme update te ndonjeres nga fushat
            else if (e.ColumnIndex<0&&e.RowIndex>=0)
            {
                DataGridViewRow rowselected = dataGridView1.Rows[row_selected];
                firstname_txt.Text = rowselected.Cells[0].Value.ToString();
                secondname_txt.Text = rowselected.Cells[1].Value.ToString();
                meadlename_txt.Text = rowselected.Cells[2].Value.ToString();
                studying_txt.Text = rowselected.Cells[3].Value.ToString();
                id_text.Text= rowselected.Cells[4].Value.ToString();
                
            }
        }
    }
}
