using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bizomet.Data.Migrations
{
	public partial class _2022000012 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "RefreshToken",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "RefreshTokenExpiryTime",
				table: "AspNetUsers");

			migrationBuilder.CreateTable(
				name: "RefreshToken",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
					Token = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
					Created = table.Column<DateTime>(type: "datetime2", nullable: false),
					CreatedByIp = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Revoked = table.Column<DateTime>(type: "datetime2", nullable: true),
					RevokedByIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
					ReplacedByToken = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_RefreshToken", x => x.Id);
					table.ForeignKey(
						name: "FK_RefreshToken_AspNetUsers_UserId",
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
				value: "a27d69d4-46f7-4e84-adca-c0e1ee86586b");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "69571a28-cb0d-4fe6-8176-3bffad6c1510",
				column: "ConcurrencyStamp",
				value: "4de5af69-e7fd-48d2-b034-f4727a294e24");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7bd1c590-9eed-44e9-a60c-6e7de0db8f01",
				column: "ConcurrencyStamp",
				value: "fac84355-4e2e-4fe7-ae54-dc6e02f9e55a");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7e6619f8-b336-4f3e-826a-5ce96cef872d",
				column: "ConcurrencyStamp",
				value: "89d18744-dac0-4d52-99c2-d963ef77c6ed");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8742075e-7145-4bd7-8215-814467809dc2",
				column: "ConcurrencyStamp",
				value: "79a19c04-b2d8-4f98-842c-b10dccf84e84");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8832961e-a631-445b-9d86-b93f9b4c767b",
				column: "ConcurrencyStamp",
				value: "386c85b4-c75c-467e-b4a6-fa93d98d3740");

			migrationBuilder.CreateIndex(
				name: "IX_RefreshToken_UserId",
				table: "RefreshToken",
				column: "UserId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "RefreshToken");

			migrationBuilder.AddColumn<string>(
				name: "RefreshToken",
				table: "AspNetUsers",
				type: "nvarchar(max)",
				nullable: true);

			migrationBuilder.AddColumn<DateTime>(
				name: "RefreshTokenExpiryTime",
				table: "AspNetUsers",
				type: "datetime2",
				nullable: true);

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "347ac56d-9576-4f4a-81be-674b4a3a9d0b",
				column: "ConcurrencyStamp",
				value: "a867a4d6-735d-430f-916d-df81c844ab4a");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "69571a28-cb0d-4fe6-8176-3bffad6c1510",
				column: "ConcurrencyStamp",
				value: "6a9a5d1b-95a8-4692-a18a-427adfe06a44");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7bd1c590-9eed-44e9-a60c-6e7de0db8f01",
				column: "ConcurrencyStamp",
				value: "42428fb3-56e4-42ae-904b-44834729dbc3");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7e6619f8-b336-4f3e-826a-5ce96cef872d",
				column: "ConcurrencyStamp",
				value: "41a4da35-28f6-402d-9c69-a6ac5958ad6e");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8742075e-7145-4bd7-8215-814467809dc2",
				column: "ConcurrencyStamp",
				value: "e4170652-08f8-45b0-b1e8-71bc0c604838");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8832961e-a631-445b-9d86-b93f9b4c767b",
				column: "ConcurrencyStamp",
				value: "7ee93e1f-17ff-4ebd-acb6-af8740c609bb");
		}
	}
}
