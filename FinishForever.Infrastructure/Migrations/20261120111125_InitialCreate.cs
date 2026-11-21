using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinishForever.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastLoginAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastActivitySyncAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StravaAthletes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StravaId = table.Column<long>(type: "bigint", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePictureUrlMedium = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Premium = table.Column<bool>(type: "bit", nullable: false),
                    Summit = table.Column<bool>(type: "bit", nullable: false),
                    ProfileMedium = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfileLarge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TokenExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ResourceState = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    FollowerCount = table.Column<int>(type: "int", nullable: false),
                    FriendCount = table.Column<int>(type: "int", nullable: false),
                    MutualFriendCount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstSyncAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastSyncAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StravaAthletes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StravaAthletes_ApplicationUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StravaActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AthleteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StravaActivityId = table.Column<long>(type: "bigint", nullable: false),
                    ExternalId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SportType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRace = table.Column<bool>(type: "bit", nullable: false),
                    IsTrainer = table.Column<bool>(type: "bit", nullable: false),
                    IsCommute = table.Column<bool>(type: "bit", nullable: false),
                    IsManual = table.Column<bool>(type: "bit", nullable: false),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false),
                    DistanceMeters = table.Column<double>(type: "float", nullable: false),
                    DistanceKm = table.Column<double>(type: "float", nullable: false),
                    TotalElevationGain = table.Column<double>(type: "float", nullable: false),
                    ElevationHigh = table.Column<double>(type: "float", nullable: false),
                    ElevationLow = table.Column<double>(type: "float", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartDateLocal = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MovingTimeSeconds = table.Column<int>(type: "int", nullable: false),
                    ElapsedTimeSeconds = table.Column<int>(type: "int", nullable: false),
                    AverageSpeed = table.Column<double>(type: "float", nullable: false),
                    MaxSpeed = table.Column<double>(type: "float", nullable: false),
                    AverageCadence = table.Column<double>(type: "float", nullable: false),
                    HasHeartRate = table.Column<bool>(type: "bit", nullable: false),
                    AverageHeartRate = table.Column<double>(type: "float", nullable: false),
                    MaxHeartRate = table.Column<double>(type: "float", nullable: false),
                    HasPower = table.Column<bool>(type: "bit", nullable: false),
                    AverageWatts = table.Column<double>(type: "float", nullable: false),
                    MaxWatts = table.Column<double>(type: "float", nullable: false),
                    WeightedAverageWatts = table.Column<double>(type: "float", nullable: false),
                    Calories = table.Column<double>(type: "float", nullable: false),
                    AchievementCount = table.Column<int>(type: "int", nullable: false),
                    KudosCount = table.Column<int>(type: "int", nullable: false),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    PhotoCount = table.Column<int>(type: "int", nullable: false),
                    TotalPhotoCount = table.Column<int>(type: "int", nullable: false),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GearId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationCountry = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SufferScore = table.Column<double>(type: "float", nullable: false),
                    RelativeEffort = table.Column<double>(type: "float", nullable: false),
                    StartLatitude = table.Column<double>(type: "float", nullable: true),
                    StartLongitude = table.Column<double>(type: "float", nullable: true),
                    EndLatitude = table.Column<double>(type: "float", nullable: true),
                    EndLongitude = table.Column<double>(type: "float", nullable: true),
                    PaceSecondsPerKm = table.Column<double>(type: "float", nullable: false),
                    AverageSpeedKmH = table.Column<double>(type: "float", nullable: false),
                    MaxSpeedKmH = table.Column<double>(type: "float", nullable: false),
                    IsPersonalBest = table.Column<bool>(type: "bit", nullable: false),
                    IsLongestRun = table.Column<bool>(type: "bit", nullable: false),
                    IsFastestRun = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Polyline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImportedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StravaActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StravaActivities_StravaAthletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "StravaAthletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StravaActivities_AthleteId",
                table: "StravaActivities",
                column: "AthleteId");

            migrationBuilder.CreateIndex(
                name: "IX_StravaActivities_StartDate",
                table: "StravaActivities",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_StravaActivities_StravaActivityId",
                table: "StravaActivities",
                column: "StravaActivityId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StravaAthletes_StravaId",
                table: "StravaAthletes",
                column: "StravaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StravaAthletes_UserId",
                table: "StravaAthletes",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StravaActivities");

            migrationBuilder.DropTable(
                name: "StravaAthletes");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");
        }
    }
}
