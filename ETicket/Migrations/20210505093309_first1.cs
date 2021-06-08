using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ETicket.Migrations
{
    public partial class first1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    PhoneNo = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    JoiningDate = table.Column<DateTime>(nullable: false),
                    UrlImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Agents");
        }
    }
}
