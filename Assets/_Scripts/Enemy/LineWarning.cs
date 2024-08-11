using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineWarning : MonoBehaviour
{
    Renderer mRenderer;

    private void Awake()
    {
        this.mRenderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        StartCoroutine(Blynk());
        Destroy(gameObject, 3f);
    }


    IEnumerator Blynk()
    {
        while (true)
        {
            this.mRenderer.enabled = false;
            yield return new WaitForSeconds(0.4f);
            this.mRenderer.enabled = true;
            yield return new WaitForSeconds(0.4f);
        }
    }

}
