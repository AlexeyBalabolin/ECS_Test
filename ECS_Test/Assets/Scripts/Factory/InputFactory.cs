using UnityEngine.InputSystem;

public static class InputFactory
{
    public static InputAction CreateMoveAction()
    {
        var _moveAction = new InputAction("move", binding: "<Gamepad>/leftStick");
        _moveAction.AddCompositeBinding("Dpad")
                .With(name: "Up", binding: "<Keyboard>/w")
                .With(name: "Down", binding: "<Keyboard>/s")
                .With(name: "Left", binding: "<Keyboard>/a")
                .With(name: "Right", binding: "<Keyboard>/d");
        return _moveAction;
    }

    public static InputAction CreateTeleportAction()
    {
        var _teleportAction = new InputAction("teleport", binding: "<Gamepad>/select");
        return _teleportAction;
    }

    public static InputAction CreateShootAction()
    {
        var _shootAction = new InputAction("shoot", binding: "<Gamepad>/rightTrigger");
        return _shootAction;
    }
}
