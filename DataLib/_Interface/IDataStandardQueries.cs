
using DataLib._Util;
using Npgsql;

namespace DataLib._Interface;

public class DataStandardQueries : ConnectionUtil
{
    public List<T> GetList<T>(string query, Dictionary<string, object> parameters) where T : new()
    {
        List<T> resultList = new List<T>();
    
        try
        {
            using (var conn = new NpgsqlConnection(GetConnectionString()))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    // Add parameters to the command if any
                    foreach (var param in parameters)
                    {
                        cmd.Parameters.AddWithValue(param.Key, param.Value);
                    }

                    using (var reader = cmd.ExecuteReader())
                    {
                        // Iterate over the data reader and create instances of T
                        while (reader.Read())
                        {
                            T item = new T();
                        
                            // Use reflection to populate the properties of T
                            foreach (var property in typeof(T).GetProperties())
                            {
                                // Check if the property exists in the reader and set the value
                                if (reader.GetOrdinal(property.Name) >= 0)
                                {
                                    var value = reader[property.Name];
                                    if (value != DBNull.Value)
                                    {
                                        property.SetValue(item, Convert.ChangeType(value, property.PropertyType));
                                    }
                                }
                            }

                            resultList.Add(item);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetList: {ex.Message}");
        }
    
        return resultList;
    }

    protected T GetById<T>(Guid id) where T : new();
    protected T Update<T>(Guid id, T UpdateObject) where T : new();
    protected T Insert<T>(T InsertObject) where T : new();
    
}