using UnityEngine;
using Unity.Entities;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class PlayerInputSystem : ComponentSystem
{
    private EntityQuery _inputQuery;
    private InputAction _moveAction;
    private InputAction _teleportAction;
    private float2 _moveVector;
    private float _teleport;

    protected override void OnCreate()
    {
        _inputQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>());
    }

    protected override void OnStartRunning()
    {
        _moveAction = InputFactory.CreateTeleportAction();
        _moveAction.performed += context => _moveVector = context.ReadValue<Vector2>();
        _moveAction.started += context => _moveVector = context.ReadValue<Vector2>();
        _moveAction.canceled += context => _moveVector = context.ReadValue<Vector2>();
        _moveAction.Enable();

        _teleportAction = InputFactory.CreateTeleportAction();
        _teleportAction.started += context => _moveVector = context.ReadValue<float>();
        _teleportAction.canceled += context => _moveVector = 0;
        _teleportAction.Enable();
    }

    protected override void OnStopRunning()
    {
        _moveAction.Disable();
        _teleportAction.Disable();
    }

    protected override void OnUpdate()
    {
        Entities.With(_inputQuery).ForEach((Entity entity, ref InputData inputData) => inputData.MoveVector = _moveVector);
    }
}
