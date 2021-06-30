using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : MonoBehaviour
{
    public Lint speed = 1;

    private void Update()
    {
        GetComponent<LintTransform>().radians.y += speed;
        
        //TODO Move in my forward direction
    }
}
