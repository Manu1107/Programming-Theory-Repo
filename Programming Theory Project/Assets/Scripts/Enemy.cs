using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject lifeBarPrefab;
    public AbilityHolder abilityHolder;
    private HealthSystem healthSystem;

    private float ENEMY_DISTANCE_MIN = 2.5f;
    private float ENEMY_DISTANCE_MAX = 8.0f;
    private int LIFE_POINTS = 100;

    private Animator animator;
    private AudioSource ouchSound;
    private GameObject lifeBar;
    private GameObject player;

    private Vector3 UIOffset = new Vector3(0f, 2.5f, 0f);
    private bool isMoving = false;
    private bool isAlive = true;

    void Start()
    {
        InitializeFields();
        AttachLifeManagementSystem();
    }

    private void InitializeFields()
    {
        animator = GetComponent<Animator>();
        ouchSound = GameObject.Find("OuchAudio").GetComponent<AudioSource>();
        player = GameObject.Find("Player");
        abilityHolder = player.GetComponent<AbilityHolder>();
    }

    private void AttachLifeManagementSystem()
    {
        lifeBar = Instantiate(lifeBarPrefab, transform.position + UIOffset, transform.rotation);
        healthSystem = new HealthSystem(LIFE_POINTS, LIFE_POINTS);
        GameObject.Find("Foreground").GetComponent<HealthBar>().Setup(healthSystem);
    }

    void LateUpdate()
    {
        if (isAlive)
        {
            lifeBar.transform.position = transform.position + UIOffset;
            lifeBar.transform.rotation = transform.rotation;

            if (Vector3.Distance(player.transform.position, transform.position) > ENEMY_DISTANCE_MIN && Vector3.Distance(player.transform.position, transform.position) < ENEMY_DISTANCE_MAX)
            {
                isMoving = true;
                Vector3 lookDirection = (player.transform.position - transform.position).normalized;
                transform.Translate(lookDirection * Time.deltaTime, Space.World);
            }
            else
            {
                isMoving = false;
            }

            if (isMoving)
            {
                animator.SetFloat("velocityVertical", 0.5f);
            }
            else
            {
                animator.SetFloat("velocityVertical", 0.0f);
            }

            transform.LookAt(player.transform.position);

            if (healthSystem.GetHealth() == 0)
            {
                Invoke("DestroyLifebar", 2f);
                animator.Play("Die");
                isAlive = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("weapon") && (abilityHolder.state == AbilityHolder.AbilityState.active))
        {
            animator.Play("GetHit");
            ouchSound.Play();
            healthSystem.Damage(10);
        }
    }

    private void DestroyLifebar()
    {
        Destroy(lifeBar);
    }
}
