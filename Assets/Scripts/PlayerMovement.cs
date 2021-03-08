using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private float speed;

    private void Update() 
    {
        float moveForward = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        float moveSide = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        controller.Move(new Vector3(this.transform.position.x + moveForward, this.transform.position.y, this.transform.position.z + moveSide));    
    }
}