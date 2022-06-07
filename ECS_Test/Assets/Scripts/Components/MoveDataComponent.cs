using System;
using System.Collections;
using Unity.Entities;
using UnityEngine;

public class MoveDataComponent:MonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField] float _speed;
    [SerializeField] float _teleportDistance;
    [SerializeField] float _teleportRechargeTime;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        dstManager.AddComponentData(entity, new MoveData(_speed, _teleportDistance, _teleportRechargeTime));
    }
}

public struct MoveData:IComponentData
{
    public float Speed { get; set; }
    public float TeleportDistance { get; set; }

    public float TeleportRechargeTime { get; set; }
    public MoveData(float speed, float teleportDistance, float teleportRechargeTime)
    {
        Speed = speed;
        TeleportDistance = teleportDistance;
        TeleportRechargeTime = teleportRechargeTime;
    }
}