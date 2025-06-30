using UnityEngine;
using System.Collections;

public class MuzzleFlash : MonoBehaviour
{
    public GameObject muzzleFlashObject;
    public float flashDuration = 0.1f;

    public void PlayFlash()
    {
        if (muzzleFlashObject != null)
        {
            StartCoroutine(FlashOnce());
        }
    }

    IEnumerator FlashOnce()
    {
        muzzleFlashObject.SetActive(true);
        yield return new WaitForSeconds(flashDuration);
        muzzleFlashObject.SetActive(false);
    }
}
