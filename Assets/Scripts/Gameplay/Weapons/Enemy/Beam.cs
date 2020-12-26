using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : Projectile {

    Dictionary<Collider, FrameComponent> UnitsInDamageArea = new Dictionary<Collider, FrameComponent>();
    public CapsuleCollider BeamCollider;
    public float MaxLength;
    public float ExpansionSpeed;
    public float DamagePerSecond;
    float length = 0.0f;
    public ParticleSystem innerParticles;
    public ParticleSystem outerParticles;
    public float innerFalloff = 1.1f;
    public float outerFalloff = 1.2f;


  
    protected override void FixedUpdate()
    {
        Damage = (int)(DamagePerSecond * Time.fixedDeltaTime);
        //if the beam is not at max range, expand it
        if(length < MaxLength)
        {
            //expand the beam
            length = Mathf.Min(length + (ExpansionSpeed * Time.fixedDeltaTime), MaxLength);
            BeamCollider.height = length;
            BeamCollider.center = new Vector3(0.0f, 0.0f, length / 2);
            //increase particle effect length
            ParticleSystem.MainModule innerMain = innerParticles.main;
            ParticleSystem.MainModule outerMain = outerParticles.main;
            innerMain.startLifetime = new ParticleSystem.MinMaxCurve((length * innerFalloff) / innerMain.startSpeed.constant);
            outerMain.startLifetime = new ParticleSystem.MinMaxCurve((length * outerFalloff) / outerMain.startSpeed.constant);
        }
        //damage units in the beam, but make sure we only damage each unit once
        Dictionary<Mob, List<FrameComponent>> DamageList = new Dictionary<Mob, List<FrameComponent>>();
        //sort the components by mob
        foreach(KeyValuePair<Collider, FrameComponent> mobCollider in UnitsInDamageArea)
        {
            if (mobCollider.Value == null)
                continue;

            Mob target = mobCollider.Value.GetComponentInParent<Mob>();
            if (target == null)
                continue;

            if (DamageList.ContainsKey(target))
            {
                DamageList[target].Add(mobCollider.Value);
            }
            else
            {
                DamageList.Add(target, new List<FrameComponent>());
                DamageList[target].Add(mobCollider.Value);
            }
        }
        //for each mob, choose a random component to apply the damage to
        foreach(KeyValuePair<Mob, List<FrameComponent>> mobToDamage in DamageList)
        {
            if (mobToDamage.Value.Count > 0)
                mobToDamage.Value[Random.Range(0, mobToDamage.Value.Count)].OnHit(this);
        }

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        //if the unit is a mob collider, add it to the damage area list
        MechComponentCollisionDetector MobCollider = other.GetComponent<MechComponentCollisionDetector>();
        if (MobCollider != null && MobCollider.GetComponentInParent<Mob>() != Source)
        {
            if (!UnitsInDamageArea.ContainsKey(other))
            {
                UnitsInDamageArea.Add(other, MobCollider.component);
                Debug.Log("Beam hitting " + MobCollider.GetComponentInParent<Mob>().name);
            }
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        //if the collider is a mob collider remove it from the damage list
        MechComponentCollisionDetector MobCollider = other.GetComponent<MechComponentCollisionDetector>();
        if (MobCollider != null)
        {
            if (UnitsInDamageArea.ContainsKey(other))
                UnitsInDamageArea.Remove(other);
        }
    }

    public override void SetDamage(int value)
    {
        DamagePerSecond = value;
    }
}
