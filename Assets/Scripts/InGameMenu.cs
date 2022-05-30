using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] GameObject inGameMenu;
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Backspace)) {
            inGameMenu.SetActive(!inGameMenu.activeSelf);
        }
    }

    public void Exit() {
        Application.Quit();
    }

    public void BackToMainMenu() {
        LoadingScreen.Instance.LoadLevel("MainMenu");
    }
}
