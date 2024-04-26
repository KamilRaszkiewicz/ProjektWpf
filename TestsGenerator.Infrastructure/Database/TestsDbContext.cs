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
        public DbSet<Answer> Answers { get; set; }

        public TestsDbContext(DbContextOptions<TestsDbContext> options) : base(options)
        {
            Database.EnsureCreated();
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
                .HasMany(x => x.Categories)
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
