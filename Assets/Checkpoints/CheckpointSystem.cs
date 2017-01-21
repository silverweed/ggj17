using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour {

    Electron trackedObject;
    Wave wave;
    List<Checkpoint> checkpoints = new List<Checkpoint>();
    bool firstCheckpointReached = false;

    Wave.Shape initialShape;
    float initialAmplitude;
    float initialFrequency;
    float initialPhase;
    float initialOffset;

    void Awake() {
        trackedObject = GameObject.FindObjectOfType<Electron>();
        wave = GameObject.FindObjectOfType<Wave>();
        foreach (Transform child in transform) { checkpoints.Add(child.gameObject.GetComponent<Checkpoint>()); }
        checkpoints.Sort(CompareByAxisX);

        initialShape = wave.shape;
        initialAmplitude = wave.amplitude;
        initialFrequency = wave.frequency;
        initialPhase = wave.phase;
        initialOffset = wave.offset;
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
                SyncWithWave(checkpoints[0]);
            }
        } else if (checkpoints.Count > 1) {
            if (!trackedObject.currentlyDestroyed && trackedObject.transform.position.x >= checkpoints[1].transform.position.x) {
                checkpoints.RemoveAt(0);
                SyncWithWave(checkpoints[0]);
            }
        }
    }

    void SyncWithWave(Checkpoint checkpoint) {
        checkpoint.shape = wave.shape;
        checkpoint.amplitude = wave.amplitude;
        checkpoint.frequency = wave.frequency;
        checkpoint.phase = wave.phase;
    }

    void SyncToWave(Checkpoint checkpoint) {
        wave.amplitude = checkpoint.amplitude;
        wave.frequency = checkpoint.frequency;
        wave.shape = checkpoint.shape;
        wave.phase = checkpoint.phase;
        wave.offset = checkpoint.transform.position.x;
    }

    public void MoveToLastCheckpoint() {
        if (!firstCheckpointReached) {
            wave.phase = initialPhase;
            wave.offset = initialOffset;
            wave.shape = initialShape;
            wave.amplitude = initialAmplitude;
            wave.frequency = initialFrequency;
            Camera.main.GetComponent<AudioSource>().time = 0f;
        } else {
            SyncToWave(checkpoints[0]);
            Camera.main.GetComponent<AudioSource>().time = wave.offset / wave.speed;
        }
    }
}
