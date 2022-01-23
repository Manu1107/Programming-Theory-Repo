using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject lifeBarPrefab;
    HealthSystem healthSystem;

    private Animator animator;
    private AudioSource ouchSound;
    private float lifePoints = 200f;
    private Vector3 UIOffset = new Vector3(0f, 2.5f, 0f);
    private GameObject lifeBar;
    private GameObject player;
    private Rigidbody enemyRb;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
        enemyRb = GetComponent<Rigidbody>();
        ouchSound = GameObject.Find("OuchAudio").GetComponent<AudioSource>();
        player = GameObject.Find("Player");

        //Recuperare children di lifebar perchè script spostato
        lifeBar = Instantiate(lifeBarPrefab, transform.position + UIOffset, transform.rotation);
        healthSystem = new HealthSystem(100, 100);
        lifeBar.GetComponent<HealthBar>().Setup(healthSystem);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        lifeBar.transform.position = transform.position + UIOffset;
        lifeBar.transform.rotation = transform.rotation;

        if (Vector3.Distance(player.transform.position, transform.position) > 2.5f)
        {
            Vector3 lookDirection = (player.transform.position - transform.position).normalized;
            enemyRb.AddForce(lookDirection * 0.5f);
        }

        if(Mathf.Abs(enemyRb.velocity.z) > 0.3)
        {
            animator.SetFloat("velocityVertical", 0.5f);
        }
        else
        {
            animator.SetFloat("velocityVertical", 0.0f);
        }

        transform.LookAt(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("weapon"))
        {
            //lifePoints -= other.gameObject.GetDamage();
            animator.Play("GetHit");
            ouchSound.Play();
            healthSystem.Damage(10);
        }
    }
}
