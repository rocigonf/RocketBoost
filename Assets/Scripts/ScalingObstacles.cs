using DG.Tweening;
using UnityEngine;

public class ScalingObstacles : MonoBehaviour
{
    [SerializeField] private Vector3 sizeScaling = new Vector3(1,1,1);
    [SerializeField] private float duration = 2f;

    void Start()
    {
        transform.DOScale(sizeScaling, duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
