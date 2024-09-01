using System;
using System.Data.SQLite;
using System.IO;

public class DatabaseHelper
{
    private readonly string _connectionString;

    public DatabaseHelper(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void InsertRecording(string fileName, byte[] audioData)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();

            string query = "INSERT INTO AudioRecordings (FileName, AudioData, CreatedAt) VALUES (@FileName, @AudioData, @CreatedAt)";

            using (var command = new SQLiteCommand(query, connection))
            {
                command.Parameters.AddWithValue("@FileName", fileName);
                command.Parameters.AddWithValue("@AudioData", audioData);
                command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                command.ExecuteNonQuery();
            }
        }
    }

    public byte[] GetAudioById(int id)
    {
        using (var connection = new SQLiteConnection(_connectionString))
        {
            connection.Open();
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT AudioData FROM AudioRecordings WHERE Id = @id";
                command.Parameters.AddWithValue("@id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return (byte[])reader["AudioData"];
                    }
                }
            }
        }
        return null;
    }
}

