using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Recording.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "AudioRecordings");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "AudioRecordings");

            migrationBuilder.RenameColumn(
                name: "RecordingName",
                table: "AudioRecordings",
                newName: "FileName");

            migrationBuilder.AddColumn<byte[]>(
                name: "AudioData",
                table: "AudioRecordings",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioData",
                table: "AudioRecordings");

            migrationBuilder.RenameColumn(
                name: "FileName",
                table: "AudioRecordings",
                newName: "RecordingName");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "AudioRecordings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "AudioRecordings",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
