using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.DAL
{
    public static class CommonDBHelper
    {
        //https://stackoverflow.com/questions/1464883/how-can-i-easily-convert-datareader-to-listt
        public static List<T> GetDataFromDB<T>(string query, string dbConnection)
        {
            using (var connection = new SqlConnection(dbConnection))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            List<T> list = new List<T>();
                            T obj = default(T);
                            while (reader.Read())
                            {
                                obj = Activator.CreateInstance<T>();
                                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                                {
                                    if (!object.Equals(reader[prop.Name], DBNull.Value))
                                    {
                                        //prop.SetValue(obj, dr[prop.Name], null);
                                        if (prop.PropertyType.ToString().Contains("Boolean"))
                                        {
                                            bool value = Convert.ToBoolean(reader[prop.Name]);
                                            if (value)
                                                prop.SetValue(obj, true);
                                            else
                                                prop.SetValue(obj, false);
                                        }
                                        else if (prop.PropertyType.ToString().Contains("Decimal"))
                                        {
                                            prop.SetValue(obj, Convert.ToDecimal(reader[prop.Name]));
                                        }
                                        else if (prop.PropertyType.ToString().Contains("Int32"))
                                        {
                                            prop.SetValue(obj, Convert.ToInt32(reader[prop.Name]));
                                        }
                                        else if (prop.PropertyType.ToString().Contains("DateTime"))
                                        {
                                            prop.SetValue(obj, Convert.ToDateTime(reader[prop.Name]));
                                        }
                                        else
                                        {
                                            prop.SetValue(obj, Convert.ChangeType(reader[prop.Name], prop.PropertyType), null);
                                        }
                                    }
                                }
                                list.Add(obj);
                            }
                            return list;

                        }
                    }
                }
            }
            return null;
        }

        public static string ExecuteSQL(string sql, string dbConnection)
        {
            string result = "";

            try
            {
                using (var connection = new SqlConnection(dbConnection))
                {
                    SqlCommand cmd = new SqlCommand(sql, connection);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result = reader.GetString(0);
                    }
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

    }
}