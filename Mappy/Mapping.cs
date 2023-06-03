using System.Linq.Expressions;
using System.Reflection;

namespace Mappy
{
    public class Mapping
    {
        Dictionary<MemberInfo, MemberInfo> memberMappings;

        public Mapping()
        {
            memberMappings = new Dictionary<MemberInfo, MemberInfo>();
        }

        Mapping ForMember<TSource, TDestination>(Expression<Func<TSource, object>> sourceMember, Expression<Func<TDestination, object>> destinationMember)
        {
            throw new NotImplementedException();
            //memberMappings.Add();
        }

        void ReverseMap()
        {
            throw new NotImplementedException();
        }
    }
}
