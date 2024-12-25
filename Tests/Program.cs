using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.IO;
using System.Text.Json;

namespace Tests
{
    public class TestCategory
    {
        public Guid _id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int Version { get; set; }
    }
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<TestCategory> testCategories = new List<TestCategory>();

                // Build the configuration
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                var configuration = builder.Build();

                // Define your connection string
                string connString = configuration.GetConnectionString("PostgresqlConnection");

                using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Define the function call query
                string query = "SELECT * FROM fn_test_categories_select(@p_name, @p_slug, @p_qty, @p_page_number)";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    // Add parameters for the function
                    cmd.Parameters.AddWithValue("p_name", DBNull.Value); // Example: NULL
                    cmd.Parameters.AddWithValue("p_slug", DBNull.Value); // Example: NULL
                    cmd.Parameters.AddWithValue("p_qty", 10);
                    cmd.Parameters.AddWithValue("p_page_number", 1);

                    // Execute the function and get the data
                    using (var reader = cmd.ExecuteReader())
                    {
                        List<TestCategory> categories = new List<TestCategory>();

                        while (reader.Read())
                        {
                            // Read each column by name or index
                            var category = new TestCategory
                            {
                                _id = reader.GetGuid(reader.GetOrdinal("_id")),
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                Slug = reader.GetString(reader.GetOrdinal("slug")),
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("createdat")),
                                UpdatedAt = reader.GetDateTime(reader.GetOrdinal("updatedat")),
                                Version = reader.GetInt32(reader.GetOrdinal("version"))
                            };

                            // Serialize the category object to JSON
                            string jsonCategory = JsonSerializer.Serialize(category);
                            Console.WriteLine($"Serialized Category (JSON): {jsonCategory}");

                            categories.Add(category);
                        }

                        Console.WriteLine($"Total categories retrieved: {categories.Count}");
                    }
                }
            }
        }
    }
}
