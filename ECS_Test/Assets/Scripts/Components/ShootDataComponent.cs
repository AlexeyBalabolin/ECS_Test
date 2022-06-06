using Unity.Entities;
using UnityEngine;

public class ShootDataComponent : MonoBehaviour, IConvertGameObjectToEntity
{
    public MonoBehaviour ShootAction;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        if(ShootAction!=null && ShootAction is IAbility)
            dstManager.AddComponentData(entity, new ShootData());
    }
}
public struct ShootData : IComponentData
{
    
}
