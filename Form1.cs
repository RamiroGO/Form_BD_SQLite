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
		List<Camion> Camions;
		public Form1()
		{
			#region Inicializr Componentes
			InitializeComponent();
			dataGridView1.Columns.Add("Nombre", "Nombre Camión");
			dataGridView1.Columns.Add("Tipo", "Tipo de Camión");
			dataGridView1.Columns.Add("Capacidad", "Capacidad del Camión");
			#endregion

			Camions = Generar_Datos();

			#region Visualizar en Tabla
			foreach (Camion camion in Camions)
				dataGridView1.Rows.Add(camion.Nombre, camion.Tipo, camion.Capacidad);
			#endregion
		}

		private List<Camion> Generar_Datos()
		{
			return new List<Camion>()
			{
				new Camion("Tesla", "Persona", 5),
				new Camion("Ford", "Carga", 50),
			};
		}

		private void Btn_Crear_Dato(object sender, EventArgs e)
		{
			Camion camion = new Camion("aaaaaa", "Persona",80);
			SQLite_DataAccess.Set_Camion(camion);

			foreach (Camion _camion in SQLite_DataAccess.Get_Camiones())
				dataGridView1.Rows.Add(_camion.Nombre, _camion.Tipo, _camion.Capacidad);
		}

		private void Btn_Modificar_Dato(object sender, EventArgs e)
		{

		}

		private void Btn_Eliminar_Dato(object sender, EventArgs e)
		{

		}
	}
}
