using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private PlayerInputActions playerInput;
    [SerializeField] private float speed;

    Vector2 movementInput;

    private void Awake()
    {
        playerInput = new PlayerInputActions();
    }

    #region Unity Events
    public void OnMove(InputAction.CallbackContext ctx) => movementInput = ctx.ReadValue<Vector2>();
    #endregion

    private void Update()
    {
        Vector3 m = new Vector3(movementInput.x, 0, movementInput.y) * Time.deltaTime * speed;
        controller.Move(m);

        Vector3 r = new Vector3(movementInput.x, 0, movementInput.y) * 100f * Time.deltaTime;
        this.transform.LookAt(transform.position + r);
    }

    private void OnEnable()
    {
        playerInput.Movement.Enable();
    }

    private void OnDisable()
    {
        playerInput.Movement.Disable();
    }
}