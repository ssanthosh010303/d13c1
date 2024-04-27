/*
 * Author: Sakthi Santhosh
 * Created on: 24/04/2024
 */
using Challenge1.Library.Models;
using Challenge1.Library.Repositories;

namespace Challenge1.Library.Services;

public class CustomerService(IBaseRepository<CustomerModel> repository) : BaseService<CustomerModel>(repository)
{
}
