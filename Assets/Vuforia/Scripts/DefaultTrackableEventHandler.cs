/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using Vuforia;
using System.Collections;
using System.Collections.Generic;


/// <summary>
///     A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{   
    public AudioSource soundTarget;
        public AudioClip clipTarget; 
        private AudioSource[] allAudioSources;
	
	//function to stop all sounds
        void StopAllAudio()
        {
            allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
            foreach (AudioSource audioS in allAudioSources)
            {
                audioS.Stop();
            }
        }

        //function to play sound
        void playSound(string ss)
        {
            clipTarget = (AudioClip)Resources.Load(ss);
            soundTarget.clip = clipTarget;
            soundTarget.loop = true;
            soundTarget.playOnAwake = false;
            soundTarget.Play();
        }


    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);

        soundTarget = (AudioSource)gameObject.AddComponent<AudioSource>();
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NOT_FOUND)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Enable rendering:
        foreach (var component in rendererComponents)
            component.enabled = true;

        // Enable colliders:
        foreach (var component in colliderComponents)
            component.enabled = true;

        // Enable canvas':
        foreach (var component in canvasComponents)
            component.enabled = true;

        if (mTrackableBehaviour.TrackableName == "2000-note-front" || mTrackableBehaviour.TrackableName == "2000-note-back")
            {
                playSound("sounds/2000");
            }

        if (mTrackableBehaviour.TrackableName == "500-note-front" || mTrackableBehaviour.TrackableName == "500-note-back")
            {
                playSound("sounds/500");
            }
        if (mTrackableBehaviour.TrackableName == "200-note-front" || mTrackableBehaviour.TrackableName == "200-note-back")
            {
                playSound("sounds/200");
            }

        if (mTrackableBehaviour.TrackableName == "100_rs_note_obverse" || mTrackableBehaviour.TrackableName == "100_rs_note_reverse")
            {
                playSound("sounds/100");
            }
        if (mTrackableBehaviour.TrackableName == "50-note-front" || mTrackableBehaviour.TrackableName == "50-note-back")
            {
                playSound("sounds/50");
            }

        if (mTrackableBehaviour.TrackableName == "20-note-front" || mTrackableBehaviour.TrackableName == "20-note-back")
            {
                playSound("sounds/20");
            }
        if (mTrackableBehaviour.TrackableName == "10-note-front" || mTrackableBehaviour.TrackableName == "10-note-back")
            {
                playSound("sounds/10");
            }

        if (mTrackableBehaviour.TrackableName == "5-note-front" || mTrackableBehaviour.TrackableName == "5-note-back")
            {
                playSound("sounds/5");
            }

    }


    protected virtual void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;

        StopAllAudio();
    }

    #endregion // PROTECTED_METHODS
}
