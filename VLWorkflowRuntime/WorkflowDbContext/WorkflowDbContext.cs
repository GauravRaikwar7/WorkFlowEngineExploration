using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VLWorkflowRuntime.WorkflowEntities.SQL;

namespace VLWorkflowRuntime.Workflow
{
    public partial class WorkflowDbContext : DbContext
    {
        private readonly IConfiguration _config;

        public WorkflowDbContext(IConfiguration config)
        {
            _config = config;
        }

        public virtual DbSet<WorkflowScheme> WorkflowSchemes { get; set; }
        public virtual DbSet<WorkflowProcessScheme> WorkflowProcessSchemes { get; set; }
        public virtual DbSet<WorkflowProcessInstance> WorkflowProcessInstance { get; set; }
        public virtual DbSet<WorkflowProcessTransitionHistory> WorkflowProcessInstanceHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkflowProcessInstance>();
        }
    }
}
