using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable {

    bool hasInteractedWith;
    public void Interact() {
        if (!hasInteractedWith) {
            hasInteractedWith = true;
            DialogSystem.DialogSystem.Instance.OnDialogEnd += NextLevel;

            DialogSystem.DialogSystem.Instance.ShowDialog("Dialog1");
        }
    }

    private void NextLevel() {
        PlayerPrefs.SetInt("LastLevel", 2);
        LoadingScreen.Instance.LoadLevel(2);
        PlayerPrefs.SetInt("SpecialUnlocked", 1);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && !hasInteractedWith) {
            UIContents.Instance.InteractText.gameObject.SetActive(true);
            GameManager.Instance.DetectedInteractables.Add(this);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            UIContents.Instance.InteractText.gameObject.SetActive(false);
        }
    }
}
