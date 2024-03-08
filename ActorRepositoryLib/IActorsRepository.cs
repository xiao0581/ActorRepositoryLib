
namespace ActorRepositoryLib
{
    public interface IActorsRepository
    {
        Actor? Add(Actor actor);
        Actor? Delete(int id);
        List<Actor> Get();
        IEnumerable<Actor> Get(int? birthdayBeforce = null, int? birthdayAfter = null, string? sortBy = null);
        Actor? GetById(int id);
        string ToString();
        Actor? Update(int id, Actor data);
    }
}