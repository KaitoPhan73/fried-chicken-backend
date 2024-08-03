using FriedChickenStore.Data;
using FriedChickenStore.Model.Entity;

namespace FriedChickenStore.Repository

{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        Task<Product> GetByIdAsync(int id);
        void Add(Product entity);
        Task AddAsync(Product entity);
        void Update(Product entity);
        void DeleteById(int id);

    }

    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Product> GetAll()
        {
            return _dbContext.Set<Product>().ToList().Where(entity => entity.Status.Equals(true))
                                                             .OrderByDescending(entity => entity.CreateTimes);
        }

        public Product GetById(int id)
        {
            return _dbContext.Set<Product>().Where(od => od.Id == id && od.Status.Equals(true)).FirstOrDefault();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Product>().FindAsync(id);
        }

        public void Add(Product entity)
        {
            _dbContext.Set<Product>().Add(entity);
            _dbContext.SaveChanges();
        }

        public async Task AddAsync(Product entity)
        {
            await _dbContext.Set<Product>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public void Update(Product entity)
        {
            _dbContext.Set<Product>().Update(entity);
            _dbContext.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var entityToDelete = _dbContext.Set<Product>().Find(id);
            if (entityToDelete != null)
            {
                _dbContext.Set<Product>().Remove(entityToDelete);
                _dbContext.SaveChanges();
            }
        }
    }
}
