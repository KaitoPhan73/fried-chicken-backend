using FriedChickenStore.Data;
using FriedChickenStore.Model.Entity;

namespace FriedChickenStore.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        IEnumerable<Order> GetAllByOrderStatus(String orderStatus);
        Order GetById(int id);
        Task<Order> GetByIdAsync(int id);
        void Add(Order entity);
        Task AddAsync(Order entity);
        void Update(Order entity);
        void DeleteById(int id);

    }

    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Order> GetAll()
        {
            return _dbContext.Set<Order>().ToList().Where(entity => entity.Status.Equals(true))
                                                             .OrderByDescending(entity => entity.CreateTimes);
        }

        public IEnumerable<Order> GetAllByOrderStatus(String orderStatus)
        {
            return _dbContext.Set<Order>().ToList().Where(entity => entity.Status.Equals(true) && entity.OrderStatus == orderStatus)
                                                             .OrderByDescending(entity => entity.CreateTimes);
        }

        public Order GetById(int id)
        {
            return _dbContext.Set<Order>().Where(od => od.Id == id && od.Status.Equals(true)).FirstOrDefault();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Order>().FindAsync(id);
        }

        public void Add(Order entity)
        {
            _dbContext.Set<Order>().Add(entity);
            _dbContext.SaveChanges();
        }

        public async Task AddAsync(Order entity)
        {
            await _dbContext.Set<Order>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public void Update(Order entity)
        {
            _dbContext.Set<Order>().Update(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var entityToDelete = _dbContext.Set<Order>().Find(id);
            if (entityToDelete != null)
            {
                _dbContext.Set<Order>().Remove(entityToDelete);
                _dbContext.SaveChanges();
            }
        }
    }
}
