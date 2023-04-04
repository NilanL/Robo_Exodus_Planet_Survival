using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{

    public float scaleSpeed = 5;
    public float maxSize = 5;
    public float initialSize = 10;
    private bool desiredState;
    private bool doScaling = false;
    private bool IsInRange = false;
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

                if (cloudObj.transform.localScale.x <= 0.1)
                {
                    cloudObj.SetActive(false);
                    shadowObj.SetActive(false);
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

    public void ToggleScale(bool setType)
    {
        desiredState = setType;
        doScaling = true;
        IsInRange = true;
    }

    public void AllowShadow()
    {
        shadowObj.SetActive(true);
    }

    public void SetIsInRange(bool isInRange)
    {
        IsInRange = isInRange;
    }
}