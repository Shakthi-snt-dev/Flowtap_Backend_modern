using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Flowtap_Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addchat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
             migrationBuilder.CreateTable(
                name: "DirectMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
                    SenderId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipientId = table.Column<Guid>(type: "uuid", nullable: false),
                    Subject = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Body = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsComplaint = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsDeletedBySender = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsDeletedByRecipient = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectMessages", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_CompanyId",
                table: "DirectMessages",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_RecipientId",
                table: "DirectMessages",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_SenderId",
                table: "DirectMessages",
                column: "SenderId");

            // Composite index for inbox query performance
            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_RecipientId_IsRead",
                table: "DirectMessages",
                columns: new[] { "RecipientId", "IsRead" });
            migrationBuilder.DropIndex(
                name: "IX_DirectMessages_CompanyId",
                table: "DirectMessages");

            migrationBuilder.DropIndex(
                name: "IX_DirectMessages_RecipientId",
                table: "DirectMessages");

            migrationBuilder.DropIndex(
                name: "IX_DirectMessages_RecipientId_IsRead",
                table: "DirectMessages");

            migrationBuilder.DropIndex(
                name: "IX_DirectMessages_SenderId",
                table: "DirectMessages");

            // migrationBuilder.DropIndex(
            //     name: "IX_AdminBroadcasts_CompanyId",
            //     table: "AdminBroadcasts");

            // migrationBuilder.DropIndex(
            //     name: "IX_AdminBroadcasts_CompanyId_IsActive",
            //     table: "AdminBroadcasts");

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "DirectMessages",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<bool>(
                name: "IsRead",
                table: "DirectMessages",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeletedBySender",
                table: "DirectMessages",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeletedByRecipient",
                table: "DirectMessages",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsComplaint",
                table: "DirectMessages",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "DirectMessages",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(4000)",
                oldMaxLength: 4000);

            // migrationBuilder.AlterColumn<string>(
            //     name: "Subject",
            //     table: "AdminBroadcasts",
            //     type: "text",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(200)",
            //     oldMaxLength: 200);

            // migrationBuilder.AlterColumn<string>(
            //     name: "Severity",
            //     table: "AdminBroadcasts",
            //     type: "text",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(20)",
            //     oldMaxLength: 20,
            //     oldDefaultValue: "Information");

            // migrationBuilder.AlterColumn<string>(
            //     name: "SentBy",
            //     table: "AdminBroadcasts",
            //     type: "text",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(100)",
            //     oldMaxLength: 100,
            //     oldDefaultValue: "Admin");

            // migrationBuilder.AlterColumn<string>(
            //     name: "Message",
            //     table: "AdminBroadcasts",
            //     type: "text",
            //     nullable: false,
            //     oldClrType: typeof(string),
            //     oldType: "character varying(2000)",
            //     oldMaxLength: 2000);

            // migrationBuilder.AlterColumn<bool>(
            //     name: "IsActive",
            //     table: "AdminBroadcasts",
            //     type: "boolean",
            //     nullable: false,
            //     oldClrType: typeof(bool),
            //     oldType: "boolean",
            //     oldDefaultValue: true);

            // migrationBuilder.AddColumn<DateTime>(
            //     name: "EndDate",
            //     table: "AdminBroadcasts",
            //     type: "timestamp with time zone",
            //     nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "Priority",
            //     table: "AdminBroadcasts",
            //     type: "text",
            //     nullable: false,
            //     defaultValue: "");

            // migrationBuilder.AddColumn<DateTime>(
            //     name: "StartDate",
            //     table: "AdminBroadcasts",
            //     type: "timestamp with time zone",
            //     nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "TargetRole",
            //     table: "AdminBroadcasts",
            //     type: "text",
            //     nullable: true);

            // migrationBuilder.AddColumn<string>(
            //     name: "TargetType",
            //     table: "AdminBroadcasts",
            //     type: "text",
            //     nullable: false,
            //     defaultValue: "");

            // migrationBuilder.AddColumn<string>(
            //     name: "Type",
            //     table: "AdminBroadcasts",
            //     type: "text",
            //     nullable: false,
            //     defaultValue: "");

            // migrationBuilder.CreateTable(
            //     name: "AnnouncementTargets",
            //     columns: table => new
            //     {
            //         Id = table.Column<Guid>(type: "uuid", nullable: false),
            //         AdminBroadcastId = table.Column<Guid>(type: "uuid", nullable: false),
            //         LocationId = table.Column<Guid>(type: "uuid", nullable: false),
            //         CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_AnnouncementTargets", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_AnnouncementTargets_AdminBroadcasts_AdminBroadcastId",
            //             column: x => x.AdminBroadcastId,
            //             principalTable: "AdminBroadcasts",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "Conversations",
            //     columns: table => new
            //     {
            //         Id = table.Column<Guid>(type: "uuid", nullable: false),
            //         CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
            //         Title = table.Column<string>(type: "text", nullable: true),
            //         IsGroup = table.Column<bool>(type: "boolean", nullable: false),
            //         CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //         UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_Conversations", x => x.Id);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "UserNotifications",
            //     columns: table => new
            //     {
            //         Id = table.Column<Guid>(type: "uuid", nullable: false),
            //         CompanyId = table.Column<Guid>(type: "uuid", nullable: false),
            //         UserId = table.Column<Guid>(type: "uuid", nullable: false),
            //         Type = table.Column<string>(type: "text", nullable: false),
            //         Title = table.Column<string>(type: "text", nullable: false),
            //         Message = table.Column<string>(type: "text", nullable: false),
            //         ReferenceId = table.Column<Guid>(type: "uuid", nullable: true),
            //         ReferenceType = table.Column<string>(type: "text", nullable: true),
            //         IsRead = table.Column<bool>(type: "boolean", nullable: false),
            //         ReadAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            //         CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_UserNotifications", x => x.Id);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "ChatMessages",
            //     columns: table => new
            //     {
            //         Id = table.Column<Guid>(type: "uuid", nullable: false),
            //         ConversationId = table.Column<Guid>(type: "uuid", nullable: false),
            //         SenderId = table.Column<Guid>(type: "uuid", nullable: false),
            //         SenderName = table.Column<string>(type: "text", nullable: false),
            //         Body = table.Column<string>(type: "text", nullable: false),
            //         IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
            //         CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_ChatMessages", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_ChatMessages_Conversations_ConversationId",
            //             column: x => x.ConversationId,
            //             principalTable: "Conversations",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateTable(
            //     name: "ConversationParticipants",
            //     columns: table => new
            //     {
            //         Id = table.Column<Guid>(type: "uuid", nullable: false),
            //         ConversationId = table.Column<Guid>(type: "uuid", nullable: false),
            //         UserId = table.Column<Guid>(type: "uuid", nullable: false),
            //         JoinedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
            //         LastSeenAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
            //         IsActive = table.Column<bool>(type: "boolean", nullable: false)
            //     },
            //     constraints: table =>
            //     {
            //         table.PrimaryKey("PK_ConversationParticipants", x => x.Id);
            //         table.ForeignKey(
            //             name: "FK_ConversationParticipants_Conversations_ConversationId",
            //             column: x => x.ConversationId,
            //             principalTable: "Conversations",
            //             principalColumn: "Id",
            //             onDelete: ReferentialAction.Cascade);
            //     });

            // migrationBuilder.CreateIndex(
            //     name: "IX_AnnouncementTargets_AdminBroadcastId",
            //     table: "AnnouncementTargets",
            //     column: "AdminBroadcastId");

            // migrationBuilder.CreateIndex(
            //     name: "IX_ChatMessages_ConversationId",
            //     table: "ChatMessages",
            //     column: "ConversationId");

            // migrationBuilder.CreateIndex(
            //     name: "IX_ConversationParticipants_ConversationId",
            //     table: "ConversationParticipants",
            //     column: "ConversationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnouncementTargets");

            migrationBuilder.DropTable(
                name: "ChatMessages");

            migrationBuilder.DropTable(
                name: "ConversationParticipants");

            migrationBuilder.DropTable(
                name: "UserNotifications");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "AdminBroadcasts");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "AdminBroadcasts");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "AdminBroadcasts");

            migrationBuilder.DropColumn(
                name: "TargetRole",
                table: "AdminBroadcasts");

            migrationBuilder.DropColumn(
                name: "TargetType",
                table: "AdminBroadcasts");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AdminBroadcasts");

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "DirectMessages",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "IsRead",
                table: "DirectMessages",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeletedBySender",
                table: "DirectMessages",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeletedByRecipient",
                table: "DirectMessages",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<bool>(
                name: "IsComplaint",
                table: "DirectMessages",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "Body",
                table: "DirectMessages",
                type: "character varying(4000)",
                maxLength: 4000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Subject",
                table: "AdminBroadcasts",
                type: "character varying(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Severity",
                table: "AdminBroadcasts",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "Information",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "SentBy",
                table: "AdminBroadcasts",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "Admin",
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "AdminBroadcasts",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "AdminBroadcasts",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_CompanyId",
                table: "DirectMessages",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_RecipientId",
                table: "DirectMessages",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_RecipientId_IsRead",
                table: "DirectMessages",
                columns: new[] { "RecipientId", "IsRead" });

            migrationBuilder.CreateIndex(
                name: "IX_DirectMessages_SenderId",
                table: "DirectMessages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminBroadcasts_CompanyId",
                table: "AdminBroadcasts",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminBroadcasts_CompanyId_IsActive",
                table: "AdminBroadcasts",
                columns: new[] { "CompanyId", "IsActive" });
        }
    }
}
