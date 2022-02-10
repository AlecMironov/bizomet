using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bizomet.Data.Migrations
{
	public partial class _2022000004 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "AddressLine1",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "AddressLine2",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "City",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "Country",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "FirstName",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "LastName",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "NameTitle",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "PhoneNumberBusiness",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "PhoneNumberCell",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "PhoneNumberFax",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "PhoneNumberHome",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "PostalCode",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "Province",
				table: "AspNetUsers");

			migrationBuilder.CreateTable(
				name: "UserProfile",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
					FirstName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
					LastName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
					NameTitle = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
					AddressLine1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
					AddressLine2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
					City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
					StateProvince = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
					Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
					ZipPostal = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
					PhoneNumberBusiness = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
					PhoneNumberHome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
					PhoneNumberCell = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
					PhoneNumberFax = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
					Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
					LocationCountry = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
					LocationState = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
					LocationCity = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserProfile", x => x.Id);
					table.ForeignKey(
						name: "FK_UserProfile_AspNetUsers_UserId",
						column: x => x.UserId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "347ac56d-9576-4f4a-81be-674b4a3a9d0b",
				column: "ConcurrencyStamp",
				value: "4115ebc4-1cf5-4c33-9f6b-eff5afb58c09");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "69571a28-cb0d-4fe6-8176-3bffad6c1510",
				column: "ConcurrencyStamp",
				value: "9877425f-9624-4de4-9f01-74b5a4d512b6");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7bd1c590-9eed-44e9-a60c-6e7de0db8f01",
				column: "ConcurrencyStamp",
				value: "6256d3b0-d884-4c56-a5ae-6d382ec9ecdd");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7e6619f8-b336-4f3e-826a-5ce96cef872d",
				column: "ConcurrencyStamp",
				value: "2ebb185e-f115-4ed2-ad8f-e6c74c841a97");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8742075e-7145-4bd7-8215-814467809dc2",
				column: "ConcurrencyStamp",
				value: "5a0e2206-4e10-4038-bcfe-d8c49bdff625");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8832961e-a631-445b-9d86-b93f9b4c767b",
				column: "ConcurrencyStamp",
				value: "c6eb9931-7a71-413d-aa9f-d6fad8aa4c28");

			migrationBuilder.CreateIndex(
				name: "IX_UserProfile_UserId",
				table: "UserProfile",
				column: "UserId",
				unique: true);
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "UserProfile");

			migrationBuilder.AddColumn<string>(
				name: "AddressLine1",
				table: "AspNetUsers",
				type: "nvarchar(100)",
				maxLength: 100,
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "AddressLine2",
				table: "AspNetUsers",
				type: "nvarchar(100)",
				maxLength: 100,
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "City",
				table: "AspNetUsers",
				type: "nvarchar(100)",
				maxLength: 100,
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "Country",
				table: "AspNetUsers",
				type: "nvarchar(100)",
				maxLength: 100,
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "FirstName",
				table: "AspNetUsers",
				type: "nvarchar(500)",
				maxLength: 500,
				nullable: false,
				defaultValue: "");

			migrationBuilder.AddColumn<string>(
				name: "LastName",
				table: "AspNetUsers",
				type: "nvarchar(500)",
				maxLength: 500,
				nullable: false,
				defaultValue: "");

			migrationBuilder.AddColumn<string>(
				name: "NameTitle",
				table: "AspNetUsers",
				type: "nvarchar(10)",
				maxLength: 10,
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "PhoneNumberBusiness",
				table: "AspNetUsers",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "PhoneNumberCell",
				table: "AspNetUsers",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "PhoneNumberFax",
				table: "AspNetUsers",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "PhoneNumberHome",
				table: "AspNetUsers",
				type: "nvarchar(50)",
				maxLength: 50,
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "PostalCode",
				table: "AspNetUsers",
				type: "nvarchar(20)",
				maxLength: 20,
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "Province",
				table: "AspNetUsers",
				type: "nvarchar(100)",
				maxLength: 100,
				nullable: true);

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "347ac56d-9576-4f4a-81be-674b4a3a9d0b",
				column: "ConcurrencyStamp",
				value: "4b6bcec0-2e48-4cb2-9489-f4a86b2322ff");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "69571a28-cb0d-4fe6-8176-3bffad6c1510",
				column: "ConcurrencyStamp",
				value: "24cf78f7-3f13-4847-a935-603d188645ac");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7bd1c590-9eed-44e9-a60c-6e7de0db8f01",
				column: "ConcurrencyStamp",
				value: "d98192c5-963e-4ca9-beee-99b0865bd39b");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7e6619f8-b336-4f3e-826a-5ce96cef872d",
				column: "ConcurrencyStamp",
				value: "9f30c8fc-88de-4975-84e6-f56f44ffe28e");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8742075e-7145-4bd7-8215-814467809dc2",
				column: "ConcurrencyStamp",
				value: "73a518bc-188d-4e5f-8541-a0c86cec9d49");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8832961e-a631-445b-9d86-b93f9b4c767b",
				column: "ConcurrencyStamp",
				value: "77d096e8-b59a-4bd1-8dc6-f0127a8b5be7");
		}
	}
}
