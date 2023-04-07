using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHider : MonoBehaviour
{

    public GameObject mapObj;
    public float desiredDistance = 8;
    public float maxDistance = 100;
    public bool doShow = true;
    public bool doScale = false;

    public bool isPrinted = false;
    private int fogOfWarLayerID;
    private Collider[] lastColliders;
    private bool isStaticObject;
    private bool isStaticFogSet;
    private int numOfUpdates;

    void Start()
    {
        mapObj = GameObject.Find("TerrainGroup_0/Clouds");
        fogOfWarLayerID = 11;
        lastColliders = null;
        isStaticObject = !gameObject.tag.Equals("Selectable");
        numOfUpdates = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isStaticObject || (isStaticObject && !isStaticFogSet))
            dissapearCheck();
    }

    private void dissapearCheck()
    {
        /*
        if (!isPrinted)
        {
            var colliders = Physics.OverlapSphere(transform.position, 35f);
            foreach (var collider in colliders)
            {
                Debug.Log($"{collider.gameObject.transform.name} is nearby");
            }
            isPrinted = true;
        }*/

        var colliders = Physics.OverlapSphere(transform.position, desiredDistance, 1 << fogOfWarLayerID);
        //Debug.Log(colliders.Length);


        /*
        if (isStaticObject)
        {
            if (lastColliders == null)
            {
                lastColliders = new Collider[1];
            }

            if (colliders.Length == lastColliders.Length)
            {
                bool isStandingStill = true;
                for (int i = 0; i < colliders.Length; i++)
                {
                    if ()
                    {
                        isStandingStill = false;
                        break;
                    }
                }   

                if (isStandingStill)
                {
                    return;
                }
            }

            lastColliders = new Collider[colliders.Length];
            System.Array.Copy(colliders, lastColliders, colliders.Length);
        }*/

        foreach (Collider collider in colliders)
        {
            /*
            if (isStaticObject)
            {
                var scaler = collider.gameObject.GetComponent<Scaler>();
                scaler.StaticBuildingInRange();

                if (scaler.IsFogCleared())
                    return;
            }*/

            var child = collider.transform;
            float distance = Vector3.Distance(child.position, transform.position);

            if (distance < desiredDistance)
            {
                if (!doShow)//Are we showing or hiding the object at this desired distance, if false we are showing objects
                {
                    if (!child.gameObject.activeInHierarchy)//Childs not active, activate it
                    {
                        child.gameObject.SetActive(true);
                    }
                }
                else//Childs is within desired distance but we want to hide it
                {
                    if (child.gameObject.activeInHierarchy && !doScale)//Childs active and we are not scaling, let's hide it
                    {
                        child.gameObject.SetActive(false);
                    }
                    else if (child.gameObject.activeInHierarchy && doScale)//Childs active and we are scaling, lets hide it
                    {
                        child.GetComponent<Scaler>().ToggleScale(false);
                    }
                }
            }
            else
            {
                child.GetComponent<Scaler>().AllowShadow();
                
                /*
                if (!doShow)//Are we showing or hiding the child at this desired distance, if false we are hiding objects
                {
                    if (child.gameObject.activeInHierarchy)//Childs active, hide it
                    {
                        child.gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (distance < maxDistance)//Are we past the max distance
                    {
                        if (!child.gameObject.activeInHierarchy && !doScale)//Childs not active and we are not scaling and is within max distance, let's show it
                        {
                            child.gameObject.SetActive(true);
                        }
                        else if (!child.gameObject.activeInHierarchy && doScale)//Childs not active and we are scaling and is within max distance, let's show it
                        {
                            child.gameObject.SetActive(true);
                            child.GetComponent<Scaler>().ToggleScale(true);
                        }
                    }
                    else
                    {
                        child.gameObject.SetActive(false);
                    }
                }*/
            }
        }

        if (numOfUpdates < 3)
            numOfUpdates++;

        if (isStaticObject && numOfUpdates >= 3)
            isStaticFogSet = true;
    }
}