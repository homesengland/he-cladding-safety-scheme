using System.Data;

namespace HE.Remediation.Core.Extensions
{
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
    }
}
