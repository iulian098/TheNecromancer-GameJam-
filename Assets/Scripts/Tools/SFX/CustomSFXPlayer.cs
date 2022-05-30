using UnityEngine;

namespace SFXPlayer {
    public class CustomSFXPlayer : MonoBehaviour {
        [SerializeField] AudioSource defaultSource;
        [SerializeField] CustomSFX[] SFX_List;

        public void PlaySFX(string name) {
            CustomSFX sfx = GetSFX(name);
            if (sfx.clip.Length == 0) {
                Debug.LogError($"SFX {sfx.name} has no clip assigned");
                return;
            }
            if (sfx.source != null) {
                sfx.source.volume = Random.Range(sfx.volumeRange.x, sfx.volumeRange.y);
                sfx.source.pitch = Random.Range(sfx.pitchRange.x, sfx.pitchRange.y);
                sfx.source.PlayOneShot(sfx.clip[Random.Range(0, sfx.clip.Length)]);
            }
            else
                defaultSource.PlayOneShot(sfx.clip[Random.Range(0, sfx.clip.Length)]);
        }

        CustomSFX GetSFX(string name) {
            for (int i = 0; i < SFX_List.Length; i++)
                if (SFX_List[i].name == name)
                    return SFX_List[i];

            Debug.LogError("No SFX found with name: " + name, gameObject);
            return null;
        }
    }
}
