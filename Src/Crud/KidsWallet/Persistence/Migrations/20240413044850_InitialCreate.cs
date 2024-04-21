using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KidsWallet.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "kids_wallet");

            migrationBuilder.CreateTable(
                name: "kid_wallets",
                schema: "kids_wallet",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    KidId = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kid_wallets", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "kid_accounts",
                schema: "kids_wallet",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    kid_wallet_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    balance = table.Column<decimal>(type: "numeric", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kid_accounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_kid_accounts_kid_wallets_kid_wallet_id",
                        column: x => x.kid_wallet_id,
                        principalSchema: "kids_wallet",
                        principalTable: "kid_wallets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "kid_account_operations",
                schema: "kids_wallet",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    kid_account_id = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<decimal>(type: "numeric", nullable: false),
                    description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    due_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    operation_type = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kid_account_operations", x => x.id);
                    table.ForeignKey(
                        name: "FK_kid_account_operations_kid_accounts_kid_account_id",
                        column: x => x.kid_account_id,
                        principalSchema: "kids_wallet",
                        principalTable: "kid_accounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_kid_account_operations_kid_account_id",
                schema: "kids_wallet",
                table: "kid_account_operations",
                column: "kid_account_id");

            migrationBuilder.CreateIndex(
                name: "IX_kid_accounts_kid_wallet_id",
                schema: "kids_wallet",
                table: "kid_accounts",
                column: "kid_wallet_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "kid_account_operations",
                schema: "kids_wallet");

            migrationBuilder.DropTable(
                name: "kid_accounts",
                schema: "kids_wallet");

            migrationBuilder.DropTable(
                name: "kid_wallets",
                schema: "kids_wallet");
        }
    }
}
