using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : AController
{
    public override void onDisabled()
    {
    }

    public override void OnInteract(Player player)
    {
        player.camera.target = this.transform;
        player.camera.offset = 0;
        player.camera.transform.rotation = transform.rotation;
        player.camera.angle.y = transform.eulerAngles.y;
        player.camera.minZoom = 0;
        player.camera.maxZoom = 0;
        base.OnInteract(player);
    }
}