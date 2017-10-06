using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OOAD_assignment_1.Data.Migrations
{
    public partial class ChangedAccountabilityKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Accountabilities",
                table: "Accountabilities");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Parties",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AccountabilityTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accountabilities",
                table: "Accountabilities",
                columns: new[] { "AccountableId", "CommissionerId", "AccountabilityTypeId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Accountabilities",
                table: "Accountabilities");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Parties",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "AccountabilityTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accountabilities",
                table: "Accountabilities",
                columns: new[] { "AccountableId", "CommissionerId" });
        }
    }
}
