using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer: MonoBehaviour{
    public TextMeshProUGUI timer;
    public float lifeTime=300f;
    private float gameTime;

    public GameObject lossText;
    

    void Start()
    {
        if (lossText != null)
        {
            lossText.SetActive(false);
        }
    }
    
    private void Update()
    {
        int minutes = Mathf.FloorToInt(lifeTime / 60);
        int seconds = Mathf.FloorToInt(lifeTime % 60);
        timer.text = $"Time: {minutes:00}:{seconds:00}";
        if (lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
        }
        else{
            if (lossText != null){
                lossText.SetActive(true);
                Time.timeScale = 0;
            }
        }
        if(lifeTime<=150){
            timer.color=Color.yellow;
        }
        if(lifeTime<=50){
            timer.color=Color.red;
        }
}
}