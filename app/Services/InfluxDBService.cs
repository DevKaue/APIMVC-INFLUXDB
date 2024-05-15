using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Writes;
using InfluxDB.Client.Core.Flux.Domain;
using Microsoft.Extensions.Configuration;
using app.Models;



namespace app.Services
{
    public class InfluxDBService
    {

        private readonly string _token;
        private readonly string _bucket;
        private readonly string _org;
        private readonly InfluxDBClient _client;
        // private readonly string _bucket = "teste-bucket";
        // private readonly string _org = "CO2COMPANY";
        private readonly string _url = "http://localhost:8086";

        [Obsolete]
        public InfluxDBService(IConfiguration configuration)
        {
            _token = configuration.GetValue<string>("InfluxDB:Token");
            _client = InfluxDBClientFactory.Create(_url, _token);
            _token = configuration.GetValue<string>("InfluxDB:Token");
            _bucket = configuration.GetValue<string>("InfluxDB:Bucket"); // Certifique-se de que o nome da chave está correto
            _org = configuration.GetValue<string>("InfluxDB:Org"); // Certifique-se de que o nome da chave está correto

        }

        // private readonly string _token;
        // private readonly InfluxDBClient _client;
        // private readonly string _bucket = "teste-bucket";
        // private readonly string _org = "CO2COMPANY";
        // private readonly string _url = "http://localhost:8086";

        // [Obsolete]
        // public InfluxDBService(IConfiguration configuration, string token, string bucket, string org)
        // {
        //     _token = configuration.GetValue<string>("InfluxDB:Token");
        //     _client = InfluxDBClientFactory.Create(_url, _token);
        //     _bucket = bucket;
        //     _org = org;
        // }

        // [Obsolete]
        // public void Write(Action<WriteApi> action)
        // {
        //     using var client = InfluxDBClientFactory.Create("http://localhost:8086", _token);
        //     using var write = client.GetWriteApi();
        //     action(write);
        // }

        // [Obsolete]
        // public async Task<T> QueryAsync<T>(Func<QueryApi, Task<T>> action)
        // {
        //     using var client = InfluxDBClientFactory.Create("http://localhost:8086", _token);
        //     var query = client.GetQueryApi();
        //     return await action(query);
        // }

        // public async Task WriteUserAsync(User user)
        // {
        //     using (var writeApi = _client.GetWriteApiAsync())
        //     {
        //         var point = PointData.Measurement("users")
        //             .Tag("id", user.Id.ToString())
        //             .Field("name", user.Name)
        //             .Field("email", user.Email)
        //             .Timestamp(DateTime.UtcNow, WritePrecision.Ns);

        //         await writeApi.WritePointAsync(point, _bucket, _org);
        //     }
        // }

        // public async Task<List<User>> QueryUsersAsync()
        // {
        //     var flux = $"from(bucket: \"{_bucket}\") |> range(start: 0) |> filter(fn: (r) => r._measurement == \"users\")";
        //     var tables = await _client.GetQueryApi().QueryAsync(flux, _org);

        //     var users = new List<User>();
        //     foreach (var table in tables)
        //     {
        //         foreach (var record in table.Records)
        //         {
        //             var user = new User
        //             {
        //                 Id = int.Parse(record.GetTagValue("id")),
        //                 Name = record.GetValueByKey("name").ToString(),
        //                 Email = record.GetValueByKey("email").ToString()
        //             };
        //             users.Add(user);
        //         }
        //     }

        //     return users;
        // }

        [Obsolete]
        public void Write(Action<WriteApi> action)
        {
            using var client = InfluxDBClientFactory.Create("http://localhost:8086", _token);
            using var write = client.GetWriteApi();
            action(write);
        }

        [Obsolete]
        public async Task<T> QueryAsync<T>(Func<QueryApi, Task<T>> action)
        {
            using var client = InfluxDBClientFactory.Create("http://localhost:8086", _token);
            var query = client.GetQueryApi();
            return await action(query);
        }

        public async Task WriteUserAsync(UserModel user)
        {
            var writeApi = _client.GetWriteApiAsync();

            var point = PointData.Measurement("users")
                .Tag("id", user.Id.ToString())
                .Field("name", user.Name)
                .Field("email", user.Email)
                .Timestamp(DateTime.UtcNow, WritePrecision.Ns);

            await writeApi.WritePointAsync(point, _bucket, _org);

        }

        // public async Task<List<UserModel>> QueryUsersAsync()
        // {
        //     var flux = $"from(bucket: \"{_bucket}\") |> range(start: 0) |> filter(fn: (r) => r._measurement == \"users\")";
        //     var tables = await _client.GetQueryApi().QueryAsync(flux, _org);

        //     var users = new List<UserModel>();
        //     foreach (var table in tables)
        //     {
        //         foreach (var record in table.Records)
        //         {
        //             var idValue = record.GetValueByKey("id")?.ToString();
        //             if (idValue != null && int.TryParse(idValue, out int id))
        //             {
        //                 var user = new UserModel
        //                 {
        //                     Id = id,
        //                     Name = record.GetValueByKey("name").ToString(),
        //                     Email = record.GetValueByKey("email").ToString()
        //                 };
        //                 users.Add(user);
        //             }
        //         }
        //     }

        //     return users;
        // }


        // public async Task<List<UserModel>> QueryUsersAsync()
        // {
        //     var flux = $"from(bucket: \"{_bucket}\") |> range(start: 0) |> filter(fn: (r) => r._measurement == \"users\")";
        //     var tables = await _client.GetQueryApi().QueryAsync(flux, _org);

        //     var users = new List<UserModel>();

        //     if (tables != null)
        //     {
        //         foreach (var table in tables)
        //         {
        //             foreach (var record in table.Records)
        //             {
        //                 var idValue = record.GetValueByKey("id")?.ToString();
        //                 if (idValue != null && int.TryParse(idValue, out int id))
        //                 {
        //                     var user = new UserModel
        //                     {
        //                         Id = id,
        //                         Name = record.GetValueByKey("name")?.ToString(),
        //                         Email = record.GetValueByKey("email")?.ToString()
        //                     };
        //                     users.Add(user);
        //                 }
        //             }
        //         }
        //     }

        //     return users;
        // }


        public async Task<List<UserModel>> QueryUsersAsync()
        {
            var flux = $"from(bucket: \"{_bucket}\") |> range(start: 0) |> filter(fn: (r) => r._measurement == \"users\")";
            var tables = await _client.GetQueryApi().QueryAsync(flux, _org);

            var users = new List<UserModel>();

            if (tables != null)
            {
                foreach (var table in tables)
                {
                    foreach (var record in table.Records)
                    {
                        var idValue = record.GetValueByKey("id")?.ToString();
                        var nameValue = record.GetValueByKey("name") as string;
                        var emailValue = record.GetValueByKey("email") as string;

                        if (idValue != null && int.TryParse(idValue, out int id))
                        {
                            var user = new UserModel
                            {
                                Id = id,
                                Name = nameValue ?? string.Empty,
                                Email = emailValue ?? string.Empty
                            };
                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }



    }
}