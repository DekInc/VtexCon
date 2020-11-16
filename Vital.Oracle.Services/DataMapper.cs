using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Vital.Oracle.Services.Model;

namespace Vital.Oracle.Services
{
    public static class DataMapper
    {
        private static List<string> columns = new List<string>();

        public static IEnumerable<T> MapReaderToObjectList<T>(IDataReader reader) where T : new()
        {
            bool columnsChecked = false;
            while (reader.Read())
            {
                if (!columnsChecked)
                {
                    columns = new List<string>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        columns.Add(reader.GetName(i).ToLower());
                    }

                    columnsChecked = true;
                }

                var item = new T();

                foreach (PropertyInfo property in item.GetType().GetProperties())
                {
                    if (item is BaseModel)
                    {
                        if (property.Name == "IsValid")
                        {
                            continue;
                        }
                    }

                    Type type = property.PropertyType;

                    var name = property.Name;

                    var attribute = typeof(T).GetProperty(name).GetCustomAttributes(typeof(DataParameterAttribute), true);

                    if (attribute != null && attribute.Count() > 0 && attribute.SingleOrDefault() is DataParameterAttribute)
                    {
                        var dataParameterAttribute = (DataParameterAttribute)attribute.SingleOrDefault();

                        if (dataParameterAttribute.Ignore)
                        {
                            continue;
                        }

                        var columnName = dataParameterAttribute.ColumnName;

                        name = string.IsNullOrEmpty(columnName) ? name : columnName;
                    }

                    if (columns.Contains(name.ToLower()) && reader[name].GetType() != typeof(DBNull))
                    {
                        if (property.PropertyType == typeof(bool))
                        {
                            if (reader[name].ToString() == "0" || reader[name].ToString().ToLower() == "false")
                            {
                                property.SetValue(item, false, null);
                            }
                            else
                            {
                                property.SetValue(item, true, null);
                            }
                        }
                        else
                        {
                            property.SetValue(item, reader[name], null);
                        }
                    }
                }

                yield return item;
            }
        }

        public static dynamic MapReaderToDynamicList(IDataReader reader)
        {
            /*Se modifica la lógica para retornar una lista en el caso de retornar varios cursores.
              NextResult sirve para verificar la presencia de un nuevo cursor*/

            List<dynamic> listOfList = new List<dynamic>();
            dynamic list = null;

            do
            {
                list = new List<dynamic>();

                while (reader.Read())
                {
                    var item = new ExpandoObject() as IDictionary<string, object>;

                    for (var i = 0; i < reader.FieldCount; i++)
                    {
                        item.Add(reader.GetName(i), reader[i]);
                    }

                    list.Add(item);
                }

                listOfList.Add(list);

            }
            while (reader.NextResult());

            /*Caso en que se retorna 1 cursor, se devuelve el cursor y no la lista para mantener
              la lógica inicial*/
            if (listOfList.Count == 1)
                return listOfList[0];

            //Varios cursores
            return listOfList;
        }
    }
}
