using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Partidas",
                columns: table => new
                {
                    IdPartida = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidas", x => x.IdPartida);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tiers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "longtext", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tiers", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tropas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    Vida = table.Column<float>(type: "float", nullable: false),
                    Damage = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tropas", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    Correo = table.Column<string>(type: "longtext", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false),
                    imageUrl = table.Column<string>(type: "longtext", nullable: true),
                    Visible = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Rol = table.Column<string>(type: "longtext", nullable: false),
                    TierId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                    table.ForeignKey(
                        name: "FK_Usuarios_Tiers_TierId",
                        column: x => x.TierId,
                        principalTable: "Tiers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Nodos",
                columns: table => new
                {
                    IdNodo = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    DuenoNodoUsuarioId = table.Column<int>(type: "int", nullable: true),
                    PartidaIdPartida = table.Column<string>(type: "varchar(255)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodos", x => x.IdNodo);
                    table.ForeignKey(
                        name: "FK_Nodos_Partidas_PartidaIdPartida",
                        column: x => x.PartidaIdPartida,
                        principalTable: "Partidas",
                        principalColumn: "IdPartida",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nodos_Usuarios_DuenoNodoUsuarioId",
                        column: x => x.DuenoNodoUsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Movimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    TropaId = table.Column<int>(type: "int", nullable: false),
                    NodoDestinoIdNodo = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    PartidaIdPartida = table.Column<string>(type: "varchar(255)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimientos_Nodos_NodoDestinoIdNodo",
                        column: x => x.NodoDestinoIdNodo,
                        principalTable: "Nodos",
                        principalColumn: "IdNodo",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Movimientos_Partidas_PartidaIdPartida",
                        column: x => x.PartidaIdPartida,
                        principalTable: "Partidas",
                        principalColumn: "IdPartida",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimientos_Tropas_TropaId",
                        column: x => x.TropaId,
                        principalTable: "Tropas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Nodos",
                columns: new[] { "IdNodo", "DuenoNodoUsuarioId", "PartidaIdPartida" },
                values: new object[,]
                {
                    { (byte)1, null, null },
                    { (byte)2, null, null },
                    { (byte)3, null, null }
                });

            migrationBuilder.InsertData(
                table: "Partidas",
                column: "IdPartida",
                values: new object[]
                {
                    "partida-001",
                    "partida-002"
                });

            migrationBuilder.InsertData(
                table: "Tiers",
                columns: new[] { "Id", "FechaCreacion", "Titulo" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 8, 22, 2, 0, 561, DateTimeKind.Local).AddTicks(5859), "Bronce" },
                    { 2, new DateTime(2026, 3, 8, 22, 2, 0, 561, DateTimeKind.Local).AddTicks(5901), "Plata" },
                    { 3, new DateTime(2026, 3, 8, 22, 2, 0, 561, DateTimeKind.Local).AddTicks(5903), "Oro" }
                });

            migrationBuilder.InsertData(
                table: "Tropas",
                columns: new[] { "Id", "Damage", "Nombre", "Vida" },
                values: new object[,]
                {
                    { 1, 50f, "Triangulo", 100f },
                    { 2, 50f, "Cuadrado", 100f },
                    { 3, 50f, "Circulo", 100f }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "Correo", "FechaCreacion", "Nombre", "Password", "Rol", "TierId", "Visible", "imageUrl" },
                values: new object[,]
                {
                    { 1, "Jhon@gmail.com", new DateTime(2026, 3, 8, 22, 2, 0, 561, DateTimeKind.Local).AddTicks(6741), "Ejemplo", "asd", "Admin", 1, true, null },
                    { 2, "Mary@gmail.com", new DateTime(2026, 3, 8, 22, 2, 0, 561, DateTimeKind.Local).AddTicks(6745), "Ejemplo2", "asdasd", "Admin", 1, true, null },
                    { 3, "player1@gmail.com", new DateTime(2026, 3, 8, 22, 2, 0, 561, DateTimeKind.Local).AddTicks(6746), "Player1", "pass123", "Usuario", 2, true, null },
                    { 4, "player2@gmail.com", new DateTime(2026, 3, 8, 22, 2, 0, 561, DateTimeKind.Local).AddTicks(6747), "Player2", "pass456", "Usuario", 3, true, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_NodoDestinoIdNodo",
                table: "Movimientos",
                column: "NodoDestinoIdNodo");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_PartidaIdPartida",
                table: "Movimientos",
                column: "PartidaIdPartida");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_TropaId",
                table: "Movimientos",
                column: "TropaId");

            migrationBuilder.CreateIndex(
                name: "IX_Nodos_DuenoNodoUsuarioId",
                table: "Nodos",
                column: "DuenoNodoUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Nodos_PartidaIdPartida",
                table: "Nodos",
                column: "PartidaIdPartida");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_TierId",
                table: "Usuarios",
                column: "TierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimientos");

            migrationBuilder.DropTable(
                name: "Nodos");

            migrationBuilder.DropTable(
                name: "Tropas");

            migrationBuilder.DropTable(
                name: "Partidas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Tiers");
        }
    }
}
