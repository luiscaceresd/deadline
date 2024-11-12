using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectController : MonoBehaviour
{
    public Button backButton;

    void Start()
    {
        // Add listener for the Back button to call OnBackButtonClick
        backButton.onClick.AddListener(OnBackButtonClick);
    }

    void OnBackButtonClick()
    {
        
        SceneManager.LoadScene("MainMenu");
    }
}
