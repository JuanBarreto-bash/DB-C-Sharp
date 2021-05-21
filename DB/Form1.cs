using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DB {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e) {
            MySqlConnection conexion = new MySqlConnection(
                "server=192.168.1.44;" +
                "userid=root;" +
                "password=1234;" +
                "database=basesita"
            );

            conexion.Open();
            
            MySqlCommand comando = new MySqlCommand();
            comando.Connection = conexion;
            comando.CommandText = "INSERT INTO persona(nombre,apellido,edad,email) VALUES(@nombre,@apellido,@edad,@email)";

            comando.Parameters.AddWithValue("@nombre", txtNombre.Text);
            comando.Parameters.AddWithValue("@apellido", txtApellido.Text);
            comando.Parameters.AddWithValue("@edad", txtEdad.Text);
            comando.Parameters.AddWithValue("@email", txtEmail.Text);

            comando.Prepare();


            comando.ExecuteNonQuery();

            conexion.Close();

            MessageBox.Show("Quedo pronto");
            ListarDesdeDB();
        }

        private void btnListar_Click(object sender, EventArgs e) {
            ListarDesdeDB();

        }

        private void ListarDesdeDB() {

            MySqlConnection conexion = new MySqlConnection(
                           "server=192.168.1.44;" +
                           "userid=root;" +
                           "password=1234;" +
                           "database=basesita"
                       );

            conexion.Open();

            MySqlDataReader lector;

            MySqlCommand comando = new MySqlCommand();
            comando.Connection = conexion;
            comando.CommandText = "SELECT * FROM persona WHERE id > @numero";
            comando.Parameters.AddWithValue("@numero", 0);
            comando.Prepare();


            lector = comando.ExecuteReader();

            DataTable tabla = new DataTable();
            tabla.Load(lector);

            dgLista.DataSource = tabla;

            conexion.Close();
        }

        private void Form1_Load(object sender, EventArgs e) {
            ListarDesdeDB();
        }
    }
}
