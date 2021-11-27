using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Floor))]
public class CreateFloor : Editor {
    
    public override void OnInspectorGUI() 
    {
        base.OnInspectorGUI();

        Floor floor = (Floor)target;

        if (GUILayout.Button("Generate Floor")) 
        {
            floor.GenerateFloor();
        }
        if (GUILayout.Button("Clear Floor"))
        {
            floor.ClearObjects();
        }

    }
}
