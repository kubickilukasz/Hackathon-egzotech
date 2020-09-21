using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MovableParams))]
public class MovableParamsEditor : Editor
{
    float sampleDiff;

    MovableParams movableParams;
    private void OnEnable() {
        movableParams = target as MovableParams;
    }

    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (GUILayout.Button("Randomize")) {
            movableParams.RandomizeParams();
        }
    }
}
