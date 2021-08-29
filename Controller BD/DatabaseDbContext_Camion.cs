using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Vehiculo;

namespace Form_BD_SQLite.Controller_BD
{
	class DatabaseDbContext_Camion:DbContext
	{
		public static async Task IniciarAsync()
		{
			List<Camion> camiones = new List<Camion>()
			{
				new Camion("f", "f",2)
			};

			//using (var db = new SQLiteConnection(LoadConnectionString()))
			//{
			//	await db.Database.EnsureCreatedAsync();


			//	foreach (Camion dato in camiones)
			//		db.Camions.Add(dato);

			//	await db.SaveChangesAsync();
			//}
		}



		/***/
		//private string database;

		//internal DatabaseDbContext(string database) => this.database = database;

		//internal DbSet<Beer> Beers { get; set; }
		//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		//{
		//	optionsBuilder.UseSqlite(connectionString: "Filename=" + database, sqliteOptionsAction: op =>
		//	{
		//		op.MigrationsAssembly(
		//			Assembly.GetExecutingAssembly().FullName);
		//	});
		//	base.OnConfiguring(optionsBuilder);
		//}
		//protected override void OnModelCreating(ModelBuilder model_build)
		//{
		//	model_build.Entity<Camion>().ToTable("Beers");
		//	model_build.Entity<Camion>(entity =>
		//	{
		//		entity.HasKey(e => e.BeerId);
		//	});

		//	base.OnModelCreating(model_build);
		//}
		/***/
	}
}
