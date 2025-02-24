using Dapper;

namespace HE.Remediation.Core.TypeHandlers
{
    internal class TypeHandlerRegistry
    {
        public static void RegisterHandlers()
        {
            SqlMapper.AddTypeHandler(new DateOnlyTypeHandler());
        }
    }
}
