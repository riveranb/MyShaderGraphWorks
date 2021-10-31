using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmplifyControl_001 : MonoBehaviour
{
    private static int _Id_Amplify = Shader.PropertyToID("_Amplify");
    private Material material;
    private float amplifiness;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().sharedMaterial;
    }

    private void Update()
    {
        material.SetFloat(_Id_Amplify, amplifiness);

        if (amplifiness > 0)
        {
            amplifiness = Mathf.Lerp(amplifiness, 0, Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            amplifiness += 1;
        }
    }
}
