using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private InputAction movementInput;
    private Vector2 movementVector;

    public Vector2 MovementVector { get => movementVector; }

    private void Awake()
    {
        movementVector = new Vector2();
    }

    private void OnEnable()
    {
        movementInput.Enable();
    }
    private void OnDisable()
    {
        movementInput.Disable();
    }

    private void Update()
    {
        movementVector = movementInput.ReadValue<Vector2>();
    }
}
