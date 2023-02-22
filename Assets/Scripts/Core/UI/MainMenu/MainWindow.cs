using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class MainWindow : MonoBehaviour
    {
        [SerializeField] private MemberEquipmentInfo equipmentInfo;

        void Start()
        {
            ShowDefaultView();
        }

        public void ShowDefaultView()
        {
            foreach(Transform child in transform)
            {
                if (child.GetComponent<PartyMemberInfo>() != null)
                    child.gameObject.SetActive(true);
                else
                    child.gameObject.SetActive(false);
            }
        }

        public void ShowEquipmentView(PartyMember member)
        {
            foreach (Transform child in transform)
            {
                if (child.GetComponent<PartyMemberInfo>() == null)
                    continue;

                else if (child.GetComponent<PartyMemberInfo>().PartyMember != member)
                    child.gameObject.SetActive(false);
            }

            equipmentInfo.SetPartyMember(member);
            equipmentInfo.gameObject.SetActive(true);
        }
    }
}
