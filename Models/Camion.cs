namespace Vehiculo
{
	// Importación namespaces externos.

	public class Camion
	{
		// Declaración de Variables de Clase
		public int Id { get; set; }
		public string Nombre { get; set; }
		public long Nombre_Date_LastEdit { get; set; }
		public string Tipo { get; set; }
		public long Tipo_Date_LastEdit { get; set; }
		public int Capacidad { get; set; }
		public long Capacidad_Date_LastEdit { get; set; }

		// Constructores
		public Camion()
		{
			//Se requiere un constructor vacio para que el proyecto pueda crear nuevos elementos en la medida en que se va recibiendo la información de cada elemento desde la base de datos.
		}

		public Camion(string Nombre, string Tipo, int Capacidad, long Date_All_LastEdit)
		{
			this.Nombre = Nombre;
			this.Nombre_Date_LastEdit = Date_All_LastEdit;
			this.Tipo = Tipo;
			this.Tipo_Date_LastEdit = Date_All_LastEdit;
			this.Capacidad = Capacidad;
			this.Capacidad_Date_LastEdit = Date_All_LastEdit;
		}

		public Camion(Camion camion)
		{
			this.Nombre = camion.Nombre;
			this.Nombre_Date_LastEdit = camion.Nombre_Date_LastEdit;
			this.Tipo = camion.Tipo;
			this.Tipo_Date_LastEdit = camion.Tipo_Date_LastEdit;
			this.Capacidad = camion.Capacidad;
			this.Capacidad_Date_LastEdit = camion.Capacidad_Date_LastEdit;
		}

		// Declaración de Métodos de Clase
		internal static string Get_DateColumn(int sel_column) => Get_NameColumn(sel_column) + "_Date_LastEdit";

		private static string Get_NameColumn(int sel_column)
		{
			switch(sel_column)
			{
				case 1:
					return "Nombre";
				case 3:
					return "Tipo";
				case 5:
					return "Capacidad";
				default:
					return "";
			}
		}

		internal string Get_ValueColumn(int sel_column)
		{
			string Name_Column = "";
			switch(sel_column)
			{
				case 1:
					Name_Column = this.Nombre;
					break;
				case 3:
					Name_Column = this.Tipo;
					break;
				case 5:
					Name_Column = this.Capacidad.ToString();
					break;
			}
			return Name_Column;
		}

		internal void Set_ValueColumn(int columnIndex, string Value)
		{
			switch(columnIndex)
			{
				case 1:
					this.Nombre = Value;
					break;
				case 3:
					this.Tipo = Value;
					break;
				case 5:
					this.Capacidad = int.Parse(Value);
					break;
			}
		}
	}
}