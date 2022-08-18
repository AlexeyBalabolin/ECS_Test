using System.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PlayerMoveSystem : ComponentSystem
{
    //query for Entity selection
    private EntityQuery _moveQuery;
    private bool _canTeleport;
    private float _teleportTimer;

    protected override void OnCreate()
    {
        //get all Entities with some Components
        //ReadOnly - without component's changes
        _moveQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(), 
            ComponentType.ReadOnly<MoveData>(),ComponentType.ReadOnly<Transform>());
        _canTeleport = true;
        _teleportTimer = 0;
    }

    protected override void OnUpdate()
    {
        Entities.With(_moveQuery).ForEach((Entity entity, CharacterController characterController,
            ref InputData inputData, ref MoveData moveData) =>
        {
            MovePlayer(characterController, inputData, moveData);
            TeleportPlayer(characterController.transform,ref inputData, moveData);
        });
    }

    private void MovePlayer(CharacterController characterController, InputData inputData, MoveData moveData)
    {
        var position = new Vector3(inputData.MoveVector.x, 0, inputData.MoveVector.y) * Time.DeltaTime * moveData.Speed;
        
        characterController.Move(position);
        if (inputData.MoveVector.x!=0 || inputData.MoveVector.y!=0)
            characterController.transform.forward = new Vector3(inputData.MoveVector.x, 0, inputData.MoveVector.y);
    }

    private void TeleportPlayer(Transform transform, ref InputData inputData, MoveData moveData)
    {
        if (inputData.IsTeleport)
        {
            if(_canTeleport)
            {
                UpdatePlayerPosition(transform, moveData);
                _canTeleport = false;
            }
            inputData.IsTeleport = false;
        }
        if(!_canTeleport)
        {
            RechargeTeleport(moveData);
        }
    }

    private void UpdatePlayerPosition(Transform transform, MoveData moveData)
    {
        var position = transform.position;
        position += transform.forward * moveData.TeleportDistance;
        transform.position = position;
    }

    private void RechargeTeleport(MoveData moveData)
    {
        _teleportTimer += Time.DeltaTime;
        if (_teleportTimer >= moveData.TeleportRechargeTime)
        {
            _canTeleport = true;
            _teleportTimer = 0;
        }
    }
}
