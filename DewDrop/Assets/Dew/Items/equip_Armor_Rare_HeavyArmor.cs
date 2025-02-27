﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equip_Armor_Rare_HeavyArmor : Equipment
{
    public override void OnEquip(Entity owner)
    {
        owner.stat.bonusMaximumHealth += 350f;
        owner.stat.bonusHealthRegenerationPerSecond += 3.5f;
    }

    public override void OnUnequip(Entity owner)
    {
        owner.stat.bonusMaximumHealth -= 350f;
        owner.stat.bonusHealthRegenerationPerSecond -= 3.5f;
    }
}
