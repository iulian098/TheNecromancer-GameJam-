using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] GameObject tutorial;
    // Start is called before the first frame update
    private void Start() {
        if(PlayerPrefs.GetInt("Tutorial", 0) == 0) {
            tutorial.SetActive(true);
            PlayerPrefs.SetInt("Tutorial", 1);
        }
        else {
            tutorial.SetActive(false);
        }
    }
}
