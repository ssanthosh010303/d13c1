/*
 * Author: Sakthi Santhosh
 * Created on: 24/04/2024
 */
using Challenge1.Library.Models;
using Challenge1.Library.Repositories;

namespace Challenge1.Library.Services;

public interface IBaseService<T> where T : BaseModel
{
    T GetById(int id);
    IList<T> GetAll();

    T Add(T obj);
    void Update(T obj);
    void Delete(T obj);
}

public abstract class BaseService<T>(IBaseRepository<T> baseRepository) : IBaseService<T> where T : BaseModel
{
    protected readonly IBaseRepository<T> _entityRepository = baseRepository;

    public virtual T Add(T obj)
    {
        return _entityRepository.Add(obj);
    }

    public virtual void Delete(T obj)
    {
        _entityRepository.Delete(obj);
    }

    public virtual IList<T> GetAll()
    {
        return _entityRepository.GetAll();
    }

    public virtual T GetById(int id)
    {
        return _entityRepository.GetById(id);
    }

    public virtual void Update(T obj)
    {
        _entityRepository.Update(obj);
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index > _entityRepository.GetCount())
                throw new IndexOutOfRangeException();
            return _entityRepository[index];
        }
        set
        {
            if (index < 0 || index > _entityRepository.GetCount())
                throw new IndexOutOfRangeException();
            _entityRepository[index] = (T)value;
        }
    }

    public void FlashMessage(string message, string messageLevel = "info")
    {
        int boxLength = message.Length + 4;
        string boundary = new('-', boxLength);

        Console.WriteLine(boundary);
        Console.WriteLine($"| {messageLevel.ToUpper().PadRight(boxLength - messageLevel.Length)} |");
        Console.WriteLine(boundary);
        Console.WriteLine($"| {message} |");
        Console.WriteLine(boundary);
        Console.WriteLine();
    }
}
