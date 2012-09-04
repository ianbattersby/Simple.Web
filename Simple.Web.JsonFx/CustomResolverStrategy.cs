using System.Collections.Generic;
using System.Reflection;
using JsonFx.Serialization;
using JsonFx.Serialization.Resolvers;

namespace Simple.Web.JsonFx
{
    public class CustomResolverStrategy : IResolverStrategy
    {
        public virtual bool IsPropertyIgnored(PropertyInfo member, bool isImmutableType)
        {
            // must be public read/write (or anonymous object)
            MethodInfo getter = member.CanRead ? member.GetGetMethod() : null;
            MethodInfo setter = member.CanWrite ? member.GetSetMethod() : null;

            return
                (getter == null || !getter.IsPublic) || member.GetIndexParameters().Length > 0 ||
                (!isImmutableType && (setter == null || !setter.IsPublic));
        }

        public virtual bool IsFieldIgnored(FieldInfo member)
        {
            // must be public read/write
            return (!member.IsPublic || member.IsInitOnly);
        }

        public virtual ValueIgnoredDelegate GetValueIgnoredCallback(MemberInfo member)
        {
            return null;
        }

        public virtual IEnumerable<DataName> GetName(MemberInfo member)
        {
            return null;
        }

        public virtual IEnumerable<MemberMap> SortMembers(IEnumerable<MemberMap> members)
        {
            return members;
        }
    }
}