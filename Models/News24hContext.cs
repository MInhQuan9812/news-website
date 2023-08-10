using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace news24h.Models
{
    public partial class News24hContext : DbContext
    {

        public News24hContext(DbContextOptions<News24hContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<Topic> Topics { get; set; }
        public DbSet<PostTopic> PostTopic { get; set; }
        public DbSet<Comment> Comment { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureUserModel(modelBuilder);
            ConfigurePostModel(modelBuilder);
            ConfigurePostTopicModel(modelBuilder);
            ConfigurePostCommentModel(modelBuilder);
        }

        private void ConfigurePostModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>()
              .HasKey(x => x.Id);

            modelBuilder.Entity<Post>()
                .HasOne(post => post.Creator)
                .WithMany(user => user.Posts)
                .HasForeignKey(post => post.CreatorId);

            //modelBuilder.Entity<Post>()
            //    .HasMany(post => post.PostTopics)
            //    .WithOne(topic => topic.Post);
        }

        private void ConfigureUserModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable(nameof(User))
                .HasKey(x => x.Id);
        }
        
        private void ConfigurePostTopicModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostTopic>()
              .HasKey(x => x.Id);

            modelBuilder.Entity<PostTopic>()
                .HasOne(postTopic => postTopic.Topic)
                .WithMany(topic => topic.PostTopics)
                .HasForeignKey(postTopic => postTopic.TopicId);


            modelBuilder.Entity<PostTopic>()
                .HasOne(postTopic => postTopic.Post)
                .WithMany(post => post.PostTopics)
                .HasForeignKey(postTopic => postTopic.PostId);     
        }

        private void ConfigurePostCommentModel(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>()
                .ToTable(nameof(Comment))
                .HasKey(x => x.Id);

            modelBuilder.Entity<Comment>()
                .HasOne(comment => comment.User)
                .WithMany(user => user.Comments)
                .HasForeignKey(comment => comment.UserId);

            modelBuilder.Entity<Comment>()
               .HasOne(comment => comment.Post)
               .WithMany(post => post.Comments)
               .HasForeignKey(comment => comment.PostId);
        }
    }
}
