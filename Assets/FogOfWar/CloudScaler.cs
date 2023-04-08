using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudScaler : MonoBehaviour
{

    public float scaleSpeed = 5;
    public float maxSize = 5;
    public float initialSize = 10;
    public float shadowFadeIncrements = 0.5f;
    private bool inBuildingRange = false;
    private bool desiredState;
    private bool doScaling = false;
    private bool IsInRange = false;
    private bool coroutineIsrunning = false;
    private GameObject cloudObj;
    private GameObject shadowObj;

    private void Start()
    {
        //var initialSizeVector = new Vector3(transform.localScale.x - Time.deltaTime * 0.1f, transform.localScale.y - Time.deltaTime * 0.1f, transform.localScale.z - Time.deltaTime * 0.1f);
        //transform.localScale = initialSizeVector;
        cloudObj = transform.GetChild(0).gameObject;
        shadowObj = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (doScaling)
        {
            if (!desiredState)
            {
                //gameObject.SetActive(false);

                Vector3 desiredSize = new Vector3(cloudObj.transform.localScale.x - Time.deltaTime * scaleSpeed, cloudObj.transform.localScale.y - Time.deltaTime * scaleSpeed, cloudObj.transform.localScale.z - Time.deltaTime * scaleSpeed);
                cloudObj.transform.localScale = desiredSize;

                //var shadowMaterial = shadowObj.GetComponent<Renderer>().material;
                //shadowMaterial = ChangeAlpha(shadowMaterial, shadowMaterial.color.a - 0.1f);

                if (cloudObj.transform.localScale.x <= 0.1)
                {
                    cloudObj.SetActive(false);
                    StartCoroutine(FadeTo(0.0f, shadowFadeIncrements, false));
                    doScaling = false;
                }
            }
            else if (desiredState || !IsInRange)
            {
                /*
                Vector3 desiredSize = new Vector3(cloudObj.transform.localScale.x + Time.deltaTime * scaleSpeed, cloudObj.transform.localScale.y + Time.deltaTime * scaleSpeed, cloudObj.transform.localScale.z + Time.deltaTime * scaleSpeed);
                cloudObj.transform.localScale = desiredSize;

                if (cloudObj.transform.localScale.x >= maxSize)
                {
                    cloudObj.transform.localScale = new Vector3(maxSize, maxSize, maxSize);
                    shadowObj.SetActive(true);
                    doScaling = false;
                }*/
            }
        }
    }

    IEnumerator FadeTo(float aValue, float aTime, bool active)
    {
        coroutineIsrunning = true;

        Material material = shadowObj.GetComponent<Renderer>().material;
        float red = material.color.r;
        float green = material.color.g;
        float blue = material.color.b;
        float alpha = material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(red, green, blue, Mathf.Lerp(alpha, aValue, t));
            material.color = newColor;
            yield return new WaitForEndOfFrame();
        }

        Color lastColor = new Color(red, green, blue, aValue);
        material.color = lastColor;

        coroutineIsrunning = false;
    }

    public void ToggleScale(bool setType)
    {
        desiredState = setType;
        doScaling = true;
        IsInRange = true;
    }

    public void AllowShadow()
    {
        if (!inBuildingRange)
        {
            var shadowMaterial = shadowObj.GetComponent<Renderer>().material;
            StartCoroutine(FadeTo(0.5f, shadowFadeIncrements, true));
        }
    }

    public void SetIsInRange(bool isInRange)
    {
        IsInRange = isInRange;
    }

    public void StaticBuildingInRange()
    {
        inBuildingRange = true;
    }

    public bool IsFogCleared()
    {
        return !cloudObj.activeSelf && !shadowObj.activeSelf;
    }
}