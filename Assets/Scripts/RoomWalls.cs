using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomWalls : MonoBehaviour
{
    [SerializeField] MeshRenderer[] meshRenderers;
    [SerializeField] Material targetMaterial;
    [SerializeField] Vector3 triggerOffset;
    [SerializeField] Vector3 triggerSize;
    [SerializeField] bool playerInRoom;
    [SerializeField] bool materialUpdated;

    [ContextMenu("Get Meshes")]
    public void GetMeshes() {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();
        meshRenderers = System.Array.FindAll(renderers, x => x.sharedMaterial.name.Contains(targetMaterial.name));
    }

    private void Start() {
        for (int i = 0; i < meshRenderers.Length; i++) {
            meshRenderers[i].material.SetFloat("_CutPos", 10);
        }
    }

    private void FixedUpdate() {
        Collider[] colls = Physics.OverlapBox(transform.position + triggerOffset, triggerSize, Quaternion.identity, GameManager.Instance.GameData.PlayerMask);
        
        if (colls.Length > 0 && !playerInRoom)
            playerInRoom = true;
        else if (colls.Length == 0 && playerInRoom)
            playerInRoom = false;

        if (playerInRoom && !materialUpdated) {
            for (int i = 0; i < meshRenderers.Length; i++)
                meshRenderers[i].material.SetFloat("_CutPos", 1);
            materialUpdated = true;
            
        }
        else if(!playerInRoom && materialUpdated) {
            for (int i = 0; i < meshRenderers.Length; i++) 
                meshRenderers[i].material.SetFloat("_CutPos", 10);
            materialUpdated = false;
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.green;
        Gizmos.DrawCube(transform.position + triggerOffset, triggerSize * 2);
    }
}
