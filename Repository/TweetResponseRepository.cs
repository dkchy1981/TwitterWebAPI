using CoreWebAPIDapperPractice1.Reposiroty;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using System.Data.SqlClient;
using TwitterDemoAPI.Models;

namespace TwitterDemoAPI.Repository
{
    public class TweetResponseRepository : ITweetResponseRepository
    {
        private readonly IConfiguration configuration;

        public TweetResponseRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<int> AddAsync(TweetResponse entity)
        {
            var sql = "Insert into TweetResponse (UserID,TweetId,[Like],Comments) VALUES (@userID,@TweetId,@Like,@Comments)";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = $"Delete From TweetResponse Where id={id}";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql);
                return result;
            }
        }

        public async Task<IEnumerable<TweetResponse>> GetAllAsync()
        {
            var sql = "select id,UserId,TweetId,[Like],Comments from TweetResponse";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<TweetResponse>(sql);
                return result;
            }
        }

        public async Task<IEnumerable<TweetResponse>> GetAllByUserIdAsync(string userid)
        {
            var sql = $"select id,UserId,TweetId,[Like],Comments from TweetResponse where userid='{userid}'";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<TweetResponse>(sql);
                return result;
            }
        }

        public async Task<TweetResponse> GetByIdAsync(int id)
        {
            var sql = $"select id,UserId,TweetId,[Like],Comments from TweetResponse where id='{id}'";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<TweetResponse>(sql);
                return result.FirstOrDefault();
            }
        }

        public Task<int> UpdateAsync(TweetResponse entity)
        {
            var sql = $"Update TweetResponse Set [Like]='{entity.Like}',Comments='{entity.Comments}'where id={entity.Id} ";
            using (var connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
            {
                connection.Open();
                var result = connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}
