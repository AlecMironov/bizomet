using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bizomet.Data.Migrations
{
	public partial class _2022000001 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<string>(
				name: "FirstName",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: false,
				defaultValue: "");

			migrationBuilder.AddColumn<string>(
				name: "LastName",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: false,
				defaultValue: "");

			migrationBuilder.CreateTable(
				name: "Companies",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
					Address = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
					Country = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Companies", x => x.Id);
				});

			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[,]
				{
					{ "41980fe2-3c75-46b4-81f6-3f1a8d48a316", "5184b893-f964-450d-8175-5ba47d6023b3", "Lifter", "LIFTER" },
					{ "6248a7d8-95be-4c7b-9a32-5d981252eafb", "c9410b69-b73a-4734-ac7c-34ef4c053616", "Administrator", "ADMINISTRATOR" },
					{ "6808df6c-4cf1-412f-9789-e9bafcec4142", "38254412-ec65-4077-ba23-fe0d300affd8", "MediaAssistant", "MEDIAASSISTANT" },
					{ "940fd610-0402-4e32-9634-e5598e75d950", "c51979dc-73c9-49f1-8078-23030754f226", "Promoter", "PROMOTER" },
					{ "d1ccff9d-c132-4c9e-85e4-892cc39eba12", "47eeb989-f0ad-4321-aaf0-72fb12aa2f29", "Talent", "TALENT" }
				});

			migrationBuilder.InsertData(
				table: "Companies",
				columns: new[] { "Id", "Address", "Country", "Name" },
				values: new object[,]
				{
					{ new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "312 Forest Avenue, BF 923", "USA", "Admin_Solutions Ltd" },
					{ new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "583 Wall Dr. Gwynn Oak, MD 21207", "USA", "IT_Solutions Ltd" }
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Companies");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "41980fe2-3c75-46b4-81f6-3f1a8d48a316");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "6248a7d8-95be-4c7b-9a32-5d981252eafb");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "6808df6c-4cf1-412f-9789-e9bafcec4142");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "940fd610-0402-4e32-9634-e5598e75d950");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "d1ccff9d-c132-4c9e-85e4-892cc39eba12");

			migrationBuilder.DropColumn(
				name: "FirstName",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "LastName",
				table: "AspNetUsers");
		}
	}
}
