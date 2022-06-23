using System;
using Unity.Netcode;
using UnityEngine;

// NetworkTransform컴포넌트가 싱크를 자동으로 잘 맞추는지 확인용
// 규칙적으로 플레이어를 움직임
public class NetworkTransformTest : NetworkBehaviour
{
    void Update()
    {
        if (IsServer)
        {
            float theta = Time.frameCount / 10.0f;
            transform.position = new Vector3((float)Math.Cos(theta), 0.0f, (float)Math.Sin(theta));
        }
    }
}