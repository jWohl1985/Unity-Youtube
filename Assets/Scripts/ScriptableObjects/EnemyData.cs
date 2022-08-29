using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Data", menuName = "New Enemy Data")]
public class EnemyData : ScriptableObject
{
    [SerializeField] private GameObject actorPrefab;
    [SerializeField] private BattleStats stats;

    public GameObject ActorPrefab => actorPrefab;
    public BattleStats Stats => stats;
}
