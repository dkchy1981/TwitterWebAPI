using CoreWebAPIDapperPractice1.Reposiroty;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;
using TwitterDemoAPI.Models;

namespace TwitterDemoAPI.Repository
{
    public class TweetRepository : ITweetRepository
    {
        private readonly IConfiguration configuration;

        public TweetRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Task<int> AddAsync(Tweet entity)
        {
            var sql = "Insert into Tweets (UserID,Tag,Message) VALUES (@userID,@Tag,@Message)";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = $"Delete From Tweets Where id={id}";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql);
                return result;
            }
        }

        public async Task<IEnumerable<Tweet>> GetAllAsync()
        {
            var sql = "select id,UserId,Tag,Message from tweets";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Tweet>(sql);
                return result;
            }
        }

        public async Task<IEnumerable<Tweet>> GetAllByUserIdAsync(string userid)
        {
            var sql = $"select id,UserId,Tag,Message from tweets where userid='{userid}'";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Tweet>(sql);
                return result;
            }
        }

        public async Task<Tweet> GetByIdAsync(int id)
        {
            var sql = $"select id,UserId,Tag,Message from tweets where id='{id}'";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Tweet>(sql);
                return result.FirstOrDefault();
            }
        }

        public Task<int> UpdateAsync(Tweet entity)
        {
            var sql = $"Update Tweets Set Tag='{entity.Tag}',Message='{entity.Message}'where id={entity.Id} ";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
