using FriedChickenStore.Data;
using FriedChickenStore.Model.Entity;
using FriedChickenStore.Repository;
using Microsoft.EntityFrameworkCore;


public interface IUserRepository
{
    IEnumerable<User> GetAll();
    User GetById(int id);
    Task<User> GetByIdAsync(int id);
    void Add(User entity);
    Task AddAsync(User entity);
    void Update(User entity);
    void DeleteById(int id);
    bool IsUserNameExists(string userName);
    bool IsEmailExists(string email);
    User GetUserByUserNameAndPassword(string userName, string password);
}

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<User> GetAll()
    {
        return _dbContext.Set<User>().ToList().Where(entity => entity.Status.Equals(true))
                                                             .OrderByDescending(entity => entity.CreateTimes);
    }

    public User GetById(int id)
    {
        return _dbContext.Set<User>().Where(od => od.Id == id && od.Status.Equals(true)).FirstOrDefault();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _dbContext.Set<User>().FindAsync(id);
    }

    public void Add(User user)
    {
        _dbContext.Set<User>().Add(user);
        _dbContext.SaveChanges();
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.Set<User>().AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public void Update(User user)
    {
        _dbContext.Set<User>().Update(user);
        _dbContext.SaveChanges();
    }

    public void DeleteById(int id)
    {
        var userToDelete = _dbContext.Set<User>().Find(id);
        if (userToDelete != null)
        {
            _dbContext.Set<User>().Remove(userToDelete);
            _dbContext.SaveChanges();
        }
    }

    public bool IsUserNameExists(string userName)
    {
        return _dbContext.Set<User>().Any(user => user.UserName == userName);
    }

    public bool IsEmailExists(string email)
    {
        return _dbContext.Set<User>().Any(user => user.Email == email);
    }

    public User GetUserByUserNameAndPassword(string userName, string password)
    {
        return _dbContext.Users.FirstOrDefault(u => u.UserName == userName && u.Password == password);
    }

}
