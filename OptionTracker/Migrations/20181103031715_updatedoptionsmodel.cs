using Microsoft.EntityFrameworkCore.Migrations;

namespace OptionTracker.Migrations
{
    public partial class updatedoptionsmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionTypes_Options_Optionid",
                table: "OptionTypes");

            migrationBuilder.DropIndex(
                name: "IX_OptionTypes_Optionid",
                table: "OptionTypes");

            migrationBuilder.DropColumn(
                name: "Optionid",
                table: "OptionTypes");

            migrationBuilder.CreateIndex(
                name: "IX_Options_OptionTypeId",
                table: "Options",
                column: "OptionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Options_OptionTypes_OptionTypeId",
                table: "Options",
                column: "OptionTypeId",
                principalTable: "OptionTypes",
                principalColumn: "OptionTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Options_OptionTypes_OptionTypeId",
                table: "Options");

            migrationBuilder.DropIndex(
                name: "IX_Options_OptionTypeId",
                table: "Options");

            migrationBuilder.AddColumn<int>(
                name: "Optionid",
                table: "OptionTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OptionTypes_Optionid",
                table: "OptionTypes",
                column: "Optionid");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionTypes_Options_Optionid",
                table: "OptionTypes",
                column: "Optionid",
                principalTable: "Options",
                principalColumn: "Optionid",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
