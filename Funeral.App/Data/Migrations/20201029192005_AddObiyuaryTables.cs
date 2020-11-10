using Microsoft.EntityFrameworkCore.Migrations;

namespace Funeral.Data.Migrations
{
    public partial class AddObiyuaryTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Crosses",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FilePath = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crosses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CustomTexts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomTexts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Frames",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FilePath = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Frames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    FilePath = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TextTemplates",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Text = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TextTemplates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Obituaries",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    FrameId = table.Column<string>(nullable: true),
                    TextTemplateId = table.Column<string>(nullable: true),
                    CustomTextId = table.Column<string>(nullable: true),
                    CrossId = table.Column<string>(nullable: true),
                    PictureId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Obituaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Obituaries_Crosses_CrossId",
                        column: x => x.CrossId,
                        principalTable: "Crosses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Obituaries_CustomTexts_CustomTextId",
                        column: x => x.CustomTextId,
                        principalTable: "CustomTexts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Obituaries_Frames_FrameId",
                        column: x => x.FrameId,
                        principalTable: "Frames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Obituaries_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Obituaries_TextTemplates_TextTemplateId",
                        column: x => x.TextTemplateId,
                        principalTable: "TextTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserObituaries",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    ObituaryId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserObituaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserObituaries_Obituaries_ObituaryId",
                        column: x => x.ObituaryId,
                        principalTable: "Obituaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Obituaries_CrossId",
                table: "Obituaries",
                column: "CrossId");

            migrationBuilder.CreateIndex(
                name: "IX_Obituaries_CustomTextId",
                table: "Obituaries",
                column: "CustomTextId");

            migrationBuilder.CreateIndex(
                name: "IX_Obituaries_FrameId",
                table: "Obituaries",
                column: "FrameId");

            migrationBuilder.CreateIndex(
                name: "IX_Obituaries_PictureId",
                table: "Obituaries",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Obituaries_TextTemplateId",
                table: "Obituaries",
                column: "TextTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserObituaries_ObituaryId",
                table: "UserObituaries",
                column: "ObituaryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserObituaries");

            migrationBuilder.DropTable(
                name: "Obituaries");

            migrationBuilder.DropTable(
                name: "Crosses");

            migrationBuilder.DropTable(
                name: "CustomTexts");

            migrationBuilder.DropTable(
                name: "Frames");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropTable(
                name: "TextTemplates");
        }
    }
}
