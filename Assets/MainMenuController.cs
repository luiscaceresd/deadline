using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuController : MonoBehaviour
{
    public Camera mainCamera;          
    public Button fightButton;         
    public Button exitButton;         
    public Image fadePanel;           
    public float zoomInDuration = 2f;  
    public float fadeDuration = 1f;    

    private bool isTransitioning = false; // To prevent multiple transitions at once

    void Start()
    {
        // Ensure that the fight button has an event listener to trigger the transition
        fightButton.onClick.AddListener(OnFightButtonClick);
        // Ensure the exit button has an event listener to close the game
        exitButton.onClick.AddListener(OnExitButtonClick);

        // Initially set the fade panel to be completely transparent
        fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, 0);
    }

    void OnFightButtonClick()
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionToCharacterSelect());
        }
    }

    void OnExitButtonClick()
    {
        
        Application.Quit();
    }

    IEnumerator TransitionToCharacterSelect()
    {
        isTransitioning = true;

        // Zoom In the Camera
        yield return StartCoroutine(ZoomInCamera());

        // Fade to Black
        yield return StartCoroutine(FadeToBlack());

        // Change Scene to Character Select
        SceneManager.LoadScene("CharacterSelect");

        
        isTransitioning = false;
    }

    IEnumerator ZoomInCamera()
    {
        Vector3 originalPosition = mainCamera.transform.position;
        Vector3 targetPosition = new Vector3(0, 0, -3); // Adjust this target position to how much zoom you want

        float timeElapsed = 0f;

        while (timeElapsed < zoomInDuration)
        {
            mainCamera.transform.position = Vector3.Lerp(originalPosition, targetPosition, timeElapsed / zoomInDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure the camera ends up at the target position
        mainCamera.transform.position = targetPosition;
    }

    IEnumerator FadeToBlack()
    {
        float timeElapsed = 0f;

        // Fade in (increase alpha from 0 to 1)
        while (timeElapsed < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, timeElapsed / fadeDuration);
            fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, alpha);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure fade is fully black
        fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, 1f);
    }
}
