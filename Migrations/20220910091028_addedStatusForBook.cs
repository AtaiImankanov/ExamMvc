using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamMvc.Migrations
{
    public partial class addedStatusForBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Books",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TakeId",
                table: "Books",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_TakeId",
                table: "Books",
                column: "TakeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Takes_TakeId",
                table: "Books",
                column: "TakeId",
                principalTable: "Takes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Takes_TakeId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_TakeId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "TakeId",
                table: "Books");
        }
    }
}
