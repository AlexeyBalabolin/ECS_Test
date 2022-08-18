using Unity.Entities;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField] private int _health;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new PlayerHealthData(_health));
    }
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
