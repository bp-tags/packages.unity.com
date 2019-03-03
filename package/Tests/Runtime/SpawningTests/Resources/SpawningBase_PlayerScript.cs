using NUnit.Framework;
using UnityEngine.Networking;

public class SpawningBase_PlayerScript : NetworkBehaviour
{
    [SyncVar]
    public int intValue;

    [SyncVar]
    public float floatValue;

    public override void OnStartServer()
    {
        Assert.AreEqual(intValue, 999);
        Assert.AreEqual(floatValue, 55.5f);
        SpawningTestBase.IncrementStartServer();
    }

    public override void OnStartClient()
    {
        Assert.AreEqual(intValue, 999);
        Assert.AreEqual(floatValue, 55.5f);
        SpawningTestBase.IncrementStartClient();
    }
}
