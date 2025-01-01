using DataLib._Util;
using DataLib.Model;
using Microsoft.Extensions.Configuration;
using Npgsql;


namespace DataLib.Data;

public class TestCategoryBase : ConnectionUtil
{
    protected long GetTestCategoryCountBase()
    {
        long result = 0;
        
        using var connection = new NpgsqlConnection(GetConnectionString());
        try
        {
            connection.Open();
        
            string? query = "SELECT COUNT(_id) FROM test_categories";
            using var command = new NpgsqlCommand(query, connection);
            result = (long)command.ExecuteScalar();
            connection.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            connection.Close();
        }
       
        return result;
    }
    protected List<TestCategoryDTO> GetTestCategoriesBase(
        int? pageSize = null,
        int? pageNumber = null,
        bool bRetrieveAllTestCategories = false
        )
    {
        List<TestCategoryDTO> categories = new();


        using (var conn = new NpgsqlConnection(GetConnectionString()))
        {
            conn.Open();
            
            string query = "SELECT * FROM fn_test_categories_select(@p_name, @p_slug, @p_qty, @p_page_number)";

            using (var cmd = new NpgsqlCommand(query, conn))
            {
                // Add parameters for the function
                cmd.Parameters.AddWithValue("p_name", DBNull.Value); // Example: NULL
                cmd.Parameters.AddWithValue("p_slug", DBNull.Value); // Example: NULL
                cmd.Parameters.AddWithValue("p_qty", pageSize ?? 10);
                cmd.Parameters.AddWithValue("p_page_number", pageNumber ?? 1);

                // Execute the function and get the data
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Read each column by name or index
                        var category = new TestCategoryDTO
                        {
                            _id = reader.GetGuid(reader.GetOrdinal("_id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Slug = reader.GetString(reader.GetOrdinal("slug")),
                            CreatedAt = reader.GetDateTime(reader.GetOrdinal("createdat")),
                            UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updatedat")),
                            Version = reader.GetInt32(reader.GetOrdinal("version"))
                        };


                        categories.Add(category);
                    }
                }
            }
        }

        return categories;
    }
}