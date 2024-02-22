
namespace ActorRepositoryLib
{
    public interface IActorsRepository
    {
        Actor? Add(Actor actor);
        Actor? Delete(int id);
        List<Actor> Get();
        IEnumerable<Actor> Get(int birthdayBeforce = 0, int birthdayAfter = 0, string? sortBy = null);
        Actor? GetById(int id);
        string ToString();
        Actor? Update(int id, Actor data);
    }
}