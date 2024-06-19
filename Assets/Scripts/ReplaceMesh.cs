using Sirenix.OdinInspector;
using UnityEngine;

public class ReplaceMesh : MonoBehaviour
{
    public GameObject prefab;

    [Button]
    public void Replace ()
    {
        // Get all children of the current game object
        Transform[] children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            children[i] = transform.GetChild(i);
        }

        // Remove all components from the children and replace with prefab
        foreach (Transform child in children)
        {
            // Remove all components from the child
            foreach (Component component in child.GetComponents<Component>())
            {
                DestroyImmediate(component);
            }

            // Instantiate the prefab as a child of the child
            GameObject newChild = Instantiate(prefab, child);
            newChild.transform.localPosition = Vector3.zero;
            newChild.transform.localRotation = Quaternion.identity;
            newChild.transform.localScale = Vector3.one;

            // Move newChild to the same level as its parent in the hierarchy
            newChild.transform.parent = child.parent;
        }

        // Remove the old children
        foreach (Transform child in children)
        {
            DestroyImmediate(child.gameObject);
        }
    }
}
