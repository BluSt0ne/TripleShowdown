using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DiceRoller : MonoBehaviour
{
    // 주사위의 각 면에 해당하는 스프라이트 배열
    private Sprite[] diceSides;

    // 주사위 이미지를 표시할 UI Image 컴포넌트
    public Image diceImage;

    // UI 버튼을 눌렀을 때 주사위를 굴리는 함수
    public void OnRollDiceButtonPressed()
    {
        StartCoroutine(RollTheDice());
    }

    // 주사위 굴리는 코루틴
    private IEnumerator RollTheDice()
    {
        // 주사위 면의 랜덤한 숫자 (0부터 5까지)
        int randomSide = 0;

        // 주사위 이미지를 20번 정도 랜덤하게 바꾸기
        for (int i = 0; i < 20; i++)
        {
            randomSide = Random.Range(0, 6); // 0 ~ 5 범위의 숫자

            diceImage.sprite = diceSides[randomSide]; // 랜덤하게 주사위 이미지 변경

            // 0.05초 간격으로 주사위 면을 바꿈
            yield return new WaitForSeconds(0.05f);
        }

        // 최종 주사위 면
        Debug.Log("Final Dice Side: " + (randomSide + 1));

        // 주사위의 최종 면을 결정한 후, 실제 면을 보여줍니다.
        diceImage.sprite = diceSides[randomSide];
    }

    // Start 메서드에서 주사위 이미지들 로드
    void Start()
    {
        // Resources 폴더에서 주사위 면에 해당하는 스프라이트를 로드
        diceSides = Resources.LoadAll<Sprite>("DiceSides/");
    }
}
