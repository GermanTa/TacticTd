using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightning : MonoBehaviour
{
    public GameObject lineRendererPrefab;

    //длина цепи
    public int chainLength;
    //ширина молнии
    public int zipperWidth;
    public int segmentsLine;
    public float lineWidth;
    

    public LineRenderer[] lineRenderers { get; set; }
    void Start()
    {
        lineRenderers = new LineRenderer[1];
        for (int i = 0; i < 1; i++)
        {
            LineRenderer lineRenderer = lineRendererPrefab.GetComponent<LineRenderer>();

            lineRenderer.SetWidth(lineWidth, lineWidth);
            lineRenderer.SetVertexCount(segmentsLine);
            

            lineRenderers[i] = Instantiate(lineRenderer);
        }



        StartCoroutine(test());
    }

    public void DrawLightning()
    {
        var segments = segmentsLine;
        for (int i = 0; i < lineRenderers.Length; i++)
        {
            lineRenderers[i].SetPosition(0, new Vector2(5f,5f));
            Vector2 lastPosition = this.transform.position;
            for (int j = 0; j < segments ; j++)
            {
                Vector2 tmp = Vector2.Lerp(this.transform.position, new Vector2(5f, 5f), (float)j / (float)segments);
                lastPosition = new Vector2(tmp.x + Random.Range(-0.1f, 0.1f), tmp.y + Random.Range(-0.3f, 0.3f));
                lineRenderers[i].SetPosition(j, lastPosition);
            }
            
        }
    }

    private IEnumerator test()
    {
        
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            DrawLightning();
        }
        
    }

    void Update()
    {
        

    }
}
