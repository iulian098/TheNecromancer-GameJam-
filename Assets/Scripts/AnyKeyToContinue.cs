using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class AnyKeyToContinue : MonoBehaviour
{
    [SerializeField] int levelIndex;
    void Update()
    {
        if (Input.anyKeyDown) {
            PlayerPrefs.DeleteKey("LastLevel");
            PlayerPrefs.DeleteKey("SpecialUnlocked");
            SceneManager.LoadScene(levelIndex);
        }
    }
}
