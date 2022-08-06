using CodeBase.Minic;
using System;
using UnityEngine;

public class MinicDeath : MonoBehaviour
{
    public MinicHealth minicHealth;
    public event Action HappenedDeath;
    private ISpawnerService spawnerService;
    private MinicComponents minicComponents;
    void Start()
    {
        minicComponents = GetComponent<MinicComponents>();
        minicHealth.HealthChanged += HealthChanged;
       
    }

    private void HealthChanged()
    {
        
        if (minicComponents.minicHealth.CurrentHp <= 0)
        {
           
            Death();
        }
    }

    private void Death()
    {
        spawnerService.DeleteMinicFromList(minicComponents.id);
        minicComponents.minicHealth.HealthChanged -= HealthChanged;
        Destroy(gameObject);
        HappenedDeath?.Invoke();        
    }

    
}
