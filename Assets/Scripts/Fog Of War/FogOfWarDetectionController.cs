using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarDetectionController : MonoBehaviour
{

    public GameObject mapObj;
    public float desiredDistance = 8;
    public float maxDistance = 100;
    public bool doShow = true;
    public bool doScale = true;

    public bool isPrinted = false;
    private bool isDefeated = false;
    private int fogOfWarLayer;
    bool started = true;
    /*
    private LayerMask enemyLayerMask;
    private Collider[] lastColliders;
    private bool isStaticObject;
    private bool isStaticFogSet;
    private int numOfUpdates;
    */

    void Start()
    {
        mapObj = GameObject.Find("TerrainGroup_0/Clouds");
        fogOfWarLayer = LayerMask.GetMask("FogOfWar");
        /*
        enemyLayerMask = LayerMask.GetMask("EnemyUnit");
        lastColliders = null;
        isStaticObject = !gameObject.tag.Equals("Selectable");
        numOfUpdates = 0;
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDefeated)
            if (started)
            {
                started = false;
                StartCoroutine(FogOfWarCheck());
                started = true;
            }
    }




    IEnumerator FogOfWarCheck()
    {
        yield return new WaitForSeconds(1f);
        var colliders = Physics.OverlapSphere(transform.position, desiredDistance + 1f, fogOfWarLayer);

        foreach (Collider collider in colliders)
        {
            Transform child = collider.transform;
            FogScaler scaler = child.GetComponent<FogScaler>();
            float distance = Vector3.Distance(child.position, transform.position);

            if (distance < desiredDistance)
            {
                if (!doShow) //Are we showing or hiding the object at this desired distance, if false we are showing objects
                {
                    if (!child.gameObject.activeInHierarchy) //Childs not active, activate it
                    {
                        child.gameObject.SetActive(true);
                    }
                }
                else //Childs is within desired distance but we want to hide it
                {
                    if (child.gameObject.activeInHierarchy && !doScale) //Childs active and we are not scaling, let's hide it
                    {
                        child.gameObject.SetActive(false);
                    }
                    else if (child.gameObject.activeInHierarchy && doScale) //Childs active and we are scaling, lets hide it
                    {
                        scaler.HideFog(child.gameObject.GetInstanceID());
                    }
                }
            }
            else
            {
                scaler.ShowFog(child.gameObject.GetInstanceID());
            }
        }
    }

    public void UnitDefeated()
    {
        isDefeated = true;
        var colliders = Physics.OverlapSphere(transform.position, desiredDistance + 1f, fogOfWarLayer);

        foreach (Collider collider in colliders)
        {
            Transform child = collider.transform;
            FogScaler scaler = child.GetComponent<FogScaler>();
            scaler.ShowFog(child.gameObject.GetInstanceID());
        }
    }
}