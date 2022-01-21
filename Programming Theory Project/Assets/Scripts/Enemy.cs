using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Animator animator;
    private AudioSource ouchSound;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
        ouchSound = GameObject.Find("OuchAudio").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("weapon"))
        {
            animator.Play("GetHit");
            ouchSound.Play();
        }
    }
}
