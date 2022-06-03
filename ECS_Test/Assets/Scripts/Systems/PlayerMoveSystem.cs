using Unity.Entities;
using UnityEngine;

public class PlayerMoveSystem : ComponentSystem
{
    //query for Entity selection
    private EntityQuery _query;

    protected override void OnCreate()
    {
        //get all Entities with some Component
        //ReadOnly - without component's changes
        _query = GetEntityQuery(ComponentType.ReadOnly<PlayerMoveComponent>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_query).ForEach((Entity entity, Transform transform, PlayerMoveComponent playerMoveComponent) =>
        {
            MovePlayer(transform, playerMoveComponent);
        });
    }

    private void MovePlayer(Transform transform, PlayerMoveComponent playerMoveComponent)
    {
        var pos = transform.position;
        pos.y += playerMoveComponent.MoveSpeed * Time.DeltaTime;
        transform.position = pos;
    }
}
