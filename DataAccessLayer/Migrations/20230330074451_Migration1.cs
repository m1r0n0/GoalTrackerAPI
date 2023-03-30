using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MembersIds_GoalList_GoalId",
                table: "MembersIds");

            migrationBuilder.AlterColumn<int>(
                name: "GoalId",
                table: "MembersIds",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MembersIds_GoalList_GoalId",
                table: "MembersIds",
                column: "GoalId",
                principalTable: "GoalList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MembersIds_GoalList_GoalId",
                table: "MembersIds");

            migrationBuilder.AlterColumn<int>(
                name: "GoalId",
                table: "MembersIds",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MembersIds_GoalList_GoalId",
                table: "MembersIds",
                column: "GoalId",
                principalTable: "GoalList",
                principalColumn: "Id");
        }
    }
}
