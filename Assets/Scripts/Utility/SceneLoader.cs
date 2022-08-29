using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    private static int battleSceneBuildIndex = 1;
    private static int savedSceneBuildIndex;
    private static Vector2 savedPlayerLocation;

    public static void LoadBattleScene()
    {
        savedSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        savedPlayerLocation = Game.Player.CurrentCell.Center2D();
        //Game.Player.gameObject.SetActive(false);
        SceneManager.LoadScene(battleSceneBuildIndex);
        SceneManager.sceneLoaded += DisablePlayerObject;
    }

    public static void ReloadSavedSceneAfterBattle()
    {
        SceneManager.sceneLoaded += RestorePlayerPositionAndGameObject;
        SceneManager.LoadScene(savedSceneBuildIndex);
    }

    public static void RestorePlayerPositionAndGameObject(Scene scene, LoadSceneMode mode)
    {
        Game.Player.transform.position = savedPlayerLocation;
        Game.Player.gameObject.SetActive(true);
        SceneManager.sceneLoaded -= RestorePlayerPositionAndGameObject;
    }

    private static void DisablePlayerObject(Scene scene, LoadSceneMode mode)
    {
        Game.Player.gameObject.SetActive(false);
    }

}
