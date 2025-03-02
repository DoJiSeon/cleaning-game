using UnityEngine;
using System.Collections.Generic;

public class RandomObjectSpawner : MonoBehaviour
{
    public GameObject rangeObject; // 소환 범위를 정의하는 오브젝트
    MeshCollider rangeCollider; // 범위 오브젝트의 메쉬 콜라이더
    public GameObject capsul; // 소환할 캡슐 오브젝트
    public int spawnCount = 10; // 소환할 캡슐의 개수

    private List<Vector3> spawnedPositions = new List<Vector3>(); // 생성된 위치 목록

    private void Start()
    {
        rangeCollider = rangeObject.GetComponent<MeshCollider>(); // 메쉬 콜라이더 컴포넌트 가져오기

        if (rangeCollider == null)
        {
            Debug.LogError("Range Object에 메쉬 콜라이더가 없습니다.");
            return;
        }

        // 지정된 개수만큼 소환
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = Return_RandomPosition();

            // 중복 체크: 이미 생성된 위치와 겹치는지 확인
            while (spawnedPositions.Contains(spawnPosition))
            {
                spawnPosition = Return_RandomPosition(); // 새로운 랜덤 위치 생성
            }

            spawnedPositions.Add(spawnPosition); // 생성된 위치 저장
            Debug.Log($"소환 위치: {spawnPosition}"); // 소환 위치 로그
            Instantiate(capsul, spawnPosition, Quaternion.identity); // 캡슐 소환
        }
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position; // 범위 오브젝트의 위치
        float range_X = rangeCollider.bounds.size.x; // X축 범위
        float range_Z = rangeCollider.bounds.size.z; // Z축 범위

        // 랜덤 위치 생성
        float randomX = Random.Range((range_X / 2) * -1, range_X / 2);
        float randomZ = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 randomPosition = new Vector3(randomX, 0f, randomZ); // 랜덤 위치 벡터 생성

        // 최종 랜덤 위치 계산
        Vector3 respawnPosition = originPosition + randomPosition;
        return respawnPosition; // 최종 랜덤 위치 반환
    }
}
