using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Spell : MonoBehaviour
{
    [SerializeField] Image border;
    [SerializeField] Sprite selectedSprite;
    [SerializeField] Sprite normalSprite;

    public void Hightlight(bool isSelected) {
        border.sprite = isSelected ? selectedSprite : normalSprite;
    }
}
