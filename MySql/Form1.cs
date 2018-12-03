using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MySql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
            

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            using (var conn = new MySqlConnection("Server=localhost;Database=kutya;Uid=root;Pwd="))
            {
                conn.Open();

                string nev = NevTextBox.Text;
                string fajta = FajtaTextBox.Text;
                DateTime orokbe = dateTimePicker1.Value;

                var lekerdezesCommand = conn.CreateCommand();
                lekerdezesCommand.CommandText = @"select count(*) from kutyak where nev=@nev";
                lekerdezesCommand.Parameters.AddWithValue("@nev", nev);
                long db=(long) lekerdezesCommand.ExecuteScalar();
                if (db > 0)
                {
                    MessageBox.Show("Ilyen kutya mar van");
                    return;
                }

                var command = conn.CreateCommand();
                command.CommandText= @"INSERT INTO `kutya`.`kutyak` (`nev`, `fajta`, `orokbefogadas_datum`) 
                                VALUES (@nev, @fajta, @orokbe)";
                command.Parameters.AddWithValue("@nev", nev);
                command.Parameters.AddWithValue("@fajta", fajta);
                command.Parameters.AddWithValue("@orokbe", orokbe);
                command.ExecuteNonQuery();    
                
                        
            };
            
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            using (var conn = new MySqlConnection("Server=localhost;Database=kutya;Uid=root;Pwd="))
            {
                conn.Open();
                string nev = NevTextBox.Text;

                          
                var deleteComm = conn.CreateCommand();
                deleteComm.CommandText = @"DELETE FROM kutyak WHERE nev =@nev";
                deleteComm.Parameters.AddWithValue("@nev", nev);
                int num=deleteComm.ExecuteNonQuery();

                if (num == 0)
                {
                    MessageBox.Show("Nem volt ilyen adat amit törölni lehetett volna");
                }
                else
                {
                    MessageBox.Show("Sikeresen törölve");
                }

            };
        }

        private void Update_Click(object sender, EventArgs e)
        {
            using (var conn = new MySqlConnection("Server=localhost;Database=kutya;Uid=root;Pwd="))
            {
                conn.Open();
                string nev = NevTextBox.Text;
                string fajta = FajtaTextBox.Text;
                DateTime orokbe = dateTimePicker1.Value;

                

                var command = conn.CreateCommand();
                command.CommandText = @"UPDATE kutyak SET fajta=@fajta, orokbefogadas_datum=@orokbe WHERE nev=@nev"; 
                               
                command.Parameters.AddWithValue("@nev", nev);
                command.Parameters.AddWithValue("@fajta", fajta);
                command.Parameters.AddWithValue("@orokbe", orokbe);
                int num=command.ExecuteNonQuery();
                

                if (num == 0)
                {
                    MessageBox.Show("Nem volt ilyen adat amit módostani lehetett volna");
                }
                else
                {
                    MessageBox.Show("Módosítva");
                }

            };
        }
    }
}
