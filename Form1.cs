using Form_BD_SQLite;
using Form_BD_SQLite.Models;

using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Vehiculo;

namespace Form_BD_SQLite
{
	public partial class Form1 : Form
	{
		private List<Camion> grid_camiones;
		public Form1()
		{
			#region Inicializar Componentes
			InitializeComponent();
			dataGridView1.Columns.Add("Id", "Id");
			dataGridView1.Columns.Add("Nombre", "Nombre Camión");
			dataGridView1.Columns.Add("Tipo", "Tipo de Camión");
			dataGridView1.Columns.Add("Capacidad", "Capacidad del Camión");
			#endregion
			Show_Table();
		}

		private void Btn_Crear_Dato(object sender, EventArgs e)
		{
			SQLite_DataAccess.Set_Camion(new Camion(textBox1.Text, textBox2.Text, (int)numericUpDown1.Value));
			Show_Table();
		}
		private void Btn_Modificar_Dato(object sender, EventArgs e)
		{
			for (int i = 0; i != dataGridView1.RowCount; i++)
			{

				//SQLite_DataAccess.Edit_Camion_Set_Id(i);
			}

			Show_Table();
		}
		private void Btn_Eliminar_Dato(object sender, EventArgs e)
		{
			SQLite_DataAccess.Delete_Camion_byId((int)numericUpDown2.Value);
			Show_Table();
		}

		/// <summary>
		/// Visualizar lista en la tabla
		/// </summary>
		private void Show_Table()
		{
			dataGridView1.Rows.Clear();
			grid_camiones = new List<Camion>();
			foreach (Camion camion in SQLite_DataAccess.Get_Camiones())
			{
				dataGridView1.Rows.Add(camion.Id, camion.Nombre, camion.Tipo, camion.Capacidad);
				grid_camiones.Add(new Camion(camion.Nombre, camion.Tipo, camion.Capacidad));
			}
		}

		private List<Camion> Generar_Datos()
		{
			return new List<Camion>()
			{
				new Camion("Tesla", "Persona", 5),
				new Camion("Ford", "Carga", 50),
			};
		}
	}
}
