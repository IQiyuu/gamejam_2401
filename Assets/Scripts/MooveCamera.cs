using UnityEngine;

public class MooveCamera : MonoBehaviour
{
    public GameObject player;
    private Vector3 playerpos;
    private Vector3 camerapos;
    private bool canMove = true;

    void Update()
    {
        CameraPosition();
        Vector3 playerPosition = player.transform.position;
       if (canMove)
       {
        if (playerPosition.x + 15 < camerapos.x)
            CameraMoovePos(0);
        if (playerPosition.x - 15 > camerapos.x)
            CameraMoovePos(1);
        if (playerPosition.y + 8 < camerapos.y)
            CameraMoovePos(2);
        if (playerPosition.y - 8 > camerapos.y)
             CameraMoovePos(3);
       }
    }

    void Start()
    {
        CameraPosition();
    }


    void CameraMoovePos(int pos)
    {
        if (!canMove) 
            return;
        if (pos == 0)
            transform.position += Vector3.left * 30;
        else if (pos == 1)
            transform.position += Vector3.right * 30;
        else if (pos == 2)
            transform.position += Vector3.down * 17;
        else if (pos == 3)
            transform.position += Vector3.up * 17;
        StartCoroutine(DisableMovementTemporarily());
    }
    void CameraPosition()
    {
        camerapos = transform.position;
    }

    private System.Collections.IEnumerator DisableMovementTemporarily()
    {
        canMove = false;            
        yield return new WaitForSeconds(1f);
        canMove = true;             
    }
}