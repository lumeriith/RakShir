﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class BossNethergosBehaviour : MonoBehaviourPun
{

    private Material bodyMaterial;
    private Material wingMaterial;

    private Entity livingThing;

    private bool shouldRevive = true;

    private void OnEnable()
    {
        bodyMaterial.SetFloat("_DissolveCutoff", 0f);
        wingMaterial.SetFloat("_DissolveCutoff", 0f);
    }

    private void OnDisable()
    {
        bodyMaterial.SetFloat("_DissolveCutoff", 0f);
        wingMaterial.SetFloat("_DissolveCutoff", 0f);
    }

    private void Awake()
    {
        bodyMaterial = transform.Find("Model/onyxia_body").GetComponent<SkinnedMeshRenderer>().material;
        wingMaterial = transform.Find("Model/onyxia_wing").GetComponent<SkinnedMeshRenderer>().material;

    }

    private void Start()
    {
        livingThing = GetComponent<Entity>();
        if (photonView.IsMine)
        {
            livingThing.OnDeath += Death;
        }
    }

    void Death(InfoDeath info)
    {
        if (!shouldRevive) return;
        livingThing.Revive();
        livingThing.PlayCustomAnimation("BossNethergos - Shout", 5f);
        StatusEffect hot = new StatusEffect(StatusEffectType.HealOverTime, 5, livingThing.maximumHealth);
        livingThing.statusEffect.ApplyStatusEffect(hot, null);

        StatusEffect invul = new StatusEffect(StatusEffectType.Invulnerable, 5);
        livingThing.statusEffect.ApplyStatusEffect(invul, null);
    }

    public void Disappear()
    {
        if (!shouldRevive) return;
        shouldRevive = false;
        livingThing.Kill();

        StartCoroutine(CoroutineDisappear());
    }

    private IEnumerator CoroutineDisappear()
    {
        yield return new WaitForSeconds(0.1f);
        for (float t = 0; t < 10f; t += Time.deltaTime)
        {
            bodyMaterial.SetFloat("_DissolveCutoff", t / 10f);
            wingMaterial.SetFloat("_DissolveCutoff", t / 10f);
            yield return null;
        }

        PlayerViewCamera.instance.visionMultiplier /= 1.2f;
    }

}
