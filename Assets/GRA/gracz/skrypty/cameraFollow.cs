using Unity.Cinemachine;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
  [SerializeField] private CinemachineFollow _cinemachineFollow;

   public void CameraFollow(bool czyMaSledzic)
    {
        if(czyMaSledzic){
              _cinemachineFollow.TrackerSettings.PositionDamping=new Vector3(1,1,1);
        }else{
        _cinemachineFollow.TrackerSettings.PositionDamping=new Vector3(0,0,0);
        }
    }
}
