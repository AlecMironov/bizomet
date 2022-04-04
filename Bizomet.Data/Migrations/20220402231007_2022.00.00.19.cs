using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bizomet.Data.Migrations
{
	public partial class _2022000019 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "ProjectAttachment",
				columns: table => new
				{
					Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
					FileName = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
					Title = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
					Size = table.Column<long>(type: "bigint", nullable: false),
					BinaryContent = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
					Thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_ProjectAttachment", x => x.Id);
					table.ForeignKey(
						name: "FK_ProjectAttachment_Project_ProjectId",
						column: x => x.ProjectId,
						principalTable: "Project",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "347ac56d-9576-4f4a-81be-674b4a3a9d0b",
				column: "ConcurrencyStamp",
				value: "3069f73e-5261-4d9f-8e54-7553f662535e");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "69571a28-cb0d-4fe6-8176-3bffad6c1510",
				column: "ConcurrencyStamp",
				value: "ad081f65-810a-4f47-b001-841e41c8103a");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7bd1c590-9eed-44e9-a60c-6e7de0db8f01",
				column: "ConcurrencyStamp",
				value: "025857f5-b16c-422a-86f6-bef669da5bd6");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7e6619f8-b336-4f3e-826a-5ce96cef872d",
				column: "ConcurrencyStamp",
				value: "70f6d293-dc24-4d75-8e63-dd76659c7190");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8742075e-7145-4bd7-8215-814467809dc2",
				column: "ConcurrencyStamp",
				value: "d1d5d9be-257e-4869-9320-708e9b5254f2");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8832961e-a631-445b-9d86-b93f9b4c767b",
				column: "ConcurrencyStamp",
				value: "c3beaf8b-0bfc-4de6-ad00-5decb4effe2f");

			migrationBuilder.CreateIndex(
				name: "IX_ProjectAttachment_ProjectId",
				table: "ProjectAttachment",
				column: "ProjectId");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "ProjectAttachment");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "347ac56d-9576-4f4a-81be-674b4a3a9d0b",
				column: "ConcurrencyStamp",
				value: "9b561eff-6b3a-4b4d-8303-b00c9ff882b6");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "69571a28-cb0d-4fe6-8176-3bffad6c1510",
				column: "ConcurrencyStamp",
				value: "59f82bba-cbb5-4dd1-9735-f90cc8eb47ba");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7bd1c590-9eed-44e9-a60c-6e7de0db8f01",
				column: "ConcurrencyStamp",
				value: "b78d4f85-eeba-4e75-81bf-2e1ff8f9b332");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7e6619f8-b336-4f3e-826a-5ce96cef872d",
				column: "ConcurrencyStamp",
				value: "2dbb9b91-f95b-403e-a7a8-088b809476ca");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8742075e-7145-4bd7-8215-814467809dc2",
				column: "ConcurrencyStamp",
				value: "4c773c33-e6ba-4f20-aaa6-b0d974f63e96");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8832961e-a631-445b-9d86-b93f9b4c767b",
				column: "ConcurrencyStamp",
				value: "f3b63446-1a20-4039-9f3b-43afe35a5475");
		}
	}
}
