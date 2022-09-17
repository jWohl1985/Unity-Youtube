using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public static class Party
    {
        private static List<PartyMember> activeMembers = new List<PartyMember>();
        private static List<PartyMember> reserveMembers = new List<PartyMember>();

        public static IReadOnlyList<PartyMember> ActiveMembers => activeMembers;
        public static IReadOnlyList<PartyMember> ReserveMembers => reserveMembers;

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
        }
    }
}
