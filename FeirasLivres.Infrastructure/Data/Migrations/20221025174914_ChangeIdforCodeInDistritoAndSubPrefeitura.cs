using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeirasLivres.Infrastructure.Data.Migrations
{
    public partial class ChangeIdforCodeInDistritoAndSubPrefeitura : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "cd_Distrito",
                table: "TS01_Distrito",
                type: "varchar",
                maxLength: 9,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "char",
                oldFixedLength: true,
                oldMaxLength: 2);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "cd_Distrito",
                table: "TS01_Distrito",
                type: "char",
                fixedLength: true,
                maxLength: 2,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar",
                oldMaxLength: 9);
        }
    }
}
