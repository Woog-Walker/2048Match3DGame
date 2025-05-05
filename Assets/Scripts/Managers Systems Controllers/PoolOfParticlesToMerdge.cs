using System.Collections.Generic;
using UnityEngine;

public class PoolOfParticlesToMerdge : MonoBehaviour
{
    [SerializeField] List<ParticleSystem> listOfMerdgeVfxs = new List<ParticleSystem>();

    // simple pool depends on disabled vfx objects
    // when we check for disabled one and grab object from pool after we activate it
    // when we are going to back vfx back to pool - we disable it - back transform to that parent object

    public ParticleSystem GetVfxFromPool()
    {
        foreach (var vfx in listOfMerdgeVfxs)
        {
            if (!vfx.gameObject.activeInHierarchy)
            {
                vfx.gameObject.SetActive(true);
                return vfx;
            }
        }

        return null;
    }

    public void PutBackVFxToPool(ParticleSystem incVfx) 
    {
        incVfx.gameObject.SetActive(false);
        incVfx.transform.SetParent(transform);
    }
}