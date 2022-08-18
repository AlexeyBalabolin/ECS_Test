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
    private DefaultInputActions _inputActions;

    protected override void OnCreate()
    {
        _inputQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>());
        _inputActions = new DefaultInputActions();
    }

    protected override void OnStartRunning()
    {
        _moveAction = _inputActions.Player.Move;
        _moveAction.performed += context => _moveVector = context.ReadValue<Vector2>();
        _moveAction.started += context => _moveVector = context.ReadValue<Vector2>();
        _moveAction.canceled += context => _moveVector = context.ReadValue<Vector2>();
        _moveAction.Enable();

        _teleportAction = _inputActions.Player.Teleport;
        _teleportAction.started += context => Teleport();
        _teleportAction.Enable();

        _shootAction = _inputActions.Player.Fire;
        _shootAction.started += context => Shoot();
        _shootAction.Enable();
    }

    protected override void OnStopRunning()
    {
        _moveAction.Disable();
        _teleportAction.Disable();
        _shootAction.Disable();
    }

    public void Teleport()
    {
        Entities.With(_inputQuery).ForEach((Entity entity, ref InputData inputData) =>
        {
            inputData.IsTeleport = true;
        });
    }

    public void Shoot()
    {
        Entities.With(_inputQuery).ForEach((Entity entity, ref InputData inputData) =>
        {
            inputData.IsShoot = true;
        });
    }

    protected override void OnUpdate()
    {
        Entities.With(_inputQuery).ForEach((Entity entity, ref InputData inputData) => 
        { 
            inputData.MoveVector = _moveVector;
        });
    }
}
