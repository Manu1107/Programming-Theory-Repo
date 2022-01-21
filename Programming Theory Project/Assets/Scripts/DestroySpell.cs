using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySpell : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("DestroySpellAfterDelay");
    }
    private IEnumerator DestroySpellAfterDelay()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}
