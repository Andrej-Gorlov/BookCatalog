using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookCatalog.DAL.Migrations
{
    public partial class AddTablesToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearOfIssue = table.Column<int>(type: "int", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookId);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    GenreName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.GenreId);
                    table.ForeignKey(
                        name: "FK_Genre_Book_BookId",
                        column: x => x.BookId,
                        principalTable: "Book",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "Author", "BookName", "ISBN", "ImageUrl", "ShortDescription", "YearOfIssue" },
                values: new object[] { 1, "Иван Ефремов", "Туманность андромеды", "978-0-82851856-7", "https://azbukivedia.ru/wa-data/public/shop/products/26/52/25226/images/63362/63362.750x0.jpg", "Первая линия сюжета: рассказывает о 37-й звёздной экспедиции звездолёта «Тантра», несколько лет находившегося в межзвёздном пространстве, под началом командира корабля Эрга Ноора. Выполнив все задачи своей экспедиции к планете Зирда в созвездии Змееносца, которая погибла от радиационного излучения в результате «неосторожных опытов» местной цивилизации с ядерной энергией, звездолёт возвращается к Земле.\n Вторая линия сюжета: примерно, в это же время, на планете Земля, у Дара Ветра, заведующего Внешними Станциями, обнаруживается серьёзная психологическая болезнь — «приступы равнодушия к работе и жизни» (эмоциональное выгорание). Будучи не в состоянии справляться со своими обязанностями, он принимает приглашение своей подруги историка Веды Конг (возлюбленной Эрга Ноора), поучаствовать в археологических раскопках кургана скифов в приалтайских степях.", 1957 });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "Author", "BookName", "ISBN", "ImageUrl", "ShortDescription", "YearOfIssue" },
                values: new object[] { 2, "Иван Ефремов", "Час Быка", "978-5-04160931-3", "https://productforhomeandgarden.ru/img/1014002896.jpg", "Произведение построено по схеме «рассказ в рассказе». Действие начинается на планете Земля, в далёком коммунистическом будущем (4160 г.), в Эру Встретившихся Рук (ЭВР) — период, когда появление сверхсветовых звездолётов Прямого Луча (ЗПЛ), перемещавшихся в гиперпространстве, позволило достигать далёких миров в относительно короткие сроки и устанавливать прямой контакт с их разумными обитателями.\n При этом в романе в ходе обучения на планете Земля юному (подрастающему) поколению в школе третьего цикла (где изучаются закономерности развития общества) объясняется, что общество в своём развитии обязательно должно перейти на высшую, коммунистическую фазу, либо погибнуть самоуничтожившись.", 1970 });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "Author", "BookName", "ISBN", "ImageUrl", "ShortDescription", "YearOfIssue" },
                values: new object[] { 3, "Еськов Кирилл Юрьевич", "Последний кольценосец", "966-03-2724-2", "https://coollib.net/i/18/338318/cover1.jpg", "Сюжет книги представляет собой приключенческий шпионский боевик, для которого «альтернативное» Средиземье служит фоном.\n Радикально настроенный маг Гэндальф смещает более прагматичного Сарумана и ставит на повестку дня «окончательное решение мордорского вопроса» — требует воспользоваться неустойчивым положением Мордора и уничтожить его.\n В противном случае, как говорит предсказание находящегося в руках магов волшебного Зеркала, \n через пару веков Мордор подчинит себе такие силы природы,\n что никакая магия не сможет ему противостоять.", 1999 });

            migrationBuilder.InsertData(
                table: "Genre",
                columns: new[] { "GenreId", "BookId", "GenreName" },
                values: new object[,]
                {
                    { 1, 1, "Научная фантастика" },
                    { 2, 1, "Утопия" },
                    { 3, 2, "Научная фантастика" },
                    { 4, 2, "Утопия" },
                    { 5, 3, "Фэнтези" },
                    { 6, 3, "Тёмное фэнтези" },
                    { 7, 3, "Высокое фэнтези" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genre_BookId",
                table: "Genre",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Genre");

            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
