﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class ai_Monster_Archer_Trueshot : AbilityInstance
{
    private ParticleSystem fly;
    private ParticleSystem land;
    private Vector3 startPosition;

    public float speed = 10f;
    public float damage = 60f;
    public float slowAmount = 30f;
    public float slowDuration = 1.5f;

    public float distance;

    public TargetValidator targetValidator;

    protected override void OnCreate(CastInfo castInfo, object[] data)
    {
        fly = transform.Find("Fly").GetComponent<ParticleSystem>();
        land = transform.Find("Land").GetComponent<ParticleSystem>();
        fly.Play();
        startPosition = transform.position;
    }

    protected override void AliveUpdate()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
        if (!photonView.IsMine) return;
        if(Vector3.Distance(startPosition, transform.position) > distance)
        {
            fly.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            
            Despawn();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine) return;
        Entity lv = other.GetComponent<Entity>();
        if (lv == null || !targetValidator.Evaluate(info.owner, lv)) return;

        info.owner.DoMagicDamage(lv, damage, false, this);
        lv.statusEffect.ApplyStatusEffect(StatusEffect.Slow(slowDuration, slowAmount), this);

        photonView.RPC("RpcLanded", RpcTarget.All);
        
        Despawn();
    }

    [PunRPC]
    private void RpcLanded()
    {
        land.Play();
        fly.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
    }
}
