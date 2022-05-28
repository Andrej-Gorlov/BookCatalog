using BookCatalog.Domain.Entity.Dto;
using BookCatalog.Domain.Paging;
using System.Collections.Generic;
using System.Linq;

namespace BookCatalog.Test.MockData
{
    internal class BookMockData
    {
        public static int pageNumber = 1;
        public static int pageSize = Get().Count();

        public static  BookDto Model() =>
             new BookDto()
             {
                 BookId = 4,
                 BookName = "ASP.NET Core в действии",
                 Author = "Эндрю Лок",
                 Genres = new (){
                     new GenreDto { BookId = 4, GenreName = "Образование" },
                     new GenreDto { BookId = 4, GenreName = "Программирование" }
                 },
                 YearOfIssue =2018,
                 ISBN = "9785970605509",
                 ImageUrl= "https://avatars.mds.yandex.net/i?id=5244c21d33ac7388d86f1d1ce832c051-5885436-images-thumbs&n=13",
                 ShortDescription= "Перед вами исчерпывающее руководство по созданию веб-приложений с помощью ASP.NET Core 5.0. Переходите от базовых концепций HTTP к расширенной настройке фреймворка. Иллюстрации и код с аннотациями делают процесс обучения наглядным и легким. Освойте вопросы, касающиеся входа в приложение, внедрения зависимостей, безопасности и узнайте о новейших функциях ASP.NET Core, включая Razor Pages и новую парадигму хостинга."
             };
        public static List<BookDto> Get() =>
            new List<BookDto>
            {
                new BookDto
                {
                    BookId = 1,
                    BookName = "Туманность андромеды",
                    Author= "Иван Ефремов",
                    YearOfIssue = 1957,
                    ISBN = "978-0-82851856-7",
                    Genres= new (){
                        new GenreDto { GenreId = 1, BookId = 1, GenreName = "Научная фантастика" },
                        new GenreDto { GenreId = 2, BookId = 1, GenreName = "Утопия" }},
                    ImageUrl = "https://azbukivedia.ru/wa-data/public/shop/products/26/52/25226/images/63362/63362.750x0.jpg",
                    ShortDescription = "Первая линия сюжета: рассказывает о 37-й звёздной экспедиции звездолёта «Тантра», несколько лет находившегося в межзвёздном пространстве, под началом командира корабля Эрга Ноора. Выполнив все задачи своей экспедиции к планете Зирда в созвездии Змееносца, которая погибла от радиационного излучения в результате «неосторожных опытов» местной цивилизации с ядерной энергией, звездолёт возвращается к Земле.\n Вторая линия сюжета: примерно, в это же время, на планете Земля, у Дара Ветра, заведующего Внешними Станциями, обнаруживается серьёзная психологическая болезнь — «приступы равнодушия к работе и жизни» (эмоциональное выгорание). Будучи не в состоянии справляться со своими обязанностями, он принимает приглашение своей подруги историка Веды Конг (возлюбленной Эрга Ноора), поучаствовать в археологических раскопках кургана скифов в приалтайских степях."
                }, new BookDto
                {
                    BookId = 2,
                    BookName = "Час Быка",
                    Author = "Иван Ефремов",
                    YearOfIssue = 1970,
                    ISBN = "978-5-04160931-3",
                    ImageUrl = "https://productforhomeandgarden.ru/img/1014002896.jpg",
                    Genres= new (){
                        new GenreDto { GenreId = 3, BookId = 2, GenreName = "Научная фантастика" },
                        new GenreDto { GenreId = 4, BookId = 2, GenreName = "Утопия" }},
                    ShortDescription = "Произведение построено по схеме «рассказ в рассказе». Действие начинается на планете Земля, в далёком коммунистическом будущем (4160 г.), в Эру Встретившихся Рук (ЭВР) — период, когда появление сверхсветовых звездолётов Прямого Луча (ЗПЛ), перемещавшихся в гиперпространстве, позволило достигать далёких миров в относительно короткие сроки и устанавливать прямой контакт с их разумными обитателями.\n При этом в романе в ходе обучения на планете Земля юному (подрастающему) поколению в школе третьего цикла (где изучаются закономерности развития общества) объясняется, что общество в своём развитии обязательно должно перейти на высшую, коммунистическую фазу, либо погибнуть самоуничтожившись."
                }, new BookDto
                {
                    BookId = 3,
                    BookName = "Последний кольценосец",
                    Author = "Еськов Кирилл Юрьевич",
                    YearOfIssue = 1999,
                    ISBN = "966-03-2724-2",
                    ImageUrl = "https://coollib.net/i/18/338318/cover1.jpg",
                    Genres= new (){
                        new GenreDto { GenreId = 5, BookId = 3, GenreName = "Фэнтези" },
                        new GenreDto { GenreId = 6, BookId = 3, GenreName = "Тёмное фэнтези" },
                        new GenreDto { GenreId = 7, BookId = 3, GenreName = "Высокое фэнтези" }},
                    ShortDescription = "Сюжет книги представляет собой приключенческий шпионский боевик, для которого «альтернативное» Средиземье служит фоном.\n Радикально настроенный маг Гэндальф смещает более прагматичного Сарумана и ставит на повестку дня «окончательное решение мордорского вопроса» — требует воспользоваться неустойчивым положением Мордора и уничтожить его.\n В противном случае, как говорит предсказание находящегося в руках магов волшебного Зеркала, \n через пару веков Мордор подчинит себе такие силы природы,\n что никакая магия не сможет ему противостоять."
                }
            };

        public static BookDto? GetById(int id) =>
            Get().FirstOrDefault(x => x.BookId == id);

        public static bool Delete(int id)
        {
            var user = Get().FirstOrDefault(x => x.BookId == id);
            if (user is null)
                return false;
            return true;
        }

        public static PagedList<string> BookByGenre(string genre) =>

             PagedList<string>.ToPagedList(Get().Where(book => book.Genres
                .Any(g => g.GenreName.ToUpper().Replace(" ", "") == genre.ToUpper().Replace(" ", "")))
                .Select(x => x.BookName).ToList(), pageNumber, pageSize);

        public static PagedList<BookDto> BookByAuthor(string author) =>

            PagedList<BookDto>.ToPagedList(Get().Where(x => x.Author.ToUpper().Replace(" ", "") == author.ToUpper().Replace(" ", "")).ToList(), pageNumber, pageSize);
    }
}
