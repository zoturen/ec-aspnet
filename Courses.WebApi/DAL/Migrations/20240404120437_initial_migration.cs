using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Courses.WebApi.DAL.Migrations
{
    /// <inheritdoc />
    public partial class initial_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    EmailAddress = table.Column<string>(type: "text", nullable: false),
                    Service = table.Column<string>(type: "text", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tags = table.Column<string[]>(type: "text[]", nullable: false),
                    Topics = table.Column<string[]>(type: "text[]", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    SubTitle = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ReviewCount = table.Column<decimal>(type: "numeric", nullable: false),
                    StarsCount = table.Column<int>(type: "integer", nullable: false),
                    LikesCount = table.Column<decimal>(type: "numeric", nullable: false),
                    HoursOfContent = table.Column<decimal>(type: "numeric", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: false),
                    ArticleCount = table.Column<int>(type: "integer", nullable: false),
                    DownloadableResourcesCount = table.Column<int>(type: "integer", nullable: false),
                    LifetimeAccess = table.Column<bool>(type: "boolean", nullable: false),
                    CertificateOfCompletion = table.Column<bool>(type: "boolean", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    DiscountedPrice = table.Column<decimal>(type: "numeric", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subscribers",
                columns: table => new
                {
                    EmailAddress = table.Column<string>(type: "text", nullable: false),
                    DailyNewsletter = table.Column<bool>(type: "boolean", nullable: false),
                    EventUpdates = table.Column<bool>(type: "boolean", nullable: false),
                    AdvertisingUpdates = table.Column<bool>(type: "boolean", nullable: false),
                    StartupsWeekly = table.Column<bool>(type: "boolean", nullable: false),
                    WeekInReview = table.Column<bool>(type: "boolean", nullable: false),
                    Podcasts = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscribers", x => x.EmailAddress);
                });

            migrationBuilder.CreateTable(
                name: "CourseProgramDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Step = table.Column<int>(type: "integer", nullable: false),
                    CourseId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseProgramDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourseProgramDetails_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseProgramDetails_CourseId",
                table: "CourseProgramDetails",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropTable(
                name: "CourseProgramDetails");

            migrationBuilder.DropTable(
                name: "Subscribers");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}
