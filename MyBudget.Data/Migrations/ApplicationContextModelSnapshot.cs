﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyBudget.Data;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MyBudget.Data.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("character varying(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("text");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MyBudget.Domain.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CreatedByID")
                        .HasColumnType("text");

                    b.Property<string>("Icon")
                        .HasColumnType("text");

                    b.Property<bool>("IsSpendingCategory")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSystem")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            IsSpendingCategory = false,
                            IsSystem = true,
                            Name = "Без категории"
                        },
                        new
                        {
                            ID = 2,
                            IsSpendingCategory = true,
                            IsSystem = true,
                            Name = "Без категории"
                        },
                        new
                        {
                            ID = 3,
                            IsSpendingCategory = false,
                            IsSystem = true,
                            Name = "Взять в долг"
                        },
                        new
                        {
                            ID = 4,
                            IsSpendingCategory = true,
                            IsSystem = true,
                            Name = "Дать в долг"
                        },
                        new
                        {
                            ID = 5,
                            IsSpendingCategory = false,
                            IsSystem = true,
                            Name = "Остаток"
                        },
                        new
                        {
                            ID = 6,
                            IsSpendingCategory = true,
                            IsSystem = true,
                            Name = "Отдать долг"
                        },
                        new
                        {
                            ID = 7,
                            IsSpendingCategory = false,
                            IsSystem = true,
                            Name = "Получить долг"
                        },
                        new
                        {
                            ID = 8,
                            IsSpendingCategory = true,
                            IsSystem = true,
                            Name = "Пополнить цель"
                        },
                        new
                        {
                            ID = 20,
                            IsSpendingCategory = false,
                            IsSystem = false,
                            Name = "Зарплата"
                        },
                        new
                        {
                            ID = 21,
                            IsSpendingCategory = false,
                            IsSystem = false,
                            Name = "Бизнес"
                        },
                        new
                        {
                            ID = 22,
                            IsSpendingCategory = false,
                            IsSystem = false,
                            Name = "Премия"
                        },
                        new
                        {
                            ID = 23,
                            IsSpendingCategory = true,
                            IsSystem = false,
                            Name = "Здоровье"
                        },
                        new
                        {
                            ID = 24,
                            IsSpendingCategory = true,
                            IsSystem = false,
                            Name = "Жилье"
                        },
                        new
                        {
                            ID = 25,
                            IsSpendingCategory = true,
                            IsSystem = false,
                            Name = "Авто"
                        },
                        new
                        {
                            ID = 26,
                            IsSpendingCategory = true,
                            IsSystem = false,
                            Name = "Транспорт"
                        },
                        new
                        {
                            ID = 27,
                            IsSpendingCategory = true,
                            IsSystem = false,
                            Name = "Спорт"
                        },
                        new
                        {
                            ID = 28,
                            IsSpendingCategory = true,
                            IsSystem = false,
                            Name = "Еда и продукты"
                        },
                        new
                        {
                            ID = 29,
                            IsSpendingCategory = true,
                            IsSystem = false,
                            Name = "Клубы и бары"
                        },
                        new
                        {
                            ID = 30,
                            IsSpendingCategory = true,
                            IsSystem = false,
                            Name = "Развлечения"
                        },
                        new
                        {
                            ID = 31,
                            IsSpendingCategory = true,
                            IsSystem = false,
                            Name = "Одежда"
                        },
                        new
                        {
                            ID = 32,
                            IsSpendingCategory = true,
                            IsSystem = false,
                            Name = "Связь"
                        },
                        new
                        {
                            ID = 40,
                            IsSpendingCategory = true,
                            IsSystem = false,
                            Name = "Подарки"
                        });
                });

            modelBuilder.Entity("MyBudget.Domain.Goal", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("CompleteDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("CurrentAmount")
                        .HasColumnType("double precision");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("GoalName")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<double>("TotalAmount")
                        .HasColumnType("double precision");

                    b.Property<byte>("Type")
                        .HasColumnType("smallint");

                    b.Property<string>("UserID")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("MyBudget.Domain.Template", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<int>("CategoryID")
                        .HasColumnType("integer");

                    b.Property<int>("Day")
                        .HasColumnType("integer");

                    b.Property<bool>("IsSpending")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("UserID")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("UserID");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("MyBudget.Domain.Transaction", b =>
                {
                    b.Property<Guid>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<double>("Amount")
                        .HasColumnType("double precision");

                    b.Property<int>("CategoryID")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsPlaned")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSpending")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("UserID")
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("UserID");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("MyBudget.Domain.UserCategory", b =>
                {
                    b.Property<Guid>("UserID")
                        .HasColumnType("uuid");

                    b.Property<int>("CategoryID")
                        .HasColumnType("integer");

                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.HasKey("UserID", "CategoryID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("UserId");

                    b.ToTable("UserCategory");
                });

            modelBuilder.Entity("MyBudget.Domain.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<bool>("CarryoverRests")
                        .HasColumnType("boolean");

                    b.Property<string>("DefaultCurrency")
                        .HasColumnType("text");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("UseTemplates")
                        .HasColumnType("boolean");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyBudget.Domain.Goal", b =>
                {
                    b.HasOne("MyBudget.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("MyBudget.Domain.Template", b =>
                {
                    b.HasOne("MyBudget.Domain.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyBudget.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("MyBudget.Domain.Transaction", b =>
                {
                    b.HasOne("MyBudget.Domain.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyBudget.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID");
                });

            modelBuilder.Entity("MyBudget.Domain.UserCategory", b =>
                {
                    b.HasOne("MyBudget.Domain.Category", "Category")
                        .WithMany("UserCategories")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MyBudget.Domain.User", "User")
                        .WithMany("UserCategories")
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
