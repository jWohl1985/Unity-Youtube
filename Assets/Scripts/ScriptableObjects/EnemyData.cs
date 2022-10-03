using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "New Enemy Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private GameObject actorPrefab;
    [SerializeField] private EnemyStats stats;
    [SerializeField] private BattlePortrait battlePortrait;

    public GameObject ActorPrefab => actorPrefab;
    public BattleStats Stats => stats;
    public BattlePortrait BattlePortrait => battlePortrait;
}
