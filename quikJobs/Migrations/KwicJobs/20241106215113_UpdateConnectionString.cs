using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace quikJobs.Migrations.KwicJobs
{
    /// <inheritdoc />
    public partial class UpdateConnectionString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "jobs",
                columns: table => new
                {
                    job_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    created_on = table.Column<DateOnly>(type: "date", nullable: true),
                    views = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    pay = table.Column<decimal>(type: "money", nullable: true),
                    type = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobs", x => x.job_id);
                });

            migrationBuilder.CreateTable(
                name: "messages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sender_id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    receiver_id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    message = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_messages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "saved_jobs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    job_id = table.Column<int>(type: "int", nullable: false),
                    save_date = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_saved_jobs", x => x.id);
                    table.ForeignKey(
                        name: "FK_saved_jobs_jobs",
                        column: x => x.job_id,
                        principalTable: "jobs",
                        principalColumn: "job_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_saved_jobs_job_id",
                table: "saved_jobs",
                column: "job_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "messages");

            migrationBuilder.DropTable(
                name: "saved_jobs");

            migrationBuilder.DropTable(
                name: "jobs");
        }
    }
}
