using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    public static GameManagerScript Instance { get; private set; }

    private const string SAVE_CREATED = "SaveCreated";
    private const string COINS_KEY = "Coins";
    private const string UNLOCKED_LEVELS_KEY = "UnlockedLevels";

    // ---------------- LEVEL DATA ----------------

    [SerializeField]
    private List<string> levels = new List<string>
    {
        "Level_1",
        "Level_2",
        "Level_3",
        "Level_4",
        "Level_5"
    };

    // ---------------- TANK DATA ----------------

    List<List<TankData>> TankArray = new List<List<TankData>>()
    {
        new List<TankData>()
        {
            new TankData { tankType = TankType.NormalTank, isActivated = true }
        },
        new List<TankData>()
        {
            new TankData { tankType = TankType.MilitaryTank, isActivated = false }
        },
        new List<TankData>()
        {
            new TankData { tankType = TankType.SnowTank, isActivated = false }
        },
        new List<TankData>()
        {
            new TankData { tankType = TankType.GreenTank, isActivated = false }
        }
    };

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        //ResetAllSaveData();
        //CoinCheat();

        InitializeSave();
        LoadTanksFromPrefs();
    }

    // ---------------- SAVE  ----------------

    private void InitializeSave() {
        if (PlayerPrefs.HasKey(SAVE_CREATED))
            return;

        CreateNewSave();
    }

    private void CreateNewSave() {
        PlayerPrefs.SetInt(SAVE_CREATED, 1);
        PlayerPrefs.SetInt(COINS_KEY, 0);
        PlayerPrefs.SetInt(UNLOCKED_LEVELS_KEY, 1); 

        PlayerPrefs.SetInt(GetTankPrefKey(TankType.NormalTank), 1);

        PlayerPrefs.Save();
    }

    // ---------------- COINS ----------------

    public int GetCoins() {
        return PlayerPrefs.GetInt(COINS_KEY, 0);
    }

    public void AddCoins(int amount) {
        PlayerPrefs.SetInt(COINS_KEY, GetCoins() + amount);
        PlayerPrefs.Save();
    }

    private void CoinCheat() {
        AddCoins(1000);
    }

    // ---------------- LEVEL SYSTEM ----------------

    public int GetUnlockedLevels() {
        return PlayerPrefs.GetInt(UNLOCKED_LEVELS_KEY, 1);
    }

    public void UnlockNextLevel() {
        int unlocked = GetUnlockedLevels();

        if (unlocked < levels.Count) {
            PlayerPrefs.SetInt(UNLOCKED_LEVELS_KEY, unlocked + 1);
            PlayerPrefs.Save();
        }
    }

    public bool IsLevelUnlocked(int levelIndex) {
        return levelIndex <= GetUnlockedLevels();
    }

    public string GetLevelSceneName(int levelIndex) {
        return levels[levelIndex - 1];
    }

    // ================= TANK SYSTEM ===================

    private string GetTankPrefKey(TankType tankType) {
        return $"Tank_{tankType}";
    }

    private void LoadTanksFromPrefs() {
        foreach (var tankList in TankArray) {
            foreach (var tank in tankList) {
                bool defaultUnlocked = tank.tankType == TankType.NormalTank;

                tank.isActivated = PlayerPrefs.GetInt(
                    GetTankPrefKey(tank.tankType),
                    defaultUnlocked ? 1 : 0
                ) == 1;
            }
        }
    }

    public void UnlockTheTank(TankType unlockThisTank) {
        foreach (var tankList in TankArray) {
            foreach (var tank in tankList) {
                if (tank.tankType != unlockThisTank)
                    continue;

                if (tank.isActivated)
                    return;

                tank.isActivated = true;

                PlayerPrefs.SetInt(GetTankPrefKey(unlockThisTank), 1);
                PlayerPrefs.Save();

                Debug.Log($"{unlockThisTank} unlocked");
                return;
            }
        }
    }

    public bool IsTankUnlocked(TankType tankType) {
        foreach (var tankList in TankArray) {
            foreach (var tank in tankList) {
                if (tank.tankType == tankType)
                    return tank.isActivated;
            }
        }

        return false;
    }

    public List<TankData> WhichTanksAreActivated() {
        List<TankData> activatedTanks = new List<TankData>();

        foreach (var tankList in TankArray) {
            foreach (var tank in tankList) {
                if (tank.isActivated)
                    activatedTanks.Add(tank);
            }
        }

        return activatedTanks;
    }

    public void PrintAllTankData() {
        foreach (var tankList in TankArray) {
            foreach (var tank in tankList) {
                Debug.Log($"{tank.tankType} isActivated = {tank.isActivated}");
            }
        }
    }

    private void ResetAllSaveData() {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        CreateNewSave();
        LoadTanksFromPrefs();

        Debug.Log("All save data reset (Editor only)");
    }

}
