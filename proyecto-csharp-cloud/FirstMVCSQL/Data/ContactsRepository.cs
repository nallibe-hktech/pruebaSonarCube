using Dapper;
using FirstMVCSQL.Models;
using System.Data;

namespace FirstMVCSQL.Data
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly IDbConnection _dbConnection;


        public ContactsRepository(IDbConnection dbConnection) 
        {


            _dbConnection = dbConnection;
        
        }

        public  async Task Delete(int id)
        {
            var sql = @"DELETE FROM Contacts
                        WHERE Id= @Id";

             await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<Contact>> GetAll()
        {
            var sql = @"SELECT Id, FirstName, LastName, Phone, Address FROM Contacts";

            return await _dbConnection.QueryAsync<Contact>(sql, new { });
        }

        public async Task<Contact> GetDetails(int id)
        {
            var sql = @"SELECT Id, FirstName, LastName, Phone, Address FROM Contacts
                        WHERE Id= @Id";

            return await _dbConnection.QueryFirstOrDefaultAsync<Contact>(sql, new { Id = id});
        }

        public async Task Insert(Contact contac)
        {
            var sql = @"INSERT INTO Contacts ( FirstName, LastName,Phone,Address)
                        VALUES (@FirstName, @LastName, @Phone, @Address)";

             await _dbConnection.ExecuteAsync(sql, new
            {
                contac.FirstName,
            contac.LastName,
            contac.Phone,
            contac.Address});
        }

        public async Task Update(Contact contac)
        {
            var sql = @"UPDATE Contacts
                        SET FirstName = @FirstName,
                        LastName = @LastName,
                        Phone = @Phone,
                        Address = @Address
                       WHERE Id_ @Id";
                        

            await _dbConnection.ExecuteAsync(sql, new
            {
                contac.FirstName,
                contac.LastName,
                contac.Phone,
                contac.Address,
                contac.Id
            });
        }
    }
}
