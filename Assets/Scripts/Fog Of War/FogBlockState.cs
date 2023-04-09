using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FogState
{
    Unexplored,
    ExploredActive,
    ExploredInactive
}

public class FogBlockState : MonoBehaviour
{
    public FogState state;
}
