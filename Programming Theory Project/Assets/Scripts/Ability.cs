using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability : ScriptableObject
{
    public new string name;
    public float cooldownTime;
    public float activeTime;

    public ParticleSystem particlePlayed;

    public virtual void Activate(GameObject parent) { }
    public virtual void BeginCoolDown(GameObject parent) { }

}
