using Microsoft.EntityFrameworkCore.Migrations;

namespace TwitterServer.Migrations
{
    public partial class Like : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LikeTweetUserRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TweetId = table.Column<int>(type: "int", nullable: false),
                    LikerUserId = table.Column<int>(type: "int", nullable: false),
                    TweetEntityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeTweetUserRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikeTweetUserRelations_Tweets_TweetEntityId",
                        column: x => x.TweetEntityId,
                        principalTable: "Tweets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikeTweetUserRelations_TweetEntityId",
                table: "LikeTweetUserRelations",
                column: "TweetEntityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeTweetUserRelations");
        }
    }
}
