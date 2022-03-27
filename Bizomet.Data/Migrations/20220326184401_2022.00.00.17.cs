using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bizomet.Data.Migrations
{
	public partial class _2022000017 : Migration
	{
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "Location.Countries",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
					ISO3 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
					NumericCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
					ISO2 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
					PhoneCode = table.Column<string>(type: "nvarchar(510)", maxLength: 510, nullable: true),
					Capital = table.Column<string>(type: "nvarchar(510)", maxLength: 510, nullable: true),
					Currency = table.Column<string>(type: "nvarchar(510)", maxLength: 510, nullable: true),
					CurrencyName = table.Column<string>(type: "nvarchar(510)", maxLength: 510, nullable: true),
					CurrencySymbol = table.Column<string>(type: "nvarchar(510)", maxLength: 510, nullable: true),
					TLD = table.Column<string>(type: "nvarchar(510)", maxLength: 510, nullable: true),
					Region = table.Column<string>(type: "nvarchar(510)", maxLength: 510, nullable: true),
					SubRegion = table.Column<string>(type: "nvarchar(510)", maxLength: 510, nullable: true),
					Latitude = table.Column<decimal>(type: "decimal(11,8)", precision: 11, scale: 8, nullable: true),
					Longitude = table.Column<decimal>(type: "decimal(11,8)", precision: 11, scale: 8, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Location.Countries", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Location.States",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(510)", maxLength: 510, nullable: false),
					FipsCode = table.Column<string>(type: "nvarchar(510)", maxLength: 510, nullable: true),
					ISO2 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
					Type = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: true),
					Latitude = table.Column<decimal>(type: "decimal(11,8)", precision: 11, scale: 8, nullable: true),
					Longitude = table.Column<decimal>(type: "decimal(11,8)", precision: 11, scale: 8, nullable: true),
					CountryCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
					CountryId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Location.States", x => x.Id);
					table.ForeignKey(
						name: "FK_Location.States_Location.Countries_CountryId",
						column: x => x.CountryId,
						principalTable: "Location.Countries",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "Location.Cities",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(510)", maxLength: 510, nullable: false),
					Latitude = table.Column<decimal>(type: "decimal(11,8)", precision: 11, scale: 8, nullable: true),
					Longitude = table.Column<decimal>(type: "decimal(11,8)", precision: 11, scale: 8, nullable: true),
					CountryCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
					CountryId = table.Column<int>(type: "int", nullable: false),
					StateCode = table.Column<string>(type: "nvarchar(510)", maxLength: 510, nullable: true),
					StateId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Location.Cities", x => x.Id);
					table.ForeignKey(
						name: "FK_Location.Cities_Location.Countries_CountryId",
						column: x => x.CountryId,
						principalTable: "Location.Countries",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_Location.Cities_Location.States_StateId",
						column: x => x.StateId,
						principalTable: "Location.States",
						principalColumn: "Id");
				});

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "347ac56d-9576-4f4a-81be-674b4a3a9d0b",
				column: "ConcurrencyStamp",
				value: "a3d4b61e-33aa-4a28-a33c-823025905de5");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "69571a28-cb0d-4fe6-8176-3bffad6c1510",
				column: "ConcurrencyStamp",
				value: "b2a93220-414a-49ad-a021-f4d6cb4a9b15");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7bd1c590-9eed-44e9-a60c-6e7de0db8f01",
				column: "ConcurrencyStamp",
				value: "a486c6fd-9fdb-44fa-aacd-41393a0acc67");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7e6619f8-b336-4f3e-826a-5ce96cef872d",
				column: "ConcurrencyStamp",
				value: "a81332b7-0ee8-441d-95ab-df630f724dcc");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8742075e-7145-4bd7-8215-814467809dc2",
				column: "ConcurrencyStamp",
				value: "b16338c0-5012-4603-bc0b-fda52b636a61");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8832961e-a631-445b-9d86-b93f9b4c767b",
				column: "ConcurrencyStamp",
				value: "de54c918-a5ba-4381-93f6-c5b7331c5077");

			migrationBuilder.CreateIndex(
				name: "CityIndex",
				table: "Location.Cities",
				column: "Name");

			migrationBuilder.CreateIndex(
				name: "CountryStateCityByIdIndex",
				table: "Location.Cities",
				columns: new[] { "CountryId", "StateId", "Id" },
				unique: true);

			migrationBuilder.CreateIndex(
				name: "CountryStateCityIndex",
				table: "Location.Cities",
				columns: new[] { "CountryCode", "StateCode", "Name" });

			migrationBuilder.CreateIndex(
				name: "IX_Location.Cities_StateId",
				table: "Location.Cities",
				column: "StateId");

			migrationBuilder.CreateIndex(
				name: "StateCityIndex",
				table: "Location.Cities",
				columns: new[] { "StateCode", "Name" });

			migrationBuilder.CreateIndex(
				name: "CountryIndex",
				table: "Location.Countries",
				column: "Name");

			migrationBuilder.CreateIndex(
				name: "CountryStateIndex",
				table: "Location.States",
				columns: new[] { "CountryCode", "Name" });

			migrationBuilder.CreateIndex(
				name: "IX_Location.States_CountryId",
				table: "Location.States",
				column: "CountryId");

			migrationBuilder.CreateIndex(
				name: "StateIndex",
				table: "Location.States",
				column: "Name");
		}

		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "Location.Cities");

			migrationBuilder.DropTable(
				name: "Location.States");

			migrationBuilder.DropTable(
				name: "Location.Countries");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "347ac56d-9576-4f4a-81be-674b4a3a9d0b",
				column: "ConcurrencyStamp",
				value: "6634449e-d4b1-4ab8-9326-d6d84298ee96");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "69571a28-cb0d-4fe6-8176-3bffad6c1510",
				column: "ConcurrencyStamp",
				value: "4a9bab80-2724-4355-8c95-1786455298b9");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7bd1c590-9eed-44e9-a60c-6e7de0db8f01",
				column: "ConcurrencyStamp",
				value: "571bfac0-4bfb-4382-b75c-0147536044d4");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "7e6619f8-b336-4f3e-826a-5ce96cef872d",
				column: "ConcurrencyStamp",
				value: "a33e20c9-787c-4e25-9a50-b61b3184011d");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8742075e-7145-4bd7-8215-814467809dc2",
				column: "ConcurrencyStamp",
				value: "32e3175a-e279-4224-b4af-89d3331dbfee");

			migrationBuilder.UpdateData(
				table: "AspNetRoles",
				keyColumn: "Id",
				keyValue: "8832961e-a631-445b-9d86-b93f9b4c767b",
				column: "ConcurrencyStamp",
				value: "5c6495ed-2f44-40c9-94a5-e2bd8a68d446");
		}
	}
}
