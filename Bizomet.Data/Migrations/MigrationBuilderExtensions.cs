using System.Reflection;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bizomet.Data.Migrations
{
	internal static class MigrationBuilderExtensions
	{
		public static MigrationBuilder DropStoredProcedureIfExists(this MigrationBuilder builder, string shema, string storedProcedureName)
		{
			builder.Sql(
				$@"IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND OBJECT_ID = OBJECT_ID('{shema}.{storedProcedureName}'))
					DROP PROCEDURE [{shema}].[{storedProcedureName}]
				GO");

			return builder;
		}

		public static MigrationBuilder RunFile(this MigrationBuilder builder, string filename)
		{
			if (builder == null)
				throw new ArgumentNullException(nameof(builder));

			var assembly = Assembly.GetExecutingAssembly();
			var resourceName = "Bizomet.Data.Migrations.Scripts." + filename;

			using (Stream stream = assembly.GetManifestResourceStream(resourceName))
			using (StreamReader reader = new StreamReader(stream)) {
				string result = reader.ReadToEnd();
				builder.Sql(result, true);
			}

			return builder;
		}
	}
}
