using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PaymentApplyProject.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class setupdb1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InsertLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InsertedId = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false),
                    TableName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false),
                    AddedUserId = table.Column<int>(type: "integer", nullable: false),
                    EditedUserId = table.Column<int>(type: "integer", nullable: false),
                    AddDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EditDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InsertLogs", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Withdraws_BankId",
                table: "Withdraws",
                column: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Withdraws_Banks_BankId",
                table: "Withdraws",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Withdraws_Banks_BankId",
                table: "Withdraws");

            migrationBuilder.DropTable(
                name: "InsertLogs");

            migrationBuilder.DropIndex(
                name: "IX_Withdraws_BankId",
                table: "Withdraws");
        }
    }
}
