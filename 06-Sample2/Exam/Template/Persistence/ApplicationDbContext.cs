namespace Persistence;

using Core.Entities;

using Microsoft.EntityFrameworkCore;

using Base.Tools;
using System.Diagnostics;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext() : base()
    {
        //We need this constructor for migration
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    //TODO add DBSet for:
    /*
    Examinee 
    ExamineeExamQuestion
    ExamQuestion   
    Exam
    */

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //We need this for migration
            string connectionString = ConfigurationHelper.GetConfiguration().Get("DefaultConnection", "ConnectionStrings");
            optionsBuilder.UseSqlServer(connectionString);
        }
        optionsBuilder.LogTo(message => Debug.WriteLine(message));
}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //because of sql server restriction of cascade delete! Do not remove!
        modelBuilder.Entity<ExamineeExamQuestion>()
            .HasOne(p => p.ExamQuestion).WithMany()
            .HasForeignKey(p => p.ExamQuestionId)
            .OnDelete(DeleteBehavior.ClientNoAction);
    }
}