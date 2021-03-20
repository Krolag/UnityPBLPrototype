// FOR THE TESTING STAGE
// SHIFT + 1/2 - TEST ATTACK ON PLAYER 1/2
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent NavMeshAgent;
    public GameObject PlayerOne, PlayerTwo;
    public float DistanceToEndChase = 1.5f, DistanceToBeginChase = 5f;
    
    private int currentlyChasedPlayer = 0; // 0 - none, 1 - one, 2 - two

    void Update()
    {
        // Start chasing ( for now only one player can be chased till the end of chase, decide if thats okay)
        if (currentlyChasedPlayer == 0)
        {
            // chase condition is only distance
            if(DistanceFromPlayer(1) < DistanceToBeginChase) StartChase(1);
            else if(DistanceFromPlayer(2) < DistanceToBeginChase) StartChase(2);
        }
        
        // End chase if chasing someone and reached target distance
        if (currentlyChasedPlayer != 0 && DistanceFromPlayer(currentlyChasedPlayer) < DistanceToEndChase)
        {
            EndChase();
        }
        
        // If not finished chasing then update chase position
        if (currentlyChasedPlayer != 0)
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
    }
    void ContinueChase()
    {
        if (currentlyChasedPlayer == 1) NavMeshAgent.SetDestination(PlayerOne.transform.position);
        else NavMeshAgent.SetDestination(PlayerTwo.transform.position);
    }
    
    void EndChase()
    {
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