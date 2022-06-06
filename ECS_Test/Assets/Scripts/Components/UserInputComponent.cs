using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class UserInputComponent : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new InputData());
    }
}

public struct InputData:IComponentData
{
    public float2 MoveVector;
    public bool IsTeleport;
    public bool IsShoot;
}
