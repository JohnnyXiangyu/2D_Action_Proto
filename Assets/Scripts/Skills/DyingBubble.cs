using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DyingBubble : MonoBehaviour
{
    private float timer = 0;
    public float duration = 0;

    private void Start() {
        timer = Time.time;
    }

    void Update()
    {
        if (Time.time - timer >= duration) {
            Destroy(gameObject);
        }
    }
}
