using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitterServer.Migrations
{
    public partial class Tweet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Picture",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Hashtags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hashtags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tweets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    LikeCount = table.Column<int>(type: "int", nullable: false),
                    RetweetCount = table.Column<int>(type: "int", nullable: false),
                    IsRetweet = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tweets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTweetRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    TweetId = table.Column<int>(type: "int", nullable: false),
                    UserEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTweetRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTweetRelations_Users_UserEntityId",
                        column: x => x.UserEntityId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HashtagTweetRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HashtagId = table.Column<int>(type: "int", nullable: false),
                    TweetId = table.Column<int>(type: "int", nullable: false),
                    HashtagEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HashtagTweetRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HashtagTweetRelations_Hashtags_HashtagEntityId",
                        column: x => x.HashtagEntityId,
                        principalTable: "Hashtags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TweetHashtagRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TweetId = table.Column<int>(type: "int", nullable: false),
                    HashtagId = table.Column<int>(type: "int", nullable: false),
                    TweetEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetHashtagRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TweetHashtagRelations_Tweets_TweetEntityId",
                        column: x => x.TweetEntityId,
                        principalTable: "Tweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TweetRetweeterRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TweetId = table.Column<int>(type: "int", nullable: false),
                    RetweeterId = table.Column<int>(type: "int", nullable: false),
                    TweetEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TweetRetweeterRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TweetRetweeterRelations_Tweets_TweetEntityId",
                        column: x => x.TweetEntityId,
                        principalTable: "Tweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HashtagTweetRelations_HashtagEntityId",
                table: "HashtagTweetRelations",
                column: "HashtagEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TweetHashtagRelations_TweetEntityId",
                table: "TweetHashtagRelations",
                column: "TweetEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_TweetRetweeterRelations_TweetEntityId",
                table: "TweetRetweeterRelations",
                column: "TweetEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTweetRelations_UserEntityId",
                table: "UserTweetRelations",
                column: "UserEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HashtagTweetRelations");

            migrationBuilder.DropTable(
                name: "TweetHashtagRelations");

            migrationBuilder.DropTable(
                name: "TweetRetweeterRelations");

            migrationBuilder.DropTable(
                name: "UserTweetRelations");

            migrationBuilder.DropTable(
                name: "Hashtags");

            migrationBuilder.DropTable(
                name: "Tweets");

            migrationBuilder.DropColumn(
                name: "Picture",
                table: "Users");
        }
    }
}
