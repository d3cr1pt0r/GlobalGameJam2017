using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : Singleton<VfxManager> {
    private const string Tag = "GameManager";

    [SerializeField] private Universe Universe;
    [SerializeField] public GameObject CameraHolder;
    [SerializeField] public float Parallax;

    public static Game Instance;

    protected Game() {
    }

    private void Awake() {
        Log.LogDebug(Tag, "Awake");
        Instance = this;

        LevelManager.Instance.SetUniverse(Universe);
        StartGame();
    }

    private void StartGame() {
        LoadCurrentLevel();
    }

    private void PauseGame() {
	
    }

    private void StopGame() {
		
    }

    private void LoadNextLevel() {
		
    }

    private void LoadCurrentLevel() {
        Level level = LevelManager.Instance.GetCurrentLevel();

        if (level != null) {
            GameObject levelObject = Instantiate(level.LevelPrefab, level.LevelPosition, Quaternion.identity);
            levelObject.transform.SetParent(transform, false);
            LevelManager.Instance.CurrentLoadedLevel = levelObject;
        }
    }

    private void UnloadCurrentLevel() {
        if (LevelManager.Instance.CurrentLoadedLevel != null) {
            GameObject.Destroy(LevelManager.Instance.CurrentLoadedLevel);
            LevelManager.Instance.CurrentLoadedLevel = null;
        }
    }

}
