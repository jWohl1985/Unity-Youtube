using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;
using Core;

public class PartyTests
{
    [Test, Order(0)]
    public void Constructs_a_party()
    {
        // Arrange

        // Act

        // Assert
        Assert.AreEqual(4, Party.ActiveMembers.Count);
        Assert.AreEqual(0, Party.ReserveMembers.Count);
    }

    [Test, Order(1)]
    public void Removes_active_members()
    {
        // Arrange
        PartyMember member = Party.ActiveMembers[0];

        // Act
        Party.RemoveActiveMember(member);

        // 
        Assert.IsFalse(Party.ActiveMembers.Contains(member));
    }

    [Test, Order(2)]
    public void Removed_members_go_to_reserve()
    {
        // Arrange
        PartyMember member = Party.ActiveMembers[1];

        // Act
        Party.RemoveActiveMember(member);

        // Assert
        Assert.IsTrue(Party.ReserveMembers.Contains(member));
    }

    [Test, Order(3)]
    public void Doesnt_remove_inactive_members()
    {
        // Arrange
        PartyMember member = Party.ReserveMembers[0];

        // Act
        Party.RemoveActiveMember(member); // trying to remove someone who is already removed

        // Assert
        Assert.AreEqual(2, Party.ActiveMembers.Count);
        Assert.AreEqual(2, Party.ReserveMembers.Count);
    }

    [Test, Order(4)]
    public void Adding_member_adds_to_active_members()
    {
        // Arrange
        PartyMember member = Party.ReserveMembers[0];

        // Act
        Party.AddActiveMember(member);

        // Assert
        Assert.IsTrue(Party.ActiveMembers.Contains(member));
    }

    [Test, Order(5)]
    public void Adding_member_removes_from_reserve()
    {
        // Arrange
        PartyMember member = Party.ReserveMembers[0];

        // Act
        Party.AddActiveMember(member);

        // Assert
        Assert.IsFalse(Party.ReserveMembers.Contains(member));
    }

    [Test, Order(6)]
    public void Doesnt_add_duplicate_members()
    {
        // Arrange
        PartyMember member = Party.ActiveMembers[0];

        // Act
        Party.AddActiveMember(member);
        Party.AddActiveMember(member);

        // Assert
        Assert.AreEqual(4, Party.ActiveMembers.Count);
        Assert.AreEqual(0, Party.ReserveMembers.Count);
    } 
}
