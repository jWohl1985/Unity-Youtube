using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core
{
    public static class Party
    {
        private static List<PartyMember> activeMembers = new List<PartyMember>();
        private static List<PartyMember> reserveMembers = new List<PartyMember>();
        private static Inventory inventory = new Inventory();

        public static IReadOnlyList<PartyMember> ActiveMembers => activeMembers;
        public static IReadOnlyList<PartyMember> ReserveMembers => reserveMembers;
        public static Inventory Inventory => inventory;

        static Party()
        {
            GenerateStartingParty();
        }

        public static void AddActiveMember(PartyMember memberToAdd)
        {
            if (activeMembers.Contains(memberToAdd))
                return;

            activeMembers.Add(memberToAdd);
            reserveMembers.Remove(memberToAdd);
        }

        public static void RemoveActiveMember(PartyMember memberToRemove)
        {
            if (!activeMembers.Contains(memberToRemove))
                return;

            activeMembers.Remove(memberToRemove);
            reserveMembers.Add(memberToRemove);
        }

        private static void GenerateStartingParty()
        {
            PartyMember BlackWraith = ScriptableObject.Instantiate(Resources.Load<PartyMember>(Paths.BlackWraith));
            PartyMember Satyr = ScriptableObject.Instantiate(Resources.Load<PartyMember>(Paths.Satyr));
            PartyMember Wraith = ScriptableObject.Instantiate(Resources.Load<PartyMember>(Paths.Wraith));
            PartyMember Minotaur = ScriptableObject.Instantiate(Resources.Load<PartyMember>(Paths.Minotaur));
            AddActiveMember(BlackWraith);
            AddActiveMember(Satyr);
            AddActiveMember(Wraith);
            AddActiveMember(Minotaur);

            inventory.Initialize();

            BlackWraith.EquipItem((Equipment)inventory.Items.Keys.Where(i => i is Weapon).First());
            Satyr.EquipItem((Equipment)inventory.Items.Keys.Where(i => i is Armor).First());
            Wraith.EquipItem((Equipment)inventory.Items.Keys.Where(i => i is Accessory).First());

            foreach(PartyMember member in ActiveMembers)
            {
                member.Initialize(member, 1);
            }
        }
    }
}
