using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarTestScript : MonoBehaviour
{

	public GameObject m_fogOfWarPlane;
	public Transform m_player;
	public LayerMask m_fogLayer;
	public float m_radius = 5f;
	private float m_radiusSqr { get { return m_radius * m_radius; } }

	private Mesh m_mesh;
	private Vector3[] m_vertices;
	//private Color[] m_colors;
	private List<Color> m_colors;

	// Use this for initialization
	void Start()
	{
		Initialize();
	}

	// Update is called once per frame
	void Update()
	{
		Ray r = new Ray(transform.position, m_player.position - transform.position);
		Debug.DrawRay(transform.position, m_player.position - transform.position, Color.red);
		RaycastHit hit;
		//Physics.Raycast(r, out hit, m_fogLayer);
		bool b = Physics.Raycast(r, out hit, 100, 1 << m_fogLayer.value, QueryTriggerInteraction.Collide);
		//Debug.LogWarning(b);
		if (b)
		{
			//Debug.LogWarning("----------------> IT IS HIT");

			//Debug.LogWarning(m_vertices.Length);
			for (int i = 0; i < m_vertices.Length; i++)
			{
				Vector3 v = m_fogOfWarPlane.transform.TransformPoint(m_vertices[i]);
				float dist = Vector3.SqrMagnitude(v - hit.point);
				// Debug.LogWarning(dist);
				//Debug.LogWarning("----------------> IT IS HIT");
				//Debug.LogWarning(i);
				if (dist < m_radiusSqr)
				{
					//float alpha = Mathf.Min(m_colors[i].a, dist / m_radiusSqr);
					//m_colors[i].r = 255;
					//m_colors[i].g = 0;
					//m_colors[i].b = 0;
					//m_colors[i].a = 1;
				}
			}
			UpdateColor();
		}
	}

	void Initialize()
	{
		m_mesh = m_fogOfWarPlane.GetComponent<MeshFilter>().mesh;
		m_vertices = m_mesh.vertices;
		m_colors = new List<Color>(m_vertices.Length); //new Color[m_vertices.Length];
		for (int i = 0; i < m_colors.Count; i++)
		{
			//m_colors[i].
			//m_colors[i].a = 0;
			m_colors[i] = Color.red;
			//m_colors[i] = Color.Lerp(Color.red, Color.green, m_vertices[i].y);//Color.black;
		}
		UpdateColor();
	}

	void UpdateColor()
	{
		m_mesh.SetColors(m_colors);
		//m_mesh.colors = m_colors;
	}
}