using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] Material aliveMaterial;
    [SerializeField] Material deadMaterial;

    [SerializeField] public bool isAlive;

    SpriteRenderer spriteRenderer;

    Coroutine lerpColorCoroutine;

    public int aliveNeighbors;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive && spriteRenderer.material.color != deadMaterial.color)
        {
            ChangeStatusColor(deadMaterial);
        }

        if(isAlive && spriteRenderer.material.color != aliveMaterial.color)
        {
            ChangeStatusColor(aliveMaterial);
        }
    }

    void ChangeStatusColor(Material newStatus)
    {
        if(lerpColorCoroutine == null)
        {
            lerpColorCoroutine = StartCoroutine(LerpColorCoroutine(newStatus));
        }
    }

    IEnumerator LerpColorCoroutine(Material newStatus)
    {
        Color initialColor = spriteRenderer.material.color;
        float t = 0;

        float duration = .25f;

        while(t<duration)
        {
            spriteRenderer.material.color = Color.Lerp(initialColor, newStatus.color, t/duration);
            t += Time.deltaTime;

            yield return null;
        }

        spriteRenderer.material.color = newStatus.color;

        lerpColorCoroutine = null;
    }

    private void OnMouseDown()
    {
        isAlive = !isAlive;
    }
}
