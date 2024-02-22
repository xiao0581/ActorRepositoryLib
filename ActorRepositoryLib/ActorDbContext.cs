using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ActorRepositoryLib
{
    public class ActorDbContext:DbContext
    {
        public ActorDbContext(DbContextOptions<ActorDbContext> options) : base(options) { }
        
        public DbSet<Actor> Actors { get; set; }

    }
}
