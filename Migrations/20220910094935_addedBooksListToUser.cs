using Microsoft.EntityFrameworkCore.Migrations;

namespace ExamMvc.Migrations
{
    public partial class addedBooksListToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Takes_TakeId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_TakeId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "TakeId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Books",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_UserId",
                table: "Books",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Users_UserId",
                table: "Books",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Users_UserId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_UserId",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Books");

            migrationBuilder.AddColumn<int>(
                name: "TakeId",
                table: "Books",
                type: "integer",
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
    }
}
