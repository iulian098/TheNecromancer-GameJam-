using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    [SerializeField] GameData gameData;
    [SerializeField] Image playerHealthBar;
    [SerializeField] Transform enemyHealthBarParent;
    [SerializeField] PlayerController player;
    [SerializeField] SpellsManager spellsManager;
    [SerializeField] CinemachineVirtualCamera cinemachineCam;
    List<IInteractable> detectedInteractables = new List<IInteractable>();

    public GameData GameData => gameData;
    public Image PlayerHealthBar => playerHealthBar;
    public Transform EnemyHealthBarParent => enemyHealthBarParent;
    public PlayerController Player => player;
    public SpellsManager SpellsManager => spellsManager;

    public List<IInteractable> DetectedInteractables { get => detectedInteractables; set => detectedInteractables = value; }

    private void Awake() {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;

    }

    private void Start() {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        cinemachineCam.Follow = player.transform;
        cinemachineCam.LookAt = player.transform;
        Debug.Log($"{cinemachineCam.name} following {cinemachineCam.Follow ? .name : 'null'}");
        Debug.Log("[GameManager] Initialized");
        PlayerPrefs.SetInt("LastLevel", UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }

    private void Update() {
        //Debug.Log($"{cinemachineCam.name} following {cinemachineCam.Follow ? .name : 'null'}");
        if (Input.GetKeyDown(KeyCode.E)) {
            if (detectedInteractables.Count > 0) {
                detectedInteractables[0].Interact();
                detectedInteractables.Remove(detectedInteractables[0]);
                UIContents.Instance.InteractText.SetActive(false);
            }
        }
    }

    public void ShowEndScreen() {
        //Debug.Log("[GameManager] Show End Screen");
        UIContents.Instance.EndScreen.SetActive(true);
    }
}
