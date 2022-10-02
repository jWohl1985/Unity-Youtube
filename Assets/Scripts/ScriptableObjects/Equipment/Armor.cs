using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "New Armor")]
public class Armor : Equipment
{
    [SerializeField] private string armorName;
    [SerializeField] private int requiredLevel;
    [SerializeField] private int armBonus;

    public string ArmorName => armorName;
    public int RequiredLevel => requiredLevel;
    public int ArmBonus => armBonus;
}
