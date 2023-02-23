using Core;
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

    [SerializeField] private MenuSelector selector;
    [SerializeField] private TextMeshProUGUI weaponText;
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private TextMeshProUGUI accessoryText;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemBonusText;
    [SerializeField] private TextMeshProUGUI itemDescriptionText;

    public void SetPartyMember(PartyMember member)
    {
        this.partyMember = member;

        selector.SelectionChanged -= DisplaySelectedItemInfo;
        selector.SelectionChanged += DisplaySelectedItemInfo;

        weaponText.text = equippedWeapon is not null ? $"Weapon: {equippedWeapon.ItemName}" : "Weapon: None";
        armorText.text = equippedArmor is not null ? $"Armor: {equippedArmor.ItemName}" : "Armor: None";
        accessoryText.text = equippedAccessory is not null ? $"Accessory: {equippedAccessory.ItemName}" : "Accessory: None";
    }

    private void DisplaySelectedItemInfo()
    {
        Equipment selectedEquipment = selector.SelectedIndex switch
        {
            0 => equippedWeapon,
            1 => equippedArmor,
            2 => equippedAccessory,
            _ => equippedWeapon,
        };

        itemNameText.text = selectedEquipment is not null ? $"{selectedEquipment.ItemName}" : "";
        itemDescriptionText.text = selectedEquipment is not null ? $"{selectedEquipment.ItemDescription}" : "";

        if (selectedEquipment is Weapon weapon)
            itemBonusText.text = $"+{weapon.StrBonus} STR";

        else if (selectedEquipment is Armor armor)
            itemBonusText.text = $"+{armor.ArmBonus} ARM";

        else
            itemBonusText.text = "";
    }
}
