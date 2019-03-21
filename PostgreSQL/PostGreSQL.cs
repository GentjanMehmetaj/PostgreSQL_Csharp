using System;
using System.Windows.Forms;
using Npgsql; 
using System.Collections.Generic;

namespace PgSql
{
   public class PostGreSQL
   {
      List<string> dataItems = new List<string>();

      public void PostgreSQL()
      {
      }

      public List<string> PostgreSQLtest1()
      {
         try
         {
            string connstring = "Server=127.0.0.1; Port=5432; User Id=postgres; Password=b2b4cc1b2; Database=DataStudent;";
            NpgsqlConnection connection = new NpgsqlConnection(connstring);
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM public.student", connection);
            NpgsqlDataReader dataReader = command.ExecuteReader();
            for (int i=0; dataReader.Read(); i++)
            {
               dataItems.Add(dataReader[0].ToString() + "   "  + dataReader[1].ToString() + "   " + dataReader[3].ToString() + "    " + dataReader[2].ToString() + "    " + dataReader[4].ToString() + "\r\n");
            }
            connection.Close();
            return dataItems;
         }
         catch (Exception msg)
         {
            MessageBox.Show(msg.ToString());
            throw;
         }
      }

      public List<string> PostgreSQLtest2()
      {
         try
         {
            string connstring = "Server=127.0.0.1; Port=5432; User Id=postgres; Password=b2b4cc1b2; Database=DataStudent;";
            NpgsqlConnection connection = new NpgsqlConnection(connstring);
            connection.Open();
            NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM public.student WHERE id > '4' AND id < '9'", connection);
            NpgsqlDataReader dataReader = command.ExecuteReader();
            for (int i = 0; dataReader.Read(); i++)
            {
               dataItems.Add(dataReader[0].ToString() + "   " + dataReader[1].ToString() + "    " + dataReader[3].ToString() + "     " + dataReader[2].ToString() +"    " +dataReader[4].ToString()+ "\r\n");
            }
            connection.Close();
            return dataItems;
         }
         catch (Exception msg)
         {
            MessageBox.Show(msg.ToString());
            throw;
         }
      }

   }
}
