using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemyTankDestructionLevels : MonoBehaviour
{
    private BaseEnemyTankHealth baseEnemyTankHealth;

    [SerializeField] private GameObject destructedTankSprite;
    [SerializeField] private GameObject destructionLevelOne;
    [SerializeField] private GameObject destructionLevelTwo;
    [SerializeField] private GameObject fullHealthSprite;
    private void Start()
    {
        baseEnemyTankHealth = BaseEnemyTankHealth.Instance.GetComponent<BaseEnemyTankHealth>();
        baseEnemyTankHealth.onHealthLevelReachedSixty += BaseEnemyTankHealth_onHealthLevelReachedSixty; ;
        baseEnemyTankHealth.onHealthLevelReachedFourty += BaseEnemyTankHealth_onHealthLevelReachedFourty; ;
        baseEnemyTankHealth.onHealthLevelReachedEighty += BaseEnemyTankHealth_onHealthLevelReachedEighty; ;
        baseEnemyTankHealth.onHealthLevelReachedZero += BaseEnemyTankHealth_onHealthLevelReachedZero; ;
    }
    private void Update()
    {
        
    }

    private void BaseEnemyTankHealth_onHealthLevelReachedZero(object sender, System.EventArgs e)
    {
        //ShowDestructedTankSprite();
    }
    private void BaseEnemyTankHealth_onHealthLevelReachedFourty(object sender, System.EventArgs e)
    {
        //SetDestructionLevelTwo();
    }
    private void BaseEnemyTankHealth_onHealthLevelReachedSixty(object sender, System.EventArgs e)
    {
        //SetDestructionLevelOne();
    }


    private void BaseEnemyTankHealth_onHealthLevelReachedEighty(object sender, System.EventArgs e)
    {
        //ShowFullHealthSprite();
    }

    private void ShowDestructedTankSprite()
    {
        destructedTankSprite.SetActive(true);
        fullHealthSprite.SetActive(false);
        destructionLevelOne.SetActive(false);
        destructionLevelTwo.SetActive(false);
    }

    private void ShowFullHealthSprite()
    {
        fullHealthSprite.SetActive(true);
        destructedTankSprite.SetActive(false);
        destructionLevelOne.SetActive(false);
        destructionLevelTwo.SetActive(false);
    }

    private void SetDestructionLevelOne()
    {
        fullHealthSprite.SetActive(false);
        destructionLevelOne.SetActive(true);
        destructionLevelTwo.SetActive(false);
        destructedTankSprite.SetActive(false);

    }
    private void SetDestructionLevelTwo()
    {
        fullHealthSprite.SetActive(false);
        destructionLevelOne.SetActive(false);
        destructionLevelTwo.SetActive(true);
        destructedTankSprite.SetActive(false);
    }



}
