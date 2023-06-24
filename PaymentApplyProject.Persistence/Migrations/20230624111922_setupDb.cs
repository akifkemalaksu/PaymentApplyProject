using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PaymentApplyProject.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class setupDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bankalar",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: false),
                    EkleyenKullaniciId = table.Column<int>(type: "integer", nullable: false),
                    DuzenleyenKullaniciId = table.Column<int>(type: "integer", nullable: false),
                    EklemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bankalar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Durumlar",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Aciklama = table.Column<string>(type: "text", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: false),
                    EkleyenKullaniciId = table.Column<int>(type: "integer", nullable: false),
                    DuzenleyenKullaniciId = table.Column<int>(type: "integer", nullable: false),
                    EklemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Durumlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Firmalar",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ResponseCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    RequestCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Url = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Ad = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: false),
                    EkleyenKullaniciId = table.Column<int>(type: "integer", nullable: false),
                    DuzenleyenKullaniciId = table.Column<int>(type: "integer", nullable: false),
                    EklemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firmalar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kullanicilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KullaniciAdi = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Sifre = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Ad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Soyad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: false),
                    EkleyenKullaniciId = table.Column<int>(type: "integer", nullable: false),
                    DuzenleyenKullaniciId = table.Column<int>(type: "integer", nullable: false),
                    EklemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicilar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Yetkiler",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: false),
                    EkleyenKullaniciId = table.Column<int>(type: "integer", nullable: false),
                    DuzenleyenKullaniciId = table.Column<int>(type: "integer", nullable: false),
                    EklemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yetkiler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankaHesaplari",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BankaId = table.Column<short>(type: "smallint", nullable: false),
                    HesapNumarasi = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    OdemeHesabiMi = table.Column<bool>(type: "boolean", nullable: false),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: false),
                    EkleyenKullaniciId = table.Column<int>(type: "integer", nullable: false),
                    DuzenleyenKullaniciId = table.Column<int>(type: "integer", nullable: false),
                    EklemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankaHesaplari", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankaHesaplari_Bankalar_BankaId",
                        column: x => x.BankaId,
                        principalTable: "Bankalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Musteriler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirmaId = table.Column<short>(type: "smallint", nullable: false),
                    KullaniciAdi = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Ad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Soyad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    AktifMi = table.Column<bool>(type: "boolean", nullable: false),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: false),
                    EkleyenKullaniciId = table.Column<int>(type: "integer", nullable: false),
                    DuzenleyenKullaniciId = table.Column<int>(type: "integer", nullable: false),
                    EklemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musteriler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Musteriler_Firmalar_FirmaId",
                        column: x => x.FirmaId,
                        principalTable: "Firmalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KullaniciYetkiler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KullaniciId = table.Column<int>(type: "integer", nullable: false),
                    YetkiId = table.Column<short>(type: "smallint", nullable: false),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: false),
                    EkleyenKullaniciId = table.Column<int>(type: "integer", nullable: false),
                    DuzenleyenKullaniciId = table.Column<int>(type: "integer", nullable: false),
                    EklemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciYetkiler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KullaniciYetkiler_Kullanicilar_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KullaniciYetkiler_Yetkiler_YetkiId",
                        column: x => x.YetkiId,
                        principalTable: "Yetkiler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParaCekmeler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MusteriId = table.Column<int>(type: "integer", nullable: false),
                    ParaCekmeDurumId = table.Column<short>(type: "smallint", nullable: false),
                    HesapNumarasi = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Tutar = table.Column<decimal>(type: "numeric", nullable: false),
                    OnaylananTutar = table.Column<decimal>(type: "numeric", nullable: false),
                    EntegrasyonId = table.Column<int>(type: "integer", nullable: false),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: false),
                    EkleyenKullaniciId = table.Column<int>(type: "integer", nullable: false),
                    DuzenleyenKullaniciId = table.Column<int>(type: "integer", nullable: false),
                    EklemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParaCekmeler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParaCekmeler_Durumlar_ParaCekmeDurumId",
                        column: x => x.ParaCekmeDurumId,
                        principalTable: "Durumlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParaCekmeler_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParaYatirmalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MusteriId = table.Column<int>(type: "integer", nullable: false),
                    ParaYatirmaDurumId = table.Column<short>(type: "smallint", nullable: false),
                    BankaHesapId = table.Column<int>(type: "integer", nullable: false),
                    Tutar = table.Column<decimal>(type: "numeric", nullable: false),
                    OnaylananTutar = table.Column<decimal>(type: "numeric", nullable: false),
                    EntegrasyonId = table.Column<int>(type: "integer", nullable: false),
                    BankaHesabiId = table.Column<int>(type: "integer", nullable: false),
                    SilindiMi = table.Column<bool>(type: "boolean", nullable: false),
                    EkleyenKullaniciId = table.Column<int>(type: "integer", nullable: false),
                    DuzenleyenKullaniciId = table.Column<int>(type: "integer", nullable: false),
                    EklemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    GuncellemeTarihi = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParaYatirmalar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParaYatirmalar_BankaHesaplari_BankaHesabiId",
                        column: x => x.BankaHesabiId,
                        principalTable: "BankaHesaplari",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParaYatirmalar_Durumlar_ParaYatirmaDurumId",
                        column: x => x.ParaYatirmaDurumId,
                        principalTable: "Durumlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ParaYatirmalar_Musteriler_MusteriId",
                        column: x => x.MusteriId,
                        principalTable: "Musteriler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankaHesaplari_BankaId",
                table: "BankaHesaplari",
                column: "BankaId");

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciYetkiler_KullaniciId",
                table: "KullaniciYetkiler",
                column: "KullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciYetkiler_YetkiId",
                table: "KullaniciYetkiler",
                column: "YetkiId");

            migrationBuilder.CreateIndex(
                name: "IX_Musteriler_FirmaId",
                table: "Musteriler",
                column: "FirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_ParaCekmeler_MusteriId",
                table: "ParaCekmeler",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_ParaCekmeler_ParaCekmeDurumId",
                table: "ParaCekmeler",
                column: "ParaCekmeDurumId");

            migrationBuilder.CreateIndex(
                name: "IX_ParaYatirmalar_BankaHesabiId",
                table: "ParaYatirmalar",
                column: "BankaHesabiId");

            migrationBuilder.CreateIndex(
                name: "IX_ParaYatirmalar_MusteriId",
                table: "ParaYatirmalar",
                column: "MusteriId");

            migrationBuilder.CreateIndex(
                name: "IX_ParaYatirmalar_ParaYatirmaDurumId",
                table: "ParaYatirmalar",
                column: "ParaYatirmaDurumId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KullaniciYetkiler");

            migrationBuilder.DropTable(
                name: "ParaCekmeler");

            migrationBuilder.DropTable(
                name: "ParaYatirmalar");

            migrationBuilder.DropTable(
                name: "Kullanicilar");

            migrationBuilder.DropTable(
                name: "Yetkiler");

            migrationBuilder.DropTable(
                name: "BankaHesaplari");

            migrationBuilder.DropTable(
                name: "Durumlar");

            migrationBuilder.DropTable(
                name: "Musteriler");

            migrationBuilder.DropTable(
                name: "Bankalar");

            migrationBuilder.DropTable(
                name: "Firmalar");
        }
    }
}
