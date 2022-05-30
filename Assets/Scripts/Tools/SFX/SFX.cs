using UnityEngine;

namespace SFXPlayer {

    [System.Serializable]
    public class SFX {
        public string name;
        public AudioClip[] clip;
        public Vector2 pitchRange;
        public Vector2 volumeRange;
        public float maxDistance;
    }

}
