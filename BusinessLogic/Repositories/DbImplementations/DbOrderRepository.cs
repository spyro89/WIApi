using BusinessLogic.Repositories.Interfaces;
using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repositories.DbImplementations;
public class DbOrderRepository : IOrderRepository
{
    private readonly WIApiContext _context;

    public DbOrderRepository(WIApiContext context)
    {
        _context = context;
    }

    public void Add(Order order)
    {
        _context.Orders.Add(order);
        _context.SaveChanges();
    }

    public bool ExistsAnyActiveOrderWithSelectedProduct(int productId)
    {
        return _context.OrderItems
                .Where(x => x.ProductId == productId
                    && x.Order.Status == DB.Enums.OrderStatus.Created)
                .Any();
    }

    public List<Order> GetAll()
    {
        return _context.Orders
                .Include(x => x.Items)
                .ToList();
    }

    public Order GetOne(int id)
    {
        return _context.Orders
                .Include(x => x.Items)
                .SingleOrDefault(x => x.Id == id);
    }

    public void Update(Order order)
    {
        _context.Orders.Update(order);
        _context.SaveChanges();
    }
}
