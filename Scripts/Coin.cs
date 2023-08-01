using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnEnable()
    {
        transform.DOLocalJump(new Vector3(transform.position.x, 1f, transform.position.z), 1, 1, .5f);
        transform.DOLocalRotate(new Vector3(0, 0, 360), 1f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental).SetRelative().SetDelay(1f);
    }

    public void Loot()
    {
        PropertyManager.GoldHandle.Invoke(PropertyManager.instance.GoldPerEnemy);
        float time = 1f;
        transform.DOMove(Vector3.zero, time).SetEase(Ease.InElastic).OnComplete(() => gameObject.SetActive(false));
    }

    private void OnDisable()
    {
        transform.DOKill();
    }
}
