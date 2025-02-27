﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ai_Spell_Rare_ProtectionOfAgate : AbilityInstance
{
    public float protectedDuration = 2.5f;
    protected override void OnCreate(CastInfo castInfo, object[] data)
    {
        if (!photonView.IsMine) return;
        info.owner.ApplyStatusEffect(StatusEffect.Protected(protectedDuration), this);
        Despawn(info.owner);
    }
}
