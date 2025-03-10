﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equip_Boots_Rare_SwiftLightBoots : Equipment
{
    public override void OnEquip(Entity owner)
    {
        owner.stat.bonusMaximumHealth += 50f;
        owner.stat.bonusMovementSpeed += 75f;
    }

    public override void OnUnequip(Entity owner)
    {
        owner.stat.bonusMaximumHealth -= 50f;
        owner.stat.bonusMovementSpeed -= 75f;
    }
}
