using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActorRepositoryLib
{
    public class ActorsRepositoryDb : IActorsRepository
    {
        private readonly ActorDbContext _context;
        public ActorsRepositoryDb(ActorDbContext context)
        {
            _context = context; 
        }
        public Actor Add(Actor actor)
        {
           actor.Id = 0;
            _context.Actors.Add(actor);
            _context.SaveChanges();
            return actor;
          //_context.Actors.Add(actor);
          //  _context.SaveChanges();
          //  return actor;
        }

        public Actor? Delete(int id)
        {
            Actor? deletActors = _context.Actors.FirstOrDefault(a => a.Id == id);

            if (deletActors != null)
            {
                _context.Actors.Remove(deletActors);
                _context.SaveChanges();
            }
            return deletActors;

        }
        public List<Actor> Get()
        {
             return new List<Actor>(_context.Actors);
        }

        public IEnumerable<Actor> Get(int? birthdayBeforce = null, int? birthdayAfter = null, string? sortBy = null)
        {
            IQueryable<Actor> queryable = _context.Actors.AsQueryable();
            if (birthdayBeforce != null)
            {
                queryable=queryable.Where(b => b.BirthYear < birthdayBeforce);
            }
            if (birthdayAfter != null)
            {
                queryable=queryable.Where(a => a.BirthYear > birthdayAfter);
            }
            if( sortBy != null)
                               {
                switch (sortBy)
                {
                    case "Id":
                        queryable =queryable.OrderBy(a => a.Id);
                        break;
                    case "name":
                        queryable =queryable.OrderBy(a => a.Name);
                        break;
                }
            }

            return queryable;
        }

        public Actor? GetById(int id)
        {
          return _context.Actors.FirstOrDefault(a => a.Id == id);
        }

        public Actor? Update(int id, Actor data)
        {
            Actor? updateActor = _context.Actors.FirstOrDefault(a => a.Id == id);
            if (updateActor != null)
            {
                updateActor.BirthYear = data.BirthYear;
                updateActor.Name = data.Name;
                _context.SaveChanges();
            }
            return updateActor;
        }
    }
}
