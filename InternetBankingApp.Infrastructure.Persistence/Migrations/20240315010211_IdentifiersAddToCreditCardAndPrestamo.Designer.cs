﻿// <auto-generated />
using System;
using InternetBankingApp.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InternetBankingApp.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240315010211_IdentifiersAddToCreditCardAndPrestamo")]
    partial class IdentifiersAddToCreditCardAndPrestamo
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("InternetBankingApp.Core.Domain.Entities.Beneficiario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CuentaIdentifier")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedby")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Beneficiarios");
                });

            modelBuilder.Entity("InternetBankingApp.Core.Domain.Entities.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedby")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LatsName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("InternetBankingApp.Core.Domain.Entities.CuentaDeAhorro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Identifier")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedby")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Main")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("CuentasDeAhorro");
                });

            modelBuilder.Entity("InternetBankingApp.Core.Domain.Entities.Prestamo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("CurrentDebt")
                        .HasColumnType("float");

                    b.Property<int>("Identifier")
                        .HasColumnType("int");

                    b.Property<double>("InitialDebt")
                        .HasColumnType("float");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedby")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Prestamos");
                });

            modelBuilder.Entity("InternetBankingApp.Core.Domain.Entities.TarjetaDeCredito", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("Debt")
                        .HasColumnType("float");

                    b.Property<int>("Identifier")
                        .HasColumnType("int");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedby")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Limit")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("TarjetasDeCredito");
                });

            modelBuilder.Entity("InternetBankingApp.Core.Domain.Entities.Transaccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CuentaDestinoId")
                        .HasColumnType("int");

                    b.Property<int>("CuentaOrigenId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedby")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tipe")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.HasIndex("CuentaDestinoId");

                    b.HasIndex("CuentaOrigenId");

                    b.ToTable("Transacciones");
                });

            modelBuilder.Entity("InternetBankingApp.Core.Domain.Entities.Beneficiario", b =>
                {
                    b.HasOne("InternetBankingApp.Core.Domain.Entities.Cliente", "Cliente")
                        .WithMany("Beneficiarios")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("InternetBankingApp.Core.Domain.Entities.CuentaDeAhorro", b =>
                {
                    b.HasOne("InternetBankingApp.Core.Domain.Entities.Cliente", "Cliente")
                        .WithMany("CuentasDeAhorro")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("InternetBankingApp.Core.Domain.Entities.Prestamo", b =>
                {
                    b.HasOne("InternetBankingApp.Core.Domain.Entities.Cliente", "Cliente")
                        .WithMany("Prestamo")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("InternetBankingApp.Core.Domain.Entities.TarjetaDeCredito", b =>
                {
                    b.HasOne("InternetBankingApp.Core.Domain.Entities.Cliente", "Cliente")
                        .WithMany("TarjetasDeCredito")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("InternetBankingApp.Core.Domain.Entities.Transaccion", b =>
                {
                    b.HasOne("InternetBankingApp.Core.Domain.Entities.Cliente", "Cliente")
                        .WithMany("Transacciones")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InternetBankingApp.Core.Domain.Entities.CuentaDeAhorro", "CuentaDestino")
                        .WithMany()
                        .HasForeignKey("CuentaDestinoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("InternetBankingApp.Core.Domain.Entities.CuentaDeAhorro", "CuentaOrigen")
                        .WithMany()
                        .HasForeignKey("CuentaOrigenId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("CuentaDestino");

                    b.Navigation("CuentaOrigen");
                });

            modelBuilder.Entity("InternetBankingApp.Core.Domain.Entities.Cliente", b =>
                {
                    b.Navigation("Beneficiarios");

                    b.Navigation("CuentasDeAhorro");

                    b.Navigation("Prestamo");

                    b.Navigation("TarjetasDeCredito");

                    b.Navigation("Transacciones");
                });
#pragma warning restore 612, 618
        }
    }
}
