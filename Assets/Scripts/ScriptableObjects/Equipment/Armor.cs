using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "New Armor")]
public class Armor : Equipment
{
    [SerializeField] private int armBonus;
    public int ArmBonus => armBonus;
}
