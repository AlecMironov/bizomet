using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bizomet.Data.Migrations
{
	public partial class _2022000013 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "Tags",
				table: "UserProfile",
				type: "nvarchar(max)",
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(max)");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "347ac56d-9576-4f4a-81be-674b4a3a9d0b",
				column: "ConcurrencyStamp",
				value: "65dc64fe-cd75-47f8-920d-10d7862a9148");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "69571a28-cb0d-4fe6-8176-3bffad6c1510",
				column: "ConcurrencyStamp",
				value: "26a41fe5-e4dd-44a2-8dec-5b884050afbf");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7bd1c590-9eed-44e9-a60c-6e7de0db8f01",
				column: "ConcurrencyStamp",
				value: "36fdbc71-17a7-4dcf-8657-c100dc400a6f");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7e6619f8-b336-4f3e-826a-5ce96cef872d",
				column: "ConcurrencyStamp",
				value: "bdcac983-e708-4c56-b4f9-7a7d6296697d");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8742075e-7145-4bd7-8215-814467809dc2",
				column: "ConcurrencyStamp",
				value: "f46dc40b-35a0-4d4d-a76b-2011c12a0a46");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8832961e-a631-445b-9d86-b93f9b4c767b",
				column: "ConcurrencyStamp",
				value: "e7d046fc-edaa-4709-9e69-3957c27c1f84");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "Tags",
				table: "UserProfile",
				type: "nvarchar(max)",
				nullable: false,
				defaultValue: "",
				oldClrType: typeof(string),
				oldType: "nvarchar(max)",
				oldNullable: true);

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
		}
	}
}
