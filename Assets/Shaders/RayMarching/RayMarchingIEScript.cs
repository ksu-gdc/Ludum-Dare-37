using UnityEngine;
using System.Collections.Generic;

[ExecuteInEditMode]
public class RayMarchingIEScript: MonoBehaviour {

    public Material EffectMaterial;
    public List<Transform> LightOrigins;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        foreach (Transform LightOrigin in LightOrigins)
        {
            Camera main = Camera.main;
            EffectMaterial.SetVector("_WorldSpaceLightPos", main.WorldToViewportPoint(LightOrigin.position));
            EffectMaterial.SetFloat("_AspectRatio", 1 / main.aspect);
            Graphics.Blit(src, dst, EffectMaterial);
        }
    }
}
