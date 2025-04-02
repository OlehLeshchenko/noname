using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer;
    private float moveTimer;
    public override void Enter()
    {
        enemy.ResetTriggers();
        enemy.animator.SetTrigger("Search");
        enemy.Agent.SetDestination(enemy.LastKnowPos);
    }

    public override void Exit()
    {

    }

    public override void Perform()
    {
        if(enemy.CanSeePlayer())
            stateMachine.ChangeState(new AttackState());
        
        if(enemy.Agent.remainingDistance < enemy.Agent.stoppingDistance)
        {
            searchTimer += Time.deltaTime;
            moveTimer += Time.deltaTime;
            if (moveTimer > Random.Range(3,5))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere*10));
                moveTimer = 0f;
            }
            if (searchTimer > 10)
            {
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }
}
