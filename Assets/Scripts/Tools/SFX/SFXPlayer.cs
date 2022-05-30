using UnityEngine;
namespace SFXPlayer {
    public class SFXPlayer : MonoBehaviour {
        [SerializeField] AudioSource source;


        public void PlaySFX(string name) {
            SFXManager.PlaySFX(name, source);
        }
    }
}
