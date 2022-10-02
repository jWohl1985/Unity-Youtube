using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "New Weapon")]
public class Weapon : Equipment
{
    [SerializeField] private string weaponName;
    [SerializeField] private int requiredLevel;
    [SerializeField] private int strBonus;

    public string WeaponName => weaponName;
    public int RequiredLevel => requiredLevel;
    public int StrBonus => strBonus;
}
