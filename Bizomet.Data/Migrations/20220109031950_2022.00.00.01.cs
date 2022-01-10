using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bizomet.Data.Migrations
{
	public partial class _2022000001 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "DeviceCodes");

			migrationBuilder.DropTable(
				name: "Keys");

			migrationBuilder.DropTable(
				name: "PersistedGrants");

			migrationBuilder.AlterColumn<string>(
				name: "Name",
				table: "AspNetUserTokens",
				type: "nvarchar(450)",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(128)",
				oldMaxLength: 128);

			migrationBuilder.AlterColumn<string>(
				name: "LoginProvider",
				table: "AspNetUserTokens",
				type: "nvarchar(450)",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(128)",
				oldMaxLength: 128);

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

			migrationBuilder.AlterColumn<string>(
				name: "ProviderKey",
				table: "AspNetUserLogins",
				type: "nvarchar(450)",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(128)",
				oldMaxLength: 128);

			migrationBuilder.AlterColumn<string>(
				name: "LoginProvider",
				table: "AspNetUserLogins",
				type: "nvarchar(450)",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(128)",
				oldMaxLength: 128);

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
				table: "Companies",
				columns: new[] { "Id", "Address", "Country", "Name" },
				values: new object[] { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), "312 Forest Avenue, BF 923", "USA", "Admin_Solutions Ltd" });

			migrationBuilder.InsertData(
				table: "Companies",
				columns: new[] { "Id", "Address", "Country", "Name" },
				values: new object[] { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "583 Wall Dr. Gwynn Oak, MD 21207", "USA", "IT_Solutions Ltd" });
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Companies");

			migrationBuilder.DropColumn(
				name: "FirstName",
				table: "AspNetUsers");

			migrationBuilder.DropColumn(
				name: "LastName",
				table: "AspNetUsers");

			migrationBuilder.AlterColumn<string>(
				name: "Name",
				table: "AspNetUserTokens",
				type: "nvarchar(128)",
				maxLength: 128,
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(450)");

			migrationBuilder.AlterColumn<string>(
				name: "LoginProvider",
				table: "AspNetUserTokens",
				type: "nvarchar(128)",
				maxLength: 128,
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(450)");

			migrationBuilder.AlterColumn<string>(
				name: "ProviderKey",
				table: "AspNetUserLogins",
				type: "nvarchar(128)",
				maxLength: 128,
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(450)");

			migrationBuilder.AlterColumn<string>(
				name: "LoginProvider",
				table: "AspNetUserLogins",
				type: "nvarchar(128)",
				maxLength: 128,
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(450)");

			migrationBuilder.CreateTable(
				name: "DeviceCodes",
				columns: table => new
				{
					UserCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
					ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
					CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
					Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50780, nullable: false),
					Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
					DeviceCode = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
					Expiration = table.Column<DateTime>(type: "datetime2", nullable: false),
					SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
					SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_DeviceCodes", x => x.UserCode);
				});

			migrationBuilder.CreateTable(
				name: "Keys",
				columns: table => new
				{
					Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
					Algorithm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					Created = table.Column<DateTime>(type: "datetime2", nullable: false),
					Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50780, nullable: false),
					DataProtected = table.Column<bool>(type: "bit", nullable: false),
					IsX509Certificate = table.Column<bool>(type: "bit", nullable: false),
					Use = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
					Version = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Keys", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "PersistedGrants",
				columns: table => new
				{
					Key = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
					ClientId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
					ConsumedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
					CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
					Data = table.Column<string>(type: "nvarchar(max)", maxLength: 50780, nullable: false),
					Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
					Expiration = table.Column<DateTime>(type: "datetime2", nullable: true),
					SessionId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
					SubjectId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
					Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_PersistedGrants", x => x.Key);
				});

			migrationBuilder.CreateIndex(
				name: "IX_DeviceCodes_DeviceCode",
				table: "DeviceCodes",
				column: "DeviceCode",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_DeviceCodes_Expiration",
				table: "DeviceCodes",
				column: "Expiration");

			migrationBuilder.CreateIndex(
				name: "IX_Keys_Use",
				table: "Keys",
				column: "Use");

			migrationBuilder.CreateIndex(
				name: "IX_PersistedGrants_ConsumedTime",
				table: "PersistedGrants",
				column: "ConsumedTime");

			migrationBuilder.CreateIndex(
				name: "IX_PersistedGrants_Expiration",
				table: "PersistedGrants",
				column: "Expiration");

			migrationBuilder.CreateIndex(
				name: "IX_PersistedGrants_SubjectId_ClientId_Type",
				table: "PersistedGrants",
				columns: new[] { "SubjectId", "ClientId", "Type" });

			migrationBuilder.CreateIndex(
				name: "IX_PersistedGrants_SubjectId_SessionId_Type",
				table: "PersistedGrants",
				columns: new[] { "SubjectId", "SessionId", "Type" });
		}
	}
}
