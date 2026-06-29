using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Citas.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LimpiezaYAmpliacionSemilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Medicos",
                columns: new[] { "Id", "Apellido", "Especialidad", "Nombre" },
                values: new object[,]
                {
                    { 3, "Guzmán", "Ginecología", "Laura" },
                    { 4, "Villatoro", "Medicina General", "Andrés" },
                    { 5, "Castellanos", "Dermatología", "Sofía" }
                });

            migrationBuilder.InsertData(
                table: "Pacientes",
                columns: new[] { "Id", "Apellido", "DocumentoIdentidad", "Nombre" },
                values: new object[,]
                {
                    { 3, "Torres", "45678912", "Diego" },
                    { 4, "Rivas", "98765432", "Elena" },
                    { 5, "Benítez", "32165498", "Ricardo" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Medicos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Medicos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Medicos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Pacientes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Pacientes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Pacientes",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
