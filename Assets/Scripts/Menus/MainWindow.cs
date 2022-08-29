using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWindow : MonoBehaviour
{
    [SerializeField] private GameObject partyMemberInfoPrefab;

    void Start()
    {
        GeneratePartyMemberInfo();
    }

    private void GeneratePartyMemberInfo()
    {
        foreach (PartyMember member in Party.ActiveMembers)
        {
            Instantiate(partyMemberInfoPrefab, this.gameObject.transform);
        }
    }
}
