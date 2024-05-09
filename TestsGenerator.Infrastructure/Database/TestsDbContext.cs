using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using TestsGenerator.Domain.Models.Questions;
using TestsGenerator.Domain.Models.Tests;

namespace TestsGenerator.Infrastructure.Database
{

    public class TestsDbContext : DbContext
    {

        public DbSet<Test> Tests { get; set; }
        public DbSet<TestTemplate> TestTemplates { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public TestsDbContext(DbContextOptions<TestsDbContext> options) : base(options)
        {
            Database.EnsureCreated();

            var category1 = new Category
            {
                Name = "Okoń"
            };

            var category2 = new Category
            {
                Name = "Lubie placki"
            };

            Categories.AddRange(category1, category2);

            Database.EnsureCreated();

            var answer1 = new Answer
            {
                Content = "Odpowiedź1"
            };

            var answer2 = new Answer
            {
                Content = "Odpowiedź2"
            };

            var answer3 = new Answer
            {
                Content = "Odpowiedź3"
            };


            Questions.Add(
                new Question
                {
                    QuestionContent = $"Pytanie {Random.Shared.Next()} ",

                    Category = category1,

                    QuestionAnswers = new List<QuestionAnswer>
                    {
                        new QuestionAnswer
                        {
                            IsCorrect = true,
                            Answer = answer1
                        },
                        new QuestionAnswer
                        {
                            IsCorrect = Random.Shared.Next() % 2 == 0,
                            Answer = answer2
                        },
                        new QuestionAnswer
                        {
                            IsCorrect = Random.Shared.Next() % 2 == 0,
                            Answer = answer3
                        },
                    }
                }
            );

            SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TEST TEMPLATE

            modelBuilder
                .Entity<TestTemplate>()
                .HasMany(x => x.QuestionPool)
                .WithMany();

            modelBuilder
                .Entity<TestTemplate>()
                .HasMany(x => x.Tests)
                .WithOne(x => x.TestTemplate);


            // Questions

            modelBuilder
                .Entity<Question>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Questions);

            modelBuilder
                .Entity<Question>()
                .HasMany(x => x.Answers)
                .WithMany()
                .UsingEntity<QuestionAnswer>();


            // Tests

            modelBuilder
                .Entity<TestQuestionOrdinal>()
                .HasOne(x => x.Test)
                .WithMany(x => x.QuestionsOrdinals)
                .HasForeignKey(x => x.TestsId)
                .HasPrincipalKey(x => x.Id);

            modelBuilder
                .Entity<TestQuestionOrdinal>()
                .HasOne(x => x.Question)
                .WithMany()
                .HasForeignKey(x => x.QuestionsId)
                .HasPrincipalKey(x => x.Id);

            modelBuilder
                .Entity<TestQuestionAnswerOrdinal>()
                .HasOne(x => x.Answer)
                .WithMany()
                .HasForeignKey(x => x.AnswersId)
                .HasPrincipalKey(x => x.Id);

            modelBuilder
                .Entity<TestQuestionOrdinal>()
                .HasKey(x => new { x.TestsId, x.QuestionsId });

            modelBuilder
                .Entity<TestQuestionAnswerOrdinal>()
                .HasOne(x => x.Test)
                .WithMany(x => x.QuestionsAnswersOrdinals)
                .HasForeignKey(x => x.TestsId)
                .HasPrincipalKey(x => x.Id);

            modelBuilder
                .Entity<TestQuestionAnswerOrdinal>()
                .HasKey(x => new { x.TestsId, x.QuestionsId, x.AnswersId });

            base.OnModelCreating(modelBuilder);
        }
    }
}
