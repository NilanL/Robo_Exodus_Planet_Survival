using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionCollision : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    [SerializeField]
    public GameObject explosionPrefab;

    private ParticleSystem explosion;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
        explosion = Instantiate(explosionPrefab.GetComponent<ParticleSystem>());
    }

    void OnParticleCollision(GameObject other)
    {
        part.GetCollisionEvents(other, collisionEvents);
        explosion.transform.position = collisionEvents[0].intersection;
        explosion.Play();

        //int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        //
        //int i = 0;
        //
        //while (i < numCollisionEvents)
        //{
        //    explosion.transform.position = collisionEvents[i].intersection;
        //    explosion.Play();
        //    i++;
        //}
    }
}
