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
		private List<Camion> dataGridView1_backup;
		public Form1()
		{
			#region Inicializar Componentes
			InitializeComponent();
			//dataGridView1.Columns.Add("Id", "Id");
			//dataGridView1.Columns.Add("Date_LastEdit", "Date_LastEdit");
			//dataGridView1.Columns.Add("Nombre", "Nombre Camión");
			//dataGridView1.Columns.Add("Tipo", "Tipo de Camión");
			//dataGridView1.Columns.Add("Capacidad", "Capacidad del Camión");
			#endregion
			Show_Table();
		}

		private void Btn_Crear_Dato(object sender, EventArgs e)
		{
			SQLite_DataAccess.Set_Camion(new Camion(textBox1.Text, textBox2.Text, (int)numericUpDown1.Value, date_last_edit: DateTime.UtcNow.Ticks));
			Show_Table();
		}
		private void Btn_Modificar_Dato(object sender, EventArgs e)
		{
			string Value_now, Value_old, NameColumn;

			for (int corre_column = 0; corre_column != dataGridView1.ColumnCount; corre_column++)
			{
				// Exploración de Columnas
				NameColumn = dataGridView1.Columns[corre_column].Name;
				for (int corre_id = 0; corre_id != dataGridView1.RowCount; corre_id++)
				{
					// Comparación de datos previos con los actuales para identificar ediciones.
					Value_old = dataGridView1_backup[corre_id].Nombre;
					Value_now = dataGridView1[NameColumn, corre_id].Value.ToString();
					if (Value_old != Value_now) // Optimizar: Actuar si los valores son diferentes.
						if ((long)dataGridView1["Date_LastEdit", corre_id].Value > dataGridView1_backup[corre_id].Date_LastEdit)
							SQLite_DataAccess.Edit_Camion_Set_Id(_set: NameColumn + " = '" + Value_now + "'", _where: "WHERE Id = " + (corre_id + 1).ToString("#") + ";");
						else
						{
							// Si la edición del backup es más reciente que la manipulación en esta interfaz, entonces esta se tiene que actualizar con el backup.
							//dataGridView1[NameColumn, corre_id].Value = dataGridView1_backup[corre_id];
							dataGridView1["Date_LastEdit", corre_id].Value = dataGridView1_backup[corre_id].Date_LastEdit;
						}
				}
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
			// Limpiar tabla y evidencias de cambios.
			dataGridView1.Rows.Clear();
			dataGridView1_backup = new List<Camion>();
			// Guardar original y copia
			foreach (Camion camion in SQLite_DataAccess.Get_Camiones())
			{
				dataGridView1.Rows.Add(camion.Id, camion.Nombre, camion.Tipo, camion.Capacidad, camion.Date_LastEdit);
				dataGridView1_backup.Add(new Camion(camion.Nombre, camion.Tipo, camion.Capacidad, camion.Date_LastEdit));
			}
		}

		private List<Camion> Generar_Datos()
		{
			return new List<Camion>()
			{
				new Camion("Persona", "Tesla",5,DateTime.UtcNow.Ticks),
				new Camion("Ford", "Carga", 50, DateTime.UtcNow.Ticks),
			};
		}

		private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex > 0)
				dataGridView1["Date_LastEdit", e.RowIndex].Value = long.Parse(DateTime.UtcNow.Ticks.ToString());
		}
	}
}
