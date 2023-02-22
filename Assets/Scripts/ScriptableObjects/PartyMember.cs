using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;
using Core;

[CreateAssetMenu(fileName = "New Party Member", menuName = "New Party Member")]
public class PartyMember : ScriptableObject
{
    [SerializeField] private string moniker;
    [SerializeField] private GameObject actorPrefab;
    [SerializeField] private Sprite menuPortrait;
    [SerializeField] private BattlePortrait battlePortrait;
    [SerializeField] private Job job;
    private PartyMemberStats stats;

    private Weapon equippedWeapon;
    private Armor equippedArmor;
    private Accessory equippedAccessory;

    public string Name => moniker;
    public GameObject ActorPrefab => actorPrefab;
    public BattleStats Stats => stats;
    public Sprite MenuPortrait => menuPortrait;
    public BattlePortrait BattlePortrait => battlePortrait;
    public Job Job => job;
    public Weapon EquippedWeapon => equippedWeapon;
    public Armor EquippedArmor => equippedArmor;
    public Accessory EquippedAccessory => equippedAccessory;

    public void EquipItem(Equipment itemToEquip)
    {
        if (itemToEquip is Weapon weapon)
            EquipWeapon(weapon);

        else if (itemToEquip is Armor armor)
            EquipArmor(armor);

        else if (itemToEquip is Accessory accessory)
            EquipAccessory(accessory);
    }

    private void EquipWeapon(Weapon weapon)
    {
        if (equippedWeapon is not null)
        {
            Party.Inventory.AddItem(equippedWeapon);
        }

        Party.Inventory.RemoveItem(weapon);
        equippedWeapon = weapon;
    }

    private void EquipArmor(Armor armor)
    {
        if (equippedArmor is not null)
        {
            Party.Inventory.AddItem(equippedArmor);
        }

        Party.Inventory.RemoveItem(armor);
        equippedArmor = armor;
    }

    private void EquipAccessory(Accessory accessory)
    {
        if (equippedAccessory is not null)
        {
            Party.Inventory.AddItem(equippedAccessory);
        }

        Party.Inventory.RemoveItem(accessory);
        equippedAccessory = accessory;
    }

    public void Initialize(PartyMember member, int level)
    {
        stats = PartyMemberStats.CreateStats(member, level);
    }
}
