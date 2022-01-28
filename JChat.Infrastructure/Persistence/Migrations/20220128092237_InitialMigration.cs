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
                    body_type = table.Column<short>(type: "smallint", nullable: false),
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

            migrationBuilder.CreateTable(
                name: "message_projections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    channel_id = table.Column<Guid>(type: "uuid", nullable: false),
                    recipient_id = table.Column<Guid>(type: "uuid", nullable: false),
                    message_id = table.Column<Guid>(type: "uuid", nullable: false),
                    replying_to_id = table.Column<Guid>(type: "uuid", nullable: true),
                    forwarded_by_id = table.Column<Guid>(type: "uuid", nullable: true),
                    body = table.Column<string>(type: "text", nullable: false),
                    meta = table.Column<string>(type: "text", nullable: false),
                    body_type = table.Column<int>(type: "integer", nullable: false),
                    priority_id = table.Column<Guid>(type: "uuid", nullable: false),
                    reactions = table.Column<string>(type: "jsonb", nullable: false),
                    received_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    read_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    confirmed_visualization_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_message_projections", x => x.id);
                    table.ForeignKey(
                        name: "fk_message_projections_channels_channel_id",
                        column: x => x.channel_id,
                        principalTable: "channels",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_message_projections_message_priorities_priority_id",
                        column: x => x.priority_id,
                        principalTable: "message_priorities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_message_projections_messages_message_id",
                        column: x => x.message_id,
                        principalTable: "messages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_message_projections_messages_replying_to_id",
                        column: x => x.replying_to_id,
                        principalTable: "messages",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_message_projections_users_forwarded_by_id",
                        column: x => x.forwarded_by_id,
                        principalTable: "users",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_message_projections_users_recipient_id",
                        column: x => x.recipient_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "message_body_types",
                columns: new[] { "id", "body_type", "created_at", "deleted_at", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("319f34b9-af34-447d-bb6a-cab9143fa96b"), (short)3, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.type.video", null },
                    { new Guid("3eb4f953-c45f-4076-b9e7-ecc3484b89d3"), (short)4, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.type.gif", null },
                    { new Guid("b6db22e2-bd19-41b0-8b8d-c5ce3f1cf1e0"), (short)0, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.type.text", null },
                    { new Guid("d79fc8df-fda7-4ec5-b83e-dc4b05853df2"), (short)1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.type.audio", null },
                    { new Guid("e01c901d-3beb-49a2-8625-2e3d51bbc127"), (short)2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.type.image", null }
                });

            migrationBuilder.InsertData(
                table: "message_priorities",
                columns: new[] { "id", "created_at", "deleted_at", "name", "priority", "updated_at" },
                values: new object[,]
                {
                    { new Guid("19b2180a-ed00-4bad-9fe2-fb3c09119303"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.priority.snooze", (short)50, null },
                    { new Guid("47ed3f27-b5ad-4570-b0a2-70551231010e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.priority.requires_confirmation_snooze", (short)128, null },
                    { new Guid("b33c075f-f5d3-4ee4-8cc2-0248d8ec1d1e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.priority.normal", (short)0, null },
                    { new Guid("d05c7d68-0413-493b-ac1d-70c26a0a0b0c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "message.priority.requires_confirmation", (short)100, null }
                });

            migrationBuilder.InsertData(
                table: "reactions",
                columns: new[] { "id", "color", "created_at", "deleted_at", "icon", "name", "updated_at" },
                values: new object[,]
                {
                    { new Guid("028bc8f7-00d0-4ba4-8ec6-5adab2e39351"), "DCB9DA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "curious", "reaction.curious", null },
                    { new Guid("08f5332a-bf09-4432-a704-c3ce1d19ff06"), "1A85BA", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "thumbs-up", "reaction.like", null },
                    { new Guid("23615f68-f0fb-43d6-ae1f-29c208f81ef1"), "6EAD51", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "celebrate", "reaction.celebrate", null },
                    { new Guid("61bb08a5-de68-4b84-8450-a99ba5a796b4"), "F0B85F", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "insightful", "reaction.insightful", null },
                    { new Guid("910ce222-b526-46e4-a7c9-6bf73bd9861a"), "DA7150", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "love", "reaction.love", null },
                    { new Guid("da361619-dca3-4835-8d8a-00487f031bc6"), "FFFFFF", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "eyes", "reaction.eyes", null },
                    { new Guid("fcb06e77-80eb-4f8e-9ef0-cd715e488fff"), "CE5044", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "rocket", "reaction.rocket", null }
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
                name: "ix_message_projections_channel_id_recipient_id",
                table: "message_projections",
                columns: new[] { "channel_id", "recipient_id" });

            migrationBuilder.CreateIndex(
                name: "ix_message_projections_forwarded_by_id",
                table: "message_projections",
                column: "forwarded_by_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_projections_message_id",
                table: "message_projections",
                column: "message_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_projections_priority_id",
                table: "message_projections",
                column: "priority_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_projections_recipient_id",
                table: "message_projections",
                column: "recipient_id");

            migrationBuilder.CreateIndex(
                name: "ix_message_projections_replying_to_id",
                table: "message_projections",
                column: "replying_to_id");

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
                name: "message_body_types");

            migrationBuilder.DropTable(
                name: "message_highlights");

            migrationBuilder.DropTable(
                name: "message_projections");

            migrationBuilder.DropTable(
                name: "message_reactions");

            migrationBuilder.DropTable(
                name: "message_recipients");

            migrationBuilder.DropTable(
                name: "reactions");

            migrationBuilder.DropTable(
                name: "user_workspaces");

            migrationBuilder.DropTable(
                name: "channels");

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
