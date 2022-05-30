using UnityEngine;

namespace SFXPlayer {
    public class SFXManager : MonoBehaviour {
        [SerializeField] SFXContainer container;
        static SFX[] SFX_List;

        private void Awake() {
            SFX_List = container.SFX_List;
        }

        public static void PlaySFX(string name, AudioSource source) {
            SFX sfx = GetSFX(name);
            if (sfx == null) return;
            source.PlayOneShot(sfx.clip[Random.Range(0, sfx.clip.Length)]);
        }

        public static void PlaySFX(string name, Transform location) {
            SFX sfx = GetSFX(name);
            if (sfx == null)
                return;
            GameObject soundObj = new GameObject();
            AudioSource audioS = soundObj.AddComponent<AudioSource>();
            AudioClip clip = sfx.clip[Random.Range(0, sfx.clip.Length)];

            soundObj.transform.position = location.position;

            audioS.maxDistance = sfx.maxDistance;
            audioS.spatialBlend = 1;
            audioS.volume = Random.Range(sfx.volumeRange.x, sfx.volumeRange.y);
            audioS.pitch = Random.Range(sfx.pitchRange.x, sfx.pitchRange.y);
            audioS.PlayOneShot(clip);

            Destroy(soundObj, clip.length);
        }

        static SFX GetSFX(string name) {
            for (int i = 0; i < SFX_List.Length; i++)
                if (SFX_List[i].name == name)
                    return SFX_List[i];

            Debug.LogError("No SFX found with name: " + name);
            return null;
        }
    }
}
