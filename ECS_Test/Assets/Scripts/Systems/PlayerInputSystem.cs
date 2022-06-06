using UnityEngine;
using Unity.Entities;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class PlayerInputSystem : ComponentSystem
{
    private EntityQuery _inputQuery;
    private InputAction _moveAction;
    private InputAction _teleportAction;
    private InputAction _shootAction;
    private float2 _moveVector;
    private bool _isTeleport;
    private bool _isShoot;

    protected override void OnCreate()
    {
        _inputQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>());
    }

    protected override void OnStartRunning()
    {
        _moveAction = InputFactory.CreateMoveAction();
        _moveAction.performed += context => _moveVector = context.ReadValue<Vector2>();
        _moveAction.started += context => _moveVector = context.ReadValue<Vector2>();
        _moveAction.canceled += context => _moveVector = context.ReadValue<Vector2>();
        _moveAction.Enable();

        _teleportAction = InputFactory.CreateTeleportAction();
        _teleportAction.started += context => _isTeleport = true;
        _teleportAction.canceled += context => _isTeleport = false;
        _teleportAction.Enable();

        _shootAction = InputFactory.CreateShootAction();
        _shootAction.started += context => _isShoot = true;
        _shootAction.canceled += context => _isShoot = false;
        _shootAction.Enable();
    }

    protected override void OnStopRunning()
    {
        _moveAction.Disable();
        _teleportAction.Disable();
        _shootAction.Disable();
    }

    protected override void OnUpdate()
    {
        Entities.With(_inputQuery).ForEach((Entity entity, ref InputData inputData) => 
        { 
            inputData.MoveVector = _moveVector;
            inputData.IsTeleport = _isTeleport;
            inputData.IsShoot = _isShoot;
        });
    }
}
