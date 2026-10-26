using UnityEngine;
using DG.Tweening;

public class MovingObstacles : MonoBehaviour
{
    [SerializeField] private Vector3 verticalMovement = new Vector3(0,3,0);
    [SerializeField] private float duration = 2f;
    [SerializeField] private float delay = 0f;

    void Start()
    {
        transform.DOLocalMove(transform.localPosition + verticalMovement, duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .SetDelay(delay);
    }
}
