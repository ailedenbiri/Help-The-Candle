using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItems : MonoBehaviour
{
    
   private GameObject _collectCandle;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectableCandle"))
        {
            Destroy(other.gameObject);
            DOTween.Kill(transform);
            transform.DOScaleY(1.2f, 1).OnComplete(() => {
                transform.DOScaleY(0, 8);
            });

        }
    }

    


}
