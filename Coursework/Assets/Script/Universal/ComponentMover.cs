using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class ComponentMover : EditorWindow
{
    public GameObject source;
    public GameObject destination;

    [MenuItem("Tools/Transfer Components")]
    public static void ShowWindow() => GetWindow<ComponentMover>("Transfer Components");

    void OnGUI()
    {
        source = (GameObject)
            EditorGUILayout.ObjectField("Source Object", source, typeof(GameObject), true);
        destination = (GameObject)
            EditorGUILayout.ObjectField("Target Character", destination, typeof(GameObject), true);

        if (GUILayout.Button("Transfer All Scripts"))
        {
            if (source == null || destination == null)
                return;

            foreach (var component in source.GetComponents<Component>())
            {
                // Skip the Transform component
                if (component is Transform)
                    continue;

                ComponentUtility.CopyComponent(component);
                ComponentUtility.PasteComponentAsNew(destination);
                Debug.Log($"Transferred: {component.GetType().Name}");
            }
        }
    }
}
