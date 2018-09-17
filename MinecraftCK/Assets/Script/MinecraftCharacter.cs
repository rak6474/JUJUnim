using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinecraftCharacter : MonoBehaviour
{
    void GenerateMinecraftCharacter()
    {
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh m = new Mesh();

        Vector3[] vertices;
        Vector2[] uvs;
        Vector3 HeadSize = new Vector3(5, 5, 5);
        // Head
        GenerateCube(HeadSize, new Vector2(0f, 0.75f), new Vector3(0.125f, 0.125f, 0.125f), out vertices, out uvs);

        // Body 

        // Left Arm 

        // Right Arm

        // Left Leg

        // Right Leg


        m.vertices = vertices;
        m.uv = uvs;
        m.RecalculateNormals();
        m.triangles = GetTriangles(m.vertices);
        mf.mesh = m;
    }

    void GenerateCube(Vector3 size, Vector2 uvOrigin, Vector3 uvSize, out Vector3[] vertices, out Vector2[] uvs)
    {
        vertices = GetQuadVertices(size);
        uvs = GetQuadUVs(uvOrigin, uvSize);
    }

    Vector3[] GetQuadVertices(Vector3 size)
    {
        List<Vector3> vertices = new List<Vector3>();

        Vector3[] front = new Vector3[]
        {
            new Vector3(-size.x, -size.y, -size.z),
            new Vector3(-size.x, size.y, -size.z),
            new Vector3(size.x, size.y, -size.z),
            new Vector3(size.x, -size.y, -size.z)
        };

        Vector3[] top = new Vector3[]
        {
            new Vector3(-size.x, size.y, -size.z),
            new Vector3(-size.x, size.y, size.z),
            new Vector3(size.x, size.y, size.z),
            new Vector3(size.x, size.y, -size.z)
        };

        Vector3[] back = new Vector3[]
        {
            new Vector3(size.x, -size.y, size.z),
            new Vector3(size.x, size.y, size.z),
            new Vector3(-size.x, size.y, size.z),
            new Vector3(-size.x, -size.y, size.z)
        };

        Vector3[] left = new Vector3[]
        {
            new Vector3(size.x, -size.y, -size.z),
            new Vector3(size.x, size.y, -size.z),
            new Vector3(size.x, size.y, size.z),
            new Vector3(size.x, -size.y, size.z)
        };

        Vector3[] right = new Vector3[]
        {
            new Vector3(-size.x, -size.y, size.z),
            new Vector3(-size.x, size.y, size.z),
            new Vector3(-size.x, size.y, -size.z),
            new Vector3(-size.x, -size.y, -size.z)
        };

        Vector3[] bottom = new Vector3[]
        {
            new Vector3(size.x, -size.y, -size.z),
            new Vector3(size.x, -size.y, size.z),
            new Vector3(-size.x, -size.y, size.z),
            new Vector3(-size.x, -size.y, -size.z)
        };

        vertices.AddRange(front);
        vertices.AddRange(top);
        vertices.AddRange(back);
        vertices.AddRange(left);
        vertices.AddRange(right);
        vertices.AddRange(bottom);

        return vertices.ToArray();
    }

    int[] GetQuadTriangleArray(int startIndex)
    {
        return new int[]
        {
            startIndex,
            startIndex + 1,
            startIndex + 2,
            startIndex,
            startIndex + 2,
            startIndex + 3
        };
    }

    int[] GetTriangles(Vector3[] vertices)
    {
        List<int> triangles = new List<int>();

        for (int i = 0; i < vertices.Length / 4; i++)
        {
            int[] quadTriangles = GetQuadTriangleArray(i * 4);
            triangles.AddRange(quadTriangles);
        }

        return triangles.ToArray();
    }

    Vector2[] GetQuadUVs(Vector2 origin, Vector3 uvSize)
    {
        List<Vector2> uvs = new List<Vector2>();

        Vector2[] front = new Vector2[]
        {
            new Vector2(origin.x + uvSize.z, origin.y),
            new Vector2(origin.x + uvSize.z, origin.y + uvSize.y),
            new Vector2(origin.x + uvSize.z + uvSize.x, origin.y + uvSize.y),
            new Vector2(origin.x + uvSize.z + uvSize.x, origin.y)
        };

        Vector2[] top = new Vector2[]
        {
            new Vector2(origin.x + uvSize.z, origin.y + uvSize.y),
            new Vector2(origin.x + uvSize.z, origin.y + uvSize.y + uvSize.z),
            new Vector2(origin.x + uvSize.z + uvSize.x, origin.y + uvSize.y + uvSize.z),
            new Vector2(origin.x + uvSize.z + uvSize.x, origin.y + uvSize.y)
        };

        Vector2[] back = new Vector2[]
        {
            new Vector2(origin.x + uvSize.x + uvSize.z * 2, origin.y),
            new Vector2(origin.x + uvSize.x + uvSize.z * 2, origin.y + uvSize.y),
            new Vector2(origin.x + uvSize.x * 2 + uvSize.z * 2, origin.y + uvSize.y),
            new Vector2(origin.x + uvSize.x * 2 + uvSize.z * 2, origin.y),
        };

        Vector2[] left = new Vector2[]
        {
            new Vector2(origin.x + uvSize.x + uvSize.z, origin.y),
            new Vector2(origin.x + uvSize.x + uvSize.z, origin.y + uvSize.y),
            new Vector2(origin.x + uvSize.x + uvSize.z * 2, origin.y + uvSize.y),
            new Vector2(origin.x + uvSize.x + uvSize.z * 2, origin.y)
        };

        Vector2[] right = new Vector2[]
        {
            new Vector2(origin.x , origin.y),
            new Vector2(origin.x , origin.y + uvSize.y),
            new Vector2(origin.x + uvSize.z , origin.y + uvSize.y),
            new Vector2(origin.x + uvSize.z , origin.y),
        };

        Vector2[] bottom = new Vector2[]
        {
            new Vector2(origin.x + uvSize.z + uvSize.x * 2, origin.y + uvSize.y),
            new Vector2(origin.x + uvSize.z + uvSize.x * 2, origin.y + uvSize.y + uvSize.z),
            new Vector2(origin.x + uvSize.z + uvSize.x, origin.y + uvSize.y + uvSize.z),
            new Vector2(origin.x + uvSize.z + uvSize.x, origin.y + uvSize.y)
        };

        uvs.AddRange(front);
        uvs.AddRange(top);
        uvs.AddRange(back);
        uvs.AddRange(left);
        uvs.AddRange(right);
        uvs.AddRange(bottom);

        return uvs.ToArray();
    }

    // Use this for initialization
    void Start()
    {
        GenerateMinecraftCharacter();

    }

}
