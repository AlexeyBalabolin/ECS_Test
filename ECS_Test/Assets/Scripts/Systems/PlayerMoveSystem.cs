using Unity.Entities;
using UnityEngine;

public class PlayerMoveSystem : ComponentSystem
{
    private EntityQuery _query; //Запрос для выбора Entity

    protected override void OnCreate()
    {
        //получаем все Entities, у которых есть нужный компонент
        //ReadOnly - только для чтения, без изменения и удаления компонента
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
        pos.y += playerMoveComponent.moveSpeed * Time.DeltaTime;
        transform.position = pos;
    }
}
