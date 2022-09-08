using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;

[CreateAssetMenu(fileName = "New Party Member", menuName = "New Party Member")]
public class PartyMember : ScriptableObject
{
    [SerializeField] private string moniker;
    [SerializeField] private GameObject actorPrefab;
    [SerializeField] private BattleStats stats;
    [SerializeField] private Sprite menuPortrait;
    [SerializeField] private BattlePortrait battlePortrait;
    [SerializeField] private string job = "Some job";

    public string Name => moniker;
    public GameObject ActorPrefab => actorPrefab;
    public BattleStats Stats => stats;
    public Sprite MenuPortrait => menuPortrait;
    public BattlePortrait BattlePortrait => battlePortrait;
    public string Job => job;
}
