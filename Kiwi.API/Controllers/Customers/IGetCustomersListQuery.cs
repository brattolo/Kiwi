using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kiwi.API.Controllers
{
    public interface IGetCustomersListQuery
    {
        Task<List<CustomerModel>> Execute();
    }
}