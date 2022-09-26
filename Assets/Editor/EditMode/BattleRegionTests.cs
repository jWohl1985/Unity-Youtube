using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Battle;
using Core;

public class BattleRegionTests
{
    private BattleRegion sut = Resources.Load<BattleRegion>("ScriptableObjects/BattleRegions/TestRegion");

    [Test]
    public void Test_region_loaded_successfully()
    {
        Assert.IsNotNull(sut);
    }

    [Test]
    public void Get_pack_returns_an_enemy_pack()
    {
        // Arrange
        EnemyPack pack;

        // Act
        pack = sut.GetRandomEnemyPack();

        // Assert
        Assert.IsNotNull(pack);
        Assert.IsInstanceOf<EnemyPack>(pack);
    }

    [Test]
    public void Can_return_more_than_one_pack()
    {
        // Arrange
        EnemyPack pack1;
        EnemyPack pack2;
        pack1 = sut.GetRandomEnemyPack();
        int i = 0;

        // Act (if we get the same pack 100 times there's probably something wrong!)
        while (i <= 100)
        {
            pack2 = sut.GetRandomEnemyPack();
            i++;

            if (pack1 != pack2)
                Assert.Pass();
        }

        // Assert
        Assert.Fail();
    }
}

