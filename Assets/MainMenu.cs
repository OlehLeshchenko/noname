using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenu : MonoBehaviour
{
    public void PlayGame(){
        SceneManager.LoadScene("GameScene");
    }

    public void ExitGame(){
        Debug.Log("Exit game!");
        Application.Quit();
    }
}
