using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpotScript : MonoBehaviour
{
    [SerializeField] private GameObject exitSpot;
    public GameObject GetExitSpot() { return exitSpot; }
}
