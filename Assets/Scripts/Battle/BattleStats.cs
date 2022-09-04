using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BattleStats
{
    private const int MAXIMUM_POSSIBLE_LEVEL = 99;
    private const int MAXIMUM_POSSIBLE_HP = 999;
    private const int MAXIMUM_POSSIBLE_STR = 99;
    private const int MAXIMUM_POSSIBLE_ARM = 99;
    private const int MAXIMUM_POSSIBLE_SPD = 99;

    [SerializeField] private int level;
    [SerializeField] private int hp;
    [SerializeField] private int maxhp;
    [SerializeField] private int str;
    [SerializeField] private int arm;
    [SerializeField] private int spd;

    public BattleStats(int level, int hp, int maxhp, int str, int arm, int spd)
    {
        this.level = level;
        this.hp = hp;
        this.maxhp = maxhp;
        this.str = str;
        this.arm = arm;
        this.spd = spd;
    }

    public int Initiative => SPD + Random.Range(-1, 2);

    public int Level
    {
        get => level;
        set
        {
            level = Mathf.Clamp(value, 1, MAXIMUM_POSSIBLE_LEVEL);
        }
    }

    public int HP
    {
        get => hp;
        set
        {
            hp = Mathf.Clamp(value, 0, maxhp);
        }
    }

    public int MaxHP
    {
        get => maxhp;
        set
        {
            maxhp = Mathf.Clamp(value, 1, MAXIMUM_POSSIBLE_HP);
        }
    }

    public int STR
    {
        get => str;
        set
        {
            str = Mathf.Clamp(value, 0, MAXIMUM_POSSIBLE_STR);
        }
    }

    public int ARM
    {
        get => arm;
        set
        {
            arm = Mathf.Clamp(value, 0, MAXIMUM_POSSIBLE_ARM);
        }
    }

    public int SPD
    {
        get => spd;
        set
        {
            spd = Mathf.Clamp(value, 0, MAXIMUM_POSSIBLE_SPD);
        }
    }
}
