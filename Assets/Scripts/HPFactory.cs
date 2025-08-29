using System.Collections.Generic;
using UnityEngine;

public class HPFactory : MonoBehaviour
{
    private static Queue<HPController> _hpPool = new Queue<HPController>();

    public static HPController GetHpController()
    {
        if (!_hpPool.TryDequeue(out HPController hp))
        {
            hp = Resources.Load<HPController>("UI/HP");
        }

        return hp;
    }

    public static void ReleaseHpController(HPController hpController)
    {
        hpController.gameObject.SetActive(false);
        _hpPool.Enqueue(hpController);
    }
}
