using Unity.Entities;
using UnityEngine;

public class BulletFlyComponent : MonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField] private float _flySpeed;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new BulletFlyData(_flySpeed));
    }
}
public struct BulletFlyData : IComponentData
{
    public float FlySpeed;
    public BulletFlyData(float flySpeed)
    {
        FlySpeed = flySpeed;
    }
}
