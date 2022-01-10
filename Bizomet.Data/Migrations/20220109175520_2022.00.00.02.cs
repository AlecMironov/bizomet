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
					{ "0feb7790-a619-40e2-88b7-ebda1bc53cb9", "24bdf40d-de50-45f2-b504-0b005d279de8", "Talent", "TALENT" },
					{ "1a42289f-6b6f-4c1d-ad82-92c0a993122f", "08d3710a-6d25-4df0-b527-8a4f06223f15", "MediaAssistant", "MEDIAASSISTANT" },
					{ "53f55fb0-cce4-4d63-901a-49a7defe04f2", "b1b0acdb-c48d-4806-b9a9-8331f8b25cdc", "Lifter", "LIFTER" },
					{ "98509d4a-e3bd-4df7-bd68-736584abd1dd", "48427531-d906-46d9-92b6-20940cce2f3e", "Promoter", "PROMOTER" },
					{ "b8039f27-f4bc-49aa-bed5-bd3c1ba4cff3", "f5ed3627-8a92-4921-ae65-64fdb0bf1cf9", "Administrator", "ADMINISTRATOR" }
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "0feb7790-a619-40e2-88b7-ebda1bc53cb9");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "1a42289f-6b6f-4c1d-ad82-92c0a993122f");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "53f55fb0-cce4-4d63-901a-49a7defe04f2");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "98509d4a-e3bd-4df7-bd68-736584abd1dd");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "b8039f27-f4bc-49aa-bed5-bd3c1ba4cff3");
		}
	}
}
