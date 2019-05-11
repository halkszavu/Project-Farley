using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.DAL.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Meetings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Place = table.Column<string>(nullable: true),
                    Notes = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meetings", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Populii",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    MartialState = table.Column<string>(nullable: false),
                    SiblingState = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Populii", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Notes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Notes = table.Column<string>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false, defaultValue: new DateTime(2019, 5, 11, 22, 14, 53, 713, DateTimeKind.Local).AddTicks(8878)),
                    PersonID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Notes_Populii_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Populii",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonMeeting",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PersonID = table.Column<int>(nullable: false),
                    MeetingID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonMeeting", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PersonMeeting_Meetings_MeetingID",
                        column: x => x.MeetingID,
                        principalTable: "Meetings",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonMeeting_Populii_PersonID",
                        column: x => x.PersonID,
                        principalTable: "Populii",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Meetings",
                columns: new[] { "ID", "Date", "Notes", "Place" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Zenekar", null },
                    { 2, new DateTime(2019, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Házi átvétel", "BME L épület" }
                });

            migrationBuilder.InsertData(
                table: "Populii",
                columns: new[] { "ID", "DateOfBirth", "MartialState", "Name", "SiblingState" },
                values: new object[,]
                {
                    { 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Single", "Bálint", "Middle" },
                    { 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Single", "Zsófia", "Eldest" }
                });

            migrationBuilder.InsertData(
                table: "Notes",
                columns: new[] { "ID", "Notes", "PersonID" },
                values: new object[] { 1, "barbarbarbar", 1 });

            migrationBuilder.InsertData(
                table: "PersonMeeting",
                columns: new[] { "ID", "MeetingID", "PersonID" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.InsertData(
                table: "PersonMeeting",
                columns: new[] { "ID", "MeetingID", "PersonID" },
                values: new object[] { 2, 1, 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Notes_PersonID",
                table: "Notes",
                column: "PersonID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonMeeting_MeetingID",
                table: "PersonMeeting",
                column: "MeetingID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonMeeting_PersonID",
                table: "PersonMeeting",
                column: "PersonID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notes");

            migrationBuilder.DropTable(
                name: "PersonMeeting");

            migrationBuilder.DropTable(
                name: "Meetings");

            migrationBuilder.DropTable(
                name: "Populii");
        }
    }
}
