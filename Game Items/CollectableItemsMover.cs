using UnityEngine;

public class CollectableItemsMover : MonoBehaviour
{
    private Vector3 targetPosition;
    private float speed = 1.5f;

    private bool reachedTarget = false;

    private void Start()
    {
        targetPosition = RandomPositionGenerator();
    }

    private void Update()
    {
        if (targetPosition != transform.position && !reachedTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            reachedTarget = false;
        }
        else
        {
            reachedTarget = true;
        }

        if (reachedTarget)
        {
            targetPosition = RandomPositionGenerator();
            reachedTarget = false;
        }

    }

    private Vector3 RandomPositionGenerator()
    {
        float randomSpawnPositionX = Random.Range(-1.82f, 2f);
        float randomSpawnPositionY = Random.Range(-0.22f, 4.59f);

        return new Vector3(randomSpawnPositionX, randomSpawnPositionY, transform.position.z);
    }
}
