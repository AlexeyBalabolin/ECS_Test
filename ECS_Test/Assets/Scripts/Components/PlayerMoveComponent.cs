using UnityEngine;

public class PlayerMoveComponent : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    public float MoveSpeed { get => moveSpeed; }
}
