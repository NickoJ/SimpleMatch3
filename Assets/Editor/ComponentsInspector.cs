using System;
using Klyukay.SimpleMatch3.Core.Components;
using Leopotam.Ecs;
using Leopotam.Ecs.UnityIntegration.Editor;
using UnityEditor;

public class ComponentsInspector
{
    
    class StoneComponentInspector : IEcsComponentInspector {
        Type IEcsComponentInspector.GetFieldType () {
            return typeof (Stone);
        }

        void IEcsComponentInspector.OnGUI (string label, object value, EcsWorld world, int entityId) {
            var component = value as Stone;
            EditorGUILayout.LabelField (label, EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.TextField ("EID", component?.Eid.ToString());
            EditorGUILayout.TextField ("Position", component?.Position.ToString());
            EditorGUILayout.TextField ("Color", component?.Color.ToString());
            EditorGUI.indentLevel--;
        }
    }
    
}
