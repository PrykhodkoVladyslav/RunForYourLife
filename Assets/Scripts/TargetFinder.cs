using System.Collections;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    [SerializeField] private int findingIntervalInSeconds;
    private ChasingController _chasingController;

    private void Start()
    {
        _chasingController = GetComponent<ChasingController>();

        StartCoroutine(FindTarget());
    }

    private IEnumerator FindTarget()
    {
        while (true)
        {
            _chasingController.Target = GetClosestPlayer()?.transform;

            yield return new WaitForSeconds(findingIntervalInSeconds);
        }
    }

    private GameObject GetClosestPlayer()
    {
        var players = GameObject.FindGameObjectsWithTag("Player");
        GameObject closestPlayer = null;

        var minDistance = Mathf.Infinity;

        foreach (var player in players)
        {
            var distance = Vector3.Distance(transform.position, player.transform.position);

            if (!(distance < minDistance))
                continue;

            minDistance = distance;
            closestPlayer = player;
        }

        return closestPlayer;
    }
}