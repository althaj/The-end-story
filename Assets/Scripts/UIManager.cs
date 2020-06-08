using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject inGame;
    public GameObject death;

    public Animator levelLoader;

    private Image levelLoaderImage;
    private Animator deathAnimator;

    public void Die()
    {
        death.SetActive(true);
        inGame.SetActive(false);

        deathAnimator.SetTrigger("Death");
    }

    public void Play()
    {
        SceneManager.LoadScene(2);
        mainMenu.SetActive(false);
        death.SetActive(false);
        inGame.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(1);
        mainMenu.SetActive(true);
        death.SetActive(false);
        inGame.SetActive(false);
    }

    public void GoToNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            StartCoroutine(LoadNextLevel(SceneManager.GetActiveScene().buildIndex + 1));
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        } else
        {
            StartCoroutine(LoadNextLevel(3));
            //SceneManager.LoadScene(3);
        }
    }

    private IEnumerator LoadNextLevel(int levelId)
    {
        levelLoader.SetTrigger("Load level");

        while(levelLoaderImage.color.a != 0)
        {
            yield return null;
        }

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(levelId);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        levelLoader.SetTrigger("Level loaded");
    }

    private void Start()
    {
        levelLoaderImage = levelLoader.GetComponent<Image>();
        deathAnimator = death.GetComponent<Animator>();

        DontDestroyOnLoad(gameObject);
        MainMenu();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.R))
        {
            if (SceneManager.GetActiveScene().buildIndex > 1)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                death.SetActive(false);
                inGame.SetActive(false);
            }
        }
    }
}
