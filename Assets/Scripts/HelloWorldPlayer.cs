
using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    // 플레이어 움직임 동기화 테스트
    // 플레이어 프리팹에 부착
    // MonoBehaviour가 아닌 NetworkBehaviour를 상속받음
    public class HelloWorldPlayer : NetworkBehaviour
    {
        // 네트워크 동기화를위한 포지션 변수
        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();


        // NetworkObject컴포넌트를 달고있는 오브젝트가 생성되고 네트워크에 연결되면 호출됨
        public override void OnNetworkSpawn()
        {
            Move();
        }

        public void Move()
        {
            // 이 프로세스가 서버(호스트)라면 이쪽을 실행
            if (NetworkManager.Singleton.IsServer)
            {
                // 플레이어의 Unity 포지션을 변경해줌
                var randomPosition = GetRandomPositionOnPlane();
                transform.position = randomPosition;

                // 자동 동기화되는 NetworkVariable에 Value로 현재 포지션을 넣어줌
                Position.Value = randomPosition;
            }
            // 클라이언트라면 RPC함수를 호출
            else
            {
                SubmitPositionRequestServerRpc();
            }
        }

        // RPC함수 정의
        [ServerRpc]
        void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            // 서버(호스트)의 프로세스에있는 이 플레이어의 포지션값을 보내줌
            // NetworkVariable의 Value에 넣으면 자동으로 동기화 되는듯
            Position.Value = GetRandomPositionOnPlane();
        }

        static Vector3 GetRandomPositionOnPlane()
        {
            return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
        }

        void Update()
        {
            // 동기화된 포지션값을 바탕으로 Unity Transform.position을 업데이트해줌
            transform.position = Position.Value;
        }
    }
}