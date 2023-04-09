using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(ParticleSystem))]
public class Miner_Animation_Controller : MonoBehaviour
{
    private Animator animator;
    private ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        particleSystem = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
