using System.Collections.Generic;
using UnityEngine;

public class LevelData : MonoBehaviour {

    public static LevelData Instance { get; private set; }

    private void Awake() {

        Instance = this;
    }

    public enum LevelId {

        Level_1 = 3,
        Level_2 = 4,
        Level_3 = 5,
        Level_4 = 6,
        Level_5 = 7

    }

    public static List<int> coinsPerLevel = new List<int> {
        20, 
        25, 
        30, 
        35, 
        40  
    };

}





/*
using System.Collections.Generic;

var LevelValues = new Dictionary<LevelId, int>
{
    { LevelId.Level_1, 3 },
    { LevelId.Level_2, 4 },
    { LevelId.Level_3, 5 },
    { LevelId.Level_4, 6 },
    { LevelId.Level_5, 7 }
};




public enum LevelId {

    Level_1 = 3,
    Level_2 = 4,
    Level_3 = 5,
    Level_4 = 6,
    Level_5 = 7

}

*/