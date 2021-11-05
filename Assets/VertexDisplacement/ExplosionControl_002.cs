using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionControl_002 : MonoBehaviour
{
    private static int _Id_CoefFactor = Shader.PropertyToID("_CoefFactor");
    private static int _Id_BurnOffset = Shader.PropertyToID("_BurnOffset");
    private static int _Id_BurnExtent = Shader.PropertyToID("_BurnExtent");
    private static int _Id_Vanish = Shader.PropertyToID("_Vanish");

    private Material material;
    private float scaler = 0.05f;
    private float burnOffset = 1;
    private float burnExtent = 1;
    private float clip = 0;

    private void Start()
    {
        material = GetComponent<MeshRenderer>().sharedMaterial;
        material.SetFloat(_Id_BurnOffset, burnOffset);
        transform.localScale = new Vector3(scaler, scaler, scaler);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            scaler = 0.05f;
            burnOffset = 1;
            burnExtent = 1;
            clip = 0;
        }

        if (scaler < 0.98f)
        {
            scaler = Mathf.Lerp(scaler, 1, Time.deltaTime * 3);
            transform.localScale = new Vector3(scaler, scaler, scaler);
        }

        if (burnOffset > 0.02f)
        {
            burnOffset = Mathf.Lerp(burnOffset, 0, Time.deltaTime * 0.5f);
            burnExtent = Mathf.Lerp(burnExtent, 0.5f, Time.deltaTime);
            clip = Mathf.Lerp(clip, 1, Time.deltaTime);
            material.SetFloat(_Id_BurnOffset, burnOffset);
            material.SetFloat(_Id_BurnExtent, burnExtent);
            material.SetFloat(_Id_Vanish, clip);
        }

        var coef = new Vector4();
        float baseTime = Time.time * 0.5f;
        coef.x = Mathf.Sin(baseTime * Mathf.PI) * 0.5f + 0.25f;
        coef.y = Mathf.Sin((baseTime + 0.6f) * Mathf.PI) * 0.5f + 0.25f;
        coef.z = Mathf.Sin((baseTime + 1.2f) * Mathf.PI) * 0.5f + 0.25f;
        float correction = 0.6666f / (coef.x + coef.y + coef.z);
        coef *= correction;

        material.SetVector(_Id_CoefFactor, coef);
    }
}
