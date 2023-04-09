using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFogOfWarController : MonoBehaviour
{
    private LayerMask fogOfWarLayer;
    private int defaultLayerID;
    private int invisibleLayerID;
    private Vector3 overlapCheckPosition;
    private int visibleCounter;

    // Start is called before the first frame update
    void Start()
    {
        fogOfWarLayer = LayerMask.GetMask("FogOfWar");
        defaultLayerID = LayerMask.NameToLayer("Default");
        invisibleLayerID = LayerMask.NameToLayer("Invisiable");
        overlapCheckPosition = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        visibleCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //var colliders = Physics.OverlapCapsule(transform.position, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), 4f, fogOfWarLayer);
        var colliders = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), 6f, fogOfWarLayer);
        bool inRange = false;

        foreach (Collider collider in colliders)
        {
            //Debug.Log(colliders.Length);
            if (collider.gameObject.GetComponent<FogScaler>().GetFogState() == FogState.ExploredActive)
            {
                inRange = true;
                break;
            } 
        }

        if (inRange)
        {
            ShowEnemyUnit();
        }
        else
        {
            HideEnemyUnit();
        }
    }

    void OnDrawGizmos()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), 6f);
    }

    private void ShowEnemyUnit()
    {
        if (gameObject.layer != defaultLayerID)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
                gameObject.transform.GetChild(i).gameObject.layer = defaultLayerID;

            gameObject.layer = defaultLayerID;
        }
    }

    private void HideEnemyUnit()
    {
        if (gameObject.layer != invisibleLayerID)
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
                gameObject.transform.GetChild(i).gameObject.layer = invisibleLayerID;

            gameObject.layer = invisibleLayerID;
        }
    }
}
