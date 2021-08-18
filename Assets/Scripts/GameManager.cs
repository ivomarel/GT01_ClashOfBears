using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Color[] teamColors;
    [SerializeField] private int _randomSeed;
    private void Start()
    {
        Random.seed = _randomSeed; //establish a random seed to make the random always the same relative to the seed
    }
}
