using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTurnOff : MonoBehaviour
{
    void OnEnable()
    {
        StartCoroutine(nameof(CheckIfAlive));
    }

    IEnumerator CheckIfAlive()
    {
        ParticleSystem ps = this.GetComponent<ParticleSystem>();

        while (true && ps != null)
        {
            yield return new WaitForSeconds(1);
            if (!ps.IsAlive(true))
            {
                this.gameObject.SetActive(false);
            }

            break;
        }
    }
}