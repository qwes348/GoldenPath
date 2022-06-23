using System;
using Unity.Netcode;
using UnityEngine;

// NetworkTransform������Ʈ�� ��ũ�� �ڵ����� �� ���ߴ��� Ȯ�ο�
// ��Ģ������ �÷��̾ ������
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