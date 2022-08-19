using System;
using Unity.Entities;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    public event Action DieEvent;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new PlayerHealthData(_health));
    }

    private void OnEnable()
    {
        _health = _maxHealth;
        DieEvent += Deactivate;
    }

    private void OnDisable()
    {
        DieEvent -= Deactivate;
    }

    public void ChangeHealth(int health)
    {
        _health += health;
        if (_health <= 0)
        {
            health = 0;
            DieEvent?.Invoke();
        }
        if (_health > _maxHealth)
            _health = _maxHealth;
    }

    private void Deactivate() => gameObject.SetActive(false);
}
public struct PlayerHealthData : IComponentData
{
    public int MaxHealth;
    public int CurrentHealth;

    public PlayerHealthData(int health)
    {
        MaxHealth = health;
        CurrentHealth = health;
    }
}
