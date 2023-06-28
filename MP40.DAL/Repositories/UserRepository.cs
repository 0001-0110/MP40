using MP40.DAL.DataBaseContext;
using MP40.DAL.Models;

namespace MP40.DAL.Repositories
{
	public class UserRepository : Repository<User>
    {
        public UserRepository(RwaMoviesContext rwaMoviesContext) : base(rwaMoviesContext) { }

        public override IEnumerable<User> GetAll()
        {
            return base.GetAll().Where(user => user.DeletedAt == null);
        }
    }
}
