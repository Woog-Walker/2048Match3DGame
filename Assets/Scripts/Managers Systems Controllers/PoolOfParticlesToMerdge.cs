using System.Collections.Generic;
using UnityEngine;

namespace DiceGame.Mechanics.PoolOfVFX
{
    public class PoolOfParticlesToMerdge : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> listOfMerdgeVfxs = new List<ParticleSystem>();

        // Simple pool based on inactive VFX objects.
        // When we need one — we check for an inactive one, activate it and return.
        // When returning VFX — we disable it and return it to this GameObject's transform.

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
}
