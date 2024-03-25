using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo1_Day2.Migrations
{
    /// <inheritdoc />
    public partial class m2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Departments_DeptNo",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_DeptNo",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DeptNo",
                table: "Departments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeptNo",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DeptNo",
                table: "Departments",
                column: "DeptNo");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Departments_DeptNo",
                table: "Departments",
                column: "DeptNo",
                principalTable: "Departments",
                principalColumn: "DeptId");
        }
    }
}
