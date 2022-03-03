﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TradeConsole.ExchangeData;

#nullable disable

namespace TradeConsole.Migrations
{
    [DbContext(typeof(TradingContext))]
    [Migration("20220217150336_BinanceData")]
    partial class BinanceData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TradeConsole.ExchangeData.BinanceData", b =>
                {
                    b.Property<int>("BinanceDataId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BinanceDataId"), 1L, 1);

                    b.Property<string>("BinanceAPIKey")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BinanceAPISecret")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BinanceDataId");

                    b.ToTable("binanceDatas");
                });

            modelBuilder.Entity("TradeConsole.ExchangeData.BinanceObjects+AccountInfoResponse", b =>
                {
                    b.Property<int>("AccountInfoResponseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountInfoResponseId"), 1L, 1);

                    b.Property<string>("accountType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("buyerCommiss")
                        .HasColumnType("int");

                    b.Property<bool>("canDeposit")
                        .HasColumnType("bit");

                    b.Property<bool>("canTrade")
                        .HasColumnType("bit");

                    b.Property<bool>("canWithdraw")
                        .HasColumnType("bit");

                    b.Property<string>("makerCommission")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("sellerCommiss")
                        .HasColumnType("int");

                    b.Property<int>("takerCommission")
                        .HasColumnType("int");

                    b.Property<long>("updateTime")
                        .HasColumnType("bigint");

                    b.HasKey("AccountInfoResponseId");

                    b.ToTable("BinanceAccountInfo");
                });

            modelBuilder.Entity("TradeConsole.ExchangeData.BinanceObjects+Balances", b =>
                {
                    b.Property<int>("BalancesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BalancesId"), 1L, 1);

                    b.Property<int?>("AccountInfoResponseId")
                        .HasColumnType("int");

                    b.Property<string>("Asset")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Balance")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Locked")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BalancesId");

                    b.HasIndex("AccountInfoResponseId");

                    b.ToTable("BinanceBalances");
                });

            modelBuilder.Entity("TradeConsole.ExchangeData.BinanceObjects+TradeHistoryEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Commission")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CommissionAsset")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<int>("OrderListId")
                        .HasColumnType("int");

                    b.Property<string>("Price")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Quantity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("QuoteQty")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("TimeStamp")
                        .HasColumnType("bigint");

                    b.Property<long>("TradeId")
                        .HasColumnType("bigint");

                    b.Property<bool>("isBestMatch")
                        .HasColumnType("bit");

                    b.Property<bool>("isBuyer")
                        .HasColumnType("bit");

                    b.Property<bool>("isMaker")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("BinanceTradeHistory");
                });

            modelBuilder.Entity("TradeConsole.ExchangeData.BinanceObjects+Balances", b =>
                {
                    b.HasOne("TradeConsole.ExchangeData.BinanceObjects+AccountInfoResponse", null)
                        .WithMany("Balances")
                        .HasForeignKey("AccountInfoResponseId");
                });

            modelBuilder.Entity("TradeConsole.ExchangeData.BinanceObjects+AccountInfoResponse", b =>
                {
                    b.Navigation("Balances");
                });
#pragma warning restore 612, 618
        }
    }
}
