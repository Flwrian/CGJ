using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pauseMenu;
    [SerializeField]

    private GameObject canvaGame;//canva of game
    [SerializeField]

    private KeyCode pauseCode;
    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pauseCode = KeyCode.Escape;
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(pauseCode)){
            if(isPaused)
            {
                ResumeGame();
            }else
            {
                PauseGame();
            }
        }

    }


    public void PauseGame()
    {
        canvaGame.SetActive(false);
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        canvaGame.SetActive(true);

        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        canvaGame.SetActive(true);
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
