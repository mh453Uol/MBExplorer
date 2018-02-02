using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MBExplorer.Migrations
{
    public partial class fixedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Bookmarks");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Bookmarks",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParentPath",
                table: "Bookmarks",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReadOnly",
                table: "Bookmarks",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Bookmarks");

            migrationBuilder.DropColumn(
                name: "ParentPath",
                table: "Bookmarks");

            migrationBuilder.DropColumn(
                name: "ReadOnly",
                table: "Bookmarks");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Bookmarks",
                maxLength: 250,
                nullable: true);
        }
    }
}
