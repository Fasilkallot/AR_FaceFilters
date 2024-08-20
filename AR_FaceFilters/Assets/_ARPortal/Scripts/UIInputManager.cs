using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class UIInput : MonoBehaviour
{
    public GameObject DoorToSpawn;
    private ARRaycastManager _raycastManager;
    private ARPlaneManager _planeManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    //private Text _debugText;
    private bool _spawned = false;

    private void Awake()
    {
        /*_debugText = GetComponentInChildren<Text>();

        if (_debugText == null)
        {
            Debug.LogError("Text component not found in children.");
        }
        else
        {
            _debugText.text = "Application Started";
        }*/
        _raycastManager = GetComponent<ARRaycastManager>();
        _planeManager = GetComponent<ARPlaneManager>();
    }

    private void Update()
    {
        if (_raycastManager == null)
        {
            Debug.LogError("ARRaycastManager is not assigned.");
            return;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Optionally check for specific touch phases
            if (touch.phase == TouchPhase.Began)
            {
                Vector2 touchPosition = touch.position;

                hits.Clear(); // Clear previous results

                if (_raycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
                {
                    // Show number of hits
                   // _debugText.text = $"Hits: {hits.Count}";

                    if (hits.Count > 0)
                    {
                        Pose hitPose = hits[0].pose;
                        // Show the hit position
                        //_debugText.text += $"\nPosition: {hitPose.position}";

                        if (!_spawned)
                        {
                            Instantiate(DoorToSpawn, hitPose.position, hitPose.rotation);
                            _spawned = true;
                            _planeManager.SetTrackablesActive(false);
                            _planeManager.enabled = false;
                        }
                    }
                }
                /*else
                {
                    // Show a message when no hits are detected
                    _debugText.text = "No hit detected";
                }*/
            }
        }
    }
}
