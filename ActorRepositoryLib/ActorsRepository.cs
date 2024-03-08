using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Numerics;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace ActorRepositoryLib
{
    public class ActorsRepository : IActorsRepository
    {
        public int _nextId = 5;

        public List<Actor> actors = new(){
        new Actor(){Id=1,Name="michel",BirthYear=1998},
        new Actor(){Id=2,Name="xiao",BirthYear=1988},
        new Actor(){Id=3,Name="alex",BirthYear=2001},
        new Actor(){Id=4,Name="anders",BirthYear=1999}
        };

        public List<Actor> Get()
        {
            return new List<Actor>(actors);
        }
        public Actor? GetById(int id)
        {
            return actors.Find(actor => actor.Id == id);
        }

        public IEnumerable<Actor> Get(int? birthdayBeforce = null, int? birthdayAfter = null, string? sortBy = null)
        {
            IEnumerable<Actor> sortActor = new List<Actor>(actors);
            if (birthdayBeforce != null)
            {
                sortActor= sortActor.Where(b => b.BirthYear < birthdayBeforce);
            }
            if (birthdayAfter != null)
            {
                sortActor= sortActor.Where(b => b.BirthYear > birthdayAfter);
            }
            switch (sortBy)
            {
                case "Id":
                   sortActor= sortActor.OrderBy(m=>m.Id);
                    break;
                case "name":
                    sortActor = sortActor.OrderBy(m =>m.Name);
                    break;


            }
            return sortActor;
        }

        public Actor Add(Actor actor)
        {

            actor.validateName();
            actor.validateBirthYear();
            actor.Id = _nextId++;
            actors.Add(actor);
            return actor;

        }
        public Actor? Delete(int id)
        {

            Actor? actor = actors.Find(actor => actor.Id == id);
            if (actor != null)
            {
                actors.Remove(actor);
            }
            return actor;

        }
        public Actor? Update(int id, Actor data)
        {
            Actor? updateActor = actors.Find(actor => actor.Id == id);
            if (updateActor != null)
            {
                updateActor.Name = data.Name;
                updateActor.BirthYear = data.BirthYear;
            }
            return updateActor;
        }
        public override string ToString()
        {
            return string.Join("\n ", actors);
        }
    }
}
