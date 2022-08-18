using Unity.Entities;
using UnityEngine;

public class BulletFlySystem : ComponentSystem
{
    private EntityQuery _bulletQuery;

    protected override void OnCreate()
    {
        base.OnCreate();
        _bulletQuery = GetEntityQuery(ComponentType.ReadOnly<BulletFlyData>(),ComponentType.ReadOnly<Transform>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_bulletQuery).ForEach((Entity entity, Transform transform,
            ref BulletFlyData bulletFlyData) =>
        {
            BulletFly(transform, bulletFlyData);
        });
    }

    private void BulletFly(Transform transform, BulletFlyData bulletFlyData)
    {
        var position = transform.position;
        position += transform.forward * bulletFlyData.FlySpeed * Time.DeltaTime;
        transform.position = position;
    }
}
