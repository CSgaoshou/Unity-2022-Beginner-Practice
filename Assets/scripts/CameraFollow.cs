using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("跟随设置")]
    public Transform target;        // 要跟随的目标（人物）
    public Vector2 offset;          // 相机相对于目标的偏移 (仅X, Y生效)

    [Header("位置限制")]
    public float minY;              // 相机最低Y坐标
    public float maxY;              // 相机最高Y坐标

    [Header("平滑设置")]
    public bool useSmoothing = true;
    public float smoothSpeed = 5f;

    private float _fixedZ;          // 记录相机初始的Z轴位置

    void Start()
    {
        // 记录相机一开始的Z轴位置，保持不变
        _fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (target == null) return;

        // 1. 计算目标位置 (仅X, Y跟随目标并加偏移，Z保持固定)
        Vector3 targetPos = new Vector3(
            target.position.x + offset.x,
            target.position.y + offset.y,
            _fixedZ // Z轴永远不变
        );

        // 2. 限制Y轴高度
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        // 3. 移动相机
        if (useSmoothing)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, smoothSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = targetPos;
        }
    }

    // 绘制调试线
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(
            new Vector3(transform.position.x, minY, _fixedZ),
            new Vector3(transform.position.x, maxY, _fixedZ)
        );
    }
}