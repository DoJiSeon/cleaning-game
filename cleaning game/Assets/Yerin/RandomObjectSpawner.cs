using UnityEngine;
using System.Collections.Generic;

public class RandomObjectSpawner : MonoBehaviour
{
    public GameObject rangeObject; // ��ȯ ������ �����ϴ� ������Ʈ
    MeshCollider rangeCollider; // ���� ������Ʈ�� �޽� �ݶ��̴�
    public GameObject capsul; // ��ȯ�� ĸ�� ������Ʈ
    public int spawnCount = 10; // ��ȯ�� ĸ���� ����

    private List<Vector3> spawnedPositions = new List<Vector3>(); // ������ ��ġ ���

    private void Start()
    {
        rangeCollider = rangeObject.GetComponent<MeshCollider>(); // �޽� �ݶ��̴� ������Ʈ ��������

        if (rangeCollider == null)
        {
            Debug.LogError("Range Object�� �޽� �ݶ��̴��� �����ϴ�.");
            return;
        }

        // ������ ������ŭ ��ȯ
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 spawnPosition = Return_RandomPosition();

            // �ߺ� üũ: �̹� ������ ��ġ�� ��ġ���� Ȯ��
            while (spawnedPositions.Contains(spawnPosition))
            {
                spawnPosition = Return_RandomPosition(); // ���ο� ���� ��ġ ����
            }

            spawnedPositions.Add(spawnPosition); // ������ ��ġ ����
            Debug.Log($"��ȯ ��ġ: {spawnPosition}"); // ��ȯ ��ġ �α�
            Instantiate(capsul, spawnPosition, Quaternion.identity); // ĸ�� ��ȯ
        }
    }

    Vector3 Return_RandomPosition()
    {
        Vector3 originPosition = rangeObject.transform.position; // ���� ������Ʈ�� ��ġ
        float range_X = rangeCollider.bounds.size.x; // X�� ����
        float range_Z = rangeCollider.bounds.size.z; // Z�� ����

        // ���� ��ġ ����
        float randomX = Random.Range((range_X / 2) * -1, range_X / 2);
        float randomZ = Random.Range((range_Z / 2) * -1, range_Z / 2);
        Vector3 randomPosition = new Vector3(randomX, 0f, randomZ); // ���� ��ġ ���� ����

        // ���� ���� ��ġ ���
        Vector3 respawnPosition = originPosition + randomPosition;
        return respawnPosition; // ���� ���� ��ġ ��ȯ
    }
}
