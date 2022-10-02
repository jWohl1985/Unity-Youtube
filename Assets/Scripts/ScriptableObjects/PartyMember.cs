using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;

[CreateAssetMenu(fileName = "New Party Member", menuName = "New Party Member")]
public class PartyMember : ScriptableObject
{
    [SerializeField] private string moniker;
    [SerializeField] private GameObject actorPrefab;
    [SerializeField] private BattleStats stats;
    [SerializeField] private Sprite menuPortrait;
    [SerializeField] private BattlePortrait battlePortrait;
    [SerializeField] private string job = "Some job";

    private Weapon equippedWeapon;
    private Armor equippedArmor;
    private Accessory equippedAccessory;

    public string Name => moniker;
    public GameObject ActorPrefab => actorPrefab;
    public BattleStats Stats => stats;
    public Sprite MenuPortrait => menuPortrait;
    public BattlePortrait BattlePortrait => battlePortrait;
    public string Job => job;
    public Weapon EquippedWeapon => equippedWeapon;
    public Armor EquippedArmor => equippedArmor;
    public Accessory EquippedAccessory => equippedAccessory;

    public void EquipItem(Equipment itemToEquip)
    {
        if (itemToEquip is Weapon weapon)
            equippedWeapon = weapon;

        else if (itemToEquip is Armor armor)
            equippedArmor = armor;

        else if (itemToEquip is Accessory accessory)
            equippedAccessory = accessory;
    }
}
