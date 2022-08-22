using System;

public interface IHealth
{
    public event Action HealthChanged;
    public event Action<string> DeathEvent;
    public int CurrentHp { get; set; }
    public int MaxHp { get; set; }
    void TakeDamage(int damage);
}
