using FirstMVCSQL.Models;

namespace FirstMVCSQL.Data
{
    public interface IContactsRepository
    {
        Task<IEnumerable<Contact>> GetAll();

        Task<Contact> GetDetails(int id);
        Task Insert(Contact contac);
        Task Update(Contact contac);
        Task Delete(int id);


    }
}
