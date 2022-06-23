
using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    // �÷��̾� ������ ����ȭ �׽�Ʈ
    // �÷��̾� �����տ� ����
    // MonoBehaviour�� �ƴ� NetworkBehaviour�� ��ӹ���
    public class HelloWorldPlayer : NetworkBehaviour
    {
        // ��Ʈ��ũ ����ȭ������ ������ ����
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();


        // NetworkObject������Ʈ�� �ް��ִ� ������Ʈ�� �����ǰ� ��Ʈ��ũ�� ����Ǹ� ȣ���
        public override void OnNetworkSpawn()
        {
            Move();
        }

        public void Move()
        {
            // �� ���μ����� ����(ȣ��Ʈ)��� ������ ����
            if (NetworkManager.Singleton.IsServer)
            {
                // �÷��̾��� Unity �������� ��������
                var randomPosition = GetRandomPositionOnPlane();
                transform.position = randomPosition;

                // �ڵ� ����ȭ�Ǵ� NetworkVariable�� Value�� ���� �������� �־���
                Position.Value = randomPosition;
            }
            // Ŭ���̾�Ʈ��� RPC�Լ��� ȣ��
            else
            {
                SubmitPositionRequestServerRpc();
            }
        }

        // RPC�Լ� ����
        [ServerRpc]
        void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            // ����(ȣ��Ʈ)�� ���μ������ִ� �� �÷��̾��� �����ǰ��� ������
            // NetworkVariable�� Value�� ������ �ڵ����� ����ȭ �Ǵµ�
            Position.Value = GetRandomPositionOnPlane();
        }

        static Vector3 GetRandomPositionOnPlane()
        {
            return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
        }

        void Update()
        {
            // ����ȭ�� �����ǰ��� �������� Unity Transform.position�� ������Ʈ����
            transform.position = Position.Value;
        }
    }
}