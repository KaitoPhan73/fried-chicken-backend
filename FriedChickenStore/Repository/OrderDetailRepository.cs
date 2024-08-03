using FriedChickenStore.Data;
using FriedChickenStore.Model.Entity;
using Microsoft.EntityFrameworkCore;

namespace FriedChickenStore.Repository
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetail> GetAll();
        OrderDetail GetById(int id);

        IEnumerable<OrderDetail> GetAllByOrderId(int orderId);
        Task<OrderDetail> GetByIdAsync(int id);
        void Add(OrderDetail entity);
        Task AddAsync(OrderDetail entity);
        void Update(OrderDetail entity);
        void DeleteById(int id);

    }

    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderDetailRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<OrderDetail> GetAll()
        {
            return _dbContext.Set<OrderDetail>().ToList().Where(entity => entity.Status.Equals(true))
                                                             .OrderByDescending(entity => entity.CreateTimes);
        }

        public OrderDetail GetById(int id)
        {
            return _dbContext.Set<OrderDetail>().Where(od => od.Id == id && od.Status.Equals(true)).FirstOrDefault();
        }

        public IEnumerable<OrderDetail> GetAllByOrderId(int orderId)
        {
            return _dbContext.Set<OrderDetail>().ToList().Where(od => od.OrderId == orderId && od.Status.Equals(true));
        }


        public async Task<OrderDetail> GetByIdAsync(int id)
    {
        return await _dbContext.Set<OrderDetail>().FindAsync(id);
    }

    public void Add(OrderDetail entity)
    {
        _dbContext.Set<OrderDetail>().Add(entity);
        _dbContext.SaveChanges();
    }

    public async Task AddAsync(OrderDetail entity)
    {
        await _dbContext.Set<OrderDetail>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
    }

    public void Update(OrderDetail entity)
    {
        _dbContext.Set<OrderDetail>().Update(entity);
        _dbContext.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var entityToDelete = _dbContext.Set<OrderDetail>().Find(id);
        if (entityToDelete != null)
        {
            _dbContext.Set<OrderDetail>().Remove(entityToDelete);
            _dbContext.SaveChanges();
        }
    }
}
}
