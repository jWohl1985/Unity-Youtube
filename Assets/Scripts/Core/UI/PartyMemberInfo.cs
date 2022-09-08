using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Battle;

namespace Core
{
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
            DisplayInformation();
        }

        public void DisplayInformation()
        {
            BattleStats stats = partyMember.Stats;

            memberName.text = partyMember.Name;
            memberPortrait.sprite = partyMember.MenuPortrait;
            string levelJob = $"Level {stats.Level} {partyMember.Job}";
            memberLevelJob.text = levelJob;

            memberHP.text = $"HP: {stats.HP}/{stats.MaxHP}";
            memberSpecial.text = "MP 0:0";
            memberBaseSTR.text = $"STR: {stats.STR}";
            memberBaseARM.text = $"ARM: {stats.ARM}";
            memberBaseSPD.text = $"SPD: {stats.SPD}";
            memberEquipSTR.text = $"STR: {stats.STR}";
            memberEquipARM.text = $"ARM: {stats.ARM}";
            memberEquipSPD.text = $"SPD: {stats.SPD}";
        }
    }
}
