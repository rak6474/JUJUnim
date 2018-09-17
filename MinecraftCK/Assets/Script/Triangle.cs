using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Triangle : MonoBehaviour {

    public Transform[] bones;
    SkinnedMeshRenderer smr;

    public Vector2 [] uvStart;

    [System.Serializable]
    public struct UVSize
    {
        public float uvSizeFrontX;
        public float uvSizeFrontY;
    };

    public UVSize [] uvSize;

    void Start () {
        Mesh m = new Mesh();

        List<Vector3> vertices = new List<Vector3>();

        vertices.AddRange(MakeTriangle(1, 0, 0));//Head

        vertices.AddRange(MakeTriangle(2, 0, -3));//Spine

        vertices.AddRange(MakeTriangle(0.5f, 0, -5)); //Pelvis

        vertices.AddRange(MakeTriangle(1, 1, -5));//LReg
        vertices.AddRange(MakeTriangle(1, -1, -5));//RReg
        vertices.AddRange(MakeTriangle(0.5f, 1, -6));//LKnee
        vertices.AddRange(MakeTriangle(0.5f, -1, -6));//RKnee

        vertices.AddRange(MakeTriangle(1, 3, -2));//LShoulder
        vertices.AddRange(MakeTriangle(1, -3, -2));//RShoulder
        vertices.AddRange(MakeTriangle(0.5f, 3, -3.5f));//LElbow
        vertices.AddRange(MakeTriangle(0.5f, -3, -3.5f));//RElbow

        m.vertices = vertices.ToArray();
        m.triangles = GetTriangles(m.vertices);

        m.bindposes = new Matrix4x4[]
        {
            bones[0].worldToLocalMatrix * transform.localToWorldMatrix,
            bones[1].worldToLocalMatrix * transform.localToWorldMatrix,
            bones[2].worldToLocalMatrix * transform.localToWorldMatrix,
            bones[3].worldToLocalMatrix * transform.localToWorldMatrix,
            bones[4].worldToLocalMatrix * transform.localToWorldMatrix,
            bones[5].worldToLocalMatrix * transform.localToWorldMatrix,
            bones[6].worldToLocalMatrix * transform.localToWorldMatrix,
            bones[7].worldToLocalMatrix * transform.localToWorldMatrix,
            bones[8].worldToLocalMatrix * transform.localToWorldMatrix,
            bones[9].worldToLocalMatrix * transform.localToWorldMatrix,
            bones[10].worldToLocalMatrix * transform.localToWorldMatrix
        };

        m.boneWeights = new BoneWeight[]
        {
            new BoneWeight(){boneIndex0 = 6, weight0 =1},
            new BoneWeight(){boneIndex0 = 6, weight0 =1},
            new BoneWeight(){boneIndex0 = 6, weight0 =1},   //Neck

            new BoneWeight(){boneIndex0 = 5, weight0 =1},
            new BoneWeight(){boneIndex0 = 5, weight0 =1},
            new BoneWeight(){boneIndex0 = 5, weight0 =1},   //Spine

            new BoneWeight(){boneIndex0 = 0, weight0 =1},
            new BoneWeight(){boneIndex0 = 0, weight0 =1},
            new BoneWeight(){boneIndex0 = 0, weight0 =1},   //Pelvis

            new BoneWeight(){boneIndex0 = 1, weight0 =1},
            new BoneWeight(){boneIndex0 = 1, weight0 =1},
            new BoneWeight(){boneIndex0 = 1, weight0 =1},   //LReg

            new BoneWeight(){boneIndex0 = 3, weight0 =1},
            new BoneWeight(){boneIndex0 = 3, weight0 =1},
            new BoneWeight(){boneIndex0 = 3, weight0 =1},   //RReg
            
            new BoneWeight(){boneIndex0 = 2, weight0 =1},
            new BoneWeight(){boneIndex0 = 2, weight0 =1},
            new BoneWeight(){boneIndex0 = 2, weight0 =1},   //LKnee
            
            new BoneWeight(){boneIndex0 = 4, weight0 =1},
            new BoneWeight(){boneIndex0 = 4, weight0 =1},
            new BoneWeight(){boneIndex0 = 4, weight0 =1},   //RKnee
            
            new BoneWeight(){boneIndex0 = 7, weight0 =1},
            new BoneWeight(){boneIndex0 = 7, weight0 =1},
            new BoneWeight(){boneIndex0 = 7, weight0 =1},   //LShoulder

            new BoneWeight(){boneIndex0 = 9, weight0 =1},
            new BoneWeight(){boneIndex0 = 9, weight0 =1},
            new BoneWeight(){boneIndex0 = 9, weight0 =1},   //RShoulder

            new BoneWeight(){boneIndex0 = 8, weight0 =1},
            new BoneWeight(){boneIndex0 = 8, weight0 =1},
            new BoneWeight(){boneIndex0 = 8, weight0 =1},   //LElbow

            new BoneWeight(){boneIndex0 = 10, weight0 =1},
            new BoneWeight(){boneIndex0 = 10, weight0 =1},
            new BoneWeight(){boneIndex0 = 10, weight0 =1},   //RElbow

        };

        List<Vector2> uv = new List<Vector2>();

        for (int i = 0; i < uvSize.Length; i++)
        {
            uv.AddRange(GetUVs(uvStart[i], uvSize[i]));
        }

        m.uv = uv.ToArray();

        smr = GetComponent<SkinnedMeshRenderer>();
        smr.sharedMesh = m;
        smr.bones = bones;
        smr.quality = SkinQuality.Bone1;
    }

    Vector3[] MakeTriangle(float size, float center_x, float center_y)
    {
        List<Vector3> vertices = new List<Vector3>();

        Vector3 []triangle = new Vector3[]
        {
            new Vector3(center_x,center_y -size,0),
            new Vector3(center_x - size,center_y + size,0),
            new Vector3(center_x + size,center_y + size,0)
        };

        vertices.AddRange(triangle);

        return vertices.ToArray();

    }

    int[] GetTriangles(Vector3[] _vertices)
    {
        List<int> triangles = new List<int>();

        for (int i = 0; i < _vertices.Length; i += 3)
        {
            int[] Triangles = new int[]
            {
                i, i+1, i+2,
            };
            triangles.AddRange(Triangles);
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


        Debug.Log(startPixelX + "  " + startPixelY);

        Vector2[] Front = new Vector2[]
        {
            new Vector2(startPixelX + _uvSize.uvSizeFrontX/2,
                        startPixelY - _uvSize.uvSizeFrontY),

            new Vector2(startPixelX,
                        startPixelY),

            new Vector2(startPixelX + _uvSize.uvSizeFrontX,
                        startPixelY),
        };

        uvs.AddRange(Front);

        return uvs.ToArray();
    }


}
