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
                cloudObj = go.transform.GetChild(0).gameObject;
                cloudObj.transform.localRotation = Quaternion.Euler(new Vector3(Random.Range(-10f, 10f), Random.Range(-360f, 360f), Random.Range(-10f, 10f)));
                cloudObj.transform.localPosition = new Vector3(cloudObj.transform.localPosition.x, Random.Range(cloudObj.transform.localPosition.y, cloudObj.transform.localPosition.y + 0.4f), cloudObj.transform.localPosition.z);
                //RecalculateNormalsSeamless(cloudObj.GetComponent<MeshFilter>().mesh);
                go.transform.SetParent(transform);
            }
        }
    }

    static void RecalculateNormalsSeamless(Mesh mesh)
    {
        var trianglesOriginal = mesh.triangles;
        var triangles = trianglesOriginal;

        var vertices = mesh.vertices;

        var mergeIndices = new Dictionary<int, int>();

        for (int i = 0; i < vertices.Length; i++)
        {
            var vertexHash = vertices[i].GetHashCode();

            if (mergeIndices.TryGetValue(vertexHash, out var index))
            {
                for (int j = 0; j < triangles.Length; j++)
                    if (triangles[j] == i)
                        triangles[j] = index;
            }
            else
                mergeIndices.Add(vertexHash, i);
        }

        mesh.triangles = triangles;

        var normals = new Vector3[vertices.Length];

        mesh.RecalculateNormals();
        var newNormals = mesh.normals;

        for (int i = 0; i < vertices.Length; i++)
            if (mergeIndices.TryGetValue(vertices[i].GetHashCode(), out var index))
                normals[i] = newNormals[index];

        mesh.triangles = trianglesOriginal;
        mesh.normals = normals;
    }


}