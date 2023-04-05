using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class addedSubGoal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubGoalId",
                table: "GoalTasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubGoal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Progress = table.Column<int>(type: "int", nullable: false),
                    GoalId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubGoal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubGoal_GoalList_GoalId",
                        column: x => x.GoalId,
                        principalTable: "GoalList",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoalTasks_SubGoalId",
                table: "GoalTasks",
                column: "SubGoalId");

            migrationBuilder.CreateIndex(
                name: "IX_SubGoal_GoalId",
                table: "SubGoal",
                column: "GoalId");

            migrationBuilder.AddForeignKey(
                name: "FK_GoalTasks_SubGoal_SubGoalId",
                table: "GoalTasks",
                column: "SubGoalId",
                principalTable: "SubGoal",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GoalTasks_SubGoal_SubGoalId",
                table: "GoalTasks");

            migrationBuilder.DropTable(
                name: "SubGoal");

            migrationBuilder.DropIndex(
                name: "IX_GoalTasks_SubGoalId",
                table: "GoalTasks");

            migrationBuilder.DropColumn(
                name: "SubGoalId",
                table: "GoalTasks");
        }
    }
}
