using UnityEngine;

namespace SFXPlayer {
    [CreateAssetMenu(fileName = "SFX Container", menuName = "Scriptable Obejcts/Containers/SFX Container")]
    public class SFXContainer : ScriptableObject {
        [SerializeField] SFX[] SFX_list;
        public SFX[] SFX_List => SFX_list;
    }
}
