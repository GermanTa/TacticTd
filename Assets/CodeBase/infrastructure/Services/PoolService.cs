using CodeBase.infrastructure.Services;
using System.Collections.Generic;

public class PoolService : IService
{
    private readonly Dictionary<int, Queue<ItemPool>> _items = new Dictionary<int, Queue<ItemPool>>();
}
