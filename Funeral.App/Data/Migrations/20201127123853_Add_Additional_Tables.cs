using Microsoft.EntityFrameworkCore.Migrations;

namespace Funeral.Data.Migrations
{
    public partial class Add_Additional_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AfterCrossTextId",
                table: "Obituaries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CrossTextId",
                table: "Obituaries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FromId",
                table: "Obituaries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullNameId",
                table: "Obituaries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PanahidaId",
                table: "Obituaries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "YearId",
                table: "Obituaries",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AfterCrossTexts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AfterCrossTexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CrossTexts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrossTexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Froms",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Froms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FullNames",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullNames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Panahidas",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Panahidas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Years",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Years", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Obituaries_AfterCrossTextId",
                table: "Obituaries",
                column: "AfterCrossTextId");

            migrationBuilder.CreateIndex(
                name: "IX_Obituaries_CrossTextId",
                table: "Obituaries",
                column: "CrossTextId");

            migrationBuilder.CreateIndex(
                name: "IX_Obituaries_FromId",
                table: "Obituaries",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Obituaries_FullNameId",
                table: "Obituaries",
                column: "FullNameId");

            migrationBuilder.CreateIndex(
                name: "IX_Obituaries_PanahidaId",
                table: "Obituaries",
                column: "PanahidaId");

            migrationBuilder.CreateIndex(
                name: "IX_Obituaries_YearId",
                table: "Obituaries",
                column: "YearId");

            migrationBuilder.AddForeignKey(
                name: "FK_Obituaries_AfterCrossTexts_AfterCrossTextId",
                table: "Obituaries",
                column: "AfterCrossTextId",
                principalTable: "AfterCrossTexts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Obituaries_CrossTexts_CrossTextId",
                table: "Obituaries",
                column: "CrossTextId",
                principalTable: "CrossTexts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Obituaries_Froms_FromId",
                table: "Obituaries",
                column: "FromId",
                principalTable: "Froms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Obituaries_FullNames_FullNameId",
                table: "Obituaries",
                column: "FullNameId",
                principalTable: "FullNames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Obituaries_Panahidas_PanahidaId",
                table: "Obituaries",
                column: "PanahidaId",
                principalTable: "Panahidas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Obituaries_Years_YearId",
                table: "Obituaries",
                column: "YearId",
                principalTable: "Years",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Obituaries_AfterCrossTexts_AfterCrossTextId",
                table: "Obituaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Obituaries_CrossTexts_CrossTextId",
                table: "Obituaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Obituaries_Froms_FromId",
                table: "Obituaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Obituaries_FullNames_FullNameId",
                table: "Obituaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Obituaries_Panahidas_PanahidaId",
                table: "Obituaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Obituaries_Years_YearId",
                table: "Obituaries");

            migrationBuilder.DropTable(
                name: "AfterCrossTexts");

            migrationBuilder.DropTable(
                name: "CrossTexts");

            migrationBuilder.DropTable(
                name: "Froms");

            migrationBuilder.DropTable(
                name: "FullNames");

            migrationBuilder.DropTable(
                name: "Panahidas");

            migrationBuilder.DropTable(
                name: "Years");

            migrationBuilder.DropIndex(
                name: "IX_Obituaries_AfterCrossTextId",
                table: "Obituaries");

            migrationBuilder.DropIndex(
                name: "IX_Obituaries_CrossTextId",
                table: "Obituaries");

            migrationBuilder.DropIndex(
                name: "IX_Obituaries_FromId",
                table: "Obituaries");

            migrationBuilder.DropIndex(
                name: "IX_Obituaries_FullNameId",
                table: "Obituaries");

            migrationBuilder.DropIndex(
                name: "IX_Obituaries_PanahidaId",
                table: "Obituaries");

            migrationBuilder.DropIndex(
                name: "IX_Obituaries_YearId",
                table: "Obituaries");

            migrationBuilder.DropColumn(
                name: "AfterCrossTextId",
                table: "Obituaries");

            migrationBuilder.DropColumn(
                name: "CrossTextId",
                table: "Obituaries");

            migrationBuilder.DropColumn(
                name: "FromId",
                table: "Obituaries");

            migrationBuilder.DropColumn(
                name: "FullNameId",
                table: "Obituaries");

            migrationBuilder.DropColumn(
                name: "PanahidaId",
                table: "Obituaries");

            migrationBuilder.DropColumn(
                name: "YearId",
                table: "Obituaries");
        }
    }
}
