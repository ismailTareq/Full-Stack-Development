// Question 1
// Generics deh el hwa bdl ma hya specific type zay int aw string, hya bt5ly el class aw method y2bl ay type.
// why b2a 3lshan: No casting, reuse, Type safety.

// Question 2
class Container<T>
{
    private T item;

    public void Add(T value)
    {
        item = value;
    }

    public T Get()
    {
        return item;
    }
}
Container<int> box = new Container<int>();
box.Add(42);
Console.WriteLine(box.Get()); // 42

// Question 3
class Pair<TKey, TValue>
{
    public TKey Key   { get; set; }
    public TValue Value { get; set; }

    public Pair(TKey key, TValue value)
    {
        Key   = key;
        Value = value;
    }
}
Pair<int, string> p = new Pair<int, string>(1, "Ahmed");
Console.WriteLine($"{p.Key} - {p.Value}"); // 1 - Ahmed

// Question 4
static void Swap<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}
int x = 5, y = 10;
Swap(ref x, ref y);
Console.WriteLine($"{x} {y}"); // 10 5

// Question 5
static T FindMax<T>(T[] arr) where T : IComparable<T>
{
    T max = arr[0];
    foreach (T item in arr)
    {
        if (item.CompareTo(max) > 0)
            max = item;
    }
    return max;
}
int[] nums = { 3, 1, 4, 1, 5 };
Console.WriteLine(FindMax(nums)); // 5

// Question 6
interface IRepository<T>
{
    void Add(T item);
    T GetById(int id);
    void Delete(int id);
    IEnumerable<T> GetAll();
}

// Question 7
class ValueBox<T> where T : struct
{
    public T Value { get; set; }
}
ValueBox<int>    good  = new ValueBox<int>();    // fine
// ValueBox<string> w7sh = new ValueBox<string>(); // compile error, string is a class

// Question 8
class NullableWrapper<T> where T : class
{
    public T Value { get; set; } = null; // allowed because T is a reference type
}
NullableWrapper<string> ok  = new NullableWrapper<string>(); // fine
// NullableWrapper<int>    bad = new NullableWrapper<int>();    // compile error

// Question 9
class Factory<T> where T : new()
{
    public T CreateInstance()
    {
        return new T();
    }
}

// Question 10
class Printer<T> where T : IPrintable
{
    public void Print(T item)
    {
        item.Print();
    }
}

// Question 11
class AnimalShelter<T> where T : Animal
{
    public void Feed(T a)
    {
        a.Eat(); // Animal has this method, fa 2shta 3adi a call it
    }
}

// Question 12
class Repository<T> where T : class, IComparable<T>, new()
{
    public T CreateAndCompare(T other)
    {
        T fresh = new T();
        return fresh.CompareTo(other) > 0 ? fresh : other;
    }
}

// Question 13
T GetDefault<T>()
{
    return default(T); // works for any T
}

Console.WriteLine(GetDefault<int>());    // 0
Console.WriteLine(GetDefault<string>()); // nothing printed
Console.WriteLine(GetDefault<bool>());   // False

// Question 14
class SafeList<T>
{
    private List<T> items = new List<T>();

    public void Add(T item) => items.Add(item);

    public T Get(int index)
    {
        if (index < 0 || index >= items.Count)
            return default(T); // instead of crashing
        return items[index];
    }
}
SafeList<string> list = new SafeList<string>();
list.Add("hello");
Console.WriteLine(list.Get(0));  // hello
Console.WriteLine(list.Get(99)); // null bas mfish crash

// Question 15 , 16 , 17
// Covariance means you can use a more derived type than what was originally specified
// out = producer = returns stuff = covariant
interface IProducer<out T>
{
    T Produce();
}
class CatProducer : IProducer<Cat>
{
    public Cat Produce() => new Cat();
}
IProducer<Animal> producer = new CatProducer(); // works because of 'out'

// Contravariance means you can use a less derived type than what was originally specified
// in = consumer = takes stuff = contravariant
interface IConsumer<in T>
{
    void Consume(T item);
}
class AnimalConsumer : IConsumer<Animal>
{
    public void Consume(Animal a) => Console.WriteLine("Eating animal");
}
IConsumer<Cat> consumer = new AnimalConsumer(); // works because of 'in'

// Question 18
// kol wa7id 3ando el counter bta3o
class Counter<T>
{
    public static int Count = 0;
    public Counter() { Count++; }
}

new Counter<int>();
new Counter<int>();
new Counter<string>();

Console.WriteLine(Counter<int>.Count); // 2, because we created 2 Counter<int>
Console.WriteLine(Counter<string>.Count); // 1, because we created 1 Counter<string>
 
// Question 19
// fi 3 tor2 
// 1. class IntContainer : Container<int> {} // subclass specific to int
// 2. Container<int> intBox = new Container<int>(); // just use the generic

// Question 20
class CacheEntry<TValue>
{
    public TValue Value      { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsExpired => DateTime.Now > ExpiresAt;
}

class Cache<TKey, TValue>
{
    private Dictionary<TKey, CacheEntry<TValue>> store
        = new Dictionary<TKey, CacheEntry<TValue>>();

    public void Add(TKey key, TValue value, int secondsToLive = 60)
    {
        store[key] = new CacheEntry<TValue>
        {
            Value     = value,
            ExpiresAt = DateTime.Now.AddSeconds(secondsToLive)
        };
    }
    public TValue Get(TKey key)
    {
        if (!store.ContainsKey(key))
            return default(TValue);

        if (store[key].IsExpired)
        {
            store.Remove(key);
            return default(TValue);
        }

        return store[key].Value;
    }
    public void Remove(TKey key)
    {
        store.Remove(key);
    }
    public bool Contains(TKey key)
    {
        if (!store.ContainsKey(key))
            return false;
        if (store[key].IsExpired)
        {
            store.Remove(key);
            return false;
        }
        return true;
    }
}











