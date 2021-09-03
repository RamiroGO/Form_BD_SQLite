namespace Form_BD_SQLite
{
	using Form_BD_SQLite.Models;

	using System;
	using System.Collections.Generic;
	using System.Windows.Forms;

	using Vehiculo;

	public partial class Form1: Form
	{
		#region Declaración de Variables de Clase
		private List<Camion> dataGridView1_backup;
		// activación de evento
		private bool dataGridView1_actual_CellValueChanged_busy = false;
		#endregion

		#region Constructor del Main
		public Form1()
		{
			this.InitializeComponent();
			this.BD_GetAndShow_Table();
		}
		#endregion
		#region Eventos de Botones
		private void Btn_Crear_Dato(object sender, EventArgs e)
		{
			SQLite_DataAccess.New_Camion(new Camion(this.textBox1.Text, this.textBox2.Text, (int)this.numericUpDown1.Value, Date_All_LastEdit: DateTime.UtcNow.Ticks));
			this.BD_GetAndShow_Table();
		}
		/// <summary>
		/// Ejecutar una revisión de modificaciones en la Tabla actual
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Btn_Modificar_Dato(object sender, EventArgs e)
		{
			// Desactivar evento por edición de tabla dataGridView1_actual.
			this.dataGridView1_actual_CellValueChanged_busy = true;  
			
			// Revización de Filas y Columnas
			for(int corre_column = 1; corre_column != this.dataGridView1_actual.ColumnCount; corre_column += 2)
				for(int corre_id = 0; corre_id != this.dataGridView1_actual.RowCount; corre_id++)
					this.BD_Edit_Cell(corre_id, corre_column);

			this.BD_GetAndShow_Table();
		}
		private void Btn_Eliminar_Dato(object sender, EventArgs e)
		{
			SQLite_DataAccess.Delete_Camion_byId((int)this.numericUpDown2.Value);
			this.BD_GetAndShow_Table();
		}
		#endregion
		#region Evento de Tabla DataGridView
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
				this.dataGridView1_actual_CellValueChanged_busy = true;
				// Cambiar un solo valor de la tabla
				this.BD_Edit_Cell(e.RowIndex, e.ColumnIndex);
				// Dado que el dato recien modificado en la Base de Datos se está correspondiendo con el de la tabla
				// se puede igual su Date/momento de edición tanto la tabla en la Base de Datos como en el backup local, para no reconocer tales cambios.
				this.dataGridView1_backup[e.RowIndex].Set_ValueColumn(e.ColumnIndex, Value: this.dataGridView1_actual[e.ColumnIndex, e.RowIndex].Value.ToString());
				this.dataGridView1_actual_CellValueChanged_busy = false;
			}
		}
		#endregion
		/// <summary>
		/// Visualizar lista en la tabla
		/// </summary>
		private void BD_GetAndShow_Table()
		{
			// Desactivar evento por edición de tabla dataGridView1_actual.
			this.dataGridView1_actual_CellValueChanged_busy = true;

			// Limpiar tabla y evidencias de cambios.
			this.dataGridView1_actual.Rows.Clear();
			this.dataGridView1_backup = new List<Camion>();
			// Guardar original y copia
			foreach(Camion camion in SQLite_DataAccess.Get_Camiones())
			{
				this.dataGridView1_actual.Rows.Add(
					camion.Id,
					camion.Nombre,
					camion.Nombre_Date_LastEdit,
					camion.Tipo,
					camion.Tipo_Date_LastEdit,
					camion.Capacidad,
					camion.Capacidad_Date_LastEdit);

				this.dataGridView1_backup.Add(new Camion(camion));
			}

			// Reactivar evento por edición de tabla dataGridView1_actual.
			this.dataGridView1_actual_CellValueChanged_busy = false;
		}

		private void BD_Edit_Cell(int rowIndex, int columnIndex)
		{
			string
				NameColumn = this.dataGridView1_actual.Columns[columnIndex].Name,
				NameColumn_Date = this.dataGridView1_actual.Columns[columnIndex + 1].Name,
				valu_old = this.dataGridView1_backup[rowIndex].Get_ValueColumn(columnIndex),
				valu_now = this.dataGridView1_actual.Rows[rowIndex].Cells[columnIndex].Value.ToString();
			// Comprobar la existencia de un evento de edición.
			if(valu_now != valu_old)
			{
				// Guardar el Date/Momento de la edición.
				string time_utc = DateTime.UtcNow.Ticks.ToString();
				this.dataGridView1_actual[Camion.Get_DateColumn(columnIndex), rowIndex].Value = long.Parse(time_utc);

				// Realización de la consulta para editar en el momento de confirmar la terminación de la edición de la celda.
				SQLite_DataAccess.Edit_Camion_byId(
					Set: NameColumn + " = '" + valu_now + "', " +
					NameColumn_Date + " = '" + time_utc + "'",
					Where: "WHERE Id = " + (rowIndex + 1).ToString("#") + ";");
			}
		}
	}
}
