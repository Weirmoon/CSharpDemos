using DataLib._Interface;
using DataLib.Model;
using DataLib.Model.Admin;
using Npgsql;

namespace DataLib.Data;

public class UserBase :  DataStandardQueries
{
    protected string? Login(string username, string password)
    {
        string? authKey = null;
        try
        {
            using (NpgsqlConnection conn = new(GetConnectionString()))
            {
                conn.Open();
                string query = "SELECT username, password FROM users WHERE username = @username AND password = @password";
                using (NpgsqlCommand cmd = new(query, conn))
                {
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("password", password);
                    using (var reader = cmd.ExecuteReader())
                    {
                        
                        while (reader.Read())
                        {
                            
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
        return authKey;
    }
    protected List<UserDTO> GetUsers(int? pageSize = null, int? pageNumber = null, bool bRetreiveAllUsers = false)
    {
        List<UserDTO> users = [];
        try
        {
            using (NpgsqlConnection conn = new(GetConnectionString()))
            {
                conn.Open();
                string query = "CALL ";
                using (NpgsqlCommand cmd = new(query, conn))
                {
                    cmd.Parameters.AddWithValue("@pageSize", pageSize);
                    cmd.Parameters.AddWithValue("@pageNumber", pageNumber);
                    cmd.Parameters.AddWithValue("@bRetreiveAllUsers", bRetreiveAllUsers);

                    using (var reader = cmd.ExecuteReader())    
                    {
                        while (reader.Read())
                        {
                            UserDTO user = new()
                            {
                                Id = reader.GetGuid(reader.GetOrdinal("Id")),
                                Username = reader.GetString(reader.GetOrdinal("Username")),
                                Firstname = reader.GetString(reader.GetOrdinal("Firstname")),
                                Lastname = reader.GetString(reader.GetOrdinal("Lastname")),
                                Email = reader.GetString(reader.GetOrdinal("Email")),
                                ProfileImageUrl = reader.GetString(reader.GetOrdinal("ProfileImageUrl")),
                                CreatedDate = reader.GetDateTime(reader.GetOrdinal("CreatedDate")),
                                LastModifiedDate = reader.GetDateTime(reader.GetOrdinal("LastModifiedDate")),
                                Version = reader.GetInt32(reader.GetOrdinal("Version"))
                            };
                            
                            users.Add(user);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return users;
    }

    public List<UserDTO> GetList()
    {
        throw new NotImplementedException();
    }

    public T GetById<T>(Guid id) where T : new()
    {
        throw new NotImplementedException();
    }

    public T Update<T>(Guid id, T UpdateObject) where T : new()
    {
        throw new NotImplementedException();
    }

    public T Insert<T>(T InsertObject) where T : new()
    {
        throw new NotImplementedException();
    }
}