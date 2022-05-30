using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen Instance;
    [SerializeField] Animator anim;
    [SerializeField] bool isOpen;
    private void Awake() {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else
            Instance = this;
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1) {
        StartCoroutine(FadeOut());
    }

    public void LoadLevel(int levelIndex) {
        StartCoroutine(FadeIn(levelIndex));
    }
    public void LoadLevel(string levelIndex) {
        StartCoroutine(FadeIn(levelIndex));
    }

    public IEnumerator FadeIn(int levelIndex) {
        anim.Play("FadeIn");
        isOpen = true;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(levelIndex);
    }

    public IEnumerator FadeIn(string levelIndex) {
        anim.Play("FadeIn");
        isOpen = true;
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(levelIndex);
    }

    public IEnumerator FadeOut() {
        yield return new WaitForSeconds(0.5f);
        if (isOpen) {
            anim.Play("FadeOut");
            isOpen = false;
        }
    }
}
