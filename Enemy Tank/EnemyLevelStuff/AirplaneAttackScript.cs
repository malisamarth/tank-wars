using AudienceNetwork;
using System;

using System.Collections.Generic;
using UnityEngine;

public class AirplaneAttackScript : MonoBehaviour
{
    [SerializeField] private List<Vector3> AirplaneStartPositions = new List<Vector3>();
    [SerializeField] private List<Vector3> AirplaneEndPositions = new List<Vector3>();

    [SerializeField] private float damagePerSecond = 25f;

    private AirplaneAttackType selectedAttackType;

    private Vector3 startPositionOfAttack;

    private enum AirplaneAttackType
    {
        TopDownMiddle,
        TopDownLeft,
        TopDownRight,

        DownTopMiddle,
        DownTopLeft,
        DownTopRight,

        RightLeft,
        LeftRight,

        None
    }

    [SerializeField] private float speed = 9f;

    private Vector3 currentEndPosition;
    private bool isAttacking;

    private void Start()
    {
        SetupAttack();   
    }

    private void Update()
    {
        if (isAttacking)
        {
            MoveAirplane(); 
        }

        DestroyAfterOutOfScreen();
    }



    private void OnTriggerEnter2D(Collider2D collision2D) {

        IDamageable damageable = collision2D.GetComponent<IDamageable>();

        Debug.Log("Bombing Lane intrusion : " + collision2D.gameObject.name);


        if (damageable != null) {
            damageable.TakeDamageByAirplaneBombing(damagePerSecond * Time.deltaTime * 2f);
        }

    }


    private void SetupAttack()
    {
        selectedAttackType = RandomAttackType();

        Vector3 startPosition = Vector3.zero;
        Vector3 endPosition = Vector3.zero;
        Vector3 airplaneRotation = Vector3.zero;

        switch (selectedAttackType)
        {
            case AirplaneAttackType.TopDownMiddle:
                startPosition = AirplaneStartPositions[0];
                endPosition = AirplaneEndPositions[0];
                airplaneRotation = Vector3.zero;
                break;

            case AirplaneAttackType.TopDownLeft:
                startPosition = AirplaneStartPositions[3];
                endPosition = AirplaneEndPositions[3];
                airplaneRotation = new Vector3(0f, 0f, 28.91f);
                break;

            case AirplaneAttackType.TopDownRight:
                startPosition = AirplaneStartPositions[1];
                endPosition = AirplaneEndPositions[1];
                airplaneRotation = new Vector3(0f, 0f, -31.53f);
                break;

            case AirplaneAttackType.DownTopMiddle:
                startPosition = AirplaneStartPositions[2];
                endPosition = AirplaneEndPositions[2];
                airplaneRotation = new Vector3(0f, 0f, 180f);
                break;

            case AirplaneAttackType.DownTopLeft:
                startPosition = AirplaneStartPositions[4];
                endPosition = AirplaneEndPositions[4];
                airplaneRotation = new Vector3(0f, 0f, 145.31f);
                break;

            case AirplaneAttackType.DownTopRight:
                startPosition = AirplaneStartPositions[5];
                endPosition = AirplaneEndPositions[5];
                airplaneRotation = new Vector3(0f, 0f, 217.95f);
                break;

            case AirplaneAttackType.RightLeft:
                startPosition = AirplaneStartPositions[6];
                endPosition = AirplaneEndPositions[6];
                airplaneRotation = new Vector3(0f, 0f, 90f);
                break;

            case AirplaneAttackType.LeftRight:
                startPosition = AirplaneStartPositions[7];
                endPosition = AirplaneEndPositions[7];
                airplaneRotation = new Vector3(0f, 0f, -90f);
                break;
        }

        startPositionOfAttack = startPosition;

        transform.position = startPosition;
        transform.eulerAngles = airplaneRotation;

        currentEndPosition = endPosition;
        isAttacking = true;
    }

    private void MoveAirplane()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            currentEndPosition,
            speed * Time.deltaTime
        );
    }

    private void DestroyAfterOutOfScreen()
    {
        if (transform.position == currentEndPosition)
        {
            Destroy(gameObject);
        }
    }

    private AirplaneAttackType RandomAttackType()
    {
        int attackTypeNumber = UnityEngine.Random.Range(1, 8);

        return (AirplaneAttackType)attackTypeNumber;
    }
}
