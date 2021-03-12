using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputAction WASD;
    private Vector2 movementVector;

    public Vector2 MovementVector { get => movementVector; }

    private void Awake()
    {
        movementVector = new Vector2();
    }

    private void OnEnable()
    {
        WASD.Enable();
    }
    private void OnDisable()
    {
        WASD.Disable();
    }

    private void Update()
    {
        movementVector = WASD.ReadValue<Vector2>();
    }
}
