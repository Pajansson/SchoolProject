using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OOAD_assignment_1.Data.Migrations
{
    public partial class TimePeriodAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimePeriodId",
                table: "Accountabilities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TimePeriods",
                columns: table => new
                {
                    TimePeriodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimePeriods", x => x.TimePeriodId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accountabilities_TimePeriodId",
                table: "Accountabilities",
                column: "TimePeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accountabilities_TimePeriods_TimePeriodId",
                table: "Accountabilities",
                column: "TimePeriodId",
                principalTable: "TimePeriods",
                principalColumn: "TimePeriodId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accountabilities_TimePeriods_TimePeriodId",
                table: "Accountabilities");

            migrationBuilder.DropTable(
                name: "TimePeriods");

            migrationBuilder.DropIndex(
                name: "IX_Accountabilities_TimePeriodId",
                table: "Accountabilities");

            migrationBuilder.DropColumn(
                name: "TimePeriodId",
                table: "Accountabilities");
        }
    }
}
