using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public Material material1;
    public Material material2;
    public float duration = 2.0F;
    public Renderer rend;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = material1;
    }
    void Update()
    {
        //float lerp = Mathf.PingPong(Time.time, duration) / duration;
        float lerp = Mathf.Lerp(0, 1, Time.deltaTime * 10f);
        rend.material.Lerp(material1, material2, lerp);
    }
}
