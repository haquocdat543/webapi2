using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace webapi.Migrations
{
  /// <inheritdoc />
  public partial class CreateUserTable : Migration
  {
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
	  migrationBuilder.CreateTable(
		  name: "user",
		  columns: table => new
		  {
			Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
			Name = table.Column<string>(type: "text", nullable: false),
			Email = table.Column<string>(type: "text", nullable: false),
			Password = table.Column<string>(type: "text", nullable: false),
			Dob = table.Column<string>(type: "date", nullable: true),
			Role = table.Column<string>(type: "text", nullable: true),
			Address = table.Column<string>(type: "text", nullable: true),

			// Timestamp columns
			CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW()"),
			UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
			DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)

		  },
		  constraints: table =>
		  {
			table.PrimaryKey("PK_Users", x => x.Id);
		  });
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
	  migrationBuilder.DropTable(
		  name: "user");
	}
  }
}
