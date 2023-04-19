 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogScaler : MonoBehaviour
{

    public float scaleSpeed = 2.5f;
    public float maxSize = 5;
    public float initialSize = 10;
    public float shadowFadeIncrements = 0.75f;
    private bool inBuildingRange = false;
    private bool desiredState;
    private bool doHiding = false;
    private bool doShowing = false;
    private GameObject cloudObj;
    private GameObject shadowObj;
    private FogBlockState fogState;
    private HashSet<int> inRangeObjects;

    private void Start()
    {
        cloudObj = transform.GetChild(0).gameObject;
        shadowObj = transform.GetChild(1).gameObject;
        fogState = gameObject.GetComponent<FogBlockState>();
        fogState.state = FogState.Unexplored;
        inRangeObjects = new HashSet<int>();
    }

    // Update is called once per frame
    void Update()
    {
        if (doHiding)
        {
            doHiding = false;
            StartCoroutine(FadeFog(shadowFadeIncrements));
        }
        else if (doShowing)
        {
            doShowing = false;
            StartCoroutine(FadeTo(0.5f, shadowFadeIncrements));
        }
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
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
    }

    IEnumerator FadeFog(float aTime)
    {
        if (!cloudObj || cloudObj.transform.localScale.x <= 0.1)
        {
            StartCoroutine(FadeTo(0.0f, shadowFadeIncrements));
        }
        else
        {
            for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
            {
                StartCoroutine(FadeTo(0.0f, shadowFadeIncrements));

                if (cloudObj.transform.localScale.x > 0.1)
                {
                    Vector3 desiredSize = new Vector3(cloudObj.transform.localScale.x - Time.deltaTime * scaleSpeed, cloudObj.transform.localScale.y - Time.deltaTime * scaleSpeed, cloudObj.transform.localScale.z - Time.deltaTime * scaleSpeed);
                    cloudObj.transform.localScale = desiredSize;
                }

                if (cloudObj.transform.localScale.x <= 0.1)
                {
                    if (cloudObj.activeInHierarchy)
                        cloudObj.SetActive(false);
                }
                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void HideFog(int objectID)
    {
        // Keeps track of objects that are hiding up this fog block
        if (!inRangeObjects.Contains(objectID))
        {
            inRangeObjects.Add(objectID);
        }

        fogState.state = FogState.ExploredActive;
        doHiding = true;
    }

    public void ShowFog(int objectID)
    {
        if (inRangeObjects.Contains(objectID))
        {
            if (inRangeObjects.Count <= 1)
            {
                fogState.state = FogState.ExploredInactive;
                doShowing = true;
            }

            inRangeObjects.Remove(objectID);
        }
    }

    public FogState GetFogState()
    {
        return fogState.state;
    }
}