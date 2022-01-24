using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityHolder : MonoBehaviour
{
    public Image UIBasicAbility;
    public Image UIAdvanceAbility;
    public Ability ability;

    public GameObject weapon;

    float cooldownTime;
    float activeTime;

    public enum AbilityState
    {
        ready,
        active,
        cooldown
    }

    public AbilityState state = AbilityState.ready;

    public KeyCode key;

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case AbilityState.ready:
                if (Input.GetKeyDown(key))
                {
                    ability.Activate(gameObject);
                    state = AbilityState.active;
                    activeTime = ability.activeTime;
                }
            break;
            case AbilityState.active:
                if(activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    ability.BeginCoolDown(gameObject);
                    state = AbilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
                }
            break;
            case AbilityState.cooldown:
                if(cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.ready;
                }
            break;
        }

        SyncEffectWithCharacter();
    }

    private void SyncEffectWithCharacter()
    {
        if(ability.particlePlayed != null)
        {
            ability.particlePlayed.transform.position = weapon.transform.position; 
        }
    }
}
