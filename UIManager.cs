using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Tooltip("Panel with the pause UI")]
    [SerializeField] 
    private GameObject pauseUI;

    [Tooltip("Panel with the death UI")]
    [SerializeField]
    private GameObject deathUI;

    [Tooltip("GameObject of the score text")]
    [SerializeField]
    private Text scoreTextObject;

    [Tooltip("GameObject of the health scrollbar")]
    [SerializeField]
    private Scrollbar healthBarObject;

    private bool isGamePaused = false;
    private string initialText = "";

    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreText(0);
        UpdateHealthBar(1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    //===================
    // Pause Menu Methods
    //===================

    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isGamePaused = false;
    }

    public void PauseGame()
    {
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
        isGamePaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    //===================
    // Dead Menu Methods
    //===================

    public void ShowDeathUI()
    {
        deathUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResetUI()
    {
        Debug.Log("Restarting UI...");

        UpdateScoreText(0);
        UpdateHealthBar(1f);

        deathUI.SetActive(false);
        Time.timeScale = 1f;
    }

    //===================
    // Score/Health UI Methods
    //===================
 
    public void UpdateScoreText(int score)
    {
        scoreTextObject.text = initialText + score.ToString();
    }

    public void UpdateHealthBar(float percentage)
    {
        healthBarObject.size = Mathf.Clamp(percentage, 0, 1);
    }
}
