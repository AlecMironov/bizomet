using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bizomet.Data.Migrations
{
	public partial class _2022000015 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AlterColumn<string>(
				name: "InterviewResultComment",
				table: "Project",
				type: "nvarchar(4000)",
				maxLength: 4000,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(500)",
				oldMaxLength: 500,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "InterviewConditionComment",
				table: "Project",
				type: "nvarchar(4000)",
				maxLength: 4000,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(500)",
				oldMaxLength: 500,
				oldNullable: true);

			migrationBuilder.AddColumn<string>(
				name: "Location",
				table: "Project",
				type: "nvarchar(1000)",
				maxLength: 1000,
				nullable: true);

			migrationBuilder.AddColumn<bool>(
				name: "RemoteLocation",
				table: "Project",
				type: "bit",
				nullable: false,
				defaultValue: false);

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "347ac56d-9576-4f4a-81be-674b4a3a9d0b",
				column: "ConcurrencyStamp",
				value: "2814d760-b4cd-47b9-a8f3-794f5a249f52");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "69571a28-cb0d-4fe6-8176-3bffad6c1510",
				column: "ConcurrencyStamp",
				value: "90e99155-210b-4c1b-afef-184fcc66b75c");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7bd1c590-9eed-44e9-a60c-6e7de0db8f01",
				column: "ConcurrencyStamp",
				value: "620c263b-a55f-48f5-9ced-177fa002b00d");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7e6619f8-b336-4f3e-826a-5ce96cef872d",
				column: "ConcurrencyStamp",
				value: "cc242b29-3add-4e62-a4fb-4527f79cb8a5");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8742075e-7145-4bd7-8215-814467809dc2",
				column: "ConcurrencyStamp",
				value: "7dc8c8cb-b0e9-40d9-adc1-dd829b0d8965");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8832961e-a631-445b-9d86-b93f9b4c767b",
				column: "ConcurrencyStamp",
				value: "6f85cee8-a158-48b7-8b11-5383c6f24b74");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "Location",
				table: "Project");

			migrationBuilder.DropColumn(
				name: "RemoteLocation",
				table: "Project");

			migrationBuilder.AlterColumn<string>(
				name: "InterviewResultComment",
				table: "Project",
				type: "nvarchar(500)",
				maxLength: 500,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(4000)",
				oldMaxLength: 4000,
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "InterviewConditionComment",
				table: "Project",
				type: "nvarchar(500)",
				maxLength: 500,
				nullable: true,
				oldClrType: typeof(string),
				oldType: "nvarchar(4000)",
				oldMaxLength: 4000,
				oldNullable: true);

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "347ac56d-9576-4f4a-81be-674b4a3a9d0b",
				column: "ConcurrencyStamp",
				value: "46297139-3159-42b9-991c-22d059dc417a");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "69571a28-cb0d-4fe6-8176-3bffad6c1510",
				column: "ConcurrencyStamp",
				value: "44f65df6-73f4-464e-863c-d632d5eab853");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7bd1c590-9eed-44e9-a60c-6e7de0db8f01",
				column: "ConcurrencyStamp",
				value: "336c95d2-db42-4e14-aa3f-02a4f52a894a");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7e6619f8-b336-4f3e-826a-5ce96cef872d",
				column: "ConcurrencyStamp",
				value: "1fe556ca-06ca-4ac2-83ef-2c8dafbac873");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8742075e-7145-4bd7-8215-814467809dc2",
				column: "ConcurrencyStamp",
				value: "87de4409-65da-44f3-b5b2-9fbe9c908f2f");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8832961e-a631-445b-9d86-b93f9b4c767b",
				column: "ConcurrencyStamp",
				value: "954f7442-dbfc-4e51-8fdc-e5237dd8f326");
		}
	}
}
