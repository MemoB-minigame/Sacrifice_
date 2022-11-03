using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class InDreamEffect : MonoBehaviour
{
    [SerializeField] private Volume volumn;
    //public VolumeComponent test;
    

    [SerializeField] private LensDistortion lensDistortion;
    [SerializeField] private ChromaticAberration chromaticAberration;

    [SerializeField] private float lensDistortionValue = 0;
    [SerializeField] private float lensDistortionSpeed = 0.5f / 60;
    [SerializeField] private float chromaticAberrationSpeed = 1.0f / 60;

    [SerializeField] private float sin = 0;
    [SerializeField] private float sinSpeed = 5;

    [SerializeField] private bool isStartEffect = false;

    void Start()
    {
        volumn = gameObject.GetComponent<Volume>();
        volumn.profile.TryGet<LensDistortion>(out lensDistortion);
        volumn.profile.TryGet<ChromaticAberration>(out chromaticAberration);

        //lensDistortion = gameObject.GetComponent<LensDistortion>();
        //chromaticAberration = gameObject.GetComponent<ChromaticAberration>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartInDreamEffect();
        }

        if (isStartEffect)
        {
            sin += Time.deltaTime * sinSpeed;
            lensDistortionValue += lensDistortionSpeed * Time.deltaTime;
            lensDistortion.intensity.value = lensDistortionValue * Mathf.Sin(sin);

            chromaticAberration.intensity.value += chromaticAberrationSpeed * Time.deltaTime;
        }
    }

    public void StartInDreamEffect()
    {
        isStartEffect = true;
    }
}
