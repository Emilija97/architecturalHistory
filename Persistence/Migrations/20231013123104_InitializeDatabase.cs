using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "estates",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    architectural_style_name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    architectural_style_period = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    architectural_style_description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    year_built_year = table.Column<int>(type: "integer", nullable: false),
                    location_latitude = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    location_longitude = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    location_address = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_estates", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "experts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    email = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    biography = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    architectural_style_expertise = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_experts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "participants",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_participants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "historical_events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    estate_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    impact = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_historical_events", x => x.id);
                    table.ForeignKey(
                        name: "fk_historical_events_estates_estate_temp_id",
                        column: x => x.estate_id,
                        principalTable: "estates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "estate_id",
                columns: table => new
                {
                    EstateId = table.Column<Guid>(type: "uuid", nullable: false),
                    expert_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_estate_id", x => x.EstateId);
                    table.ForeignKey(
                        name: "fk_estate_id_experts_expert_temp_id",
                        column: x => x.expert_id,
                        principalTable: "experts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reservations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    participant_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reservations", x => x.id);
                    table.ForeignKey(
                        name: "fk_reservations_participants_participant_id1",
                        column: x => x.participant_id,
                        principalTable: "participants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "multimedia_contents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    url = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    creation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    historical_event_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_multimedia_contents", x => x.id);
                    table.ForeignKey(
                        name: "fk_multimedia_contents_historical_events_historical_event_temp",
                        column: x => x.historical_event_id,
                        principalTable: "historical_events",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "virtual_tours",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    estate_id = table.Column<Guid>(type: "uuid", nullable: false),
                    reservation_id = table.Column<Guid>(type: "uuid", nullable: false),
                    duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    organized_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    narration_language = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    tour_price_amount = table.Column<decimal>(type: "numeric", nullable: false),
                    tour_price_currency = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_virtual_tours", x => x.id);
                    table.ForeignKey(
                        name: "fk_virtual_tours_estates_estate_temp_id1",
                        column: x => x.estate_id,
                        principalTable: "estates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_virtual_tours_reservations_reservation_temp_id",
                        column: x => x.reservation_id,
                        principalTable: "reservations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "highlights",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    virtual_tour_id = table.Column<Guid>(type: "uuid", nullable: false),
                    MultimediaUrlsString = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_highlights", x => x.id);
                    table.ForeignKey(
                        name: "fk_highlights_virtual_tours_virtual_tour_temp_id2",
                        column: x => x.virtual_tour_id,
                        principalTable: "virtual_tours",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "interactive_sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    virtual_tour_id = table.Column<Guid>(type: "uuid", nullable: false),
                    expert_id = table.Column<Guid>(type: "uuid", nullable: false),
                    sheduled_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    duration = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_interactive_sessions", x => x.id);
                    table.ForeignKey(
                        name: "fk_interactive_sessions_experts_expert_id1",
                        column: x => x.expert_id,
                        principalTable: "experts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_interactive_sessions_virtual_tours_virtual_tour_temp_id2",
                        column: x => x.virtual_tour_id,
                        principalTable: "virtual_tours",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_estate_id_expert_id",
                table: "estate_id",
                column: "expert_id");

            migrationBuilder.CreateIndex(
                name: "ix_highlights_virtual_tour_id",
                table: "highlights",
                column: "virtual_tour_id");

            migrationBuilder.CreateIndex(
                name: "ix_historical_events_estate_id",
                table: "historical_events",
                column: "estate_id");

            migrationBuilder.CreateIndex(
                name: "ix_interactive_sessions_expert_id",
                table: "interactive_sessions",
                column: "expert_id");

            migrationBuilder.CreateIndex(
                name: "ix_interactive_sessions_virtual_tour_id",
                table: "interactive_sessions",
                column: "virtual_tour_id");

            migrationBuilder.CreateIndex(
                name: "ix_multimedia_contents_historical_event_id",
                table: "multimedia_contents",
                column: "historical_event_id");

            migrationBuilder.CreateIndex(
                name: "ix_participants_email",
                table: "participants",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_reservations_participant_id",
                table: "reservations",
                column: "participant_id");

            migrationBuilder.CreateIndex(
                name: "ix_virtual_tours_estate_id",
                table: "virtual_tours",
                column: "estate_id");

            migrationBuilder.CreateIndex(
                name: "ix_virtual_tours_reservation_id",
                table: "virtual_tours",
                column: "reservation_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "estate_id");

            migrationBuilder.DropTable(
                name: "highlights");

            migrationBuilder.DropTable(
                name: "interactive_sessions");

            migrationBuilder.DropTable(
                name: "multimedia_contents");

            migrationBuilder.DropTable(
                name: "experts");

            migrationBuilder.DropTable(
                name: "virtual_tours");

            migrationBuilder.DropTable(
                name: "historical_events");

            migrationBuilder.DropTable(
                name: "reservations");

            migrationBuilder.DropTable(
                name: "estates");

            migrationBuilder.DropTable(
                name: "participants");
        }
    }
}
