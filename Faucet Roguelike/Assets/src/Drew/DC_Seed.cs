using System.IO;
using UnityEngine;

public class DC_Seed : MonoBehaviour
{   
    private long seed;
    
    public void GenerateSeed()
    {
        seed = (SystemInfo.deviceUniqueIdentifier.GetHashCode() + System.DateTime.UtcNow.ToFileTimeUtc());
        // Adds a hash of the HWID with the current system time as expressed as a long.
    }
    
    public long GetSeed()
    {
        return seed;
    }

    public void SetSeed(long s)
    {
        seed = s;
    }
}
