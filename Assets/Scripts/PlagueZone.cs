using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlagueZone : MonoBehaviour
{
    // Start is called before the first frame update
    public PlagueCamp Camp;
    public Transform UpPoint;
    public Transform DownPoint;

    public enum PlagueCamp
    {
        SICK,
        RIVER,
        SAFE,
    }
}
