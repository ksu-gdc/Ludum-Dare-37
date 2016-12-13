using UnityEngine;
using System.Collections.Generic;
using UnityStandardAssets.ImageEffects;

[ExecuteInEditMode]
[RequireComponent (typeof(Camera))]
[AddComponentMenu("Image Effects/2D Shadow Caster")]
public class ShadowCaster : PostEffectsBase{

    public RenderTexture OcclusionMap = null;
    public List<Transform> SceneLights = new List<Transform>();
    public float LightIntensity;
    public float SourceHeight;
    public float ShadowStepResolution;

    [HideInInspector]
    public Shader CastingShader = null;
    private Material CastingMaterial = null;

    [HideInInspector]
    public Shader AveragingShader = null;
    private Material AveragingMaterial = null;

    [HideInInspector]
    public Shader ClearShader = null;
    private Material ClearMaterial = null;

    public override bool CheckResources()
    {
        CheckSupport(false);

        CastingMaterial = CheckShaderAndCreateMaterial(CastingShader, CastingMaterial);
        AveragingMaterial = CheckShaderAndCreateMaterial(AveragingShader, AveragingMaterial);
        ClearMaterial = CheckShaderAndCreateMaterial(ClearShader, ClearMaterial);

        if (!isSupported)
            ReportAutoDisable();

        return isSupported;
    }

    void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if(CheckResources() == false)
        {
            Graphics.Blit(source, destination);
            return;
        }

        int rtW = source.width;
        int rtH = source.height;

        //float aspect = (1.0f * rtW) / (1.0f * rtH);

        Camera main = Camera.main;
        float aspect = 1 / main.aspect;
        float oneOverBaseSize = 1.0f / 512.0f;

        RenderTexture averageTexture = RenderTexture.GetTemporary(rtW, rtH, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear, 1);
        RenderTexture currentLight = RenderTexture.GetTemporary(rtW, rtH, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear, 1);
        RenderTexture previousAverage = RenderTexture.GetTemporary(rtW, rtH, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear, 1);

        Graphics.Blit(averageTexture, averageTexture, ClearMaterial);

        foreach(Transform light in SceneLights)
        {
            Vector3 light_texture_pos = main.WorldToViewportPoint(light.position);

            CastingMaterial.SetVector("_WorldSpaceLightPos", light_texture_pos);
            CastingMaterial.SetFloat("_AspectRatio", aspect);
            CastingMaterial.SetFloat("_Intensity", LightIntensity);
            CastingMaterial.SetFloat("_MarchDist", SourceHeight);
            CastingMaterial.SetFloat("_MaxIterations", ShadowStepResolution);
            Graphics.Blit(OcclusionMap, currentLight, CastingMaterial, -1);

            Graphics.Blit(averageTexture, previousAverage);
            AveragingMaterial.SetInt("_NumberOfIterations", SceneLights.Count);
            AveragingMaterial.SetTexture("_PreviousTex", previousAverage);
            Graphics.Blit(currentLight, averageTexture, AveragingMaterial);
        }

        //RenderTexture output = currentLight;
        RenderTexture output = averageTexture;
        Graphics.Blit(output, destination);

        RenderTexture.ReleaseTemporary(currentLight);
        RenderTexture.ReleaseTemporary(previousAverage);
        RenderTexture.ReleaseTemporary(averageTexture);

        //Blur average next?

        //Combine with final Image
    }
}
