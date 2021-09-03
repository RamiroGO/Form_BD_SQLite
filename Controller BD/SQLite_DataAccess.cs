namespace Form_BD_SQLite.Models
{
	// Importación namespaces externos.
	using Vehiculo;
	using System.Collections.Generic;
	using System.Configuration;   // Referencia añadida: No tiene efecto en el proyecto actual sino en la exportación de la Dll.
	using System.Data;            // Nuget: IDbConnection
	using System.Data.SQLite;     // Nuget: SQLiteConnection
	using Dapper;                 // Nuget: Permite hacer consultas con lenguaje Query
	using System.Linq;

	public class SQLite_DataAccess
	{
		/// <summary>
		/// Se buscara en el archivo del Proyecto "App.config" por la información contenida en la etiqueta "connectionStrings" para localizar y acceder a la base de datos SQLite; Bajo un nombre "Default" y de nombre "DemoDB.db".
		/// </summary>
		/// <returns></returns>
		private static SQLiteConnection SQLiteConnection_Loaded() => new SQLiteConnection(
			  ConfigurationManager.ConnectionStrings["Default"].ConnectionString);

		/// <summary>
		/// Establecer Conección con la Base de Datos para hacer una consulta de sus elementos.
		/// </summary>
		/// <returns></returns>
		public static List<Camion> Get_Camiones()
		{
			IEnumerable<Camion> output;
			string consulta_sql = "SELECT * FROM Camion";

			// Establecer Conección con SQLite
			using(IDbConnection connection = SQLiteConnection_Loaded())
				output = connection.Query<Camion>(consulta_sql);
			return output.ToList();
		}

		internal static void Edit_Camion_byId(string Set = "", string Where = "")
		{
			if(!Set.StartsWith("SET ")) Set = "SET " + Set;
			if(!Where.StartsWith("WHERE ")) Where = "WHERE " + Where;
			string consulta_sql = "UPDATE Camion " + Set + " " + Where;

			// Establecer Conección con SQLite
			using(IDbConnection connection = SQLiteConnection_Loaded())
				connection.Execute(consulta_sql);
		}

		internal static void Delete_Camion_byId(int sel_id)
		{
			string consulta_sql = "DELETE FROM Camion WHERE Id = " + sel_id;

			// Establecer Conección con SQLite
			using(IDbConnection connection = SQLiteConnection_Loaded())
				connection.Execute(consulta_sql);
		}

		public static void New_Camion(Camion camion)
		{
			string consulta_sql = "INSERT INTO Camion" +
				" (Nombre, Nombre_Date_LastEdit, Tipo, Tipo_Date_LastEdit, Capacidad, Capacidad_Date_LastEdit) values" +
				" (@Nombre, @Nombre_Date_LastEdit, @Tipo, @Tipo_Date_LastEdit, @Capacidad, @Capacidad_Date_LastEdit)";

			DynamicParameters parameters = new DynamicParameters();

			parameters.AddDynamicParams(camion);

			// Establecer Conección con SQLite
			using(IDbConnection connection = SQLiteConnection_Loaded())
				connection.Execute(consulta_sql, parameters);
		}
	}
}
