﻿// <auto-generated />
using System;
using ColmanAppStore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ColmanAppStore.Migrations
{
    [DbContext(typeof(ColmanAppStoreContext))]
    partial class ColmanAppStoreContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AppUser", b =>
                {
                    b.Property<int>("AppListUserId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("AppListUserId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("AppUser");
                });

            modelBuilder.Entity("ColmanAppStore.Models.App", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("AverageRaiting")
                        .HasColumnType("real");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeveloperName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<float>("Size")
                        .HasColumnType("real");

                    b.Property<int>("countReview")
                        .HasColumnType("int");

                    b.Property<DateTime>("publishDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Apps");
                });

            modelBuilder.Entity("ColmanAppStore.Models.AppImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AppId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.ToTable("AppsImage");
                });

            modelBuilder.Entity("ColmanAppStore.Models.AppVideo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AppId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Video")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.ToTable("AppVideo");
                });

            modelBuilder.Entity("ColmanAppStore.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("ColmanAppStore.Models.Logo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AppsId")
                        .HasColumnType("int");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AppsId")
                        .IsUnique();

                    b.ToTable("Logo");
                });

            modelBuilder.Entity("ColmanAppStore.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AppId")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaymentMethodId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.HasIndex("PaymentMethodId");

                    b.ToTable("Payment");
                });

            modelBuilder.Entity("ColmanAppStore.Models.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CVV")
                        .HasColumnType("int");

                    b.Property<long>("CardNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("ExpiredDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("IdNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("NameOnCard")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PaymentMethod");
                });

            modelBuilder.Entity("ColmanAppStore.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AppId")
                        .HasColumnType("int");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<float>("Raiting")
                        .HasColumnType("real");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserNameId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AppId");

                    b.HasIndex("UserNameId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("ColmanAppStore.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("PaymentMethodUser", b =>
                {
                    b.Property<int>("PaymentMethodsId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("PaymentMethodsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("PaymentMethodUser");
                });

            modelBuilder.Entity("AppUser", b =>
                {
                    b.HasOne("ColmanAppStore.Models.App", null)
                        .WithMany()
                        .HasForeignKey("AppListUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ColmanAppStore.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ColmanAppStore.Models.App", b =>
                {
                    b.HasOne("ColmanAppStore.Models.Category", "Category")
                        .WithMany("Apps")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ColmanAppStore.Models.AppImage", b =>
                {
                    b.HasOne("ColmanAppStore.Models.App", "App")
                        .WithMany("Images")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("App");
                });

            modelBuilder.Entity("ColmanAppStore.Models.AppVideo", b =>
                {
                    b.HasOne("ColmanAppStore.Models.App", "App")
                        .WithMany("Videos")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("App");
                });

            modelBuilder.Entity("ColmanAppStore.Models.Logo", b =>
                {
                    b.HasOne("ColmanAppStore.Models.App", "Apps")
                        .WithOne("Logo")
                        .HasForeignKey("ColmanAppStore.Models.Logo", "AppsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Apps");
                });

            modelBuilder.Entity("ColmanAppStore.Models.Payment", b =>
                {
                    b.HasOne("ColmanAppStore.Models.App", "App")
                        .WithMany()
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ColmanAppStore.Models.PaymentMethod", "PaymentMethod")
                        .WithMany("Payments")
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("App");

                    b.Navigation("PaymentMethod");
                });

            modelBuilder.Entity("ColmanAppStore.Models.Review", b =>
                {
                    b.HasOne("ColmanAppStore.Models.App", "App")
                        .WithMany("Review")
                        .HasForeignKey("AppId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ColmanAppStore.Models.User", "UserName")
                        .WithMany()
                        .HasForeignKey("UserNameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("App");

                    b.Navigation("UserName");
                });

            modelBuilder.Entity("PaymentMethodUser", b =>
                {
                    b.HasOne("ColmanAppStore.Models.PaymentMethod", null)
                        .WithMany()
                        .HasForeignKey("PaymentMethodsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ColmanAppStore.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ColmanAppStore.Models.App", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("Logo")
                        .IsRequired();

                    b.Navigation("Review");

                    b.Navigation("Videos");
                });

            modelBuilder.Entity("ColmanAppStore.Models.Category", b =>
                {
                    b.Navigation("Apps");
                });

            modelBuilder.Entity("ColmanAppStore.Models.PaymentMethod", b =>
                {
                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
