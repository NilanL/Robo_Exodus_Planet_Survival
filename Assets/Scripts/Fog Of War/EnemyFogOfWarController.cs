using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFogOfWarController : MonoBehaviour
{
    private LayerMask fogOfWarLayer;
    private int enemyLayerID;
    private int invisibleLayerID;
    private int uiLayerID;
    private int objLayerID;
    private Vector3 overlapCheckPosition;
    private int visibleCounter;
    private bool isFOWActive = false;

    // Start is called before the first frame update
    void Start()
    {
        fogOfWarLayer = LayerMask.GetMask("FogOfWar");
        enemyLayerID = LayerMask.NameToLayer("EnemyLayer");
        invisibleLayerID = LayerMask.NameToLayer("Invisiable");
        uiLayerID = LayerMask.NameToLayer("UI");
        objLayerID = this.gameObject.layer;
        overlapCheckPosition = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        visibleCounter = 0;
        isFOWActive = GameObject.Find("FogOfWarManager").GetComponent<FogOfWarGenerator>().GenerateFogOfWar;

        if (isFOWActive)
            HideEnemyUnit();
        else
            ShowEnemyUnit();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFOWActive)
        {
            //var colliders = Physics.OverlapCapsule(transform.position, new Vector3(transform.position.x, transform.position.y + 10, transform.position.z), 4f, fogOfWarLayer);
            var colliders = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y + 20, transform.position.z), 6f, fogOfWarLayer);
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

            if (inRange && this.gameObject.layer == invisibleLayerID)
            {
                ShowEnemyUnit();
            }
            else if (!inRange && this.gameObject.layer != invisibleLayerID)
            {
                HideEnemyUnit();
            }
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
        SetGameLayerRecursive(gameObject, enemyLayerID);
        //if (gameObject.layer != defaultLayerID)
        //{
        //    for (int i = 0; i < gameObject.transform.childCount; i++)
        //        gameObject.transform.GetChild(i).gameObject.layer = defaultLayerID;
        //
        //    gameObject.layer = defaultLayerID;
        //}
    }

    private void HideEnemyUnit()
    {
        SetGameLayerRecursive(gameObject, invisibleLayerID);

        //if (gameObject.layer != invisibleLayerID)
        //{
        //    for (int i = 0; i < gameObject.transform.childCount; i++)
        //        gameObject.transform.GetChild(i).gameObject.layer = invisibleLayerID;
        //
        //    gameObject.layer = invisibleLayerID;
        //}
    }

    private void SetGameLayerRecursive(GameObject _go, int _layer)
    {
        setGameLayer(_go, _layer);

        foreach (Transform child in _go.transform)
        {
            setGameLayer(child.gameObject, _layer);

            Transform _HasChildren = child.GetComponentInChildren<Transform>();
            if (_HasChildren != null)
                SetGameLayerRecursive(child.gameObject, _layer);
        }
    }

    private void setGameLayer(GameObject _go, int _layer)
    {
        if (_layer == invisibleLayerID)
        {
            _go.layer = _layer;
        }
        else
        {
            if (_go.tag == "Health_Bar")
            {
                _go.layer = uiLayerID;
            }
            else
            {
                _go.layer = _layer;
            }
        }
    }

}
