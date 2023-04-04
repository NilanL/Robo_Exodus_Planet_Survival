using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGen : MonoBehaviour
{

    public GameObject cloud;
    public float width = 10;
    public float height = 10;
    public float cloudSize = 5;
    public float initialSize = 6;

    private TerrainCollider terrain;
    private LayerMask layerMask;

    private GameObject cloudObj;
    private GameObject shadowObj;

    // Use this for initialization
    void Start()
    {
        terrain = GameObject.Find("Terrain_0_0_6c7ab805-d6b8-439a-8805-7f652d662845").GetComponent<TerrainCollider>();
        layerMask = LayerMask.GetMask("Ground");

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Create cloud object
                GameObject go = Instantiate(cloud, new Vector3(transform.position.x + x * cloudSize, transform.position.y, transform.position.z + y * cloudSize), Quaternion.identity);

                //cloudObj = go.transform.GetChild(0).gameObject;
                //shadowObj = go.transform.GetChild(1).gameObject;

                // Rescale height to align with terrain
                float maxDistance = transform.position.y - 0.1f;
                Ray ray = new Ray(go.transform.position, Vector3.down);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
                {
                    if (hit.collider.gameObject == terrain.gameObject)
                    {
                        go.transform.position = new Vector3(transform.position.x + x * cloudSize, (transform.position.y + (transform.position.y - hit.distance)) - 90, transform.position.z + y * cloudSize);
                    }
                }

                go.name = "Cloud_" + x + "_" + y;
                go.transform.localScale = new Vector3(initialSize, initialSize, initialSize);
                //cloudObj.transform.localScale = new Vector3(initialSize, initialSize, initialSize);
                go.transform.SetParent(transform);
            }
        }
    }
} 