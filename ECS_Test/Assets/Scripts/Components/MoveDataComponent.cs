using Unity.Entities;
using UnityEngine;

public class MoveDataComponent:MonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField] float _speed;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new MoveData(_speed));
    }
}

public struct MoveData:IComponentData
{
    public float Speed { get; set; }

    public MoveData(float speed)
    {
        Speed = speed;
    }
}