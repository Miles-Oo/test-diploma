using UnityEngine;

public class QuestMark : MonoBehaviour
{
    private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 0f, 0);

    public void SetTarget(Transform t)
    {
        target = t;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        transform.position = target.position + offset;
    }
}