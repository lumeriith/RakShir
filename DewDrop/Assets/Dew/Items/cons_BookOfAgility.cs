﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cons_BookOfAgility : Consumable
{
    public override void OnUse(CastInfo info)
    {
        AbilityInstanceManager.CreateAbilityInstance("ai_cons_BookOfAgility", info.owner.transform.position, Quaternion.identity, info);
        SFXManager.CreateSFXInstance("si_cons_BookOfAgility", info.owner.transform.position);
        DestroySelf();
    }

    public override InfoTextIcon infoTextIcon => InfoTextIcon.Book;
}
