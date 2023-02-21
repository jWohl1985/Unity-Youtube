using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class ThiefStats : PartyMemberStats
    {
        protected PartyMember member;
        protected int level;

        // PartyMemberStats
        public override int BaseMaxHP => level * 9;
        public override int BaseSTR => 5 + (int)(level * 1.5);
        public override int BaseARM => 5 + (int)(level * 1.5);
        public override int BaseSPD => level * 2;

        // BattleStats
        public override int Level => level;
        public override int HP => hp;
        public override int MaxHP => BaseMaxHP;
        public override int STR => member.EquippedWeapon is null ? BaseSTR : BaseSTR + member.EquippedWeapon.StrBonus;
        public override int ARM => member.EquippedArmor is null ? BaseARM : BaseARM + member.EquippedArmor.ArmBonus;
        public override int SPD => BaseSPD;
        public int Stealth { get; private set; }

        public ThiefStats(PartyMember member, int level)
        {
            this.member = member;
            this.level = level;
            this.hp = BaseMaxHP;
            this.Stealth = level * 2;
        }

        public void GainStealth(int amount)
        {
            if (amount <= 0)
                return;

            Stealth = Mathf.Min(Stealth + amount, Level * 2);
        }

        public void LoseStealth(int amount)
        {
            if (amount <= 0)
                return;

            Stealth = Mathf.Max(Stealth - amount, 0);
        }
    }
}
