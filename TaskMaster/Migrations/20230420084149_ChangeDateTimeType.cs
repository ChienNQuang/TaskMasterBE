using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace TaskMaster.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDateTimeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<LocalDateTime>(
                name: "DueDate",
                table: "Tasks",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<LocalDateTime>(
                name: "StartDate",
                table: "Projects",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<LocalDateTime>(
                name: "EndDate",
                table: "Projects",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "DueDate",
                table: "Tasks",
                type: "date",
                nullable: false,
                oldClrType: typeof(LocalDateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "StartDate",
                table: "Projects",
                type: "date",
                nullable: false,
                oldClrType: typeof(LocalDateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "EndDate",
                table: "Projects",
                type: "date",
                nullable: false,
                oldClrType: typeof(LocalDateTime),
                oldType: "timestamp without time zone");
        }
    }
}
