using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JernaClassLib.Migrations
{
    /// <inheritdoc />
    public partial class first : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "auth_code",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    is_valid_until = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("auth_code_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "period_length",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    display_name = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: true),
                    timespan = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("period_length_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tag_name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tag_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    email = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    isadmin = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "item",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    item_name = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    price = table.Column<decimal>(type: "money", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    image = table.Column<byte[]>(type: "bytea", nullable: true),
                    isdisplayed = table.Column<bool>(type: "boolean", nullable: false),
                    mediafile = table.Column<byte[]>(type: "bytea", nullable: true),
                    is_digital = table.Column<bool>(type: "boolean", nullable: true),
                    is_physical = table.Column<bool>(type: "boolean", nullable: true),
                    period_length_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("item_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_period_length_id",
                        column: x => x.period_length_id,
                        principalTable: "period_length",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "cart",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cart_pkey", x => x.id);
                    table.ForeignKey(
                        name: "cart_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "purchase",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    purchase_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    taxpercent = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("purchase_pkey", x => x.id);
                    table.ForeignKey(
                        name: "purchase_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "temp_code",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    createdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    userid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("authcodes_pkey", x => x.id);
                    table.ForeignKey(
                        name: "authcodes_userid_fkey",
                        column: x => x.userid,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_auth_code",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    auth_code_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_auth_code_pkey", x => x.id);
                    table.ForeignKey(
                        name: "user_auth_code_auth_code_id_fkey",
                        column: x => x.auth_code_id,
                        principalTable: "auth_code",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "user_auth_code_user_id_fkey",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tag_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    tag_id = table.Column<int>(type: "integer", nullable: true),
                    item_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tag_item_pkey", x => x.id);
                    table.ForeignKey(
                        name: "tag_item_item_id_fkey",
                        column: x => x.item_id,
                        principalTable: "item",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "tag_item_tag_id_fkey",
                        column: x => x.tag_id,
                        principalTable: "tag",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "tools_for_parents_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sub_id = table.Column<int>(type: "integer", nullable: false),
                    item_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("tools_for_parents_item_pkey", x => x.id);
                    table.ForeignKey(
                        name: "tools_for_parents_item_item_id_fkey",
                        column: x => x.item_id,
                        principalTable: "item",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "tools_for_parents_item_sub_id_fkey",
                        column: x => x.sub_id,
                        principalTable: "item",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "cart_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    cart_id = table.Column<int>(type: "integer", nullable: true),
                    item_id = table.Column<int>(type: "integer", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: false),
                    contact_info = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cart_item_pkey", x => x.id);
                    table.ForeignKey(
                        name: "cart_item_cart_id_fkey",
                        column: x => x.cart_id,
                        principalTable: "cart",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "cart_item_item_id_fkey",
                        column: x => x.item_id,
                        principalTable: "item",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "admin_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    purchase_id = table.Column<int>(type: "integer", nullable: true),
                    fulfilled_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    notes = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("admin_history_pkey", x => x.id);
                    table.ForeignKey(
                        name: "admin_history_purchase_id_fkey",
                        column: x => x.purchase_id,
                        principalTable: "purchase",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "purchase_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    purchase_id = table.Column<int>(type: "integer", nullable: true),
                    item_id = table.Column<int>(type: "integer", nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("purchase_item_pkey", x => x.id);
                    table.ForeignKey(
                        name: "purchase_item_item_id_fkey",
                        column: x => x.item_id,
                        principalTable: "item",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "purchase_item_purchase_id_fkey",
                        column: x => x.purchase_id,
                        principalTable: "purchase",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    purchase_price = table.Column<decimal>(type: "money", nullable: true),
                    purchase_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("transaction_pkey", x => x.id);
                    table.ForeignKey(
                        name: "transaction_purchase_id_fkey",
                        column: x => x.purchase_id,
                        principalTable: "purchase",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "admin_history_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    admin_history_id = table.Column<int>(type: "integer", nullable: true),
                    item_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("admin_history_item_pkey", x => x.id);
                    table.ForeignKey(
                        name: "admin_history_item_admin_history_id_fkey",
                        column: x => x.admin_history_id,
                        principalTable: "admin_history",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "admin_history_item_item_id_fkey",
                        column: x => x.item_id,
                        principalTable: "item",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_admin_history_purchase_id",
                table: "admin_history",
                column: "purchase_id");

            migrationBuilder.CreateIndex(
                name: "IX_admin_history_item_admin_history_id",
                table: "admin_history_item",
                column: "admin_history_id");

            migrationBuilder.CreateIndex(
                name: "IX_admin_history_item_item_id",
                table: "admin_history_item",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_cart_user_id",
                table: "cart",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_cart_item_cart_id",
                table: "cart_item",
                column: "cart_id");

            migrationBuilder.CreateIndex(
                name: "IX_cart_item_item_id",
                table: "cart_item",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_item_period_length_id",
                table: "item",
                column: "period_length_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_user_id",
                table: "purchase",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_item_item_id",
                table: "purchase_item",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_purchase_item_purchase_id",
                table: "purchase_item",
                column: "purchase_id");

            migrationBuilder.CreateIndex(
                name: "IX_tag_item_item_id",
                table: "tag_item",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_tag_item_tag_id",
                table: "tag_item",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_temp_code_userid",
                table: "temp_code",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_tools_for_parents_item_item_id",
                table: "tools_for_parents_item",
                column: "item_id");

            migrationBuilder.CreateIndex(
                name: "IX_tools_for_parents_item_sub_id",
                table: "tools_for_parents_item",
                column: "sub_id");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_purchase_id",
                table: "transaction",
                column: "purchase_id");

            migrationBuilder.CreateIndex(
                name: "user_un",
                table: "user",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_auth_code_auth_code_id",
                table: "user_auth_code",
                column: "auth_code_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_auth_code_user_id",
                table: "user_auth_code",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admin_history_item");

            migrationBuilder.DropTable(
                name: "cart_item");

            migrationBuilder.DropTable(
                name: "purchase_item");

            migrationBuilder.DropTable(
                name: "tag_item");

            migrationBuilder.DropTable(
                name: "temp_code");

            migrationBuilder.DropTable(
                name: "tools_for_parents_item");

            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "user_auth_code");

            migrationBuilder.DropTable(
                name: "admin_history");

            migrationBuilder.DropTable(
                name: "cart");

            migrationBuilder.DropTable(
                name: "tag");

            migrationBuilder.DropTable(
                name: "item");

            migrationBuilder.DropTable(
                name: "auth_code");

            migrationBuilder.DropTable(
                name: "purchase");

            migrationBuilder.DropTable(
                name: "period_length");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
