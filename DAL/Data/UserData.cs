using DAL.Interfaces;
using MODELS;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DAL.DTO;

namespace DAL.Data
{
    public class UserData : IUserInterface
    {
        private readonly IMongoCollection<User> _usersCollection;
        private readonly IMapper _mapper;
        public UserData(IMongoClient mongoClient, string databaseName, IMapper mapper)
        {
            var database = mongoClient.GetDatabase(databaseName);
            _usersCollection = database.GetCollection<User>("Users");
            _mapper = mapper;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _usersCollection.Find(_ => true).ToListAsync();
        }

        public async Task<User> GetUserById(string id)
        {
            return await _usersCollection.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _usersCollection.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<bool> AddUser(UserDTO user)
        {
            await _usersCollection.InsertOneAsync(_mapper.Map<User>(user));
            return true;
        }

        public async Task UpdateUser(string id, UserDTO user)
        {
            await _usersCollection.ReplaceOneAsync(u => u.Id == id, _mapper.Map<User>(user));
        }

        public async Task DeleteUser(string id)
        {
            await _usersCollection.DeleteOneAsync(u => u.Id == id);
        }
    }
}
