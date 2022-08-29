using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PartyMemberInfo : MonoBehaviour
{
    private PartyMember partyMember;

    [SerializeField] private Image memberPortrait;
    [SerializeField] private TextMeshProUGUI memberName;
    [SerializeField] private TextMeshProUGUI memberLevelJob;
    [SerializeField] private TextMeshProUGUI memberHP;
    [SerializeField] private TextMeshProUGUI memberSpecial;
    [SerializeField] private TextMeshProUGUI memberBaseSTR;
    [SerializeField] private TextMeshProUGUI memberBaseARM;
    [SerializeField] private TextMeshProUGUI memberBaseSPD;
    [SerializeField] private TextMeshProUGUI memberEquipSTR;
    [SerializeField] private TextMeshProUGUI memberEquipARM;
    [SerializeField] private TextMeshProUGUI memberEquipSPD;

    void Start()
    {
        int siblingIndex = this.gameObject.transform.GetSiblingIndex();
        partyMember = Party.ActiveMembers[siblingIndex];
        memberName.text = partyMember.Name;
        memberPortrait.sprite = partyMember.Portrait;
        GetStats();
    }

    public void GetStats()
    {
        string levelJob = $"Level {partyMember.Stats.Level} {partyMember.Job}";
        memberLevelJob.text = levelJob;

        memberHP.text = $"HP: {partyMember.Stats.HP}/{partyMember.Stats.MaxHP}";
        memberSpecial.text = "MP 0:0";
        memberBaseSTR.text = $"STR: {partyMember.Stats.STR}";
        memberBaseARM.text = $"ARM: {partyMember.Stats.ARM}";
        memberBaseSPD.text = $"SPD: {partyMember.Stats.SPD}";
        memberEquipSTR.text = $"STR: {partyMember.Stats.STR}";
        memberEquipARM.text = $"ARM: {partyMember.Stats.ARM}";
        memberEquipSPD.text = $"SPD: {partyMember.Stats.SPD}";
    }
}
