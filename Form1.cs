using Form_BDSQlite;

using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
				dataGridView1.Rows.Add(camion.Nombre, camion.tipo, camion.capacidad);
			#endregion
		}

		private List<Camion> Generar_Datos()
		{
			return new List<Camion>()
			{
				new Camion("Tesla", Camion_Tipo.Persona, 5),
				new Camion("Ford", Camion_Tipo.Carga, 50),
			};
		}

		private void btn_Crear_Dato(object sender, EventArgs e)
		{

		}

		private void btn_Modificar_Dato(object sender, EventArgs e)
		{

		}

		private void btn_Eliminar_Dato(object sender, EventArgs e)
		{

		}
	}
}
