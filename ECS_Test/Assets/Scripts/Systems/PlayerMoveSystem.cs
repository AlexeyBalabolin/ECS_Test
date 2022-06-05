using Unity.Entities;
using UnityEngine;

public class PlayerMoveSystem : ComponentSystem
{
    //query for Entity selection
    private EntityQuery _moveQuery;

    protected override void OnCreate()
    {
        //get all Entities with some Components
        //ReadOnly - without component's changes
        _moveQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(), 
            ComponentType.ReadOnly<MoveData>(),ComponentType.ReadOnly<Transform>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_moveQuery).ForEach((Entity entity, Transform transform, ref InputData inputData, ref MoveData moveData) =>
        {
            moveData = MovePlayer(transform, inputData, moveData);
        });
    }

    private MoveData MovePlayer(Transform transform, InputData inputData, MoveData moveData)
    {
        var position = transform.position;
        position += new Vector3(inputData.MoveVector.x, 0, inputData.MoveVector.y) * Time.DeltaTime * moveData.Speed;
        transform.position = position;
        return moveData;
    }
}
