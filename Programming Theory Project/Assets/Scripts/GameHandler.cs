using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    private HealthBar UIlifeBar;
    HealthSystem healthSystem;

    public GameObject enemyPrefab;
    public GameObject enemySpawner;
    // Start is called before the first frame update
    void Start()
    {
        UIlifeBar = GameObject.Find("Bar").GetComponent<HealthBar>();
        healthSystem = new HealthSystem(100, 100);
        UIlifeBar.Setup(healthSystem);
        
        Instantiate(enemyPrefab, enemySpawner.transform.position, enemyPrefab.transform.rotation);
    }

    private void DamageTimeout()
    {
    }

}
