﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class equip_Boots_ElementalDetermination : Equipment
{
    public override void OnEquip(Entity owner)
    {
        owner.stat.bonusMovementSpeed += 50f;
    }

    public override void OnUnequip(Entity owner)
    {
        owner.stat.bonusMovementSpeed -= 50f;
    }
}
