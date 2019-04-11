using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;


namespace PgSql
{
    public partial class Login_Form : Form
    {
        data_server_connection DtServer = new data_server_connection();
        PostGreSQL pg_Connect = new PostGreSQL();
        private NpgsqlConnection connection;
        //private NpgsqlCommand command;
        //private NpgsqlDataReader dataReader;
        //private object dataItems;

        public Login_Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (textBox1.Text == "user1" && textBox2.Text == "1")
            //{
                DtServer = pg_Connect.connect_database();
                string connstring = DtServer.dt_connection;
                bool conn_True = DtServer.fileExist;
                if (conn_True)
                {
                    try
                    {
                       connection = new NpgsqlConnection(connstring);
                        connection.Open();
                        Form1 frm1 = new Form1();
                        this.Hide();
                        frm1.Show();
                        connection.Close();

                    }
                    catch (Exception msg)
                    {
                        MessageBox.Show("You can't connect with database!Please chek data connections saved in the file and try again!" + "Server=127.0.0.1; Port=5432; User Id=postgres; Password=b2b4cc1b2; Database=DataStudent;");
                        //MessageBox.Show(msg.ToString());
                        //throw;
                        //-------

                    }
                }
                else
                {
                    MessageBox.Show("Connection to dataBase has Failed Because File with data connections not Exist or name of the file has changed!");
                }
            //}
            //else
            //{
            //    MessageBox.Show("Incorrect user name or password!(Or both!)");
            //}
        
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("user name is 'user1' password '1'");
        }

      
    }
}
