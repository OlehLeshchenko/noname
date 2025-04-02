using UnityEngine;

public class PatrolState : BaseState
{
    public int waypointIndex;
    public float waitTimer;
    public float waitTime = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemy.animator.ResetTrigger("Attack");
        enemy.animator.ResetTrigger("Search");
        enemy.animator.ResetTrigger("Normal");
        enemy.animator.SetTrigger("Normal");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Enter ()
    {
        enemy.ResetTriggers();
        enemy.animator.SetTrigger("Normal");
    }

    public override void Perform ()
    {
        PatrolCycle();
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }
    }

    public override void Exit ()
    {
        
    }

    public void PatrolCycle ()
    {
        if (enemy.Agent.remainingDistance < 0.2f)
        {
            waitTimer+= Time.deltaTime;

            if (waitTimer > waitTime) 
            {
                if (waypointIndex < enemy.path.waypoints.Count - 1)
                waypointIndex++;
                else
                    waypointIndex = 0;
                enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
                waitTimer = 0f;
            }
            

        }
    }
}
