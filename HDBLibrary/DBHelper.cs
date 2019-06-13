using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBLibrary
{
    public static class DBHelper
    {
        const string connectionName = "DefaultConnection";

        public static string GetConnectionString(string connName = connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connName].ConnectionString;
        }

        public static List<T> LoadDataList<T>(string query, object parameters = null, string cnnName = connectionName) where T : class, new()
        {
            var output = new List<T>();

            var cmdType = query.ToUpper().Contains("SELECT") || query.Contains("INSERT") || query.Contains("UPDATE") ? CommandType.Text : CommandType.StoredProcedure;

            using (IDbConnection cnn = new SqlConnection(GetConnectionString(cnnName)))
            {
                if (parameters != null)
                {
                    output = cnn.Query<T>(query, parameters, commandType: cmdType).ToList();
                }
                else
                {
                    output = cnn.Query<T>(query, commandType: cmdType).ToList();
                }
            }

            return output;
        }

        public static T LoadData<T>(string query, object parameters = null, string cnnName = connectionName) where T : class, new()
        {
            var output = new T();

            var cmdType = query.ToUpper().Contains("SELECT") || query.Contains("INSERT") || query.Contains("UPDATE") ? CommandType.Text : CommandType.StoredProcedure;

            try
            {
                using (IDbConnection cnn = new SqlConnection(GetConnectionString(cnnName)))
                {
                    if (parameters != null)
                    {
                        output = cnn.QueryFirstOrDefault<T>(query, parameters, commandType: cmdType);
                    }
                    else
                    {
                        output = cnn.QueryFirstOrDefault<T>(query, commandType: cmdType);
                    }
                }
            }
            catch (Exception e)
            {
                var ex = e.Message;
            }

            return output;
        }

        public static T ExecuteSQL<T>(string query, Hashtable parameters = null, string returnParameter = null, string cnnName = connectionName)
        {
            T output = default(T);
            using (IDbConnection cnn = new SqlConnection(GetConnectionString(cnnName)))
            {
                var cmdType = query.ToUpper().Contains("SELECT") || query.Contains("INSERT") || query.Contains("UPDATE") ? CommandType.Text : CommandType.StoredProcedure;

                var p = new DynamicParameters();
                if (parameters != null)
                {
                    foreach (DictionaryEntry prm in parameters)
                    {
                        if (returnParameter != null && prm.Key.ToString() == returnParameter)
                        {
                            p.Add($"@{prm.Key.ToString().Replace("@", "")}", null, dbType: DbType.String, direction: ParameterDirection.Output);
                        }
                        else
                        {
                            p.Add($"@{prm.Key.ToString().Replace("@", "")}", prm.Value);
                        }
                    }

                    cnn.Execute(query, p, commandType: cmdType);

                    if (returnParameter != null)
                    {
                        output = p.Get<T>($"@{returnParameter.Replace("@", "")}");
                    }
                }
                else
                {
                    cnn.Execute(query, commandType: cmdType);
                }
            }
            parameters = null;
            return output;
        }

        public static T ExecuteSQL<T>(string query, T obj, string returnParameter = null, int returnParamSize = 0, string cnnName = connectionName)
        {
            //T output = default(T);
            var output = obj;

            var cmdType = query.ToUpper().Contains("SELECT") || query.Contains("INSERT") || query.Contains("UPDATE") ? CommandType.Text : CommandType.StoredProcedure;

            using (IDbConnection connection = new SqlConnection(GetConnectionString(cnnName)))
            {
                var p = new DynamicParameters();
                var props = typeof(T).GetProperties();
                foreach (var prop in props)
                {
                    if (returnParameter != null && prop.Name == returnParameter)
                    {
                        p.Add($"@{prop.Name}", prop.GetValue(obj), dbType: DbType.String, size: returnParamSize, direction: ParameterDirection.InputOutput);
                    }
                    else
                    {
                        p.Add($"@{prop.Name}", prop.GetValue(obj));
                    }
                }

                //connection.Execute(query, p, commandType: cmdType);
                connection.Execute(query, obj, commandType: cmdType);
                if (returnParameter != null)
                {
                    var value = p.Get<string>($"@{returnParameter.Replace("@", "")}");
                    var prop = props.Where(x => x.Name == returnParameter).FirstOrDefault();
                    prop.SetValue(obj, value);
                }

            }
            return output;
        }

        public static T ExecuteSQL<T>(string query, object parameters = null, string cnnName = connectionName)
        {
            T output = default(T);
            var cmdType = query.ToUpper().Contains("SELECT") || query.Contains("INSERT") || query.Contains("UPDATE") ? CommandType.Text : CommandType.StoredProcedure;

            using (IDbConnection cnn = new SqlConnection(GetConnectionString(cnnName)))
            {
                output = cnn.QueryFirstOrDefault<T>(query, parameters, commandType: cmdType);
            }
            return output;
        }

        public static T ExecuteScalarSQL<T>(string query, object parameters = null, string cnnName = connectionName)
        {
            T output = default(T);
            var cmdType = query.ToUpper().Contains("SELECT") || query.Contains("INSERT") || query.Contains("UPDATE") ? CommandType.Text : CommandType.StoredProcedure;

            using (IDbConnection cnn = new SqlConnection(GetConnectionString(cnnName)))
            {
                if (parameters != null)
                {
                    output = (T)cnn.ExecuteScalar(query, parameters, commandType: cmdType);
                }
                else
                {
                    output = (T)cnn.ExecuteScalar(query, commandType: cmdType);
                }
            }
            return output;
        }

        public static string GetAppSettings(string key, bool addSlash = false)
        {
            try
            {
                string keyValue = null;
                if (ConfigurationManager.AppSettings[key] != null)
                {
                    if (addSlash)
                    {
                        keyValue = ConfigurationManager.AppSettings[key].Trim() + (ConfigurationManager.AppSettings[key].Substring(ConfigurationManager.AppSettings[key].Trim().Length - 1, 1) != "\\" ? "\\" : "");
                    }
                    else
                    {
                        keyValue = ConfigurationManager.AppSettings[key];
                    }
                }
                return keyValue;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
