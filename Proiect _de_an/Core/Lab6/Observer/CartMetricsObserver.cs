using System.Threading;

namespace Proiect__de_an.Core.Lab6.Observer;

public class CartMetricsObserver : ICartObserver
{
    private static long _cartEventCount;

    public static long CartEventCount => Interlocked.Read(ref _cartEventCount);

    public void Update(CartChangeEvent data)
    {
        Interlocked.Increment(ref _cartEventCount);
    }
}
