using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProsjektOppgaveWebAPI.EntityFramework.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    HashedPassword = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsOpen = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserFk = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_Users_UserFk",
                        column: x => x.UserFk,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ColorRgb = table.Column<string>(type: "TEXT", nullable: false),
                    UserFk = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Users_UserFk",
                        column: x => x.UserFk,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    BlogFk = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_Blogs_BlogFk",
                        column: x => x.BlogFk,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UserFk = table.Column<long>(type: "INTEGER", nullable: false),
                    PostFk = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Posts_PostFk",
                        column: x => x.PostFk,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserFk",
                        column: x => x.UserFk,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Post Tags",
                columns: table => new
                {
                    PostFk = table.Column<long>(type: "INTEGER", nullable: false),
                    TagFk = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post Tags", x => new { x.PostFk, x.TagFk });
                    table.ForeignKey(
                        name: "FK_Post Tags_Posts_PostFk",
                        column: x => x.PostFk,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Post Tags_Tags_TagFk",
                        column: x => x.TagFk,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Post Users",
                columns: table => new
                {
                    PostFk = table.Column<long>(type: "INTEGER", nullable: false),
                    UserFk = table.Column<long>(type: "INTEGER", nullable: false),
                    IsNotified = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post Users", x => new { x.PostFk, x.UserFk });
                    table.ForeignKey(
                        name: "FK_Post Users_Posts_PostFk",
                        column: x => x.PostFk,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Post Users_Users_UserFk",
                        column: x => x.UserFk,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "HashedPassword", "Username" },
                values: new object[] { 1L, "admin", "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_UserFk",
                table: "Blogs",
                column: "UserFk");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PostFk",
                table: "Comments",
                column: "PostFk");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserFk",
                table: "Comments",
                column: "UserFk");

            migrationBuilder.CreateIndex(
                name: "IX_Post Tags_TagFk",
                table: "Post Tags",
                column: "TagFk");

            migrationBuilder.CreateIndex(
                name: "IX_Post Users_UserFk",
                table: "Post Users",
                column: "UserFk");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_BlogFk",
                table: "Posts",
                column: "BlogFk");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_UserFk",
                table: "Tags",
                column: "UserFk");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Post Tags");

            migrationBuilder.DropTable(
                name: "Post Users");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
