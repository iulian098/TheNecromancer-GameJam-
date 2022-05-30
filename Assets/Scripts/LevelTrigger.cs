using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    [SerializeField] int sceneIndex = 1;
    [SerializeField] bool isLastLevel;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            Character player = other.GetComponent<Character>();
            if(player is PlayerController) {
                {
                    LoadingScreen.Instance.FadeIn(sceneIndex);
                    if (!isLastLevel)
                        PlayerPrefs.SetInt("LastLevel", sceneIndex);
                    else
                        PlayerPrefs.DeleteKey("LastLevel");
                    SceneManager.LoadScene(sceneIndex);
                }
            }
        }
    }
}
