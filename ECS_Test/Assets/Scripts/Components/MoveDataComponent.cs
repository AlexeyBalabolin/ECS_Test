using Unity.Entities;
using UnityEngine;

public class MoveDataComponent:MonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField] float _speed;
    [SerializeField] float _teleportDistance;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new MoveData(_speed, _teleportDistance));
    }
}

public struct MoveData:IComponentData
{
    public float Speed { get; set; }
    public float TeleportDistance { get; set; }
    public MoveData(float speed, float teleportDistance)
    {
        Speed = speed;
        TeleportDistance = teleportDistance;
    }
}