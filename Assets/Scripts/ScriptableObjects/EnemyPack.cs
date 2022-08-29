using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Pack", menuName = "New Enemy Pack")]
public class EnemyPack : ScriptableObject
{
    [SerializeField] private List<EnemyData> enemies;
    [SerializeField] private List<float> xSpawnCoordinates;
    [SerializeField] private List<float> ySpawnCoordinates;

    public List<EnemyData> Enemies => enemies;
    public IReadOnlyList<float> XSpawnCoordinates => xSpawnCoordinates;
    public IReadOnlyList<float> YSpawnCoordinates => ySpawnCoordinates;
}
