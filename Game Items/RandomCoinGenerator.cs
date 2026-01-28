using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomCoinGenerator : MonoBehaviour {

    [SerializeField] private GameObject coinPrefab;

    private int levelCoinLimit = 0;

    private int curreCoinSpwaned = 0;

    private LevelData levelData;

    private void OnEnable() {
        CoinScript.OnCoinCollected += SpawnCoin;
    }

    private void OnDisable() {
        CoinScript.OnCoinCollected -= SpawnCoin;
    }


    private void Start() {

        levelData = LevelData.Instance;

        LevelCoinLimitSetter();
        SpawnCoin();
    }

    private void SpawnCoin() {

        if (curreCoinSpwaned <= levelCoinLimit) {

            float randomSpwanPostionX = UnityEngine.Random.Range(-1.7f, 1.74f);
            float randomSpwanPostionY = UnityEngine.Random.Range(-4.31f, 4.08f);

            Vector2 randomSpwanCoinPosition = new Vector2(randomSpwanPostionX, randomSpwanPostionY);
            Instantiate(coinPrefab, randomSpwanCoinPosition, transform.rotation);

            curreCoinSpwaned++;

            Debug.Log("Coin Remaining : " + (levelCoinLimit - curreCoinSpwaned));

        } else {

            Debug.Log("Coin Limit " + levelCoinLimit + " Reached!!");

        }

    }

    private void LevelCoinLimitSetter() {

        int currentIndex = -1;

        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;

        foreach (LevelData.LevelId level in Enum.GetValues(typeof(LevelData.LevelId))) {

            currentIndex++;

            int levelChecker = (int)level;

            if (levelChecker == currentLevelIndex) {

                levelCoinLimit = LevelData.coinsPerLevel[currentIndex];

                Debug.Log("Level Coin Limit : " + levelCoinLimit);

                break; 
            }


        }

    }




}
