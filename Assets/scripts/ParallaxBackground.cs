using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxInfiniteParent : MonoBehaviour
{
    [Header("核心设置")]
    public Camera targetCamera;
    [Range(0f, 1f)] public float parallaxX = 0.5f;
    [Range(0f, 1f)] public float parallaxY = 0.5f;

    [Header("无限循环设置")]
    public bool enableInfiniteX = true;
    public bool enableInfiniteY = true;

    [Header("背景单元尺寸 (必须准确)")]
    public Vector2 manualTileSize = Vector2.zero;

    // 私有变量
    private Transform _camTrans;
    private Vector3 _startCamPos;    // 记录游戏开始时相机的位置
    private Vector3 _startBgPos;     // 记录游戏开始时背景的位置
    private float _tileWidth;
    private float _tileHeight;

    void Start()
    {
        // 1. 初始化
        if (targetCamera == null) targetCamera = Camera.main;
        if (targetCamera == null) { Debug.LogError("找不到相机！请赋值。"); enabled = false; return; }

        _camTrans = targetCamera.transform;
        _startCamPos = _camTrans.position; // 记下起点，这个永远不变
        _startBgPos = transform.position;   // 记下背景起点，这个也永远不变

        // 2. 获取尺寸
        CalculateSize();
    }

    void CalculateSize()
    {
        if (manualTileSize != Vector2.zero)
        {
            _tileWidth = manualTileSize.x;
            _tileHeight = manualTileSize.y;
            return;
        }

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            _tileWidth = sr.bounds.size.x;
            _tileHeight = sr.bounds.size.y;
        }
        else
        {
            Debug.LogError("请手动填写 Manual Tile Size！");
        }
    }

    void LateUpdate()
    {
        if (_camTrans == null) return;

        // 1. 计算相机从游戏开始到现在，总共移动了多少距离
        Vector3 camTotalMovement = _camTrans.position - _startCamPos;

        // 2. 计算背景如果不循环，应该在的视差位置
        Vector3 parallaxPosition = _startBgPos;
        parallaxPosition.x += camTotalMovement.x * parallaxX;
        parallaxPosition.y += camTotalMovement.y * parallaxY;

        // 3. 【核心算法】修正方向：将减号改为加号
        Vector3 finalPosition = parallaxPosition;

        if (enableInfiniteX && _tileWidth > 0)
        {
            float realMoveX = camTotalMovement.x * (1 - parallaxX);
            int numTilesX = Mathf.RoundToInt(realMoveX / _tileWidth);
            // 这里改成 += 
            finalPosition.x += numTilesX * _tileWidth;
        }

        if (enableInfiniteY && _tileHeight > 0)
        {
            float realMoveY = camTotalMovement.y * (1 - parallaxY);
            int numTilesY = Mathf.RoundToInt(realMoveY / _tileHeight);
            // 这里改成 +=
            finalPosition.y += numTilesY * _tileHeight;
        }

        // 4. 应用最终位置
        transform.position = finalPosition;
    }
}