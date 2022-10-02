using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

[CreateAssetMenu(fileName = "New Accessory", menuName = "New Accessory")]
public class Accessory : Equipment
{
    [SerializeField] private string accessoryName;
    [SerializeField] private EquipmentEffect effect;
    [SerializeField] private int requiredLevel;

    public string AccessoryName => accessoryName;
    public EquipmentEffect Effect => effect;
    public int RequiredLevel => requiredLevel;
}
