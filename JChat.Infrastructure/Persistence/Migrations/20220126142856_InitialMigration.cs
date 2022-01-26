using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JChat.Infrastructure.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "message_priorities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    priority = table.Column<short>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_message_priorities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "message_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_message_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reactions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    icon = table.Column<string>(type: "text", nullable: false),
                    color = table.Column<string>(type: "char(9)", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reactions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    firstname = table.Column<string>(type: "text", nullable: false),
                    lastname = table.Column<string>(type: "text", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "message_highlights",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    message_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modified_by_id = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_by_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_message_highlights", x => x.id);
                    table.ForeignKey(
                        name: "fk_message_highlights_users_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_message_highlights_users_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_message_highlights_users_last_modified_by_id",
                        column: x => x.last_modified_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "message_recipients",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    recipient_id = table.Column<Guid>(type: "uuid", nullable: false),
                    message_id = table.Column<Guid>(type: "uuid", nullable: false),
                    channel_id = table.Column<Guid>(type: "uuid", nullable: false),
                    forwarded_by_id = table.Column<Guid>(type: "uuid", nullable: true),
                    received_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    read_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    confirmed_visualization_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modified_by_id = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_by_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_message_recipients", x => x.id);
                    table.ForeignKey(
                        name: "fk_message_recipients_message_recipients_forwarded_by_id",
                        column: x => x.forwarded_by_id,
                        principalTable: "message_recipients",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_message_recipients_users_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_message_recipients_users_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_message_recipients_users_last_modified_by_id",
                        column: x => x.last_modified_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    message_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    message_priority_id = table.Column<Guid>(type: "uuid", nullable: false),
                    replying_to_id = table.Column<Guid>(type: "uuid", nullable: true),
                    body = table.Column<string>(type: "text", nullable: false),
                    meta = table.Column<string>(type: "jsonb", nullable: false),
                    expiration_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modified_by_id = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_by_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_messages", x => x.id);
                    table.ForeignKey(
                        name: "fk_messages_users_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_messages_users_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_messages_users_last_modified_by_id",
                        column: x => x.last_modified_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "workspaces",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modified_by_id = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_by_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_workspaces", x => x.id);
                    table.ForeignKey(
                        name: "fk_workspaces_users_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_workspaces_users_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_workspaces_users_last_modified_by_id",
                        column: x => x.last_modified_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "message_reactions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    message_id = table.Column<Guid>(type: "uuid", nullable: false),
                    reaction_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modified_by_id = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_by_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_message_reactions", x => x.id);
                    table.ForeignKey(
                        name: "fk_message_reactions_messages_message_id",
                        column: x => x.message_id,
                        principalTable: "messages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_message_reactions_users_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_message_reactions_users_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_message_reactions_users_last_modified_by_id",
                        column: x => x.last_modified_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "channels",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    workspace_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    is_private = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modified_by_id = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_by_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_channels", x => x.id);
                    table.ForeignKey(
                        name: "fk_channels_users_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_channels_users_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_channels_users_last_modified_by_id",
                        column: x => x.last_modified_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_channels_workspaces_workspace_id",
                        column: x => x.workspace_id,
                        principalTable: "workspaces",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_workspaces",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    workspace_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    admin = table.Column<bool>(type: "boolean", nullable: false),
                    banishment_reason = table.Column<string>(type: "text", nullable: true),
                    banished_by_id = table.Column<Guid>(type: "uuid", nullable: true),
                    accepted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    rejected_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by_id = table.Column<Guid>(type: "uuid", nullable: true),
                    last_modified_by_id = table.Column<Guid>(type: "uuid", nullable: true),
                    deleted_by_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_workspaces", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_workspaces_users_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_user_workspaces_users_deleted_by_id",
                        column: x => x.deleted_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_user_workspaces_users_last_modified_by_id",
                        column: x => x.last_modified_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_user_workspaces_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_user_workspaces_workspaces_workspace_id",
                        column: x => x.workspace_id,
                        principalTable: "workspaces",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "channel_users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    channel_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    is_admin = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_channel_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_channel_users_channels_channel_id",
                        column: x => x.channel_id,
                        principalTable: "channels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_channel_users_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "message_priorities",
                columns: new[] { "id", "created_at", "deleted_at", "name", "priority", "updated_at" },
                values: new object[,]
                {
                    { new Guid("17b04c0f-58f3-48ba-901f-0c8266469d3f"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.priority.requires_confirmation_snooze", (short)128, null },
                    { new Guid("2f0d6895-2f60-43d2-82a6-3e5d361ab1de"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.priority.requires_confirmation", (short)100, null },
                    { new Guid("633f5c7a-d1b6-4b9f-8bf7-2dbe53ce9922"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.priority.normal", (short)0, null },
                    { new Guid("b01b3ea0-e48f-4a3e-aad9-d3b26cfdedad"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.priority.snooze", (short)50, null }
                });

            migrationBuilder.InsertData(
                table: "message_types",
                columns: new[] { "id", "created_at", "deleted_at", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("001dbae7-c0d5-43bc-8ce8-2d0e0ec2c54d"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.type.video", null },
                    { new Guid("0ef0564b-7b4a-49fb-92c7-7ab14067716b"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.type.audio", null },
                    { new Guid("5ddb5056-ddf4-4b1b-9cc0-3a7f232b8148"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.type.gif", null },
                    { new Guid("65cab42b-3ff7-4f13-b90f-91abaa8228f1"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.type.text", null },
                    { new Guid("90f23ea7-ddcc-423e-ad40-a37bc9c8bc80"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.type.image", null }
                });

            migrationBuilder.InsertData(
                table: "reactions",
                columns: new[] { "id", "color", "created_at", "deleted_at", "icon", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("4abe01ae-55ce-4976-be42-29069dc74b68"), "DCB9DA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "curious", "reaction.curious", null },
                    { new Guid("5cc8585c-c6de-49fc-b15b-614d2b3988c1"), "6EAD51", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "celebrate", "reaction.celebrate", null },
                    { new Guid("843c64cb-c38a-431f-93a5-096655efca0a"), "CE5044", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "rocket", "reaction.rocket", null },
                    { new Guid("9514777c-4d6a-4c93-8a85-489405a7828a"), "F0B85F", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "insightful", "reaction.insightful", null },
                    { new Guid("c05b0de3-541e-4cdb-bcbb-b9f3aaef0187"), "DA7150", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "love", "reaction.love", null },
                    { new Guid("cca15e67-9ee5-47ea-bcfa-15061d97f70f"), "FFFFFF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "eyes", "reaction.eyes", null },
                    { new Guid("ec025488-93e2-42a8-927c-7cb586bb3301"), "1A85BA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "thumbs-up", "reaction.like", null }
                });

            migrationBuilder.CreateIndex(
                name: "ix_channel_users_channel_id_user_id",
                table: "channel_users",
                columns: new[] { "channel_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_channel_users_user_id",
                table: "channel_users",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_channels_created_by_id",
                table: "channels",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_channels_deleted_by_id",
                table: "channels",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_channels_last_modified_by_id",
                table: "channels",
                column: "last_modified_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_channels_name_workspace_id",
                table: "channels",
                columns: new[] { "name", "workspace_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_channels_workspace_id",
                table: "channels",
                column: "workspace_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_highlights_created_by_id",
                table: "message_highlights",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_highlights_deleted_by_id",
                table: "message_highlights",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_highlights_last_modified_by_id",
                table: "message_highlights",
                column: "last_modified_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_reactions_created_by_id",
                table: "message_reactions",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_reactions_deleted_by_id",
                table: "message_reactions",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_reactions_last_modified_by_id",
                table: "message_reactions",
                column: "last_modified_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_reactions_message_id",
                table: "message_reactions",
                column: "message_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_recipients_created_by_id",
                table: "message_recipients",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_recipients_deleted_by_id",
                table: "message_recipients",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_recipients_forwarded_by_id",
                table: "message_recipients",
                column: "forwarded_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_recipients_last_modified_by_id",
                table: "message_recipients",
                column: "last_modified_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_created_by_id",
                table: "messages",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_deleted_by_id",
                table: "messages",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_messages_last_modified_by_id",
                table: "messages",
                column: "last_modified_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_workspaces_created_by_id",
                table: "user_workspaces",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_workspaces_deleted_by_id",
                table: "user_workspaces",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_workspaces_last_modified_by_id",
                table: "user_workspaces",
                column: "last_modified_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_workspaces_user_id_workspace_id",
                table: "user_workspaces",
                columns: new[] { "user_id", "workspace_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_user_workspaces_workspace_id",
                table: "user_workspaces",
                column: "workspace_id");

            migrationBuilder.CreateIndex(
                name: "ix_workspaces_created_by_id",
                table: "workspaces",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_workspaces_deleted_by_id",
                table: "workspaces",
                column: "deleted_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_workspaces_last_modified_by_id",
                table: "workspaces",
                column: "last_modified_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_workspaces_name",
                table: "workspaces",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "channel_users");

            migrationBuilder.DropTable(
                name: "message_highlights");

            migrationBuilder.DropTable(
                name: "message_priorities");

            migrationBuilder.DropTable(
                name: "message_reactions");

            migrationBuilder.DropTable(
                name: "message_recipients");

            migrationBuilder.DropTable(
                name: "message_types");

            migrationBuilder.DropTable(
                name: "reactions");

            migrationBuilder.DropTable(
                name: "user_workspaces");

            migrationBuilder.DropTable(
                name: "channels");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "workspaces");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
