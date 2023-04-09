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
        var colliders = Physics.OverlapSphere(overlapCheckPosition, 4f, fogOfWarLayer);

        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.GetComponent<FogBlockState>().state == FogState.ExploredActive)
            {
                visibleCounter++;
            }
        }

        if (visibleCounter >= 1)
        {
            ShowEnemyUnit();
        }
        else
        {
            HideEnemyUnit();
        }

        visibleCounter = 0;
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
