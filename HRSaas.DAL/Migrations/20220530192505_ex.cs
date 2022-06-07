using Microsoft.EntityFrameworkCore.Migrations;

namespace HRSaas.DAL.Migrations
{
    public partial class ex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenditure_Company_CompanyId",
                table: "Expenditure");

            migrationBuilder.DropIndex(
                name: "IX_Expenditure_CompanyId",
                table: "Expenditure");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Expenditure");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Expenditure",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenditure_CompanyId",
                table: "Expenditure",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenditure_Company_CompanyId",
                table: "Expenditure",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
