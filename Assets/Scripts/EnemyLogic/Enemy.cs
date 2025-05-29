using UnityEngine;

namespace EnemyLogic
{
    [RequireComponent(typeof(HealthController))]
    public class Enemy : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<HealthController>().OnDie += (sender, args) => { Destroy(gameObject); };
        }
    }
}