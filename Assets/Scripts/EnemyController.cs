// FOR THE TESTING STAGE
// SHIFT + 1/2 - TEST ATTACK ON PLAYER 1/2
// C + 1/2 - TEST CHASE ON PLAYER 1/2

using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent NavMeshAgent;
    public GameObject PlayerOne, PlayerTwo;
    
    private float distanceToEndChase = 1.5f;
    private int currentlyChasedPlayer = 0; // 0 - none, 1 - one, 2 - two

    void Update()
    {
        // Chasing ( for now only one player can be chased till the end of chase, decide if thats okay)
        if (currentlyChasedPlayer == 0) // Start chase condition?
        {
            if(Keyboard.current.digit1Key.isPressed && Keyboard.current.cKey.isPressed) StartChase(1);
            else if(Keyboard.current.digit2Key.isPressed && Keyboard.current.cKey.isPressed) StartChase(2);
        }
        if (currentlyChasedPlayer != 0 && DistanceFromPlayer(currentlyChasedPlayer) < distanceToEndChase) // End chase condition?
        {
            EndChase();
        }
        if (currentlyChasedPlayer != 0) // if not finished chasing then update chase position
        {
            ContinueChase();
        }
        
        // Attacking
        // Attack conditions?
        if (Keyboard.current.shiftKey.isPressed && Keyboard.current.digit1Key.isPressed) Attack(PlayerOne);
        if (Keyboard.current.shiftKey.isPressed && Keyboard.current.digit2Key.isPressed) Attack(PlayerTwo);
    }
    
    // Chase methods (names are self explanatory i think)
    void StartChase(int player)
    {
        currentlyChasedPlayer = player;
        Debug.Log("STARTED CHASING PLAYER " + currentlyChasedPlayer);
    }
    void ContinueChase()
    {
        if (currentlyChasedPlayer == 1) NavMeshAgent.SetDestination(PlayerOne.transform.position);
        else NavMeshAgent.SetDestination(PlayerTwo.transform.position);
        Debug.Log("STILL CHASING PLAYER " + currentlyChasedPlayer);
    }
    
    void EndChase()
    {
        Debug.Log("FINISHED CHASING PLAYER " + currentlyChasedPlayer);
        currentlyChasedPlayer = 0;
    }

    // Calculating distance from enemy to chased player
    float DistanceFromPlayer(int player)
    {
        Vector3 playerPos;
        if (player == 1) playerPos = PlayerOne.transform.position;
        else playerPos = PlayerTwo.transform.position;
        Vector3 enemyPos = NavMeshAgent.transform.position;
        return Mathf.Sqrt(
            Mathf.Pow(enemyPos.x - playerPos.x, 2) +
            Mathf.Pow(enemyPos.z - playerPos.z, 2)
        );
    }
    
    // Attack method
    void Attack(GameObject player)
    {
        Debug.Log("Attacking " + player.name);
    }
}