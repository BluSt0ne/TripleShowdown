using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DiceRoller : MonoBehaviour
{
    // �ֻ����� �� �鿡 �ش��ϴ� ��������Ʈ �迭
    private Sprite[] diceSides;

    // �ֻ��� �̹����� ǥ���� UI Image ������Ʈ
    public Image diceImage;

    // UI ��ư�� ������ �� �ֻ����� ������ �Լ�
    public void OnRollDiceButtonPressed()
    {
        StartCoroutine(RollTheDice());
    }

    // �ֻ��� ������ �ڷ�ƾ
    private IEnumerator RollTheDice()
    {
        // �ֻ��� ���� ������ ���� (0���� 5����)
        int randomSide = 0;

        // �ֻ��� �̹����� 20�� ���� �����ϰ� �ٲٱ�
        for (int i = 0; i < 20; i++)
        {
            randomSide = Random.Range(0, 6); // 0 ~ 5 ������ ����

            diceImage.sprite = diceSides[randomSide]; // �����ϰ� �ֻ��� �̹��� ����

            // 0.05�� �������� �ֻ��� ���� �ٲ�
            yield return new WaitForSeconds(0.05f);
        }

        // ���� �ֻ��� ��
        Debug.Log("Final Dice Side: " + (randomSide + 1));

        // �ֻ����� ���� ���� ������ ��, ���� ���� �����ݴϴ�.
        diceImage.sprite = diceSides[randomSide];
    }

    // Start �޼��忡�� �ֻ��� �̹����� �ε�
    void Start()
    {
        // Resources �������� �ֻ��� �鿡 �ش��ϴ� ��������Ʈ�� �ε�
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
    }
}
