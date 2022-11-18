using Cyan;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
public class ScreenEffect : MonoBehaviour
{
    public UniversalRenderPipelineAsset URPAsset;
    public BuffManager buffManager;
    public WeaponManager weaponManager;
    public PlayerController playerController;
    private bool screenEffectAvailable;
    private Material effectMaterial;

    public enum STATES { NOBUFF, BUFF1, BUFF2, BUFF3 };
    [SerializeField]private STATES state = STATES.NOBUFF;
    private Blit blitFeature;

    [ColorUsageAttribute(true, true)]
    public Color[] REFCOLORS = { Color.white, Color.blue, Color.yellow, Color.red, Color.gray };

    // Start is called before the first frame update
    void Start()
    {
        if (URPAsset is null) { URPAsset = (UniversalRenderPipelineAsset)QualitySettings.renderPipeline; }
        if (URPAsset is null) { URPAsset = (UniversalRenderPipelineAsset)GraphicsSettings.defaultRenderPipeline; }
        screenEffectAvailable = URPAsset is null ? false : true;
        if (screenEffectAvailable)
        {
            // 利用反射获取渲染管线资产URP Asset中的首个ScriptableRendererData
            FieldInfo propertyInfo = URPAsset.GetType().GetField("m_RendererDataList", BindingFlags.Instance | BindingFlags.NonPublic);
            ScriptableRendererData[] a = (ScriptableRendererData[])(propertyInfo?.GetValue(URPAsset));
            ScriptableRendererData URPData = a[0];
            List<ScriptableRendererFeature> rendererFeatures = URPData.rendererFeatures;

            // 获取名为“Blit”的附加渲染特征
            ScriptableRendererFeature feature = null;
            for (var i = 0; i < rendererFeatures.Count; i++)
            {
                feature = rendererFeatures[i];
                if (feature.name == "Blit") { break; }
            }
            blitFeature = (Blit)feature;

            // 获取“Blit”引用的材质，以便修改特效
            effectMaterial = blitFeature.blitPass.blitMaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!screenEffectAvailable) { return; }
        if (!playerController.isLife) { SetEffectDeath(); return; }
        switch (state)
        {
            case STATES.NOBUFF:
                {
                    if (buffManager.buffs[3])
                    {
                        SetEffectBUFF3();
                        break;
                    }
                    if (buffManager.buffs[2])
                    {
                        SetEffectBUFF2();
                        break;
                    }
                    if (buffManager.buffs[1])
                    {
                        SetEffectBUFF1();
                        break;
                    }
                    SetEffectNOBUFF();
                    break;
                }

            case STATES.BUFF1:
                {
                    if (buffManager.buffs[3])
                    {
                        SetEffectBUFF3();
                        break;
                    }
                    if (buffManager.buffs[2])
                    {
                        SetEffectBUFF2();
                        break;
                    }
                    if (buffManager.buffs[1]) { break; }
                    SetEffectNOBUFF();
                    break;
                }

            case STATES.BUFF2:
                {
                    if (buffManager.buffs[3])
                    {
                        SetEffectBUFF3();
                        break;
                    }
                    if (buffManager.buffs[2]) { break; }
                    if (buffManager.buffs[1])
                    {
                        SetEffectBUFF1();
                        break;
                    }
                    SetEffectNOBUFF();
                    break;
                }

            case STATES.BUFF3:
                {
                    if (buffManager.buffs[3])
                    {
                        SetEffectBUFF3();
                        break;
                    }
                    if (buffManager.buffs[2])
                    {
                        SetEffectBUFF2();
                        break;
                    }
                    if (buffManager.buffs[1])
                    {
                        SetEffectBUFF1();
                        break;
                    }
                    SetEffectNOBUFF();
                    break;
                }
        }
    }

    void SetEffectDeath()
    {
        blitFeature.SetActive(true);
        effectMaterial.SetFloat("_FuseMainTex", 1f);
        effectMaterial.SetFloat("_NoiseScale", 8f);
        effectMaterial.SetFloat("_NoiseSpeed", 0.4f);
        effectMaterial.SetFloat("_ScanLineSpeed", 0.1f);
        effectMaterial.SetFloat("_ScanLineFreq", 1900f);
    }

    void SetEffectNOBUFF()
    {
        state = STATES.NOBUFF;
        blitFeature.SetActive(false);
    }

    void SetEffectBUFF1()
    {
        state = STATES.BUFF1;
        blitFeature.SetActive(true);
        effectMaterial.SetFloat("_FullScreenIntensity", 0.2f);
        effectMaterial.SetColor("_Color", REFCOLORS[0]);
        effectMaterial.SetFloat("_VoronoiSpeed", 2f);
        effectMaterial.SetFloat("_VoronoiScale", 30f);
        effectMaterial.SetFloat("_VoronoiIntensity", 0.8f);
        effectMaterial.SetFloat("_VoronoiPower", 3f);
        effectMaterial.SetFloat("_VignetteRadiusPower", 15f);
        effectMaterial.SetFloat("_Crystallize", 1f);
        effectMaterial.SetFloat("_PixelatedIntensity", 5f);
        effectMaterial.SetFloat("_FuseMainTex", 0f);
    }

    void SetEffectBUFF2()
    {
        state = STATES.BUFF2;
        blitFeature.SetActive(true);
        effectMaterial.SetFloat("_FullScreenIntensity", 0.4f);
        effectMaterial.SetColor("_Color", REFCOLORS[1]);
        effectMaterial.SetFloat("_VoronoiSpeed", 3f);
        effectMaterial.SetFloat("_VoronoiScale", 30f);
        effectMaterial.SetFloat("_VoronoiIntensity", 0.8f);
        effectMaterial.SetFloat("_VoronoiPower", 3f);
        effectMaterial.SetFloat("_VignetteRadiusPower", 15f);
        effectMaterial.SetFloat("_Crystallize", 1f);
        effectMaterial.SetFloat("_PixelatedIntensity", 5f);
        effectMaterial.SetFloat("_FuseMainTex", 0f);
    }

    void SetEffectBUFF3()
    {
        state = STATES.BUFF3;
        blitFeature.SetActive(true);
        if (!weaponManager.GetActiveWeapon().IsDamageBoosted())
        {
            effectMaterial.SetFloat("_FullScreenIntensity", 0.6f);
            effectMaterial.SetColor("_Color", REFCOLORS[2]);
            effectMaterial.SetFloat("_VoronoiSpeed", 4f);
            effectMaterial.SetFloat("_VoronoiScale", 10f);
            effectMaterial.SetFloat("_VoronoiIntensity", 0.9f);
            effectMaterial.SetFloat("_VoronoiPower", 3f);
            effectMaterial.SetFloat("_VignetteRadiusPower", 15f);
            effectMaterial.SetFloat("_PixelatedIntensity", 1f);
        }
        else
        {
            effectMaterial.SetFloat("_FullScreenIntensity", 0.8f);
            effectMaterial.SetColor("_Color", REFCOLORS[3]);
            effectMaterial.SetFloat("_VoronoiSpeed", 2f);
            effectMaterial.SetFloat("_VoronoiScale", 10f);
            effectMaterial.SetFloat("_VoronoiIntensity", 0.8f);
            effectMaterial.SetFloat("_VoronoiPower", 1f);
            effectMaterial.SetFloat("_VignetteRadiusPower", 10f);
            effectMaterial.SetFloat("_PixelatedIntensity", 5f);
        }
        effectMaterial.SetFloat("_Crystallize", 0f);
        effectMaterial.SetFloat("_FuseMainTex", 0f);
    }
}
