using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    [SerializeField] UniversalRenderPipelineAsset universalRenderPipeline;
    [SerializeField] GameObject continueButton;
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider masterVolume;
    [SerializeField] Slider sfxVolume;
    [SerializeField] Slider musicVolume;
    [SerializeField] Slider renderScaleSlider;
    [SerializeField] TMP_Dropdown msaaDropdown;
    void Start()
    {
        if (PlayerPrefs.HasKey("LastLevel") || PlayerPrefs.GetInt("LastLevel") > 1)
            continueButton.SetActive(true);
        else
            continueButton.SetActive(false);

        mixer.SetFloat("Master", PlayerPrefs.GetFloat("MasterVolume"));
        mixer.SetFloat("SFX", PlayerPrefs.GetFloat("SFXVolume"));
        mixer.SetFloat("Music", PlayerPrefs.GetFloat("MusicVolume"));

        float masterVol, sfxVol, musicVol;


        mixer.GetFloat("Master", out masterVol);
        mixer.GetFloat("SFX", out sfxVol);
        mixer.GetFloat("Music", out musicVol);

        masterVolume.value = masterVol;
        sfxVolume.value = sfxVol;
        musicVolume.value = musicVol;

        universalRenderPipeline.renderScale = PlayerPrefs.GetFloat("RenderScale", 1);
        renderScaleSlider.value = universalRenderPipeline.renderScale;

        universalRenderPipeline.msaaSampleCount = PlayerPrefs.GetInt("MSAA", 1);

    }

    public void NewGame() {
        LoadingScreen.Instance.LoadLevel(1);
        PlayerPrefs.DeleteKey("LastLevel");
        //SceneManager.LoadScene(1);
    }

    public void Continue() {
        int lastLevel = PlayerPrefs.GetInt("LastLevel");
        LoadingScreen.Instance.LoadLevel(lastLevel);
    }

    public void SetMasterVolume() {
        mixer.SetFloat("Master", masterVolume.value);
        PlayerPrefs.SetFloat("MasterVolume", masterVolume.value);
    }

    public void SetSFXVolume() {
        mixer.SetFloat("SFX", sfxVolume.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume.value);
    }

    public void SetMusicVolume() {
        mixer.SetFloat("Music", musicVolume.value);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume.value);
    }

    public void SetAntiAliasing() {
        int index = msaaDropdown.value;
        switch (index) {
            case 0:
                universalRenderPipeline.msaaSampleCount = 1;
                break;
            case 1:
                universalRenderPipeline.msaaSampleCount = 2;
                break;
            case 2:
                universalRenderPipeline.msaaSampleCount = 4;
                break;
            case 3:
                universalRenderPipeline.msaaSampleCount = 8;
                break;
            default:
                break;
        }

        PlayerPrefs.SetInt("MSAA", universalRenderPipeline.msaaSampleCount);
    }

    public void SetRenderScale() {
        universalRenderPipeline.renderScale = renderScaleSlider.value;
        PlayerPrefs.SetFloat("RenderScale", renderScaleSlider.value);
    }
}
