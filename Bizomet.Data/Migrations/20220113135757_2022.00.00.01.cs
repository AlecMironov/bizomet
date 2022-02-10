using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bizomet.Data.Migrations
{
	public partial class _2022000001 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "665829cb-28c6-45bb-a268-93dbf61b457e");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "b59228e0-cee4-4e50-8899-da4a757f3a7b");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "e7d27fa9-8eb0-4d04-bf04-978633622c37");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "eeab18ff-9c6c-4c92-9dfa-d8048f5b9a90");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "f40e4437-f010-4a60-8bf2-6a7f72ad6df4");

			migrationBuilder.AlterColumn<string>(
				name: "Province",
				table: "AspNetUsers",
				type: "nvarchar(100)",
				maxLength: 100,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(100)",
				oldMaxLength: 100);

			migrationBuilder.AlterColumn<string>(
				name: "PostalCode",
				table: "AspNetUsers",
				type: "nvarchar(20)",
				maxLength: 20,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(20)",
				oldMaxLength: 20);

			migrationBuilder.AlterColumn<string>(
				name: "PhoneNumberHome",
				table: "AspNetUsers",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50);

			migrationBuilder.AlterColumn<string>(
				name: "PhoneNumberFax",
				table: "AspNetUsers",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50);

			migrationBuilder.AlterColumn<string>(
				name: "PhoneNumberCell",
				table: "AspNetUsers",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50);

			migrationBuilder.AlterColumn<string>(
				name: "PhoneNumberBusiness",
				table: "AspNetUsers",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50);

			migrationBuilder.AlterColumn<string>(
				name: "NameTitle",
				table: "AspNetUsers",
				type: "nvarchar(10)",
				maxLength: 10,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(10)",
				oldMaxLength: 10);

			migrationBuilder.AlterColumn<string>(
				name: "Country",
				table: "AspNetUsers",
				type: "nvarchar(100)",
				maxLength: 100,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(100)",
				oldMaxLength: 100);

			migrationBuilder.AlterColumn<string>(
				name: "City",
				table: "AspNetUsers",
				type: "nvarchar(100)",
				maxLength: 100,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(100)",
				oldMaxLength: 100);

			migrationBuilder.AlterColumn<string>(
				name: "AddressLine2",
				table: "AspNetUsers",
				type: "nvarchar(100)",
				maxLength: 100,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(100)",
				oldMaxLength: 100);

			migrationBuilder.AlterColumn<string>(
				name: "AddressLine1",
				table: "AspNetUsers",
				type: "nvarchar(100)",
				maxLength: 100,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(100)",
				oldMaxLength: 100);

			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[,]
				{
					{ "1bfc52cb-7472-4a71-9ddb-17e5e32c329c", "0fe07f18-58d7-4b7d-8319-a2abe8eb03fb", "Administrator", "ADMINISTRATOR" },
					{ "330a8560-16ff-444f-a5b9-e01a62b4a5d3", "78d34cd1-db9d-474a-a175-e775ece04ffb", "Lifter", "LIFTER" },
					{ "55e06bd5-e542-436d-bea4-2afedf2c1b8f", "6f9a2a13-c833-409d-81b6-8feb9bc51c5f", "MediaAssistant", "MEDIAASSISTANT" },
					{ "6193612c-c669-4713-bf45-d97927724b55", "d2c7894f-4063-4a93-8bc0-bf930592e6e0", "Promoter", "PROMOTER" },
					{ "d238b630-ef04-450c-a2f1-1a8346a59cbc", "197a2f9b-347a-4649-b515-322cc8a3a045", "Talent", "TALENT" }
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "1bfc52cb-7472-4a71-9ddb-17e5e32c329c");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "330a8560-16ff-444f-a5b9-e01a62b4a5d3");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "55e06bd5-e542-436d-bea4-2afedf2c1b8f");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "6193612c-c669-4713-bf45-d97927724b55");

			migrationBuilder.DeleteData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "d238b630-ef04-450c-a2f1-1a8346a59cbc");

			migrationBuilder.AlterColumn<string>(
				name: "Province",
				table: "AspNetUsers",
				type: "nvarchar(100)",
				maxLength: 100,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(100)",
				oldMaxLength: 100,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "PostalCode",
				table: "AspNetUsers",
				type: "nvarchar(20)",
				maxLength: 20,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(20)",
				oldMaxLength: 20,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "PhoneNumberHome",
				table: "AspNetUsers",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "PhoneNumberFax",
				table: "AspNetUsers",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "PhoneNumberCell",
				table: "AspNetUsers",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "PhoneNumberBusiness",
				table: "AspNetUsers",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(50)",
				oldMaxLength: 50,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "NameTitle",
				table: "AspNetUsers",
				type: "nvarchar(10)",
				maxLength: 10,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(10)",
				oldMaxLength: 10,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "Country",
				table: "AspNetUsers",
				type: "nvarchar(100)",
				maxLength: 100,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(100)",
				oldMaxLength: 100,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "City",
				table: "AspNetUsers",
				type: "nvarchar(100)",
				maxLength: 100,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(100)",
				oldMaxLength: 100,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "AddressLine2",
				table: "AspNetUsers",
				type: "nvarchar(100)",
				maxLength: 100,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(100)",
				oldMaxLength: 100,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "AddressLine1",
				table: "AspNetUsers",
				type: "nvarchar(100)",
				maxLength: 100,
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(100)",
				oldMaxLength: 100,
				oldNullable: true);

			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[,]
				{
					{ "665829cb-28c6-45bb-a268-93dbf61b457e", "39dda369-ed73-4b38-b26c-aec2730ebf52", "Promoter", "PROMOTER" },
					{ "b59228e0-cee4-4e50-8899-da4a757f3a7b", "5e32a9c5-4236-4637-aa4d-721f1933f869", "Talent", "TALENT" },
					{ "e7d27fa9-8eb0-4d04-bf04-978633622c37", "c523ce62-7c68-4e0e-bcfe-ac04da096e9f", "Administrator", "ADMINISTRATOR" },
					{ "eeab18ff-9c6c-4c92-9dfa-d8048f5b9a90", "6b7aa36b-1fcf-482f-918f-e35e8cfe2eec", "Lifter", "LIFTER" },
					{ "f40e4437-f010-4a60-8bf2-6a7f72ad6df4", "6e1a67a3-a347-41ff-8e43-114c9bef02db", "MediaAssistant", "MEDIAASSISTANT" }
				});
		}
	}
}
