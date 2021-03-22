using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Object = System.Object;

public class PlayerCombat : MonoBehaviour
{
    public Animator LeftPunchAnimator, RightPunchAnimator;
    public float PunchingTime;
    public float HealthLoss;

    private bool isPunching = false;
    private int lastPunch = 1; // 1 - left, 2 - right
    
    private float health = 100f;

    void Update()
    {
        if (this.GetComponent<PlayerInteract>().GetRowButton().triggered)
        {
            if (lastPunch == 1 && !isPunching) StartCoroutine(PunchRight());
            if (lastPunch == 2 && !isPunching) StartCoroutine(PunchLeft());
        }

        if (health <= 0)
        {
            Debug.Log("PLAYER DED");
        }
    }

    IEnumerator PunchLeft()
    {
        isPunching = true;
        lastPunch = 1;
        LeftPunchAnimator.Play("LeftPunch", -1, -0);
        
        yield return new WaitForSeconds(PunchingTime);

        isPunching = false;
    }

    IEnumerator PunchRight()
    {
        isPunching = true;
        lastPunch = 2;
        RightPunchAnimator.Play("RightPunch", -1, -0);
        
        yield return new WaitForSeconds(PunchingTime);

        isPunching = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("EnemyFists"))
        {
            health -= HealthLoss;
        }
    }
}
