using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MBExplorer.Migrations
{
    public partial class notUsingParentPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentPath",
                table: "Bookmarks");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Bookmarks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookmarks_ParentId",
                table: "Bookmarks",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookmarks_Bookmarks_ParentId",
                table: "Bookmarks",
                column: "ParentId",
                principalTable: "Bookmarks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookmarks_Bookmarks_ParentId",
                table: "Bookmarks");

            migrationBuilder.DropIndex(
                name: "IX_Bookmarks_ParentId",
                table: "Bookmarks");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Bookmarks");

            migrationBuilder.AddColumn<string>(
                name: "ParentPath",
                table: "Bookmarks",
                nullable: true);
        }
    }
}
