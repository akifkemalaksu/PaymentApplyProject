﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PaymentApplyProject.Persistence.Context;

#nullable disable

namespace PaymentApplyProject.Persistence.Migrations
{
    [DbContext(typeof(PaymentContext))]
    partial class PaymentContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Banka", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<short>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("DuzenleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EklemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EkleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("GuncellemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("SilindiMi")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Bankalar");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.BankaHesabi", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("AktifMi")
                        .HasColumnType("boolean");

                    b.Property<decimal>("AltLimit")
                        .HasColumnType("numeric");

                    b.Property<short>("BankaId")
                        .HasColumnType("smallint");

                    b.Property<int>("DuzenleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EklemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EkleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("GuncellemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("HesapNumarasi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("SilindiMi")
                        .HasColumnType("boolean");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<decimal>("UstLimit")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("BankaId");

                    b.ToTable("BankaHesaplari");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Durum", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<short>("Id"));

                    b.Property<string>("Aciklama")
                        .HasColumnType("text");

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("DuzenleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EklemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EkleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("GuncellemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("SilindiMi")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Durumlar");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Durum");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Firma", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<short>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("DuzenleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EklemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EkleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("GuncellemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("RequestCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("ResponseCode")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<bool>("SilindiMi")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Firmalar");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Kullanici", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("DuzenleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EklemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EkleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("GuncellemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("KullaniciAdi")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("Sifre")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<bool>("SilindiMi")
                        .HasColumnType("boolean");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Kullanicilar");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.KullaniciFirma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DuzenleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EklemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EkleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<short>("FirmaId")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("GuncellemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("KullaniciId")
                        .HasColumnType("integer");

                    b.Property<bool>("SilindiMi")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("FirmaId");

                    b.HasIndex("KullaniciId");

                    b.ToTable("KullaniciFirmalar");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.KullaniciYetki", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DuzenleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EklemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EkleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("GuncellemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("KullaniciId")
                        .HasColumnType("integer");

                    b.Property<bool>("SilindiMi")
                        .HasColumnType("boolean");

                    b.Property<short>("YetkiId")
                        .HasColumnType("smallint");

                    b.HasKey("Id");

                    b.HasIndex("KullaniciId");

                    b.HasIndex("YetkiId");

                    b.ToTable("KullaniciYetkiler");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Musteri", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("AktifMi")
                        .HasColumnType("boolean");

                    b.Property<int>("DuzenleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EklemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EkleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<short>("FirmaId")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("GuncellemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("KullaniciAdi")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<bool>("SilindiMi")
                        .HasColumnType("boolean");

                    b.Property<string>("Soyad")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("FirmaId");

                    b.ToTable("Musteriler");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.ParaCekme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DuzenleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EklemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EkleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<int>("EntegrasyonId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("GuncellemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("HesapNumarasi")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<int>("MusteriId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("IslemTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal?>("OnaylananTutar")
                        .HasColumnType("numeric");

                    b.Property<short>("ParaCekmeDurumId")
                        .HasColumnType("smallint");

                    b.Property<bool>("SilindiMi")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Tutar")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("MusteriId");

                    b.HasIndex("ParaCekmeDurumId");

                    b.ToTable("ParaCekmeler");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.ParaYatirma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BankaHesabiId")
                        .HasColumnType("integer");

                    b.Property<int>("DuzenleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EklemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EkleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<int>("EntegrasyonId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("GuncellemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("MusteriId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("IslemTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal?>("OnaylananTutar")
                        .HasColumnType("numeric");

                    b.Property<short>("ParaYatirmaDurumId")
                        .HasColumnType("smallint");

                    b.Property<bool>("SilindiMi")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Tutar")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("BankaHesabiId");

                    b.HasIndex("MusteriId");

                    b.HasIndex("ParaYatirmaDurumId");

                    b.ToTable("ParaYatirmalar");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Yetki", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<short>("Id"));

                    b.Property<string>("Ad")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<int>("DuzenleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EklemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EkleyenKullaniciId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("GuncellemeTarihi")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("SilindiMi")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Yetkiler");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.ParaCekmeDurum", b =>
                {
                    b.HasBaseType("PaymentApplyProject.Domain.Entities.Durum");

                    b.HasDiscriminator().HasValue("ParaCekmeDurum");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.ParaYatirmaDurum", b =>
                {
                    b.HasBaseType("PaymentApplyProject.Domain.Entities.Durum");

                    b.HasDiscriminator().HasValue("ParaYatirmaDurum");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.BankaHesabi", b =>
                {
                    b.HasOne("PaymentApplyProject.Domain.Entities.Banka", "Banka")
                        .WithMany("BankaHesaplar")
                        .HasForeignKey("BankaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Banka");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.KullaniciFirma", b =>
                {
                    b.HasOne("PaymentApplyProject.Domain.Entities.Firma", "Firma")
                        .WithMany()
                        .HasForeignKey("FirmaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaymentApplyProject.Domain.Entities.Kullanici", "Kullanici")
                        .WithMany()
                        .HasForeignKey("KullaniciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Firma");

                    b.Navigation("Kullanici");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.KullaniciYetki", b =>
                {
                    b.HasOne("PaymentApplyProject.Domain.Entities.Kullanici", "Kullanici")
                        .WithMany("KullaniciYetkiler")
                        .HasForeignKey("KullaniciId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaymentApplyProject.Domain.Entities.Yetki", "Yetki")
                        .WithMany("KullaniciYetkiler")
                        .HasForeignKey("YetkiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Kullanici");

                    b.Navigation("Yetki");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Musteri", b =>
                {
                    b.HasOne("PaymentApplyProject.Domain.Entities.Firma", "Firma")
                        .WithMany("Musteriler")
                        .HasForeignKey("FirmaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Firma");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.ParaCekme", b =>
                {
                    b.HasOne("PaymentApplyProject.Domain.Entities.Musteri", "Musteri")
                        .WithMany()
                        .HasForeignKey("MusteriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaymentApplyProject.Domain.Entities.ParaCekmeDurum", "ParaCekmeDurum")
                        .WithMany("ParaCekmeler")
                        .HasForeignKey("ParaCekmeDurumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Musteri");

                    b.Navigation("ParaCekmeDurum");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.ParaYatirma", b =>
                {
                    b.HasOne("PaymentApplyProject.Domain.Entities.BankaHesabi", "BankaHesabi")
                        .WithMany()
                        .HasForeignKey("BankaHesabiId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaymentApplyProject.Domain.Entities.Musteri", "Musteri")
                        .WithMany()
                        .HasForeignKey("MusteriId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PaymentApplyProject.Domain.Entities.ParaYatirmaDurum", "ParaYatirmaDurum")
                        .WithMany("ParaYatirmalar")
                        .HasForeignKey("ParaYatirmaDurumId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankaHesabi");

                    b.Navigation("Musteri");

                    b.Navigation("ParaYatirmaDurum");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Banka", b =>
                {
                    b.Navigation("BankaHesaplar");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Firma", b =>
                {
                    b.Navigation("Musteriler");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Kullanici", b =>
                {
                    b.Navigation("KullaniciYetkiler");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.Yetki", b =>
                {
                    b.Navigation("KullaniciYetkiler");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.ParaCekmeDurum", b =>
                {
                    b.Navigation("ParaCekmeler");
                });

            modelBuilder.Entity("PaymentApplyProject.Domain.Entities.ParaYatirmaDurum", b =>
                {
                    b.Navigation("ParaYatirmalar");
                });
#pragma warning restore 612, 618
        }
    }
}
