using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Battle
{
    public class FighterStats : PartyMemberStats
    {
        protected PartyMember member;
        protected int level;
        

        // PartyMemberStats
        public override int BaseMaxHP => level * 13;
        public override int BaseSTR => 8 + (level * 2);
        public override int BaseARM => 8 + (level * 2);
        public override int BaseSPD => level;

        // BattleStats
        public override int Level => level;
        public override int HP => hp;
        public override int MaxHP => BaseMaxHP;
        public override int STR => BaseSTR + member.EquippedWeapon.StrBonus;
        public override int ARM => BaseARM + member.EquippedArmor.ArmBonus;
        public override int SPD => BaseSPD;
        public int Rage { get; private set; }

        public FighterStats(PartyMember member, int level)
        {
            this.member = member;
            this.level = level;
            this.hp = BaseMaxHP;
            this.Rage = 0;
        }

        public void GainRage(int amount)
        {
            if (amount <= 0)
                return;

            Rage = Mathf.Min(Rage + amount, Level);
        }

        public void LoseRage(int amount)
        {
            if (amount <= 0)
                return;

            Rage = Mathf.Max(Rage - amount, 0);
        }
    }
}
