using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class MageStats : PartyMemberStats
    {
        protected PartyMember member;
        protected int level;

        // PartyMemberStats
        public override int BaseMaxHP => level * 6;
        public override int BaseSTR => level;
        public override int BaseARM => 5 + (level + 1);
        public override int BaseSPD => level * 2;

        // BattleStats
        public override int Level => level;
        public override int HP => hp;
        public override int MaxHP => BaseMaxHP;
        public override int STR => member.EquippedWeapon is null ? BaseSTR : BaseSTR + member.EquippedWeapon.StrBonus;
        public override int ARM => member.EquippedArmor is null ? BaseARM : BaseARM + member.EquippedArmor.ArmBonus;
        public override int SPD => BaseSPD;
        public int MP { get; private set; }

        public MageStats(PartyMember member, int level)
        {
            this.member = member;
            this.level = level;
            this.hp = BaseMaxHP;
            this.MP = level * 10;
        }

        public void GainMP(int amount)
        {
            if (amount <= 0)
                return;

            MP = Mathf.Min(MP + amount, (Level*10));
        }

        public void LoseMP(int amount)
        {
            if (amount <= 0)
                return;

            MP = Mathf.Max(MP - amount, 0);
        }
    }
}
