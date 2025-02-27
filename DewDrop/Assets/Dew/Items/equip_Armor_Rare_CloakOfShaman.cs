﻿public class equip_Armor_Rare_CloakOfShaman : Equipment
{
    public override void OnEquip(Entity owner)
    {
        owner.stat.bonusMaximumHealth += 100f;
        owner.stat.bonusMaximumMana += 50f;
        owner.stat.bonusHealthRegenerationPerSecond += 2f;
        owner.stat.bonusManaRegenerationPerSecond += 4f;
    }

    public override void OnUnequip(Entity owner)
    {
        owner.stat.bonusMaximumHealth -= 100f;
        owner.stat.bonusMaximumMana -= 50f;
        owner.stat.bonusHealthRegenerationPerSecond -= 2f;
        owner.stat.bonusManaRegenerationPerSecond -= 4f;
    }
}
