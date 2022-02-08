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
                name: "message_body_types",
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
                    table.PrimaryKey("pk_message_body_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "message_priorities",
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
                    table.PrimaryKey("pk_message_priorities", x => x.id);
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
                name: "message_projections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    channel_id = table.Column<Guid>(type: "uuid", nullable: false),
                    recipient_id = table.Column<Guid>(type: "uuid", nullable: false),
                    message_id = table.Column<Guid>(type: "uuid", nullable: false),
                    priority_id = table.Column<Guid>(type: "uuid", nullable: false),
                    replying_to_id = table.Column<Guid>(type: "uuid", nullable: true),
                    forwarded_by_id = table.Column<Guid>(type: "uuid", nullable: true),
                    sender_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sender_name = table.Column<string>(type: "text", nullable: false),
                    is_inbound = table.Column<bool>(type: "boolean", nullable: false),
                    body = table.Column<string>(type: "text", nullable: false),
                    meta = table.Column<string>(type: "text", nullable: false),
                    body_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    reactions = table.Column<string>(type: "jsonb", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    received_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    read_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    confirmed_visualization_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_message_projections", x => x.id);
                    table.ForeignKey(
                        name: "fk_message_projections_message_body_types_body_type_id",
                        column: x => x.body_type_id,
                        principalTable: "message_body_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_message_projections_message_priorities_priority_id",
                        column: x => x.priority_id,
                        principalTable: "message_priorities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "messages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    body_type_id = table.Column<Guid>(type: "uuid", nullable: false),
                    priority_id = table.Column<Guid>(type: "uuid", nullable: false),
                    replying_to_id = table.Column<Guid>(type: "uuid", nullable: true),
                    message_body_type_id = table.Column<Guid>(type: "uuid", nullable: false),
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
                        name: "fk_message_recipients_messages_message_id",
                        column: x => x.message_id,
                        principalTable: "messages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    workspace_id = table.Column<Guid>(type: "uuid", nullable: true),
                    meta = table.Column<string>(type: "jsonb", nullable: false, defaultValue: "{}"),
                    type = table.Column<short>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    read_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notifications", x => x.id);
                    table.ForeignKey(
                        name: "fk_notifications_users_created_by_id",
                        column: x => x.created_by_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_notifications_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_notifications_workspaces_workspace_id",
                        column: x => x.workspace_id,
                        principalTable: "workspaces",
                        principalColumn: "id");
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
                table: "message_body_types",
                columns: new[] { "id", "created_at", "deleted_at", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("0bad45cd-595e-48c9-aff8-b927e2b30851"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.type.audio", null },
                    { new Guid("35cbb295-74cc-4157-9253-91c76492a1f2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.type.video", null },
                    { new Guid("60f47ec7-5025-4e55-892b-7b1f2c444ee0"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.type.gif", null },
                    { new Guid("87159cfd-1db5-42ec-9250-084b3fc41964"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.type.text", null },
                    { new Guid("d10bef76-e162-41dd-a625-bb04d24b0064"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.type.channel_event", null },
                    { new Guid("f082a1f1-74de-4349-9ed6-24fe42188aea"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.type.image", null }
                });

            migrationBuilder.InsertData(
                table: "message_priorities",
                columns: new[] { "id", "created_at", "deleted_at", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("195348d7-5de2-465c-ab01-89e76b7823cf"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.priority.normal", null },
                    { new Guid("2efd5971-81aa-4f7e-94ff-c49b2827f945"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.priority.requires_confirmation_snooze", null },
                    { new Guid("55cd213f-5a4f-40a1-8b3e-1b0aef8c0847"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.priority.requires_confirmation", null },
                    { new Guid("8eafa8fe-dfae-4143-8abe-53e57d2d427c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.priority.snooze", null }
                });

            migrationBuilder.InsertData(
                table: "reactions",
                columns: new[] { "id", "color", "created_at", "deleted_at", "icon", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("009770db-2901-4462-aded-0b55a3d42939"), "6EAD51", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "celebrate", "reaction.celebrate", null },
                    { new Guid("58b572f9-16dd-457d-9dc3-2fc531ba2436"), "CE5044", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "rocket", "reaction.rocket", null },
                    { new Guid("a8d26691-b652-46ee-a9c0-5faeb52c340a"), "FFFFFF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "eyes", "reaction.eyes", null },
                    { new Guid("b4ac3b4a-ddba-48e4-b04b-d71aacbe5a64"), "DCB9DA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "curious", "reaction.curious", null },
                    { new Guid("c36aeceb-d569-4a34-a93f-9063c702e91d"), "F0B85F", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "insightful", "reaction.insightful", null },
                    { new Guid("e342b7ac-cd01-40fd-a60f-63a03706ccd8"), "1A85BA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "thumbs-up", "reaction.like", null },
                    { new Guid("f83577a7-7dad-4cc0-a2b9-d3e184f6a863"), "DA7150", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "love", "reaction.love", null }
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
                name: "ix_message_projections_body_type_id",
                table: "message_projections",
                column: "body_type_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_projections_channel_id_recipient_id",
                table: "message_projections",
                columns: new[] { "channel_id", "recipient_id" });

            migrationBuilder.CreateIndex(
                name: "ix_message_projections_priority_id",
                table: "message_projections",
                column: "priority_id");

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
                name: "ix_message_recipients_message_id",
                table: "message_recipients",
                column: "message_id");

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
                name: "ix_notifications_created_by_id",
                table: "notifications",
                column: "created_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_notifications_user_id",
                table: "notifications",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_notifications_workspace_id",
                table: "notifications",
                column: "workspace_id");

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
                name: "message_projections");

            migrationBuilder.DropTable(
                name: "message_reactions");

            migrationBuilder.DropTable(
                name: "message_recipients");

            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "reactions");

            migrationBuilder.DropTable(
                name: "user_workspaces");

            migrationBuilder.DropTable(
                name: "channels");

            migrationBuilder.DropTable(
                name: "message_body_types");

            migrationBuilder.DropTable(
                name: "message_priorities");

            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "workspaces");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
