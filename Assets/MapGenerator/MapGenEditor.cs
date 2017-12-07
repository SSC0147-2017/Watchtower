using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof(MapGenScript))]
public class MapGenEditor : Editor {

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MapGenScript myScript = (MapGenScript)target;
        if (GUILayout.Button("Build Object"))
        {
            myScript.PressButon();
        }
    }
}
