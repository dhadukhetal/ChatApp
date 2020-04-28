
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;

namespace ChatApi.DataServices
{
    public static class Utility
    {
        public static List<T> ConvertToList<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }

        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dr[column.ColumnName])))
                            pro.SetValue(obj, dr[column.ColumnName]);
                        //if (!string.IsNullOrEmpty(Convert.ToString(dr[column.ColumnName])))
                        //{
                        //    if (pro.PropertyType.Name == "String")
                        //        pro.SetValue(obj, Convert.ToString(dr[column.ColumnName]));
                        //    else if(pro.PropertyType.Name == "Int64")
                        //        pro.SetValue(obj, Convert.ToInt64(dr[column.ColumnName]));
                        //    else
                        //        pro.SetValue(obj, dr[column.ColumnName]);
                        //}
                    }
                    else
                        continue;
                }
            }
            return obj;
        }

        public static object ToInt32orNULL(int? obj)
        {
            if (obj == null || obj == 0)
                return DBNull.Value;
            else
                return (Int32)obj;
        }

        public static DateTime ParseDate(string date)
        {
            
            System.Globalization.DateTimeFormatInfo dateFormatProvider = new System.Globalization.DateTimeFormatInfo();
            dateFormatProvider.ShortDatePattern = "dd/MM/yyyy";
            return DateTime.Parse(date, dateFormatProvider);
        }

        public static DataTable ListToDataTable<T>(IList<T> data)
        {
            DataTable table = new DataTable();

            //special handling for value types and string
            if (typeof(T).IsValueType || typeof(T).Equals(typeof(string)))
            {

                DataColumn dc = new DataColumn("Value");
                table.Columns.Add(dc);
                foreach (T item in data)
                {
                    DataRow dr = table.NewRow();
                    dr[0] = item;
                    table.Rows.Add(dr);
                }
            }
            else
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
                foreach (PropertyDescriptor prop in properties)
                {
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
                foreach (T item in data)
                {
                    DataRow row = table.NewRow();
                    foreach (PropertyDescriptor prop in properties)
                    {
                        try
                        {
                            row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                        }
                        catch (Exception ex)
                        {
                            row[prop.Name] = DBNull.Value;
                        }
                    }
                    table.Rows.Add(row);
                }
            }
            return table;
        }

    }
}
