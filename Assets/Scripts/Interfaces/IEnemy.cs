using UnityEngine;

namespace Interfaces
{
    public interface IEnemy : IDamagable
    {
        public void MoveAlongWayPoints(Transform[] wayPoints);
        public void DestroySelf();
        public void DetectPlayer();
        public void ChasePlayer();
        public float GetEnemyHealthPercentage();
    }
    public interface IPickable
    {
        public Vector3 GetLocalPositionVector();
    }
}
