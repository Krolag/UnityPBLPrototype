using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed;
    [SerializeField] PlayerInput input;

    Vector2 movementInput;

    private void Update()
    {
        movementInput = input.MovementVector;

        Vector3 m = new Vector3(movementInput.x, 0, movementInput.y) * Time.deltaTime * speed;
        controller.Move(m);

        Vector3 r = new Vector3(movementInput.x, 0, movementInput.y) * 100f * Time.deltaTime;
        this.transform.LookAt(transform.position + r);
    }
}