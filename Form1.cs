using Form_BD_SQLite;
using Form_BD_SQLite.Models;

using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Vehiculo;

namespace Form_BD_SQLite
{
	public partial class Form1: Form
	{
		private List<Camion> dataGridView1_backup;
		public Form1()
		{
			InitializeComponent();
			Show_Table();
		}

		private void Btn_Crear_Dato(object sender, EventArgs e)
		{
			SQLite_DataAccess.New_Camion(new Camion(textBox1.Text, textBox2.Text, (int)numericUpDown1.Value, Date_All_LastEdit: DateTime.UtcNow.Ticks));
			Show_Table();
		}
		private void Btn_Modificar_Dato(object sender, EventArgs e)
		{
			// Revización de Filas y Columnas
			for(int corre_column = 1; corre_column != dataGridView1_actual.ColumnCount; corre_column += 2)
				for(int corre_id = 0; corre_id != dataGridView1_actual.RowCount; corre_id++)
					this.Edit_Cell(corre_id, corre_column);
			Show_Table();
		}
		private bool dataGridView1_actual_CellValueChanged_busy = false;
		/// <summary>
		/// Guardar momento en que los valores de celdas han sido cambiados.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DataGridView1_actual_CellValueChanged(Object sender, DataGridViewCellEventArgs e)
		{
			// Confirmación de que la tabla ya esté armada
			if(e.RowIndex > 0 && !dataGridView1_actual_CellValueChanged_busy)
			{
				dataGridView1_actual_CellValueChanged_busy = true;
				this.Edit_Cell(e.RowIndex, e.ColumnIndex);
				dataGridView1_actual_CellValueChanged_busy = false;
			}
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
			// Desactivar evento por edición de tabla dataGridView1_actual.
			dataGridView1_actual_CellValueChanged_busy = true;

			// Limpiar tabla y evidencias de cambios.
			dataGridView1_actual.Rows.Clear();
			dataGridView1_backup = new List<Camion>();
			// Guardar original y copia
			foreach(Camion camion in SQLite_DataAccess.Get_Camiones())
			{
				dataGridView1_actual.Rows.Add(
					camion.Id,
					camion.Nombre,
					camion.Nombre_Date_LastEdit,
					camion.Tipo,
					camion.Tipo_Date_LastEdit,
					camion.Capacidad,
					camion.Capacidad_Date_LastEdit);

				dataGridView1_backup.Add(new Camion(camion));
			}

			// Reactivar evento por edición de tabla dataGridView1_actual.
			dataGridView1_actual_CellValueChanged_busy = false;
		}

		private void Edit_Cell(int rowIndex, int columnIndex)
		{
			String val_old = this.dataGridView1_backup[rowIndex].Get_ValueColumn(columnIndex);
			String val_now = dataGridView1_actual.Rows[rowIndex].Cells[columnIndex].Value.ToString();
			// Comprobar la existencia de un evento de edición.
			if(val_now != val_old)
			{
				// Guardar el Date/Momento de la edición.
				this.dataGridView1_actual[Camion.Get_DateColumn(columnIndex), rowIndex].Value = long.Parse(DateTime.UtcNow.Ticks.ToString());

				string
					NameColumn = this.dataGridView1_actual.Columns[columnIndex].Name,
					Value_now = this.dataGridView1_actual[NameColumn, rowIndex].Value.ToString(); ;
				// Realización de la consulta para editar en el momento de confirmar la terminación de la edición de la celda.
				SQLite_DataAccess.Edit_Camion_Set_Id(
					_set: NameColumn + " = '" + Value_now + "'",
					_where: "WHERE Id = " + (rowIndex + 1).ToString("#") + ";");
			}
		}
	}
}
