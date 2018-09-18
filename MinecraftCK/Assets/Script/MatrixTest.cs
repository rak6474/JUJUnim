using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixTest : MonoBehaviour {

    public Transform bone;

    private void Start()
    {
        //Debug.Log("< localToWorld > \n" + transform.localToWorldMatrix);
        //Debug.Log("< worldToLocal > \n" + transform.worldToLocalMatrix); 



        Debug.Log("< bone :: worldToLocal > \n" + bone.worldToLocalMatrix);
        Debug.Log("< tr :: localToWorld > \n" + transform.localToWorldMatrix);

        Debug.Log("< tr * bone > \n" + transform.localToWorldMatrix * bone.worldToLocalMatrix); //곱한다 = 선형변환한다.
        //root와 bone사이의 거리가 결과로 나온다.

    }

}
