using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    public static bool gameAlive = true;

    public GameObject pauseDisplay;

    [HideInInspector]
    public static int score = 0;
    private Text scoreText;

    void Start()
    {
        pauseDisplay = GameObject.Find("PauseDisplay");
        pauseDisplay.SetActive(false);

        GameObject ship = (GameObject)Instantiate(player);
        ship.transform.position = new Vector3(0, 0, 0);    
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            Pause();
        }
        scoreText.text = "Score : " + score;
    }

    public void Pause()
    {
        gameAlive = !gameAlive;
        pauseDisplay.SetActive(!gameAlive);
    }

    public void InitGame()
    {
        SceneManager.LoadScene(0);
        gameAlive = true;
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("Menu");
        gameAlive = true;
        score = 0;
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
