using UnityEngine;

public class BombManager : MonoBehaviour
{
    void Start()
    {
        ManageBombs();
    }

    void ManageBombs()
    {
        GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");

        if (bombs.Length > 0)
        {
            int randomIndex = Random.Range(0, bombs.Length);

            for (int i = 0; i < bombs.Length; i++)
            {
                bombs[i].SetActive(i == randomIndex);
            }
        }
    }
}
