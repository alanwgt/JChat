﻿// <auto-generated />
using System;
using JChat.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JChat.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220115165841_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("JChat.Domain.Entities.Channel.Channel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<bool>("IsPrivate")
                        .HasColumnType("boolean")
                        .HasColumnName("is_private");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("WorkspaceId")
                        .HasColumnType("uuid")
                        .HasColumnName("workspace_id");

                    b.HasKey("Id")
                        .HasName("pk_channel");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_channel_user_id");

                    b.HasIndex("WorkspaceId")
                        .HasDatabaseName("ix_channel_workspace_id");

                    b.ToTable("channel", (string)null);
                });

            modelBuilder.Entity("JChat.Domain.Entities.Channel.ChannelUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("ChannelId")
                        .HasColumnType("uuid")
                        .HasColumnName("channel_id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean")
                        .HasColumnName("is_admin");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_channel_users");

                    b.HasIndex("ChannelId")
                        .HasDatabaseName("ix_channel_users_channel_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_channel_users_user_id");

                    b.ToTable("channel_users", (string)null);
                });

            modelBuilder.Entity("JChat.Domain.Entities.Message.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("body");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<DateTime?>("ExpirationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expiration_date");

                    b.Property<Guid>("MessagePriorityId")
                        .HasColumnType("uuid")
                        .HasColumnName("message_priority_id");

                    b.Property<Guid>("MessageTypeId")
                        .HasColumnType("uuid")
                        .HasColumnName("message_type_id");

                    b.Property<string>("Meta")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("meta");

                    b.Property<Guid?>("ReplyingToId")
                        .HasColumnType("uuid")
                        .HasColumnName("replying_to_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_messages");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_messages_user_id");

                    b.ToTable("messages", (string)null);
                });

            modelBuilder.Entity("JChat.Domain.Entities.Message.MessageHighlight", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uuid")
                        .HasColumnName("message_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_message_highlights");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_message_highlights_user_id");

                    b.ToTable("message_highlights", (string)null);
                });

            modelBuilder.Entity("JChat.Domain.Entities.Message.MessagePriority", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<int>("Priority")
                        .HasColumnType("char")
                        .HasColumnName("priority");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_message_priorities");

                    b.ToTable("message_priorities", (string)null);
                });

            modelBuilder.Entity("JChat.Domain.Entities.Message.MessageReaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uuid")
                        .HasColumnName("message_id");

                    b.Property<Guid>("ReactionId")
                        .HasColumnType("uuid")
                        .HasColumnName("reaction_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_message_reactions");

                    b.HasIndex("MessageId")
                        .HasDatabaseName("ix_message_reactions_message_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_message_reactions_user_id");

                    b.ToTable("message_reactions", (string)null);
                });

            modelBuilder.Entity("JChat.Domain.Entities.Message.MessageRecipient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<Guid>("ChannelId")
                        .HasColumnType("uuid")
                        .HasColumnName("channel_id");

                    b.Property<DateTime?>("ConfirmedVisualizationAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("confirmed_visualization_at");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<Guid?>("ForwardedById")
                        .HasColumnType("uuid")
                        .HasColumnName("forwarded_by_id");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uuid")
                        .HasColumnName("message_id");

                    b.Property<DateTime?>("ReadAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("read_at");

                    b.Property<DateTime?>("ReceivedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("received_at");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id")
                        .HasName("pk_message_recipients");

                    b.HasIndex("ForwardedById")
                        .HasDatabaseName("ix_message_recipients_forwarded_by_id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("ix_message_recipients_user_id");

                    b.ToTable("message_recipients", (string)null);
                });

            modelBuilder.Entity("JChat.Domain.Entities.Message.MessageType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_message_types");

                    b.ToTable("message_types", (string)null);
                });

            modelBuilder.Entity("JChat.Domain.Entities.Message.Reaction", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("char(9)")
                        .HasColumnName("color");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("icon");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_reactions");

                    b.ToTable("reactions", (string)null);
                });

            modelBuilder.Entity("JChat.Domain.Entities.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("firstname");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("lastname");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("username");

                    b.HasKey("Id")
                        .HasName("pk_users");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("JChat.Domain.Entities.User.UserWorkspace", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("Admin")
                        .HasColumnType("boolean")
                        .HasColumnName("admin");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uuid")
                        .HasColumnName("created_by_id");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_by_id");

                    b.Property<Guid?>("LastModifiedById")
                        .HasColumnType("uuid")
                        .HasColumnName("last_modified_by_id");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.Property<Guid>("WorkspaceId")
                        .HasColumnType("uuid")
                        .HasColumnName("workspace_id");

                    b.HasKey("Id")
                        .HasName("pk_user_workspaces");

                    b.HasIndex("CreatedById")
                        .HasDatabaseName("ix_user_workspaces_created_by_id");

                    b.HasIndex("DeletedById")
                        .HasDatabaseName("ix_user_workspaces_deleted_by_id");

                    b.HasIndex("LastModifiedById")
                        .HasDatabaseName("ix_user_workspaces_last_modified_by_id");

                    b.HasIndex("WorkspaceId")
                        .HasDatabaseName("ix_user_workspaces_workspace_id");

                    b.HasIndex("UserId", "WorkspaceId")
                        .IsUnique()
                        .HasDatabaseName("ix_user_workspaces_user_id_workspace_id");

                    b.ToTable("user_workspaces", (string)null);
                });

            modelBuilder.Entity("JChat.Domain.Entities.Workspace.Workspace", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("created_at");

                    b.Property<Guid?>("CreatedById")
                        .HasColumnType("uuid")
                        .HasColumnName("created_by_id");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("deleted_at");

                    b.Property<Guid?>("DeletedById")
                        .HasColumnType("uuid")
                        .HasColumnName("deleted_by_id");

                    b.Property<Guid?>("LastModifiedById")
                        .HasColumnType("uuid")
                        .HasColumnName("last_modified_by_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("name");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("updated_at");

                    b.HasKey("Id")
                        .HasName("pk_workspaces");

                    b.HasIndex("CreatedById")
                        .HasDatabaseName("ix_workspaces_created_by_id");

                    b.HasIndex("DeletedById")
                        .HasDatabaseName("ix_workspaces_deleted_by_id");

                    b.HasIndex("LastModifiedById")
                        .HasDatabaseName("ix_workspaces_last_modified_by_id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasDatabaseName("ix_workspaces_name");

                    b.ToTable("workspaces", (string)null);
                });

            modelBuilder.Entity("JChat.Domain.Entities.Channel.Channel", b =>
                {
                    b.HasOne("JChat.Domain.Entities.User.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_channel_users_user_id");

                    b.HasOne("JChat.Domain.Entities.Workspace.Workspace", "Workspace")
                        .WithMany()
                        .HasForeignKey("WorkspaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_channel_workspaces_workspace_id");

                    b.Navigation("User");

                    b.Navigation("Workspace");
                });

            modelBuilder.Entity("JChat.Domain.Entities.Channel.ChannelUser", b =>
                {
                    b.HasOne("JChat.Domain.Entities.Channel.Channel", null)
                        .WithMany("Users")
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_channel_users_channel_channel_id");

                    b.HasOne("JChat.Domain.Entities.User.User", null)
                        .WithMany("Channels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_channel_users_users_user_id");
                });

            modelBuilder.Entity("JChat.Domain.Entities.Message.Message", b =>
                {
                    b.HasOne("JChat.Domain.Entities.User.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_messages_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JChat.Domain.Entities.Message.MessageHighlight", b =>
                {
                    b.HasOne("JChat.Domain.Entities.User.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_message_highlights_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JChat.Domain.Entities.Message.MessageReaction", b =>
                {
                    b.HasOne("JChat.Domain.Entities.Message.Message", null)
                        .WithMany("Reactions")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_message_reactions_messages_message_id");

                    b.HasOne("JChat.Domain.Entities.User.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_message_reactions_users_user_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JChat.Domain.Entities.Message.MessageRecipient", b =>
                {
                    b.HasOne("JChat.Domain.Entities.Message.MessageRecipient", "ForwardedBy")
                        .WithMany()
                        .HasForeignKey("ForwardedById")
                        .HasConstraintName("fk_message_recipients_message_recipients_forwarded_by_id");

                    b.HasOne("JChat.Domain.Entities.User.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_message_recipients_users_user_id");

                    b.Navigation("ForwardedBy");

                    b.Navigation("User");
                });

            modelBuilder.Entity("JChat.Domain.Entities.User.UserWorkspace", b =>
                {
                    b.HasOne("JChat.Domain.Entities.User.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .HasConstraintName("fk_user_workspaces_users_created_by_id");

                    b.HasOne("JChat.Domain.Entities.User.User", "DeletedBy")
                        .WithMany()
                        .HasForeignKey("DeletedById")
                        .HasConstraintName("fk_user_workspaces_users_deleted_by_id");

                    b.HasOne("JChat.Domain.Entities.User.User", "LastModifiedBy")
                        .WithMany()
                        .HasForeignKey("LastModifiedById")
                        .HasConstraintName("fk_user_workspaces_users_last_modified_by_id");

                    b.HasOne("JChat.Domain.Entities.User.User", "User")
                        .WithMany("UserWorkspaces")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_workspaces_users_user_id");

                    b.HasOne("JChat.Domain.Entities.Workspace.Workspace", "Workspace")
                        .WithMany("UserWorkspaces")
                        .HasForeignKey("WorkspaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("fk_user_workspaces_workspaces_workspace_id");

                    b.Navigation("CreatedBy");

                    b.Navigation("DeletedBy");

                    b.Navigation("LastModifiedBy");

                    b.Navigation("User");

                    b.Navigation("Workspace");
                });

            modelBuilder.Entity("JChat.Domain.Entities.Workspace.Workspace", b =>
                {
                    b.HasOne("JChat.Domain.Entities.User.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .HasConstraintName("fk_workspaces_users_created_by_id");

                    b.HasOne("JChat.Domain.Entities.User.User", "DeletedBy")
                        .WithMany()
                        .HasForeignKey("DeletedById")
                        .HasConstraintName("fk_workspaces_users_deleted_by_id");

                    b.HasOne("JChat.Domain.Entities.User.User", "LastModifiedBy")
                        .WithMany()
                        .HasForeignKey("LastModifiedById")
                        .HasConstraintName("fk_workspaces_users_last_modified_by_id");

                    b.Navigation("CreatedBy");

                    b.Navigation("DeletedBy");

                    b.Navigation("LastModifiedBy");
                });

            modelBuilder.Entity("JChat.Domain.Entities.Channel.Channel", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("JChat.Domain.Entities.Message.Message", b =>
                {
                    b.Navigation("Reactions");
                });

            modelBuilder.Entity("JChat.Domain.Entities.User.User", b =>
                {
                    b.Navigation("Channels");

                    b.Navigation("UserWorkspaces");
                });

            modelBuilder.Entity("JChat.Domain.Entities.Workspace.Workspace", b =>
                {
                    b.Navigation("UserWorkspaces");
                });
#pragma warning restore 612, 618
        }
    }
}
