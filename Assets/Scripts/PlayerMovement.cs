using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed;
    [SerializeField] PlayerInput input;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float GroundDistance = 0.4f;
    [SerializeField] private LayerMask GroundMask;

    Vector2 movementInput;

    private void Update()
    {
        movementInput = input.MovementVector;

        Vector3 m = new Vector3(movementInput.x, 0, movementInput.y) * Time.deltaTime * speed;

        if (transform.parent == null)
        {
            if (!Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask))
            {
                m.y += gravity * Time.deltaTime;
            }
        }

        controller.Move(m);

        Vector3 r = new Vector3(movementInput.x, 0, movementInput.y) * 100f * Time.deltaTime;
        this.transform.LookAt(transform.position + r);
    }
}