using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    private GameObject player;
    private Vector3 lastKnowPos;

    public NavMeshAgent Agent{get => agent;}
    public GameObject Player { get => player;}
    public Vector3 LastKnowPos {get => lastKnowPos; set => lastKnowPos = value;}
    public Animator animator;
    public Path path;
    public Material laserMaterial;
    public AudioManager AudioManager;

    [Header("Sight Values")]
    public float sightDistance = 20f;
    public float fieldOfView = 85f;
    public float eyeHeight;

    [Header("Weapon Values")]
    public Transform gunBarrel;
    [Range(0.1f,10)] public float fireRate;
    [SerializeField] private string currentState;

    void Start ()
    {
        animator = GetComponent<Animator>();
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update ()
    {
        currentState = stateMachine.activeState.ToString();
    }

    public bool CanSeePlayer ()
    {
        if (player != null) 
        {
            if(Vector3.Distance(transform.position, player.transform.position) < sightDistance)
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);
                if (angleToPlayer >= -fieldOfView && angleToPlayer <= fieldOfView)
                {
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();
                    if(Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if (hitInfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                            
                    }
                    
                }
                
            }
        }
        return false;
        
    }

    public void ResetTriggers()
    {
        animator.ResetTrigger("Attack");
        animator.ResetTrigger("AttackLost");
        animator.ResetTrigger("Normal");
        animator.ResetTrigger("Search");
    }
}
