using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MemberEquipmentInfo : MonoBehaviour
{
    private PartyMember partyMember;

    private Weapon equippedWeapon => partyMember.EquippedWeapon;
    private Armor equippedArmor => partyMember.EquippedArmor;
    private Accessory equippedAccessory => partyMember.EquippedAccessory;

    [SerializeField] private TextMeshProUGUI weaponText;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private TextMeshProUGUI accessoryText;

    public void SetPartyMember(PartyMember member)
    {
        this.partyMember = member;

        weaponText.text = equippedWeapon is not null ? equippedWeapon.ItemName : "None";
        armorText.text = equippedArmor is not null ? equippedArmor.ItemName : "None";
        accessoryText.text = equippedAccessory is not null ? equippedAccessory.ItemName : "None";
    }
}
