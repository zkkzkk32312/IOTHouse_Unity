#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Rendering;
using System.IO;
using Sirenix.Utilities;

[RequireComponent(typeof(Renderer), typeof(MeshFilter))]
public class ExtractSubmeshes : MonoBehaviour
{
    public static Mesh ExtractSubmesh(Mesh mesh, int submesh)
    {
        Mesh newMesh = new Mesh();
        SubMeshDescriptor descriptor = mesh.GetSubMesh(submesh);

        // Extract vertices and triangles for the submesh
        int vertexCount = descriptor.vertexCount;
        int firstVertex = descriptor.firstVertex;

        // Check if vertices exceed 65536 and adjust index format accordingly
        if (vertexCount > 65536)
        {
            newMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        }
        else
        {
            newMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt16;
        }

        newMesh.vertices = ArrayUtilities.RangeSubset(mesh.vertices, firstVertex, vertexCount);

        // Extract triangles and adjust indices
        int[] triangles = ArrayUtilities.RangeSubset(mesh.triangles, descriptor.indexStart, descriptor.indexCount);
        for (int i = 0; i < triangles.Length; i++)
        {
            triangles[i] -= firstVertex; // Adjust indices to local submesh vertex indices
        }
        newMesh.triangles = triangles;

        // Assign normals if they exist and match vertex count
        if (mesh.normals != null && mesh.normals.Length == mesh.vertices.Length)
        {
            newMesh.normals = ArrayUtilities.RangeSubset(mesh.normals, firstVertex, vertexCount);
        }

        // Assign tangents if they exist and match vertex count
        if (mesh.tangents != null && mesh.tangents.Length == mesh.vertices.Length)
        {
            newMesh.tangents = ArrayUtilities.RangeSubset(mesh.tangents, firstVertex, vertexCount);
        }

        // Assign UVs if they exist and match vertex count
        if (mesh.uv != null && mesh.uv.Length == mesh.vertices.Length)
        {
            newMesh.uv = ArrayUtilities.RangeSubset(mesh.uv, firstVertex, vertexCount);
        }

        // Assign UV2 if they exist and match vertex count
        if (mesh.uv2 != null && mesh.uv2.Length == mesh.vertices.Length)
        {
            newMesh.uv2 = ArrayUtilities.RangeSubset(mesh.uv2, firstVertex, vertexCount);
        }

        // Assign UV3 if they exist and match vertex count
        if (mesh.uv3 != null && mesh.uv3.Length == mesh.vertices.Length)
        {
            newMesh.uv3 = ArrayUtilities.RangeSubset(mesh.uv3, firstVertex, vertexCount);
        }

        // Assign UV4 if they exist and match vertex count
        if (mesh.uv4 != null && mesh.uv4.Length == mesh.vertices.Length)
        {
            newMesh.uv4 = ArrayUtilities.RangeSubset(mesh.uv4, firstVertex, vertexCount);
        }

        // Assign colors if they exist and match vertex count
        if (mesh.colors != null && mesh.colors.Length == mesh.vertices.Length)
        {
            newMesh.colors = ArrayUtilities.RangeSubset(mesh.colors, firstVertex, vertexCount);
        }

        // Assign colors32 if they exist and match vertex count
        if (mesh.colors32 != null && mesh.colors32.Length == mesh.vertices.Length)
        {
            newMesh.colors32 = ArrayUtilities.RangeSubset(mesh.colors32, firstVertex, vertexCount);
        }

        // Optimize mesh and recalculate bounds
        newMesh.Optimize();
        newMesh.OptimizeIndexBuffers();
        newMesh.RecalculateBounds();
        newMesh.name = $"{mesh.name} Submesh {submesh}";

        return newMesh;
    }
    public static string LastFilePath = "";

    [Button("Extract Meshes")]
    void Create()
    {
        var meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null || meshFilter.sharedMesh == null)
        {
            Debug.LogWarning("No mesh exists on this gameObject");
            return;
        }

        if (meshFilter.sharedMesh.subMeshCount <= 1)
        {
            Debug.LogWarning("Mesh has <= 1 submesh components. No additional extraction required.");
            return;
        }

        // Ensure LastFilePath is set correctly
        if (string.IsNullOrEmpty(LastFilePath))
        {
            LastFilePath = "Assets/Models"; // Default to Assets folder if LastFilePath is empty
        }

        for (int i = 0; i < meshFilter.sharedMesh.subMeshCount; i++)
        {
            string filePath = EditorUtility.SaveFilePanelInProject("Save Procedural Mesh", $"ProceduralMesh_{i}", "asset", "");
            if (string.IsNullOrEmpty(filePath))
                continue;

            LastFilePath = Path.GetDirectoryName(filePath);

            Mesh mesh = ExtractSubmesh(meshFilter.sharedMesh, i);
            AssetDatabase.CreateAsset(mesh, filePath);
        }
    }
}
public static class ArrayUtilities
{
    public static T[] RangeSubset<T>(T[] array, int startIndex, int length)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array));
        if (startIndex < 0 || length < 0 || startIndex + length > array.Length)
            throw new ArgumentOutOfRangeException();

        // Determine the correct index format based on the size of the subset
        IndexFormat indexFormat = IndexFormat.UInt16;
        if (length > 65536)
            indexFormat = IndexFormat.UInt32;

        // Check if the array type is an integer type and adjust index format accordingly
        if (typeof(T) == typeof(int) || typeof(T) == typeof(uint) || typeof(T) == typeof(short) || typeof(T) == typeof(ushort))
        {
            indexFormat = length > 65536 ? IndexFormat.UInt32 : IndexFormat.UInt16;
        }

        T[] subset = new T[length];
        Array.Copy(array, startIndex, subset, 0, length);
        return subset;
    }
}
#else
using UnityEngine;
public class ExtractSubmeshes : MonoBehaviour
{
 
}
#endif