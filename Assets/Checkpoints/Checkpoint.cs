using UnityEngine;
using System.Collections.Generic;

public class Checkpoint : MonoBehaviour {

    public Wave.Shape shape;
    public HashSet<Wave.Shape> allowedShapes;
    public float amplitude;
    public float frequency;
    public float phase;
    public float offset;
}
