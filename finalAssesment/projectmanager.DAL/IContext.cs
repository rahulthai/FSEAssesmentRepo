using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectmanager.DAL
{
    public interface IContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        DbSet<ParentTask> ParentTask { get; set; }
        DbSet<Projects> Projects { get; set; }
        DbSet<Tasks> Tasks { get; set; }
        DbSet<Users> Users { get; set; }
        Task<int> SaveChangesAsync();
    }
}
