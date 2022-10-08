using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

namespace Battle
{
    public abstract class PartyMemberStats : BattleStats
    {
        public static PartyMemberStats CreateStats(PartyMember member, int level)
        {
            return member.Job switch
            {
                Job.Fighter => new FighterStats(member, level),
                Job.BlackMage => new MageStats(member, level),
                Job.WhiteMage => new MageStats(member, level),
                Job.Thief => new ThiefStats(member, level),
                _ => new FighterStats(member, level),
            };
        }

        protected int hp;

        public abstract int BaseMaxHP { get; }
        public abstract int BaseSTR { get; }
        public abstract int BaseARM { get; }
        public abstract int BaseSPD { get; }

        public override void ReduceHP(int amount)
        {
            if (amount <= 0)
                return;

            hp = Mathf.Clamp(hp - amount, 0, MaxHP);
        }
    }
}
