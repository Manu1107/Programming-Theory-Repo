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

    enum AbilityState
    {
        ready,
        active,
        cooldown
    }

    AbilityState state = AbilityState.ready;

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
                    UIBasicAbility.fillAmount = 0;
                }
            break;
            case AbilityState.active:
                if(activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                    RefillAbilityUI();
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
                    RefillAbilityUI();
                }
                else
                {
                    state = AbilityState.ready;
                }
            break;
        }

        SyncEffectWithCharacter();
    }

    private void RefillAbilityUI()
    {
        UIBasicAbility.fillAmount += (Time.deltaTime);
    }

    private void SyncEffectWithCharacter()
    {
        if(ability.particlePlayed != null)
        {
            ability.particlePlayed.transform.position = weapon.transform.position; 
        }
    }
}
