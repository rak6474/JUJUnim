using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quad : MonoBehaviour {

    public Vector3 boxSize;

    [Space(12)]
    public Vector2 uvStart;
    //public Vector2 uvSizeFront;
    //public Vector2 uvSizeTop;
    //public Vector2 uvSizeRight;

    [System.Serializable]
    public struct UVSize
    {
        public float uvSizeFrontX;
        public float uvSizeFrontY;
        public float uvSizeTopX;
        public float uvSizeTopY;
        public float uvSizeRightX;
        public float uvSizeRightY;
    };

    public UVSize uvSize;

    public enum Type
    {
        Haed,
        Body,
        RArm,
        LArm,
        RLeg,
        LLeg
    }
    public Type type;

	void Start () {

        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh m = new Mesh();

        //Vector3[] vertices = new Vector3[]
        //{
        //    //Front
        //    new Vector3(-5.0f, -5.0f,-5f),
        //    new Vector3(-5.0f, 5.0f,-5f),
        //    new Vector3(5.0f, 5.0f,-5f),
        //    new Vector3(5.0f, -5.0f,-5f),

        //    //Top
        //    new Vector3(-5.0f, 5.0f,-5f),
        //    new Vector3(-5.0f, 5.0f,5.0f),
        //    new Vector3(5.0f, 5.0f,5.0f),
        //    new Vector3(5.0f, 5.0f,-5f),

        //    //Left
        //    new Vector3(-5.0f, -5.0f,5.0f),
        //    new Vector3(-5.0f, 5.0f,5.0f),
        //    new Vector3(-5.0f, 5.0f,-5f),
        //    new Vector3(-5.0f, -5.0f,-5f),

        //    //Right
        //    new Vector3(5.0f, -5.0f,-5f),
        //    new Vector3(5.0f, 5.0f,-5f),
        //    new Vector3(5.0f, 5.0f,5.0f),
        //    new Vector3(5.0f, -5.0f,5.0f),

        //    //Back
        //    new Vector3(5.0f, -5.0f,5.0f),
        //    new Vector3(5.0f, 5.0f,5.0f),
        //    new Vector3(-5.0f, 5.0f,5.0f),
        //    new Vector3(-5.0f, -5.0f,5.0f),

        //    //Bottom
        //    new Vector3(-5.0f, -5.0f,5f),
        //    new Vector3(-5.0f, -5.0f,-5.0f),
        //    new Vector3(5.0f, -5.0f,-5.0f),
        //    new Vector3(5.0f, -5.0f,5f),
        //};

        ////primitive _ 랜더링쪽에서 삼각형을 지칭함
        //int[] triangles = new int[]
        //{
        //    //Front
        //    0, 1, 2, 0, 2, 3, 

        //    //Top
        //    4, 5, 6, 4, 6, 7,

        //    //Left
        //    8, 9, 10, 8, 10, 11,

        //    //Right
        //    12, 13, 14, 12, 14, 15,

        //    //Back
        //    16, 17, 18, 16, 18, 19,

        //    //Bottom
        //    20, 21, 22, 20, 22, 23
        //};

        //Vector2[] uvs = new Vector2[]
        //{
        //    //Front
        //    new Vector2(0.125f,0.75f),
        //    new Vector2(0.125f,0.875f),
        //    new Vector2(0.25f,0.875f),
        //    new Vector2(0.25f,0.75f),

        //    //Top
        //    new Vector2(0.125f,0.875f),
        //    new Vector2(0.125f,1f),
        //    new Vector2(0.25f,1f),
        //    new Vector2(0.25f,0.875f),

        //    //Left
        //    new Vector2(0.375f,0.75f),
        //    new Vector2(0.375f,0.875f),
        //    new Vector2(0.25f,0.875f),
        //    new Vector2(0.25f,0.75f),

        //    //Right
        //    new Vector2(0.125f,0.75f),
        //    new Vector2(0.125f,0.875f),
        //    new Vector2(0.0f,0.875f),
        //    new Vector2(0.0f,0.75f),

        //    //Back
        //    new Vector2(0.375f,0.75f),
        //    new Vector2(0.375f,0.875f),
        //    new Vector2(0.5f,0.875f),
        //    new Vector2(0.5f,0.75f),

        //    //Bottom
        //    new Vector2(0.25f,0.875f),
        //    new Vector2(0.25f,1f),
        //    new Vector2(0.375f,1f),
        //    new Vector2(0.375f,0.875f),
        //};

        //m.vertices = vertices;
        //m.triangles = triangles;
        //m.uv = uvs;
        //m.RecalculateNormals();

        //foreach (Vector3 n in m.normals)
        //{
        //    Debug.Log(n);
        //}

        m.vertices = GetVertices(boxSize);
        m.triangles = GetTriangles(m.vertices);
        m.uv = GetUVs(uvStart, uvSize);
        m.RecalculateNormals();

        mf.mesh = m;

        Vector3 test1 = new Vector3(0.0f, uvStart.y / height, 0.0f);

        Vector3 test2 = new Vector3(0.0f, (float)(uvStart.y / height), 0.0f);

        Debug.Log(test1.y + " , " + test2.y + " > " + uvStart.y / height);

	}


    Vector3[] GetVertices(Vector3 size)
    {
        List<Vector3> vertices = new List<Vector3>();

        Vector3[] Front = new Vector3[]
        {
            new Vector3(-size.x, -size.y,-size.z),
            new Vector3(-size.x, size.y,-size.z),
            new Vector3(size.x, size.y,-size.z),
            new Vector3(size.x, -size.y,-size.z)
        };

        Vector3[] Top = new Vector3[]
        {
            new Vector3(-size.x, size.y, -size.z),
            new Vector3(-size.x, size.y, size.z),
            new Vector3(size.x, size.y, size.z),
            new Vector3(size.x, size.y, -size.z)
        };

        Vector3[] Left = new Vector3[]
        {
            new Vector3(size.x, -size.y, -size.z),
            new Vector3(size.x, size.y, -size.z),
            new Vector3(size.x, size.y, size.z),
            new Vector3(size.x, -size.y, size.z)
        };

        Vector3[] Right = new Vector3[]
        {
            new Vector3(-size.x, -size.y, size.z),
            new Vector3(-size.x, size.y, size.z),
            new Vector3(-size.x, size.y, -size.z),
            new Vector3(-size.x, -size.y, -size.z)
        };

        Vector3[] Back = new Vector3[]
        {
            new Vector3(size.x, -size.y, size.z),
            new Vector3(size.x, size.y, size.z),
            new Vector3(-size.x, size.y, size.z),
            new Vector3(-size.x, -size.y, size.z)
        };

        Vector3[] Bottom = new Vector3[]
        {
            new Vector3(-size.x, -size.y, size.z),
            new Vector3(-size.x, -size.y, -size.z),
            new Vector3(size.x, -size.y, -size.z),
            new Vector3(size.x, -size.y, size.z)
        };

        vertices.AddRange(Front);
        vertices.AddRange(Top);
        vertices.AddRange(Left);
        vertices.AddRange(Right);
        vertices.AddRange(Back);
        vertices.AddRange(Bottom);

        return vertices.ToArray();
    }

    
    int[] GetTriangles(Vector3[] _vertices)
    {
        List<int> triangles = new List<int>();

        for(int i =0; i < _vertices.Length; i+=4 )
        {
            int[] Triangles = new int[]
            {
                i, i+1, i+2,
                i, i+2, i+3 
            };
            triangles.AddRange(Triangles);

            //int value = 0;
            //foreach (int n in Triangles)
            //{                
            //    Debug.Log(value + "  ::  " + n);
            //    value++;
            //}
        }

        return triangles.ToArray();
    }

    float height = 64;
    float width = 64;

    Vector2[] GetUVs(Vector2 _startPixel, UVSize _uvSize)
    {
        List<Vector2> uvs = new List<Vector2>();
        float startPixelX = _startPixel.x / width;
        float startPixelY = _startPixel.y / height;

        _uvSize.uvSizeFrontX = _uvSize.uvSizeFrontX / width;
        _uvSize.uvSizeFrontY = _uvSize.uvSizeFrontY / height;
        _uvSize.uvSizeTopX = _uvSize.uvSizeTopX / width;
        _uvSize.uvSizeTopY = _uvSize.uvSizeTopY / height;
        _uvSize.uvSizeRightX = _uvSize.uvSizeRightX / width;
        _uvSize.uvSizeRightY = _uvSize.uvSizeRightY / height;


        Debug.Log(startPixelX + "  " + startPixelY);

        Vector2[] Front = new Vector2[]
        {
            new Vector2(startPixelX + _uvSize.uvSizeRightX, 
                        startPixelY - _uvSize.uvSizeFrontY),

            new Vector2(startPixelX + _uvSize.uvSizeRightX, 
                        startPixelY),

            new Vector2(startPixelX + _uvSize.uvSizeRightX + _uvSize.uvSizeFrontX, 
                        startPixelY),

            new Vector2(startPixelX + _uvSize.uvSizeRightX + _uvSize.uvSizeFrontX, 
                        startPixelY - _uvSize.uvSizeFrontY)
        };

        Vector2[] Top = new Vector2[]
        {
            new Vector2(startPixelX + _uvSize.uvSizeRightX, 
                        startPixelY),

            new Vector2(startPixelX + _uvSize.uvSizeRightX, 
                        startPixelY + _uvSize.uvSizeTopY),

            new Vector2(startPixelX + _uvSize.uvSizeRightX + _uvSize.uvSizeTopX, 
                        startPixelY + _uvSize.uvSizeTopY),

            new Vector2(startPixelX + _uvSize.uvSizeRightX + _uvSize.uvSizeTopX, 
                        startPixelY )
        };
        
        Vector2[] Right = new Vector2[]
        {
            new Vector2(startPixelX, startPixelY - _uvSize.uvSizeRightY),
            new Vector2(startPixelX, startPixelY),
            new Vector2(startPixelX + _uvSize.uvSizeRightX, startPixelY),
            new Vector2(startPixelX + _uvSize.uvSizeRightX, startPixelY - _uvSize.uvSizeRightY)
        };

        Vector2[] Left;
        Vector2[] Back;

        if (type == Type.Body)
        {
            Left = new Vector2[]
            {
                new Vector2(startPixelX + _uvSize.uvSizeRightX + _uvSize.uvSizeFrontX *2 , startPixelY - _uvSize.uvSizeRightY),
                new Vector2(startPixelX + _uvSize.uvSizeRightX + _uvSize.uvSizeFrontX *2, startPixelY),
                new Vector2(startPixelX + _uvSize.uvSizeRightX*2 + _uvSize.uvSizeFrontX *2, startPixelY),
                new Vector2(startPixelX + _uvSize.uvSizeRightX*2 + _uvSize.uvSizeFrontX *2, startPixelY - _uvSize.uvSizeRightY)
            };

            Back = new Vector2[]
            {
                new Vector2(startPixelX + _uvSize.uvSizeRightX + _uvSize.uvSizeFrontX,
                            startPixelY - _uvSize.uvSizeFrontY),

                new Vector2(startPixelX + _uvSize.uvSizeRightX + _uvSize.uvSizeFrontX,
                            startPixelY),

                new Vector2(startPixelX + _uvSize.uvSizeRightX + _uvSize.uvSizeFrontX*2,
                            startPixelY),

                new Vector2(startPixelX + _uvSize.uvSizeRightX + _uvSize.uvSizeFrontX*2,
                            startPixelY - _uvSize.uvSizeFrontY)
            };
        }
        else
        {
            Left = new Vector2[]
            {
                new Vector2(startPixelX + _uvSize.uvSizeRightX + _uvSize.uvSizeFrontX , startPixelY - _uvSize.uvSizeRightY),
                new Vector2(startPixelX + _uvSize.uvSizeRightX + _uvSize.uvSizeFrontX, startPixelY),
                new Vector2(startPixelX + _uvSize.uvSizeRightX*2 + _uvSize.uvSizeFrontX, startPixelY),
                new Vector2(startPixelX + _uvSize.uvSizeRightX*2 + _uvSize.uvSizeFrontX, startPixelY - _uvSize.uvSizeRightY)
            };

            Back = new Vector2[]
            {
                new Vector2(startPixelX + _uvSize.uvSizeRightX*2 + _uvSize.uvSizeFrontX,
                            startPixelY - _uvSize.uvSizeFrontY),

                new Vector2(startPixelX + _uvSize.uvSizeRightX*2 + _uvSize.uvSizeFrontX,
                            startPixelY),

                new Vector2(startPixelX + _uvSize.uvSizeRightX*2 + _uvSize.uvSizeFrontX*2,
                            startPixelY),

                new Vector2(startPixelX + _uvSize.uvSizeRightX*2 + _uvSize.uvSizeFrontX*2,
                            startPixelY - _uvSize.uvSizeFrontY)
            };
        }


        Vector2[] Bottom = new Vector2[]
        {
            new Vector2(startPixelX + _uvSize.uvSizeRightX + _uvSize.uvSizeTopX, 
                        startPixelY),

            new Vector2(startPixelX + _uvSize.uvSizeRightX+ _uvSize.uvSizeTopX, 
                        startPixelY + _uvSize.uvSizeTopY),

            new Vector2(startPixelX + _uvSize.uvSizeRightX+ _uvSize.uvSizeTopX * 2, 
                        startPixelY + _uvSize.uvSizeTopY),

            new Vector2(startPixelX + _uvSize.uvSizeRightX+ _uvSize.uvSizeTopX * 2,
                        startPixelY)
        };

        uvs.AddRange(Front);
        uvs.AddRange(Top);
        uvs.AddRange(Left);
        uvs.AddRange(Right);
        uvs.AddRange(Back);
        uvs.AddRange(Bottom);

        return uvs.ToArray();
    }
}

