using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// デッキを入れ替える
/// </summary>
public class ArtsDeckScroll : MonoBehaviour
{

    //[SerializeField] int selectDeckNum;
    [SerializeField] float speed = 500f;
    [SerializeField] List<RectTransform> decks = new List<RectTransform>();

    Vector3[] posArr; 
    int tmpSelectDeckNum;
    bool isChange;

    // Start is called before the first frame update
    void Start()
    {
        tmpSelectDeckNum = SelectDeckNum();
        posArr = new Vector3[decks.Count];

        for (int i = 0; i < decks.Count; i++)
        {
            posArr[i] = decks[i].localPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tmpSelectDeckNum != SelectDeckNum())
        {
            var count = ChangeCountProcess(tmpSelectDeckNum, SelectDeckNum(), decks.Count);
            if (count == 0) return;
            for (int i = 0; i < count; i++) DeckChange();

            isChange = true;
            tmpSelectDeckNum = SelectDeckNum();
        }

        if (isChange)
        {
            isChange = OnDeckMove();
        }
    }

    //デッキを動かす処理
    bool OnDeckMove()
    {
        int moveCount = 0;
        for (int i = 0; i < decks.Count; i++)
        {
            decks[i].localPosition =
                Vector3.MoveTowards(decks[i].localPosition, posArr[i], speed * Time.deltaTime);

            var dis = Vector3.Distance(decks[i].localPosition, posArr[i]);
            if (dis == 0) moveCount++;
        }
        return moveCount != decks.Count;
    }

    //デッキを1つずらす
    void DeckChange()
    {
        var deck = decks[0];
        decks.RemoveAt(0);
        decks.Add(deck);

        //子の順番を変える
        deck.SetAsFirstSibling();
    }

    //スクロールする回数を出す
    int ChangeCountProcess(int num, int changeNum, int maxNum)
    {
        if (changeNum > maxNum) return 0;

        var a = num - changeNum;
        if (a > 0)
        {
            return (a - maxNum) * -1;
        }
        else
        {
            return a * -1;
        }
    }

    //デッキ番号を入れるところ
    int SelectDeckNum()
    {
        return UI_Manager.GetChoiceArtsDeckNum;//selectDeckNum;
    }
}
