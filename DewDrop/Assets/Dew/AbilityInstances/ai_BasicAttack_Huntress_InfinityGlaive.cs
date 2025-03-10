﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ai_BasicAttack_Huntress_InfinityGlaive : AbilityInstance
{
    public int maxBounceCount = 3;
    public float bounceRange = 4f;
    public float rotationSpeed = 800;
    public float projectileSpeed = 15f;
    public TargetValidator bounceTargetValidator;

    private int currentBounceCount = 0;
    private List<Entity> hitEnemies = new List<Entity>();

    private Entity currentTarget;
    
    private ParticleSystem fly;
    private ParticleSystem land;

    private void Awake()
    {
        fly = transform.Find("Fly").GetComponent<ParticleSystem>();
        land = transform.Find("Land").GetComponent<ParticleSystem>();
    }

    protected override void OnCreate(CastInfo castInfo, object[] data)
    {
        currentTarget = info.target;
        fly.Play();
    }

    protected override void AliveUpdate()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position + currentTarget.GetCenterOffset(), projectileSpeed * Time.deltaTime);

        if (!photonView.IsMine) return;

        if (Vector3.Distance(transform.position,currentTarget.transform.position + currentTarget.GetCenterOffset()) < float.Epsilon)
        {
            int nextTargetId = -1;
            info.owner.DoBasicAttackImmediately(currentTarget, this);
            hitEnemies.Add(currentTarget);
            if (currentBounceCount < maxBounceCount)
            {
                currentBounceCount++;
                List<Entity> possibleTargets = info.owner.GetAllTargetsInRange(transform.position, bounceRange, bounceTargetValidator);
                
                for(int i=0;i<possibleTargets.Count;i++)
                {
                    if (!hitEnemies.Contains(possibleTargets[i]))
                    {
                        hitEnemies.Add(possibleTargets[i]);
                        nextTargetId = possibleTargets[i].photonView.ViewID;
                        break;
                    }
                }
            }

            if (currentTarget.statusEffect.IsAffectedBy(StatusEffectType.DamageOverTime)) currentBounceCount--;
            photonView.RPC("RpcLanded", RpcTarget.All, transform.position, nextTargetId);

            if(nextTargetId == -1)
            {
                Despawn();
            }
        }
    }

    [PunRPC]
    private void RpcLanded(Vector3 position, int new_target_id)
    {
        transform.position = position;
        Instantiate(land.gameObject, position, Quaternion.identity, transform).GetComponent<ParticleSystem>().Play();
        if (new_target_id == -1)
        {
            fly.Stop();
        }
        else
        {
            currentTarget = PhotonNetwork.GetPhotonView(new_target_id).GetComponent<Entity>();
        }
    }
}
