using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dev_connect.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddInvitationTokenExpirytoUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "invitation_token_expiry",
                table: "users",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "invitation_token_expiry", "password_hash" },
                values: new object[] { new DateTime(2025, 6, 17, 13, 1, 33, 544, DateTimeKind.Utc).AddTicks(4525), null, "AQAAAAIAAYagAAAAEHX+7uKynY/U/xiuPxPfWPbicqbc9pb77vrlX8Un8hAARWIsGYpNBjORPl2K8Nkc8g==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "invitation_token_expiry",
                table: "users");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                columns: new[] { "created_at", "password_hash" },
                values: new object[] { new DateTime(2025, 6, 17, 11, 17, 28, 523, DateTimeKind.Utc).AddTicks(7515), "AQAAAAIAAYagAAAAEKd4myfPtMeNRw506C3zX159cIxzXl+LCdaMlfRJjRYYch47nkV+YcHlTZYBBHjUSQ==" });
        }
    }
}
