using System;
using System.Collections.Generic;
using JernaClassLib.Data.DatabaseObjects;
using Microsoft.EntityFrameworkCore;

namespace JernaWebApp.Data;

public partial class JernaContext : DbContext
{
    public JernaContext()
    {
    }

    public JernaContext(DbContextOptions<JernaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminHistory> AdminHistories { get; set; }

    public virtual DbSet<AdminHistoryItem> AdminHistoryItems { get; set; }

    public virtual DbSet<AuthCode> AuthCodes { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<CartItem> CartItems { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<PeriodLength> PeriodLengths { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<PurchaseItem> PurchaseItems { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagItem> TagItems { get; set; }

    public virtual DbSet<TempCode> TempCodes { get; set; }

    public virtual DbSet<ToolsForParentsItem> ToolsForParentsItems { get; set; }

    public virtual DbSet<Transaction> Transactions { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAuthCode> UserAuthCodes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
     

        modelBuilder.Entity<AdminHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("admin_history_pkey");

            entity.ToTable("admin_history");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.FulfilledDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fulfilled_date");
            entity.Property(e => e.Notes)
                .HasMaxLength(1000)
                .HasColumnName("notes");
            entity.Property(e => e.PurchaseId).HasColumnName("purchase_id");

            entity.HasOne(d => d.Purchase).WithMany(p => p.AdminHistories)
                .HasForeignKey(d => d.PurchaseId)
                .HasConstraintName("admin_history_purchase_id_fkey");
        });

        modelBuilder.Entity<AdminHistoryItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("admin_history_item_pkey");

            entity.ToTable("admin_history_item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdminHistoryId).HasColumnName("admin_history_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");

            entity.HasOne(d => d.AdminHistory).WithMany(p => p.AdminHistoryItems)
                .HasForeignKey(d => d.AdminHistoryId)
                .HasConstraintName("admin_history_item_admin_history_id_fkey");

            entity.HasOne(d => d.Item).WithMany(p => p.AdminHistoryItems)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("admin_history_item_item_id_fkey");
        });

        modelBuilder.Entity<AuthCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("auth_code_pkey");

            entity.ToTable("auth_code");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(16)
                .HasColumnName("code");
            entity.Property(e => e.IsValidUntil).HasColumnName("is_valid_until");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cart_pkey");

            entity.ToTable("cart");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Carts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("cart_user_id_fkey");
        });

        modelBuilder.Entity<CartItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("cart_item_pkey");

            entity.ToTable("cart_item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CartId).HasColumnName("cart_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.ContactInfo).HasColumnName("contact_info");

            entity.HasOne(d => d.Cart).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.CartId)
                .HasConstraintName("cart_item_cart_id_fkey");

            entity.HasOne(d => d.Item).WithMany(p => p.CartItems)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("cart_item_item_id_fkey");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("item_pkey");

            entity.ToTable("item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.IsDigital).HasColumnName("is_digital");
            entity.Property(e => e.IsPhysical).HasColumnName("is_physical");
            entity.Property(e => e.Isdisplayed).HasColumnName("isdisplayed");
            entity.Property(e => e.ItemName)
                .HasMaxLength(80)
                .HasColumnName("item_name");
            entity.Property(e => e.Mediafile).HasColumnName("mediafile");
            entity.Property(e => e.PeriodLengthId).HasColumnName("period_length_id");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("price");

            entity.HasOne(d => d.PeriodLength).WithMany(p => p.Items)
                .HasForeignKey(d => d.PeriodLengthId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_period_length_id");
        });

        modelBuilder.Entity<PeriodLength>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("period_length_pkey");

            entity.ToTable("period_length");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(60)
                .HasColumnName("display_name");
            entity.Property(e => e.Timespan).HasColumnName("timespan");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("purchase_pkey");

            entity.ToTable("purchase");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PurchaseDate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("purchase_date");
            entity.Property(e => e.Taxpercent).HasColumnName("taxpercent");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("purchase_user_id_fkey");
        });

        modelBuilder.Entity<PurchaseItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("purchase_item_pkey");

            entity.ToTable("purchase_item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.PurchaseId).HasColumnName("purchase_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Item).WithMany(p => p.PurchaseItems)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("purchase_item_item_id_fkey");

            entity.HasOne(d => d.Purchase).WithMany(p => p.PurchaseItems)
                .HasForeignKey(d => d.PurchaseId)
                .HasConstraintName("purchase_item_purchase_id_fkey");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tag_pkey");

            entity.ToTable("tag");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.TagName)
                .HasMaxLength(80)
                .HasColumnName("tag_name");
        });

        modelBuilder.Entity<TagItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tag_item_pkey");

            entity.ToTable("tag_item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.TagId).HasColumnName("tag_id");

            entity.HasOne(d => d.Item).WithMany(p => p.TagItems)
                .HasForeignKey(d => d.ItemId)
                .HasConstraintName("tag_item_item_id_fkey");

            entity.HasOne(d => d.Tag).WithMany(p => p.TagItems)
                .HasForeignKey(d => d.TagId)
                .HasConstraintName("tag_item_tag_id_fkey");
        });

        modelBuilder.Entity<TempCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("authcodes_pkey");

            entity.ToTable("temp_code");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(16)
                .HasColumnName("code");
            entity.Property(e => e.Createdate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdate");
            entity.Property(e => e.Userid).HasColumnName("userid");

            entity.HasOne(d => d.User).WithMany(p => p.TempCodes)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("authcodes_userid_fkey");
        });

        modelBuilder.Entity<ToolsForParentsItem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tools_for_parents_item_pkey");

            entity.ToTable("tools_for_parents_item");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.SubId).HasColumnName("sub_id");

            entity.HasOne(d => d.Item).WithMany(p => p.ToolsForParentsItemItems)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tools_for_parents_item_item_id_fkey");

            entity.HasOne(d => d.Sub).WithMany(p => p.ToolsForParentsItemSubs)
                .HasForeignKey(d => d.SubId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tools_for_parents_item_sub_id_fkey");
        });

        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("transaction_pkey");

            entity.ToTable("transaction");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.PurchaseId).HasColumnName("purchase_id");
            entity.Property(e => e.PurchasePrice)
                .HasColumnType("money")
                .HasColumnName("purchase_price");

            entity.HasOne(d => d.Purchase).WithMany(p => p.Transactions)
                .HasForeignKey(d => d.PurchaseId)
                .HasConstraintName("transaction_purchase_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "user_un").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(80)
                .HasColumnName("email");
            entity.Property(e => e.Isadmin)
                .HasDefaultValue(false)
                .HasColumnName("isadmin");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.Username)
                .HasMaxLength(64)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserAuthCode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_auth_code_pkey");

            entity.ToTable("user_auth_code");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AuthCodeId).HasColumnName("auth_code_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.AuthCode).WithMany(p => p.UserAuthCodes)
                .HasForeignKey(d => d.AuthCodeId)
                .HasConstraintName("user_auth_code_auth_code_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.UserAuthCodes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("user_auth_code_user_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
