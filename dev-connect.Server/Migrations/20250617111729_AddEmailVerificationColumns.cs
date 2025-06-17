using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dev_connect.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailVerificationColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "invitation_send_at",
                table: "users",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "invitation_token",
                table: "users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "is_email_verified",
                table: "users",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "invitation_send_at", "invitation_token", "is_email_verified", "password_hash" },
                values: new object[] { new DateTime(2025, 6, 17, 11, 17, 28, 523, DateTimeKind.Utc).AddTicks(7515), null, null, false, "AQAAAAIAAYagAAAAEKd4myfPtMeNRw506C3zX159cIxzXl+LCdaMlfRJjRYYch47nkV+YcHlTZYBBHjUSQ==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "invitation_send_at",
                table: "users");

            migrationBuilder.DropColumn(
                name: "invitation_token",
                table: "users");

            migrationBuilder.DropColumn(
                name: "is_email_verified",
                table: "users");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password_hash" },
                values: new object[] { new DateTime(2025, 6, 12, 13, 32, 9, 502, DateTimeKind.Utc).AddTicks(8428), "AQAAAAIAAYagAAAAEHh0e5Wjd1ah4dwaF0IuQ2ajoLMx+ezEA4ewvfNmoKNxvy0anTX2hi8lWj+jrIP6mA==" });
        }
    }
}
