using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "New Weapon")]
public class Weapon : Equipment
{
    [SerializeField] private int strBonus;
    public int StrBonus => strBonus;
}
