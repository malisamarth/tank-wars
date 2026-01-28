using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestructionLevelScript : MonoBehaviour
{
    [SerializeField] private GameObject defaultNormalVisual;
    [SerializeField] private GameObject destructionLevelOne;
    [SerializeField] private GameObject destructionLevelTwo;

    private PlayerHealthBar playerHealthBar;

    private void Start()
    {
        playerHealthBar = PlayerHealthBar.Instance.GetComponent<PlayerHealthBar>();
        playerHealthBar.onHealthLevelReachedSixty += PlayerHealthBar_onHealthLevelReachedSixty;
        playerHealthBar.onHealthLevelReachedFourty += PlayerHealthBar_onHealthLevelReachedFourty;
        playerHealthBar.onHealthLevelReachedEighty += PlayerHealthBar_onHealthLevelReachedEighty;
    }

    private void PlayerHealthBar_onHealthLevelReachedEighty(object sender, System.EventArgs e)
    {
        SetDefaultVisual();
    }

    private void PlayerHealthBar_onHealthLevelReachedFourty(object sender, System.EventArgs e)
    {
        SetDestructionLevelTwo();
    }

    private void PlayerHealthBar_onHealthLevelReachedSixty(object sender, System.EventArgs e)
    {
        SetDestructionLevelOne();
    }

    private void SetDestructionLevelOne()
    {
        defaultNormalVisual.SetActive(false);
        destructionLevelOne.SetActive(true);
        destructionLevelTwo.SetActive(false);

    }

    private void SetDestructionLevelTwo()
    {
        defaultNormalVisual.SetActive(false);
        destructionLevelOne.SetActive(false);
        destructionLevelTwo.SetActive(true);
    }

    private void SetDefaultVisual()
    {
        defaultNormalVisual.SetActive(true);
        destructionLevelOne.SetActive(false);
        destructionLevelTwo.SetActive(false);
    }

}
