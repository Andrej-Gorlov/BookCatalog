﻿// <auto-generated />
using BookCatalog.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookCatalog.DAL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220528074442_AddTablesToDB")]
    partial class AddTablesToDB
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BookCatalog.Domain.Entity.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"), 1L, 1);

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ISBN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearOfIssue")
                        .HasColumnType("int");

                    b.HasKey("BookId");

                    b.ToTable("Book");

                    b.HasData(
                        new
                        {
                            BookId = 1,
                            Author = "Иван Ефремов",
                            BookName = "Туманность андромеды",
                            ISBN = "978-0-82851856-7",
                            ImageUrl = "https://azbukivedia.ru/wa-data/public/shop/products/26/52/25226/images/63362/63362.750x0.jpg",
                            ShortDescription = "Первая линия сюжета: рассказывает о 37-й звёздной экспедиции звездолёта «Тантра», несколько лет находившегося в межзвёздном пространстве, под началом командира корабля Эрга Ноора. Выполнив все задачи своей экспедиции к планете Зирда в созвездии Змееносца, которая погибла от радиационного излучения в результате «неосторожных опытов» местной цивилизации с ядерной энергией, звездолёт возвращается к Земле.\n Вторая линия сюжета: примерно, в это же время, на планете Земля, у Дара Ветра, заведующего Внешними Станциями, обнаруживается серьёзная психологическая болезнь — «приступы равнодушия к работе и жизни» (эмоциональное выгорание). Будучи не в состоянии справляться со своими обязанностями, он принимает приглашение своей подруги историка Веды Конг (возлюбленной Эрга Ноора), поучаствовать в археологических раскопках кургана скифов в приалтайских степях.",
                            YearOfIssue = 1957
                        },
                        new
                        {
                            BookId = 2,
                            Author = "Иван Ефремов",
                            BookName = "Час Быка",
                            ISBN = "978-5-04160931-3",
                            ImageUrl = "https://productforhomeandgarden.ru/img/1014002896.jpg",
                            ShortDescription = "Произведение построено по схеме «рассказ в рассказе». Действие начинается на планете Земля, в далёком коммунистическом будущем (4160 г.), в Эру Встретившихся Рук (ЭВР) — период, когда появление сверхсветовых звездолётов Прямого Луча (ЗПЛ), перемещавшихся в гиперпространстве, позволило достигать далёких миров в относительно короткие сроки и устанавливать прямой контакт с их разумными обитателями.\n При этом в романе в ходе обучения на планете Земля юному (подрастающему) поколению в школе третьего цикла (где изучаются закономерности развития общества) объясняется, что общество в своём развитии обязательно должно перейти на высшую, коммунистическую фазу, либо погибнуть самоуничтожившись.",
                            YearOfIssue = 1970
                        },
                        new
                        {
                            BookId = 3,
                            Author = "Еськов Кирилл Юрьевич",
                            BookName = "Последний кольценосец",
                            ISBN = "966-03-2724-2",
                            ImageUrl = "https://coollib.net/i/18/338318/cover1.jpg",
                            ShortDescription = "Сюжет книги представляет собой приключенческий шпионский боевик, для которого «альтернативное» Средиземье служит фоном.\n Радикально настроенный маг Гэндальф смещает более прагматичного Сарумана и ставит на повестку дня «окончательное решение мордорского вопроса» — требует воспользоваться неустойчивым положением Мордора и уничтожить его.\n В противном случае, как говорит предсказание находящегося в руках магов волшебного Зеркала, \n через пару веков Мордор подчинит себе такие силы природы,\n что никакая магия не сможет ему противостоять.",
                            YearOfIssue = 1999
                        });
                });

            modelBuilder.Entity("BookCatalog.Domain.Entity.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenreId"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<string>("GenreName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GenreId");

                    b.HasIndex("BookId");

                    b.ToTable("Genre");

                    b.HasData(
                        new
                        {
                            GenreId = 1,
                            BookId = 1,
                            GenreName = "Научная фантастика"
                        },
                        new
                        {
                            GenreId = 2,
                            BookId = 1,
                            GenreName = "Утопия"
                        },
                        new
                        {
                            GenreId = 3,
                            BookId = 2,
                            GenreName = "Научная фантастика"
                        },
                        new
                        {
                            GenreId = 4,
                            BookId = 2,
                            GenreName = "Утопия"
                        },
                        new
                        {
                            GenreId = 5,
                            BookId = 3,
                            GenreName = "Фэнтези"
                        },
                        new
                        {
                            GenreId = 6,
                            BookId = 3,
                            GenreName = "Тёмное фэнтези"
                        },
                        new
                        {
                            GenreId = 7,
                            BookId = 3,
                            GenreName = "Высокое фэнтези"
                        });
                });

            modelBuilder.Entity("BookCatalog.Domain.Entity.Genre", b =>
                {
                    b.HasOne("BookCatalog.Domain.Entity.Book", null)
                        .WithMany("Genres")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookCatalog.Domain.Entity.Book", b =>
                {
                    b.Navigation("Genres");
                });
#pragma warning restore 612, 618
        }
    }
}
