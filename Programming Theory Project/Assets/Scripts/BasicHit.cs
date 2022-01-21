using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BasicHit : Ability
{
    public ParticleSystem attackBluePrefab;

    public override void Activate(GameObject parent)
    {
        AnimateCharacter(parent);
        PlaySoundAndEffects(parent);            
    }

    private void AnimateCharacter(GameObject parent)
    {
        Animator animator = parent.GetComponent<Animator>();
        animator.SetTrigger("attackTrig");
        animator.SetInteger("attackType_i", 1);
    }

    private void PlaySoundAndEffects(GameObject parent)
    {
        AudioSource AttackSound = GameObject.Find("Simple Hit Audio").GetComponent<AudioSource>();
        GameObject spellGenerator = GameObject.Find("Spell Generator");

        AttackSound.Play();
        particlePlayed = Instantiate(attackBluePrefab, spellGenerator.transform.position, attackBluePrefab.transform.rotation);
    }


    public override void BeginCoolDown(GameObject parent)
    {
        Animator animator = parent.GetComponent<Animator>();
        animator.SetInteger("attackType_i", 0);
    }

}
