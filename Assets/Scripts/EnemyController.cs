using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{

    public Camera cam;
    public NavMeshAgent agent;

    public ThirdPersonCharacter character;

    public Animator ani;

    public GameObject player;
    public bool chasing;
    private Vector3 startingPosition;
    void Start()
    {
        agent.updateRotation = false;
        chasing = true;
        startingPosition = new Vector3(-65.388f, 1.662f, 19.911f);

    }
    public void SetChasing(bool value)
    {
        chasing = value;
    }

    // Update is called once per frame
    void Update()
    {
        if (chasing)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.SetDestination(startingPosition);
        }
        
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
            ani.SetFloat("speed", 1);
        }
        else
        {
            ani.SetFloat("speed", 0);
            character.Move(Vector3.zero, false, false);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("XR Rig"))
        {
            GameOver();
        }
    }
    private void GameOver()
    {
        SceneManager.LoadScene("EndScreenGameOver");
    }

}