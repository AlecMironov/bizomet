using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bizomet.Data.Migrations
{
	public partial class _2022000010 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "ContactUsRequest",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
					Reason = table.Column<int>(type: "int", nullable: false),
					FirstName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
					LastName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
					AddressLine1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
					AddressLine2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
					City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
					StateProvince = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
					Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
					Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
					PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
					Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ContactUsRequest", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "UserPortfolio",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Order = table.Column<int>(type: "int", nullable: false),
					Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
					Link = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserPortfolio", x => x.Id);
					table.ForeignKey(
						name: "FK_UserPortfolio_AspNetUsers_UserId",
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
				value: "0351655d-0d96-4b09-9406-1c9e88ba51e6");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "69571a28-cb0d-4fe6-8176-3bffad6c1510",
				column: "ConcurrencyStamp",
				value: "0c978e20-aba0-4160-8778-81c1e352e793");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7bd1c590-9eed-44e9-a60c-6e7de0db8f01",
				column: "ConcurrencyStamp",
				value: "0fb4f45c-94ee-43b8-a727-f6cd95835cb2");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7e6619f8-b336-4f3e-826a-5ce96cef872d",
				column: "ConcurrencyStamp",
				value: "af8668fd-2b13-4f10-8032-0a781f92b927");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8742075e-7145-4bd7-8215-814467809dc2",
				column: "ConcurrencyStamp",
				value: "55619e2d-4045-4155-b213-afbe2addf8fd");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8832961e-a631-445b-9d86-b93f9b4c767b",
				column: "ConcurrencyStamp",
				value: "0a04f01c-a923-4246-96b8-cae723211d4e");

			migrationBuilder.CreateIndex(
				name: "UserContactUsRequestsIndex",
				table: "ContactUsRequest",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_UserPortfolio_UserId",
				table: "UserPortfolio",
				column: "UserId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "ContactUsRequest");

			migrationBuilder.DropTable(
				name: "UserPortfolio");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "347ac56d-9576-4f4a-81be-674b4a3a9d0b",
				column: "ConcurrencyStamp",
				value: "5baf80a4-f0a0-4b26-9945-ec310fd154c5");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "69571a28-cb0d-4fe6-8176-3bffad6c1510",
				column: "ConcurrencyStamp",
				value: "6fbcaa56-a07c-417b-9a34-f0cf4f77f5fb");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7bd1c590-9eed-44e9-a60c-6e7de0db8f01",
				column: "ConcurrencyStamp",
				value: "d23f21dc-69ee-49d0-8fd8-cda5117f501d");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7e6619f8-b336-4f3e-826a-5ce96cef872d",
				column: "ConcurrencyStamp",
				value: "5b916dd6-5059-4582-b08f-927075547ea4");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8742075e-7145-4bd7-8215-814467809dc2",
				column: "ConcurrencyStamp",
				value: "d0e7b982-e949-4cf0-8217-3293b1f6e910");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8832961e-a631-445b-9d86-b93f9b4c767b",
				column: "ConcurrencyStamp",
				value: "63509086-b69b-44e1-82b3-f0fd57de862d");
		}
	}
}
