using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class ResetRotation : EditorWindow
{
    string selectionName;

    [MenuItem("Tools/Reset Rotation Window")]
    public static void ShowWindow() {
        EditorWindow.GetWindow<ResetRotation>();
    }

    private void OnGUI() {
        GUILayout.Label("Selected: " + selectionName);
        if(GUILayout.Button("Reset Rotation")) {
            Transform selectedTransform = Selection.activeTransform;

            Transform[] childs = selectedTransform.GetComponentsInChildren<Transform>();

            foreach (var c in childs) {
                c.rotation = Quaternion.identity;
            }
        }
    }

    private void OnSelectionChange() {
        selectionName = Selection.activeTransform.name;
    }
}
