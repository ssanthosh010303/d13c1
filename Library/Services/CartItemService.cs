/*
 * Author: Sakthi Santhosh
 * Created on: 24/04/2024
 */
using Challenge1.Library.Models;
using Challenge1.Library.Repositories;

namespace Challenge1.Library.Services;

public class CartItemService(IBaseRepository<CartItemModel> repository) : BaseService<CartItemModel>(repository)
{
}
