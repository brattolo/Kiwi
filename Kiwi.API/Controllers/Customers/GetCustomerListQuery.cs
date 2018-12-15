using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kiwi.API.Controllers
{
    public class GetCustomersListQuery : IGetCustomersListQuery
    {
        public GetCustomersListQuery()
        {
        }

        public async Task<List<CustomerModel>> Execute()
        {
            var customers = new List<CustomerModel>();
            customers.Add(new CustomerModel());
            customers.Add(new CustomerModel());
            customers.Add(new CustomerModel());

            return await Task.FromResult(customers);
        }
    }

}
