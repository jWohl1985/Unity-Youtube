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
    public Weapon EquippedWeapon => equippedWeapon ?? Resources.Load<Weapon>(Paths.NoWeapon);
    public Armor EquippedArmor => equippedArmor ?? Resources.Load<Armor>(Paths.NoArmor);
    public Accessory EquippedAccessory => equippedAccessory ?? Resources.Load<Accessory>(Paths.NoAccessory);

    public void EquipItem(Equipment itemToEquip)
    {
        if (itemToEquip is Weapon weapon)
            equippedWeapon = weapon;

        else if (itemToEquip is Armor armor)
            equippedArmor = armor;

        else if (itemToEquip is Accessory accessory)
            equippedAccessory = accessory;
    }

    public void Initialize(PartyMember member, int level)
    {
        stats = PartyMemberStats.CreateStats(member, level);
    }
}
