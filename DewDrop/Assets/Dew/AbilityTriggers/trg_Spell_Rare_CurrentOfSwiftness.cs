﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trg_Spell_Rare_CurrentOfSwiftness : AbilityTrigger
{
    public override void OnCast(CastInfo info)
    {
        CreateAbilityInstance("ai_Spell_Rare_CurrentOfSwiftness", transform.position, Quaternion.identity);
        StartCooldown();
        SpendMana();
    }
    public override bool CanBeCast()
    {
        return !IsAnyInstanceActive();
    }
}
