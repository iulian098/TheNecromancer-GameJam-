using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellsManager : MonoBehaviour
{
    [SerializeField] UI_Spell[] spells;
    [SerializeField] Image specialSpellCooldown;

    UI_Spell selectedSpell;

    public Image SpecialSpellCooldown => specialSpellCooldown;
    private void Start() {
        SwitchSpell(0);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Z)) {
            SwitchSpell(0);
            Debug.Log("Switch to speel 1");
        }else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.X)) {
            SwitchSpell(1);
            Debug.Log("Switch to speel 2");
        }
    }

    public void SwitchSpell(int index) {
        selectedSpell?.Hightlight(false);
        selectedSpell = spells[index];
        selectedSpell.Hightlight(true);
        GameManager.Instance.Player.ChangeSpell(index);
    }
}
