using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class frstMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AuthorName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PrintingHouse = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserOperationClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    OperationClaimId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserOperationClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserOperationClaims_OperationClaims_OperationClaimId",
                        column: x => x.OperationClaimId,
                        principalTable: "OperationClaims",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserOperationClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorName", "BookName", "Image", "Price", "PrintingHouse" },
                values: new object[,]
                {
                    { 1, "Carl Sagan", "Cosmos", "http://localhost:5000/images/b4c2e771-0c0c-4287-9301-5dae7728ebec.jpg", 18m, "Premius Yayın Evi" },
                    { 2, "Stephen W. Hawking", "The Theory Of Everything", "http://localhost:5000/images/516a5d15-6bb5-4428-a574-421a111e5017.jpg", 8m, "Premius Yayın Evi" },
                    { 3, "Victor Hugo", "Notre Dame'in Kamburu", "http://localhost:5000/images/a208ba3f-79d5-49e2-b3e1-7d6b9aafb2db.jpg", 10m, "Kültür Yayın Evi" },
                    { 4, "Wulf Dorn", "Şizofren", "http://localhost:5000/images/c2df2671-66de-44d3-af88-b9b7981b95e8.jpg", 14m, "Pegasus Yayıncılık" },
                    { 5, "Paulo Coelho", "Simyacı", "http://localhost:5000/images/1b52d6bd-0b73-4937-a84a-217d859cad74.jpg", 3m, "Pegasus Yayıncılık" },
                    { 6, "Robin Sharma", "Ferrari'sini Satan Bilge", "http://localhost:5000/images/94fc2ab3-5398-4288-a01a-1847625dd739.jpg", 14m, "Pegasus Yayıncılık" }
                });

            migrationBuilder.InsertData(
                table: "OperationClaims",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "IsActive", "LastName", "PasswordHash", "PasswordSalt" },
                values: new object[,]
                {
                    { 1, "admin@gmail.com", "admin", true, "admin", new byte[] { 233, 161, 183, 192, 221, 240, 153, 35, 64, 47, 17, 92, 127, 204, 17, 145, 187, 63, 137, 126, 47, 33, 136, 115, 62, 90, 82, 247, 4, 59, 180, 206, 188, 248, 2, 254, 123, 99, 95, 153, 86, 30, 8, 186, 118, 148, 185, 238, 125, 22, 196, 180, 158, 3, 146, 231, 240, 95, 151, 54, 36, 228, 39, 206 }, new byte[] { 197, 17, 3, 157, 160, 38, 75, 94, 114, 112, 59, 28, 184, 168, 96, 79, 39, 122, 115, 24, 82, 177, 41, 204, 117, 88, 23, 156, 205, 227, 133, 7, 157, 247, 218, 128, 74, 61, 114, 85, 247, 200, 175, 69, 80, 229, 71, 30, 113, 228, 192, 203, 47, 17, 184, 42, 77, 41, 70, 92, 76, 174, 167, 126, 3, 38, 248, 145, 4, 232, 230, 109, 22, 26, 241, 197, 46, 171, 234, 239, 19, 229, 113, 6, 58, 72, 60, 200, 140, 213, 207, 57, 107, 124, 81, 128, 4, 237, 19, 120, 86, 74, 178, 232, 117, 200, 164, 220, 151, 34, 5, 72, 225, 134, 142, 118, 115, 134, 187, 87, 226, 162, 202, 64, 145, 198, 190, 245 } },
                    { 2, "user@gmail.com", "user", true, "user", new byte[] { 187, 26, 51, 6, 209, 9, 193, 85, 92, 79, 49, 72, 137, 250, 87, 86, 103, 31, 7, 147, 162, 185, 211, 57, 143, 117, 236, 186, 231, 28, 233, 148, 102, 27, 24, 244, 178, 34, 194, 66, 25, 200, 136, 31, 47, 104, 217, 83, 85, 253, 169, 197, 155, 98, 86, 138, 31, 59, 17, 135, 206, 69, 102, 173 }, new byte[] { 2, 15, 137, 75, 2, 234, 79, 147, 181, 126, 204, 64, 24, 120, 150, 175, 168, 156, 171, 1, 207, 250, 172, 198, 71, 81, 58, 39, 249, 213, 212, 205, 25, 48, 215, 211, 195, 25, 162, 151, 229, 14, 116, 93, 70, 157, 212, 239, 242, 105, 15, 84, 0, 241, 140, 205, 61, 214, 177, 123, 88, 96, 233, 12, 178, 103, 30, 152, 72, 15, 230, 243, 5, 176, 95, 1, 229, 224, 113, 180, 131, 112, 30, 123, 59, 246, 3, 163, 254, 153, 31, 181, 69, 81, 95, 197, 240, 41, 127, 248, 86, 250, 103, 20, 155, 211, 162, 119, 6, 239, 2, 161, 238, 106, 117, 134, 213, 0, 61, 118, 148, 188, 43, 167, 87, 183, 81, 102 } }
                });

            migrationBuilder.InsertData(
                table: "UserOperationClaims",
                columns: new[] { "Id", "OperationClaimId", "UserId" },
                values: new object[] { 1, 1, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_OperationClaimId",
                table: "UserOperationClaims",
                column: "OperationClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_UserOperationClaims_UserId",
                table: "UserOperationClaims",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "UserOperationClaims");

            migrationBuilder.DropTable(
                name: "OperationClaims");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
