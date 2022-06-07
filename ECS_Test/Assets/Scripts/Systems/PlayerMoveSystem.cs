using System.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMoveSystem : ComponentSystem
{
    //query for Entity selection
    private EntityQuery _moveQuery;
    private bool _isTeleport = true;
    private float _teleportTimer = 0;

    protected override void OnCreate()
    {
        //get all Entities with some Components
        //ReadOnly - without component's changes
        _moveQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(), 
            ComponentType.ReadOnly<MoveData>(),ComponentType.ReadOnly<Transform>());
    }

    protected override void OnUpdate()
    {
        Entities.With(_moveQuery).ForEach((Entity entity, Transform transform,
            ref InputData inputData, ref MoveData moveData) =>
        {
            MovePlayer(transform, inputData, moveData);
            TeleportPlayer(transform, inputData, moveData);
        });
    }

    private void MovePlayer(Transform transform, InputData inputData, MoveData moveData)
    {
        var position = transform.position;
        position += new Vector3(inputData.MoveVector.x, 0, inputData.MoveVector.y) * Time.DeltaTime * moveData.Speed;
        transform.position = position;
        if (inputData.MoveVector.x!=0 && inputData.MoveVector.y!=0)
            transform.forward = new Vector3(inputData.MoveVector.x, 0, inputData.MoveVector.y);
    }

    private void TeleportPlayer(Transform transform, InputData inputData, MoveData moveData)
    {
        if(inputData.IsTeleport)
        {
            if (_isTeleport)
            {
                var position = transform.position;
                position += transform.forward * moveData.TeleportDistance;
                transform.position = position;
                _isTeleport = false;               
            }
        }
        if(!_isTeleport)
        {
            _teleportTimer += Time.DeltaTime;
            if (_teleportTimer >= moveData.TeleportRechargeTime)
            {
                _isTeleport = true;
                _teleportTimer = 0;
            }
        }
    }

    private IEnumerator RechargeTeleport(float time)
    {
        yield return new WaitForSeconds(time);
        _isTeleport = true;
    }
}
