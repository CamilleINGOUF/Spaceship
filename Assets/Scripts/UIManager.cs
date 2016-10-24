using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    
    public void StartGame()
    {
        //Application.LoadLevel("Game");
        SceneManager.LoadScene("SpaceGame");
    }

    public void Quit()
    {
        Application.Quit();
    }
    	
}
