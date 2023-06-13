using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CurrencyExchange.APIService.IdentityDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddNewRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "bbf01e5c-ecf4-4375-aceb-1a74186bcba7", "9c75fe92-8769-4284-8e39-0d604411cc6a", "Admin", "ADMIN" },
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
