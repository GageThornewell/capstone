using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace quikJobs.Data;

public partial class KwicJobsContext : DbContext
{
    private readonly IConfiguration _config;
    public KwicJobsContext(IConfiguration config)
    {
        _config = config;
    }

    public KwicJobsContext(DbContextOptions<KwicJobsContext> options, IConfiguration config)
        : base(options)
    {
        _config = config;
    }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<SavedJob> SavedJobs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        => optionsBuilder.UseSqlServer(_config.GetConnectionString("KwicJobsConnectionString"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>(entity =>
        {
            entity.ToTable("jobs");

            entity.Property(e => e.JobId).HasColumnName("job_id");
            entity.Property(e => e.CreatedOn).HasColumnName("created_on");
            entity.Property(e => e.Description)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Pay)
                .HasColumnType("money")
                .HasColumnName("pay");
            entity.Property(e => e.Type)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("type");
            entity.Property(e => e.Url).HasColumnName("url");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("user_id");
            entity.Property(e => e.Views).HasColumnName("views");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.ToTable("messages");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Message1)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("message");
            entity.Property(e => e.ReceiverId)
                .HasMaxLength(450)
                .HasColumnName("receiver_id");
            entity.Property(e => e.SenderId)
                .HasMaxLength(450)
                .HasColumnName("sender_id");
        });

        modelBuilder.Entity<SavedJob>(entity =>
        {
            entity.ToTable("saved_jobs");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.JobId).HasColumnName("job_id");
            entity.Property(e => e.SaveDate).HasColumnName("save_date");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("user_id");

            entity.HasOne(d => d.Job).WithMany(p => p.SavedJobs)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_saved_jobs_jobs");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
