using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bizomet.Data.Migrations
{
	public partial class _2022000003 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.Sql("delete from AspNetUserRoles");
			migrationBuilder.Sql("delete from AspNetUsers");
			migrationBuilder.Sql("delete from AspNetRoles");

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

			migrationBuilder.InsertData(
				table: "AspNetRoles",
				columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
				values: new object[,]
				{
					{ "347ac56d-9576-4f4a-81be-674b4a3a9d0b", "4b6bcec0-2e48-4cb2-9489-f4a86b2322ff", "Lifter", "LIFTER" },
					{ "69571a28-cb0d-4fe6-8176-3bffad6c1510", "24cf78f7-3f13-4847-a935-603d188645ac", "Talent", "TALENT" },
					{ "7bd1c590-9eed-44e9-a60c-6e7de0db8f01", "d98192c5-963e-4ca9-beee-99b0865bd39b", "MediaAssistant", "MEDIAASSISTANT" },
					{ "7e6619f8-b336-4f3e-826a-5ce96cef872d", "9f30c8fc-88de-4975-84e6-f56f44ffe28e", "Promoter", "PROMOTER" },
					{ "8742075e-7145-4bd7-8215-814467809dc2", "73a518bc-188d-4e5f-8541-a0c86cec9d49", "Administrator", "ADMINISTRATOR" },
					{ "8832961e-a631-445b-9d86-b93f9b4c767b", "77d096e8-b59a-4bd1-8dc6-f0127a8b5be7", "Producer", "PRODUCER" }
				});
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropColumn(
				name: "RefreshToken",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "RefreshTokenExpiryTime",
				table: "AspNetUsers");
		}
	}
}
