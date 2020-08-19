using UnityEngine;
using UnityEngine.AI;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public Camera cam;

    public NavMeshAgent agent;

    public ThirdPersonCharacter character;

    public Animator ani;

    public GameObject player;
    void Start()
    {
        agent.updateRotation = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        agent.SetDestination(player.transform.position);
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
            ani.SetFloat("speed", 1);
        }
        else
        {
            ani.SetFloat("speed", 0);
            GameOver();
            character.Move(Vector3.zero, false, false);
        }


    }
    private void GameOver()
    {
        SceneManager.LoadScene(3);
    }

}