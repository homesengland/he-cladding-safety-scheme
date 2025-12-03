using System.Data;
using System.Text;

namespace HE.Remediation.Core.Extensions;

public static class IEnumerableExtensions
{
    public static DataTable ToDataTable<T>(this IEnumerable<T> list)
    {
        return CreateAndPopulateDataTable(list);
    }

    private static DataTable CreateAndPopulateDataTable<T>(this IEnumerable<T> enumerable)
    {
        var list = enumerable?.ToList() ?? new List<T>();
        var dataTable = new DataTable();

        if (typeof(T) == typeof(string) || typeof(T).IsValueType)
        {
            AddValueTypeRows(list, dataTable);
            return dataTable;
        }

        AddObjectTypeRows(list, dataTable);
        return dataTable;
    }

    private static void AddObjectTypeRows<T>(IEnumerable<T> list, DataTable dataTable)
    {
        var props = typeof(T).GetProperties();

        foreach (var property in props)
        {
            dataTable.Columns.Add(property.Name, property.PropertyType);
        }
        AddComplexRows(list, dataTable);
    }

    private static void AddValueTypeRows<T>(IEnumerable<T> list, DataTable dataTable)
    {
        dataTable.Columns.Add("Value", typeof(T));
        foreach (var item in list)
        {
            dataTable.Rows.Add(item);
        }
    }

    private static void AddComplexRows<T>(IEnumerable<T> list, DataTable dataTable)
    {
        var props = typeof(T).GetProperties();

        foreach (var item in list)
        {
            var row = dataTable.NewRow();
            foreach (var prop in props)
            {
                row[prop.Name] = prop.GetValue(item);
            }
            dataTable.Rows.Add(row);
        }
    }

    public static string ToAndOrStringList(this IEnumerable<string> stringList, string listDelimiter, EAndOr andOr)
    {
        var stringBuilder = new StringBuilder();
        var list = stringList?.ToArray();

        if (list is null || list.Length == 0)
        {
            return string.Empty;
        }

        if (list.Length == 1)
        {
            return list.First();
        }

        stringBuilder.Append(string.Join(listDelimiter, list.Take(list.Length - 1)));

        var andOrString = andOr switch
        {
            EAndOr.And => " and ",
            EAndOr.Or => " or ",
            _ => throw new ArgumentException($"Unrecognised enum value {andOr.ToString()} ({(int)andOr})", nameof(andOr))
        };

        stringBuilder.Append(andOrString);

        stringBuilder.Append(list.Last());

        return stringBuilder.ToString();
    }
}