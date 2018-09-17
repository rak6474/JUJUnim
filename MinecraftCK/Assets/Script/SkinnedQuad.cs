using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SkinnedQuad : MonoBehaviour {

    public Transform[] bones;
    SkinnedMeshRenderer smr;

    void Awake() {
        Mesh m = new Mesh();

        m.vertices = new Vector3[]
        {
            new Vector3(-1,-1,0),
            new Vector3(-1,1,0),
            new Vector3(1,1,0),
            new Vector3(1,-1,0)
        };

        m.triangles = new int[]
        {
            0,1,2,
            0,2,3
        };

        m.uv = new Vector2[]
        {
            new Vector2(0.125f, 0.75f),
            new Vector2(0.125f, 0.875f),
            new Vector2(0.25f, 0.875f),
            new Vector2(0.25f, 0.75f)
        }; 

        m.bindposes = new Matrix4x4[]
        {
            bones[0].worldToLocalMatrix * transform.localToWorldMatrix,
            bones[1].worldToLocalMatrix * transform.localToWorldMatrix
        };

        m.boneWeights = new BoneWeight[]
        {
            new BoneWeight() { boneIndex0 = 0, weight0 = 1 },
            new BoneWeight() { boneIndex0 = 0, weight0 = 1 },
            new BoneWeight() { boneIndex0 = 1, weight0 = 1 },
            new BoneWeight() { boneIndex0 = 1, weight0 = 1 }
        };

        smr = GetComponent<SkinnedMeshRenderer>();
        smr.sharedMesh = m;
        smr.bones = bones;
        smr.quality = SkinQuality.Bone1;
    }
}
