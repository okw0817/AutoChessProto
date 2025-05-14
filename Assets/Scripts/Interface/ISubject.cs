
public interface ISubject<T>
{
    public void AddObserver(T observer);
    public void DeleteObserver(T observer);
}
