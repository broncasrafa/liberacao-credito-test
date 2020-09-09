using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;

namespace Application.Extensions
{
    public static class ExtensionsMethods
    {
        /// <summary>
        /// Converte uma lista de objetos em um DataTable
        /// </summary>
        /// <typeparam name="T">Tipo do Objeto</typeparam>
        /// <param name="list">Lista de objetos</param>
        /// <returns>retorna um DataTable</returns>
        public static DataTable ConvertToDataTable<T>(this IList<T> list)
        {
            if (list == null)
                return new DataTable();

            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

            DataTable dataTable = new DataTable();

            foreach (PropertyDescriptor prop in properties)
            {
                dataTable.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in list)
            {
                DataRow row = dataTable.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
    }
}
