using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class addMembersIdsId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MembersIds",
                table: "MembersIds");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "MembersIds",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "MembersIds",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MembersIds",
                table: "MembersIds",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MembersIds",
                table: "MembersIds");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "MembersIds");

            migrationBuilder.AlterColumn<string>(
                name: "MemberId",
                table: "MembersIds",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MembersIds",
                table: "MembersIds",
                column: "MemberId");
        }
    }
}
