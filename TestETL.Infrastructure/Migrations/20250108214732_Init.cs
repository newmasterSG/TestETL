using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestETL.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var projectDir = Directory.GetCurrentDirectory();
            var sql = File.ReadAllText(Path.Combine(projectDir, "Scripts", "TestETL.sql"));
            migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TABLE IF EXISTS CSV");
        }
    }
}
