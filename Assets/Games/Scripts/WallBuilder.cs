using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBuilder : MonoBehaviour
{
    [Header("HUI Helper")]
    public bool showHelper;

    [Header("Point")]
    public MeshPoint[] points;

    [Header("Properties")]
    public string assetName;
    public float height;
    [SerializeField] private Material invisibleWallMaterial;
    [SerializeField] private Transform invisibleWall;

    private MeshCollider col;
    private MeshRenderer meshRendererInvisibleWall;
    private MeshFilter meshFilterInvisibleWall;

    private void Construct()
    {
        ConstructMesh();
        ConstructCollider();
    }

    private void Init()
    {
        meshRendererInvisibleWall = invisibleWall.GetComponent<MeshRenderer>();
        meshFilterInvisibleWall = invisibleWall.GetComponent<MeshFilter>();
        col = gameObject.GetComponent<MeshCollider>();

        if (col == null) col = gameObject.AddComponent<MeshCollider>();
        if (meshRendererInvisibleWall == null) meshRendererInvisibleWall = invisibleWall.gameObject.AddComponent<MeshRenderer>();
        if (meshFilterInvisibleWall == null) meshFilterInvisibleWall = invisibleWall.gameObject.AddComponent<MeshFilter>();
    }

    private void ConstructMesh()
    {
        WallMeshConstructor meshData = new WallMeshConstructor();
        meshData.Construct(transform, points, height, invisibleWallMaterial, assetName);
        meshRendererInvisibleWall.material = invisibleWallMaterial;
        meshFilterInvisibleWall.mesh = meshData.Mesh;
    }

    private void ConstructCollider()
    {
        col.sharedMesh = meshFilterInvisibleWall.sharedMesh;
    }

    #region DebugMode
    [Button]
    private void BuildMesh()
    {
        Init();
        Construct();
    }

    [Button]
    private void DestoryMesh()
    {
        meshFilterInvisibleWall.mesh = null;
    }
    #endregion
}

public class WallMeshConstructor
{
    public const string PATH = "Assets/Games/Models/";
    private string asset_name;

    private Transform reference;
    private Mesh mesh;
    public Mesh Mesh { get { return mesh; } }

    public void Construct(Transform reference, MeshPoint[] points, float height, Material material, string asset_name = "Invisible-Wall")
    {
        this.asset_name = asset_name;
        this.reference = reference;
        Vector3[] vertices;
        int[] tris;
        Vector2[] uvs;

        GenerateVertices(points, height, out vertices, out bool verticesConstructed);
        if (!verticesConstructed) return;
        GenerateTris(vertices, out tris);
        GenerateUVCoordinate(points, height, vertices, material, out uvs);
        GenerateMesh(vertices, tris, uvs);
    }

    #region Internal Function
    private void GenerateVertices(MeshPoint[] points, float height, out Vector3[] vertices, out bool verticesConstructed)
    {
        int wallSection = points.Length;
        int verticesCount = PredictVertices(wallSection);
        vertices = new Vector3[verticesCount];
        verticesConstructed = false;

        if (wallSection < 2) return;

        for (int i = 0; i < wallSection; i++)
        {
            vertices[(i * 2)] = (points[i].point) + (Vector3.up * height);
            vertices[(i * 2) + 1] = (points[i].point);
        }
        verticesConstructed = true;
    }

    private void GenerateTris(Vector3[] vertices, out int[] tris)
    {
        int verticesCount = vertices.Length;

        tris = new int[(verticesCount - 2) * 3]; //close loop face maker
        int[] template = { 0, 1, 2 };

        for (int i = 0; i < tris.Length; i += 3)
        {
            if (i % 2 == 0)
            {
                tris[i] = template[0];
                tris[i + 1] = template[2];
                tris[i + 2] = template[1];
            }
            else
            {
                tris[i] = template[0];
                tris[i + 1] = template[1];
                tris[i + 2] = template[2];
            }

            template[0] = ShiftValue(template[0] + 1, 0, verticesCount - 1);
            template[1] = ShiftValue(template[1] + 1, 0, verticesCount - 1);
            template[2] = ShiftValue(template[2] + 1, 0, verticesCount - 1);
        }
    }

    private void GenerateUVCoordinate(MeshPoint[] points, float height, Vector3[] vertices, Material material, out Vector2[] uvs)
    {
        uvs = new Vector2[vertices.Length];

        var meshSize = PredictMeshSize(points, height);
        float currentWidth = 0f;
        int offset = 0;
        Vector3 lastPoint = points[0].point;

        for (int i = 0; i < points.Length; i++)
        {
            offset = i * 2;
            currentWidth += (lastPoint - points[i].point).magnitude;
            lastPoint = points[i].point;

            uvs[offset] = Vector2.zero;
            uvs[offset].x = currentWidth / (float) meshSize.width;
            if (offset % 2 == 0) uvs[offset].y = 1f;
            else uvs[offset].y = 0f;

            uvs[offset + 1] = Vector2.zero;
            uvs[offset + 1].x = currentWidth / (float) meshSize.width;
            if ((offset + 1) % 2 == 0) uvs[offset + 1].y = 1f;
            else uvs[offset + 1].y = 0f;
        }

        material.mainTextureScale = new Vector2(meshSize.width * 2, meshSize.height * 2);
    }

    private void GenerateMesh(Vector3[] vertices, int[] tris, Vector2[] uvs)
    {
        Mesh tempMesh = new Mesh();

        tempMesh.vertices = vertices;
        tempMesh.triangles = tris;
        tempMesh.uv = uvs;
        tempMesh.RecalculateNormals();
        tempMesh.RecalculateBounds();
        mesh = tempMesh;

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.CreateAsset(mesh, $"{PATH}{asset_name}.asset");
        UnityEditor.AssetDatabase.SaveAssets();
#endif
    }

    private int PredictVertices(int pointsLength)
    {
        return pointsLength * 2;
    }

    private int ShiftValue(int value, int min, int max)
    {
        if (value > max) value = min;
        else if (value < min) value = max;
        return value;
    }

    private (float width, float height) PredictMeshSize(MeshPoint[] points, float wallHeight)
    {
        float height = wallHeight;
        float width = 0f;

        Vector3 lastPoint = points[0].point;

        foreach (MeshPoint meshPoint in points)
        {
            width += (meshPoint.point - lastPoint).magnitude;
            lastPoint = meshPoint.point;
        }

        return (width, height);
    }
    #endregion
}

[System.Serializable]
public struct MeshPoint
{
    public Vector3 point;
}