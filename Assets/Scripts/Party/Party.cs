using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Party
{
    private static List<PartyMember> activeMembers = new List<PartyMember>();
    private static List<PartyMember> reserveMembers = new List<PartyMember>();

    public static IReadOnlyList<PartyMember> ActiveMembers => activeMembers;
    public static IReadOnlyList<PartyMember> ReserveMembers => reserveMembers;

    static Party()
    {
        PartyMember Balfam = ResourceLoader.Load<PartyMember>(ResourceLoader.Balfam);
        PartyMember Bul = ResourceLoader.Load<PartyMember>(ResourceLoader.Bul);
        PartyMember Enna = ResourceLoader.Load<PartyMember>(ResourceLoader.Enna);
        PartyMember Maxymer = ResourceLoader.Load<PartyMember>(ResourceLoader.Maxymer);
        AddActiveMember(Balfam);
        AddActiveMember(Bul);
        AddActiveMember(Enna);
        AddActiveMember(Maxymer);
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
}
