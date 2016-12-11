using UnityEngine;

[ExecuteInEditMode]
public class RayMarchingIEScript: MonoBehaviour {

    public Material EffectMaterial;
    public Transform LightOrigin;

    void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Camera main = Camera.main;
        EffectMaterial.SetVector("_WorldSpaceLightPos", main.WorldToViewportPoint(LightOrigin.position));
        Graphics.Blit(src, dst, EffectMaterial);
    }
}
