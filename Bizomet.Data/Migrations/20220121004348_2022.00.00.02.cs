using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bizomet.Data.Migrations
{
	public partial class _2022000002 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[,]
				{
					{ "42c82834-4d88-42c1-a195-de2f68c8eca0", "92af27f0-ce98-44f7-8c82-7da17638e0b9", "Producer", "PRODUCER" },
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
		}
	}
}
