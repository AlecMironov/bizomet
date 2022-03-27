using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bizomet.Data.Migrations
{
	public partial class _2022000014 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.AddColumn<int>(
				name: "InterviewCondition",
				table: "Project",
				type: "int",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.AddColumn<string>(
				name: "InterviewConditionComment",
				table: "Project",
				type: "nvarchar(500)",
				maxLength: 500,
				nullable: true);

			migrationBuilder.AddColumn<int>(
				name: "InterviewResult",
				table: "Project",
				type: "int",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.AddColumn<string>(
				name: "InterviewResultComment",
				table: "Project",
				type: "nvarchar(500)",
				maxLength: 500,
				nullable: true);

			migrationBuilder.AddColumn<int>(
				name: "MediaAssistantFinancialService",
				table: "Project",
				type: "int",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.AddColumn<int>(
				name: "ProducerFinancialService",
				table: "Project",
				type: "int",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.AddColumn<int>(
				name: "PromoterFinancialService",
				table: "Project",
				type: "int",
				nullable: false,
				defaultValue: 0);

			migrationBuilder.AddColumn<bool>(
				name: "WishContactedByMediaAssistant",
				table: "Project",
				type: "bit",
				nullable: false,
				defaultValue: false);

			migrationBuilder.AddColumn<bool>(
				name: "WishContactedByProducer",
				table: "Project",
				type: "bit",
				nullable: false,
				defaultValue: false);

			migrationBuilder.AddColumn<bool>(
				name: "WishContactedByPromoter",
				table: "Project",
				type: "bit",
				nullable: false,
				defaultValue: false);

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

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "InterviewCondition",
				table: "Project");

			migrationBuilder.DropColumn(
				name: "InterviewConditionComment",
				table: "Project");

			migrationBuilder.DropColumn(
				name: "InterviewResult",
				table: "Project");

			migrationBuilder.DropColumn(
				name: "InterviewResultComment",
				table: "Project");

			migrationBuilder.DropColumn(
				name: "MediaAssistantFinancialService",
				table: "Project");

			migrationBuilder.DropColumn(
				name: "ProducerFinancialService",
				table: "Project");

			migrationBuilder.DropColumn(
				name: "PromoterFinancialService",
				table: "Project");

			migrationBuilder.DropColumn(
				name: "WishContactedByMediaAssistant",
				table: "Project");

			migrationBuilder.DropColumn(
				name: "WishContactedByProducer",
				table: "Project");

			migrationBuilder.DropColumn(
				name: "WishContactedByPromoter",
				table: "Project");

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
	}
}
