using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System.IO;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshCombiner : MonoBehaviour
{
    [SerializeField] bool useSharedMesh;
    MeshFilter combinedMeshes;
    MeshRenderer meshRenderer;
    List<MeshFilter> meshList = new List<MeshFilter>();
    
    [ContextMenu("Generate")]
    public void GenerateMeshAsset()
    {
#if UNITY_EDITOR

        if(meshList.Count == 0)
        {
            MeshFilter[] mf = GetComponentsInChildren<MeshFilter>();
            foreach(MeshFilter m in mf)
            {
                if(m.gameObject != this.gameObject)
                {
                    meshList.Add(m);
                }
            }
        }

        if (combinedMeshes == null)
            combinedMeshes = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        Scene s = SceneManager.GetActiveScene();
        CombineInstance[] combineInstance = new CombineInstance[meshList.Count];

        for (int i = 0; i < meshList.Count; i++)
        {
            
            combineInstance[i].mesh = meshList[i].sharedMesh;
            combineInstance[i].transform = meshList[i].transform.localToWorldMatrix;
            meshList[i].gameObject.SetActive(false);
        }

        combinedMeshes.mesh = new Mesh();
        combinedMeshes.mesh.CombineMeshes(combineInstance,true, true);
        combinedMeshes.mesh.Optimize();
        combinedMeshes.mesh.UploadMeshData(false);
        transform.gameObject.SetActive(true);

        if (!Directory.Exists($"{Application.dataPath}/Scenes/{s.name}/")) {
            Directory.CreateDirectory($"{Application.dataPath}/Scenes/{s.name}/");
        }

        if (File.Exists($"{Application.dataPath}/Scenes/{s.name}/{gameObject.name}.asset")) {
            Debug.LogError($"[MeshCombiner] Mesh {gameObject.name} already exists in {Application.dataPath}/Scenes/{s.name} folder");
            return;
        }

        AssetDatabase.CreateAsset(combinedMeshes.mesh, $"Assets/Scenes/{s.name}/{gameObject.name}.asset");
        AssetDatabase.SaveAssets();
        meshRenderer.material = meshList[0].GetComponent<MeshRenderer>().sharedMaterial;
        combinedMeshes.mesh = AssetDatabase.LoadAssetAtPath<Mesh>($"Assets/Scenes/{s.name}/{gameObject.name}.asset");
        gameObject.transform.position = Vector3.zero;
        for(int i = 0; i < meshList.Count; i++)
        {
            DestroyImmediate(meshList[i].gameObject);
        }
        DestroyImmediate(this);
#endif
    }
}
