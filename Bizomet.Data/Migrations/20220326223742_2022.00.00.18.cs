using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bizomet.Data.Migrations
{
	public partial class _2022000018 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.RunFile("countries_data.sql");
			migrationBuilder.RunFile("states_data.sql");
			migrationBuilder.RunFile("cities_data.sql");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
		}
	}
}
