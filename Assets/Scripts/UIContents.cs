using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIContents : MonoBehaviour
{
    public static UIContents Instance;

    private void Awake() {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    [SerializeField] UI_Spell[] spells;
    [SerializeField] GameObject lockIcon;
    [SerializeField] GameObject interactText;
    [SerializeField] GameObject endScreen;

    UI_Spell selectedSpell;

    public UI_Spell[] Spells => spells;
    public GameObject InteractText => interactText;
    public GameObject EndScreen => endScreen;

    private void Start() {
        if (PlayerPrefs.GetInt("SpecialUnlocked") == 1) {
            lockIcon.SetActive(false);
            GameManager.Instance.SpellsManager.SpecialSpellCooldown.fillAmount = 0;
        }
    }

    public void SwitchSpell(int index) {
        selectedSpell?.Hightlight(false);
        selectedSpell = spells[index];
        selectedSpell.Hightlight(true);
    }

}
