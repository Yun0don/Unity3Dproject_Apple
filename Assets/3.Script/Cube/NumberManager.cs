using System.Collections.Generic;
using UnityEngine;

public class NumberManager : MonoBehaviour
{
    private const int MinValue = 1; // 생성할 숫자의 최소값
    private const int MaxValue = 9; // 생성할 숫자의 최대값

    /// <summary>
    /// 외부에서 요청 시 사용 가능한 숫자 리스트 생성
    /// </summary>
    public List<int> GenerateBalancedNumbers(int total)
    {
        List<int> result = new List<int>(); // 결과 리스트
        int maxAttempts = 1000; // 최대 시도 횟수 제한

        for (int attempts = 0; attempts < maxAttempts; attempts++)
        {
            result = GenerateNumberList(total);
            if (IsBalanced(result))
            {
                Shuffle(result);
                return result;
            }
        }

        Debug.LogWarning("GenerateBalancedNumbers reached max attempts.");
        return new List<int>();
    }

    private List<int> GenerateNumberList(int total)
    {
        List<int> list = new List<int>();
        for (int i = 0; i < total; i++)
        {
            int num = Random.Range(MinValue, MaxValue + 1);
            list.Add(num);
        }
        return list;
    }

    private bool IsBalanced(List<int> list)
    {
        Dictionary<int, int> count = new Dictionary<int, int>();
        for (int i = MinValue; i <= MaxValue; i++)
        {
            count[i] = 0;
        }

        int totalSum = 0;
        foreach (int n in list)
        {
            totalSum += n;
            count[n]++;
        }

        bool validPairs =
            count[1] > count[9] &&
            count[2] > count[8] &&
            count[3] > count[7];

        return (totalSum % 10 == 0) && validPairs;
    }

    private void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
    }
}