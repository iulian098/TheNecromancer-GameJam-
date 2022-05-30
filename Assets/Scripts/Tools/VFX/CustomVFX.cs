using UnityEngine;

public class CustomVFX : MonoBehaviour
{
    [SerializeField] VFX[] VFX_List;


    public void PlayVFX(string name) {
        VFX vfx = GetVFX(name);
        if (!vfx.vfxParticleSystem.gameObject.activeSelf)
            vfx.vfxParticleSystem.gameObject.SetActive(true);

        if (vfx.vfxParticleSystem != null)
            vfx.vfxParticleSystem.Play(true);
        else
            vfx.vfxVisualEffect.Play();
    }

    public void StopVFX(string name) {
        VFX vfx = GetVFX(name);

        if (vfx.vfxParticleSystem != null)
            vfx.vfxParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        else
            vfx.vfxVisualEffect.Stop();
    }

    public void StopAll() {
        for (int i = 0; i < VFX_List.Length; i++)
            if (VFX_List[i].vfxParticleSystem.isPlaying)
                VFX_List[i].vfxParticleSystem.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    VFX GetVFX(string name) {
        for (int i = 0; i < VFX_List.Length; i++) {
            if (VFX_List[i].name == name)
                return VFX_List[i];
        }

        Debug.LogError("No VFX found with name: " + name);
        return null;
    }
}
