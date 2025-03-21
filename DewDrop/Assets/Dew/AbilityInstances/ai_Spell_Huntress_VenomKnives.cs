﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ai_Spell_Huntress_VenomKnives : AbilityInstance
{
    public float range;
    public float initialDamage = 50;
    public float poisonDamage = 50;
    public float poisonTime = 4;
    public float basicAttackBonusDamage;
    public TargetValidator targetValidator;

    private List<Entity> affectedTargets = new List<Entity>();


    private GameObject land;
    private ParticleSystem start;

    private void Awake()
    {
        land = transform.Find("Land").gameObject;
        start = transform.Find("Start").GetComponent<ParticleSystem>();
    }

    protected override void OnCreate(CastInfo castInfo, object[] data)
    {
        start.Play();
        if (!photonView.IsMine) return;
        List<Entity> targets = castInfo.owner.GetAllTargetsInRange(transform.position, range, targetValidator);
        foreach(Entity target in targets)
        {
            target.statusEffect.ApplyStatusEffect(StatusEffect.DamageOverTime(poisonTime, poisonDamage), this);
            castInfo.owner.DoMagicDamage(target, initialDamage, false, this);
            target.OnTakeBasicAttackHit += BasicAttackHit;
            affectedTargets.Add(target);
            photonView.RPC("RpcLand", RpcTarget.All, target.transform.position + target.GetCenterOffset());
            
        }

        StartCoroutine(CoroutineUnsubscribe());
    }

    [PunRPC]
    private void RpcLand(Vector3 position)
    {
        Instantiate(land, position, Quaternion.identity, transform).GetComponent<ParticleSystem>().Play();
    }

    IEnumerator CoroutineUnsubscribe()
    {
        yield return new WaitForSeconds(poisonTime);
        foreach(Entity target in affectedTargets)
        {
            target.OnTakeBasicAttackHit -= BasicAttackHit;
        }
        
        Despawn();
    }

    private void BasicAttackHit(InfoBasicAttackHit hit)
    {
        if(hit.from == info.owner)
        {
            info.owner.DoMagicDamage(hit.to, basicAttackBonusDamage, false, this);
        }
    }
}
