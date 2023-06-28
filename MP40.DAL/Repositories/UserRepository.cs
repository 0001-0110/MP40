using MP40.DAL.DataBaseContext;
using MP40.DAL.Models;

namespace MP40.DAL.Repositories
{
	public class UserRepository : Repository<User>
    {
        public UserRepository(RwaMoviesContext rwaMoviesContext) : base(rwaMoviesContext) { }

        public override IEnumerable<User> GetAll()
        {
            // The second condition shouldn't ever happen, but you never know
            return base.GetAll().Where(user => user.DeletedAt == null || user.DeletedAt < DateTime.Now);
        }
    }
}
