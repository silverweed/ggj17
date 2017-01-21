using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour {

    Electron trackedObject;
    Wave wave;
    List<Checkpoint> checkpoints = new List<Checkpoint>();
    bool firstCheckpointReached = false;

    void Awake() {
        trackedObject = GameObject.FindObjectOfType<Electron>();
        wave = GameObject.FindObjectOfType<Wave>();
        foreach (Transform child in transform) { checkpoints.Add(child.gameObject.GetComponent<Checkpoint>()); }
        checkpoints.Sort(CompareByAxisX);
    }

    static int CompareByAxisX(Checkpoint a, Checkpoint b) {
        var aX = a.transform.position.x;
        var bX = b.transform.position.x;
        return aX < bX ? -1 : aX > bX ? 1 : 0;
    }

    void Update() {
        if (!firstCheckpointReached) {
            if (!trackedObject.currentlyDestroyed && trackedObject.transform.position.x >= checkpoints[0].transform.position.x) {
                firstCheckpointReached = true;
            }
        } else if (checkpoints.Count > 1) {
            if (!trackedObject.currentlyDestroyed && trackedObject.transform.position.x >= checkpoints[1].transform.position.x) {
                checkpoints.RemoveAt(0);
            }
        }
    }

    public void MoveToLastCheckpoint() {
        if (!firstCheckpointReached) {
            wave.phase = 0;
            wave.offset = 0;
        } else {
            var data = checkpoints[0];
            wave.amplitude = data.amplitude;
            wave.frequency = data.frequency;
            wave.shape = data.shape;
            wave.phase = data.phase;
            wave.offset = data.transform.position.x;
        }
    }
}
