using System.Collections.Generic;
using System.Collections;
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
        checkpoint.offset = wave.offset;
    }

    void SyncToWave(Checkpoint checkpoint) {
        wave.amplitude = checkpoint.amplitude;
        wave.frequency = checkpoint.frequency;
        wave.shape = checkpoint.shape;
        wave.phase = checkpoint.phase;
        wave.offset = checkpoint.offset;
    }

    public void MoveToLastCheckpoint() {
        if (!firstCheckpointReached) {
            wave.shape = initialShape;
            wave.amplitude = initialAmplitude;
            wave.frequency = initialFrequency;
            wave.phase = initialPhase;
            wave.offset = initialOffset;
            Camera.main.GetComponent<AudioSource>().time = 0f;
        } else {
            SyncToWave(checkpoints[0]);
            Camera.main.GetComponent<AudioSource>().time = wave.offset / wave.speed;
			StartCoroutine(slowTimeAfterSpawn());
        }
    }

	const float slowDuration = 2;
	IEnumerator slowTimeAfterSpawn(){
		if(Time.timeScale>0){
		float prevFixed = Time.fixedDeltaTime;
		Time.timeScale = 0.65f;
		Time.fixedDeltaTime *= 0.65f;
		float curtime = 0;
		while(curtime<slowDuration){
			curtime += Time.deltaTime / Time.timeScale;
			yield return null;
		}
		Time.timeScale = 1;
		Time.fixedDeltaTime = prevFixed;
		}
	}
}
