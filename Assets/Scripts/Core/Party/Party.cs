using System.Collections;
using System.Collections.Generic;
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
            PartyMember Balfam = ScriptableObject.Instantiate(Resources.Load<PartyMember>(Paths.BlackWraith));
            PartyMember Bul = ScriptableObject.Instantiate(Resources.Load<PartyMember>(Paths.Satyr));
            PartyMember Enna = ScriptableObject.Instantiate(Resources.Load<PartyMember>(Paths.Wraith));
            PartyMember Maxymer = ScriptableObject.Instantiate(Resources.Load<PartyMember>(Paths.Minotaur));
            AddActiveMember(Balfam);
            AddActiveMember(Bul);
            AddActiveMember(Enna);
            AddActiveMember(Maxymer);

            inventory.Initialize();

            Balfam.EquipItem(inventory.Equipment[0]);
            Bul.EquipItem(inventory.Equipment[1]);
            Enna.EquipItem(inventory.Equipment[2]);

            foreach(PartyMember member in ActiveMembers)
            {
                member.Initialize(member, 1);
            }
        }
    }
}
