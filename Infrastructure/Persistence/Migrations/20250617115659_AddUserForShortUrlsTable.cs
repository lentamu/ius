using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserForShortUrlsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "user_id",
                table: "short_urls",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_short_urls_user_id",
                table: "short_urls",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_short_urls_users_id",
                table: "short_urls",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_short_urls_users_id",
                table: "short_urls");

            migrationBuilder.DropIndex(
                name: "ix_short_urls_user_id",
                table: "short_urls");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "short_urls");
        }
    }
}
