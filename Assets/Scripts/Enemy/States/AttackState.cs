using UnityEditor;
using UnityEngine;

public class AttackState : BaseState
{
    public float moveTimer;
    private float losePlayerTimer;
    public float loseTime = 2f;
    private float shotTimer;

    public override void Enter()
    {

    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
        enemy.ResetTriggers();
        if (enemy.CanSeePlayer()) 
        {
            enemy.animator.SetTrigger("Attack");
            losePlayerTimer = 0f;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            if (shotTimer > enemy.fireRate)
            {
                shotTimer = 0f;
                Shoot();
            }
            if (moveTimer > Random.Range(3,7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0f;
            }
        }
        else
        {
            enemy.animator.SetTrigger("AttackLost");
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > loseTime)
            {
                //search state
                stateMachine.ChangeState(new SearchState());
            }
        }
    }

    public void Shoot()
    {
        if (enemy.gunBarrel == null)
        {
            Debug.LogError("Gun Barrel is not assigned!");
            return;
        }

        float spreadAmount = 0.05f; 
        Vector3 spread = new Vector3(Random.Range(-spreadAmount, spreadAmount), Random.Range(-spreadAmount, spreadAmount), 0);

        float missChance = 0.2f; 
        if (Random.value < missChance)
        {
            spread *= 5; 
        }

        Vector3 targetDirection = (enemy.Player.transform.position - enemy.gunBarrel.position).normalized + spread;
        RaycastHit hit;

        if (Physics.Raycast(enemy.gunBarrel.position, targetDirection, out hit, 50f))
        {
            Debug.Log("Laser hit: " + hit.collider.name);

            if (hit.collider.CompareTag("Player"))
            {
                hit.collider.GetComponent<PlayerHealth>().TakeDamage(10);
            }

            CreateLaserEffect(enemy.gunBarrel.position, hit.point);
            enemy.LastKnowPos = enemy.Player.transform.position;
        }
        else
        {
            CreateLaserEffect(enemy.gunBarrel.position, enemy.gunBarrel.position + targetDirection * 50);
        }

        enemy.AudioManager.BeamSound();
        shotTimer = 0f;
    }

    private void CreateLaserEffect(Vector3 start, Vector3 end)
    {
        GameObject laser = new GameObject("Laser");
        LineRenderer lr = laser.AddComponent<LineRenderer>();

        lr.startWidth = 0.1f;
        lr.endWidth = 0.1f;
        lr.positionCount = 2;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);

        if (enemy.laserMaterial != null)
        {
            lr.material = enemy.laserMaterial; 
        }
        else
        {
            Debug.LogError("Laser material is missing! Assign it in the Inspector.");
        }

        UnityEngine.Object.Destroy(laser, 0.1f);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
