using Dapper;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace HE.Remediation.Core.TypeHandlers
{
    [ExcludeFromCodeCoverage]
    internal class DateOnlyTypeHandler : SqlMapper.TypeHandler<DateOnly>
    {
        public override void SetValue(IDbDataParameter parameter, DateOnly value)
        {
            parameter.DbType = DbType.Date;
            parameter.Value = value.ToDateTime(new TimeOnly(0, 0));
        }

        public override DateOnly Parse(object value)
        {
            return DateOnly.FromDateTime((DateTime)value);
        }
    }
}
