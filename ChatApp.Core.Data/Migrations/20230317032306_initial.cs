using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApp.Core.Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    ProfileImageSrc = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedByID = table.Column<Guid>(type: "uuid", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Inboxes",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerID = table.Column<Guid>(type: "uuid", nullable: false),
                    ReceiverID = table.Column<Guid>(type: "uuid", nullable: false),
                    OwnerDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ReceiverDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedByID = table.Column<Guid>(type: "uuid", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inboxes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Inboxes_Users_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Inboxes_Users_ReceiverID",
                        column: x => x.ReceiverID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uuid", nullable: false),
                    InboxID = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    SenderID = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    ReceiverDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    SeenStatus = table.Column<bool>(type: "boolean", nullable: false),
                    DeliveredStatus = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedByID = table.Column<Guid>(type: "uuid", nullable: true),
                    Deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Messages_Inboxes_InboxID",
                        column: x => x.InboxID,
                        principalTable: "Inboxes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_Users_SenderID",
                        column: x => x.SenderID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inboxes_OwnerID",
                table: "Inboxes",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Inboxes_ReceiverID",
                table: "Inboxes",
                column: "ReceiverID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_InboxID",
                table: "Messages",
                column: "InboxID");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderID",
                table: "Messages",
                column: "SenderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Inboxes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
