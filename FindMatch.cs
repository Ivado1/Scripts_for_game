using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using YG;

public class FindMatches : MonoBehaviour {
    // Game Match 3
    private Board board;
    public List<GameObject> currentMatches = new List<GameObject>();

    public Text scoreText;
    public Text[] targetLevelText;
    public Text turnsLevelText;
    private int score, sumtarget;
    
    [SerializeField] private string[] levelTargetTagName; // Предмет который нужно собирать на уровне
    [SerializeField] private int[] levelTargetAmount; // Необходимое количество для победы
    [SerializeField] public int turnsAmount; // Необходимое количество ходов на этот уровень

    public GameObject loseMenu;
    public GameObject gameDownUI;
    public GameObject winMenu;
    public GameObject boardGame;

    private int col;
    private int ro;
    private int pairCol;
    private int pairRo;
    private float timerEND ;
    private bool corutineStarted=false;
    public bool stopMove=false;

    public AudioSource specialColorSound, specialBombSound, specialArrowSound;

    // Для инциализации
    void Start()
    {
        specialBombSound.volume = 0.6f;

        score = YandexGame.savesData.playerScore;
        board = FindObjectOfType<Board>();

        turnsLevelText.text = turnsAmount.ToString();
        scoreText.text = score.ToString();

        print(levelTargetTagName.Length);
        for (int i = 0; i < levelTargetTagName.Length; i++)
        {
            targetLevelText[i].text = levelTargetAmount[i].ToString();
            sumtarget += levelTargetAmount[i];
            //print(sumtarget);
        }
    }
    public void PlusTurns(int moves)
    {
        corutineStarted = false;
        PlayerReturn();
        turnsAmount += moves;
        turnsLevelText.text = turnsAmount.ToString();
    }
    public void MinusTurns()
    {
        if (turnsAmount > 1)
        {
            turnsAmount -= 1;
            turnsLevelText.text = turnsAmount.ToString();

        }
        else
        {
            timerEND = 2;
            turnsAmount -= 1;
            turnsLevelText.text = turnsAmount.ToString();

            if (corutineStarted == false)
            {
                StartCoroutine(EndGameStart());
                corutineStarted = true;
            }
        }
    }
    private IEnumerator EndGameStart()
    {
        yield return new WaitForSeconds(1f); // частота опроса
        timerEND -= 1;
        print(timerEND+" END");
        if (timerEND < 0)
        {
            if (sumtarget>0)
            {
                PlayerLoose();
                print("Loose");
            }
            else
            {
                PlayerWin();
                print("Win");
            }
        }
        else
        {
            StartCoroutine(EndGameStart());
        }

    }
    private void PlayerReturn()
    {
        loseMenu.SetActive(false);
        boardGame.SetActive(true);
        gameDownUI.SetActive(true);
    }
    private void PlayerWin()
    {
        YandexGame.savesData.playerScore = score;
        
        int nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneLoad > YandexGame.savesData.levelOpened)YandexGame.savesData.levelOpened = nextSceneLoad;
        YandexGame.SaveProgress();

        YandexGame.NewLeaderboardScores("LeaderBoard", YandexGame.savesData.playerScore);

        stopMove = true;
        winMenu.SetActive(true);
        gameDownUI.SetActive(false);
    }

    private void PlayerLoose()
    {
        loseMenu.SetActive(true);
        gameDownUI.SetActive(false);
    }

    public void FindAllMatches() {
        StartCoroutine(FindAllMatchesCo());
    }

    private List<GameObject> IsAdjacentBomb(Dot dot1, Dot dot2, Dot dot3) {
        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.isAdjacentBomb)
        {
            currentMatches.Union(GetAdjacentPieces(dot1.column, dot1.row));
        }

        if (dot2.isAdjacentBomb)
        {
            currentMatches.Union(GetAdjacentPieces(dot2.column, dot2.row));
        }

        if (dot3.isAdjacentBomb)
        {
            currentMatches.Union(GetAdjacentPieces(dot3.column, dot3.row));
        }
        return currentDots;
    }

    private List<GameObject> IsRowBomb(Dot dot1, Dot dot2, Dot dot3) {
        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.isRowBomb)
        {
            currentMatches.Union(GetRowPieces(dot1.column, dot1.row));
        }

        if (dot2.isRowBomb)
        {
            currentMatches.Union(GetRowPieces(dot2.column, dot2.row));
        }

        if (dot3.isRowBomb)
        {
            currentMatches.Union(GetRowPieces(dot3.column, dot3.row));
        }
        return currentDots;
    }

    private List<GameObject> IsColumnBomb(Dot dot1, Dot dot2, Dot dot3)
    {
        List<GameObject> currentDots = new List<GameObject>();
        if (dot1.isColumnBomb)
        {
            currentMatches.Union(GetColumnPieces(dot1.column, dot1.row));
        }

        if (dot2.isColumnBomb)
        {
            currentMatches.Union(GetColumnPieces(dot2.column, dot2.row));
        }

        if (dot3.isColumnBomb)
        {
            currentMatches.Union(GetColumnPieces(dot3.column, dot3.row));
        }
        return currentDots;
    }

    public void DesroidObjects(GameObject obj)
    {
        timerEND = 2;
        score += 10;
        scoreText.text = score.ToString();
        
        for (int i = 0; i < levelTargetTagName.Length; i++)
        {
            if (obj.tag == levelTargetTagName[i])
            {
                if (levelTargetAmount[i] > 0)
                {
                    sumtarget -= 1;
                    levelTargetAmount[i] -= 1;
                    targetLevelText[i].text = levelTargetAmount[i].ToString();
                    if (turnsAmount < 1)
                    {
                        timerEND = 2;
                        print(timerEND + " ADD To 2");
                    }

                }
                if(levelTargetAmount[i] == 0)
                {
                    targetLevelText[i].text = "✔".ToString();
                    targetLevelText[i].color = Color.green;
                }
                if (sumtarget <= 0)
                {
                    if (turnsAmount < 1)
                    {
                        timerEND = 2;
                        print(timerEND + " ADD To 2");
                    }
                    if (corutineStarted == false)
                    {
                        stopMove = true;
                        timerEND = 2;
                        StartCoroutine(EndGameStart());
                        corutineStarted = true;
                    }
                }
            }
        }
    }

    private void AddToListAndMatch(GameObject dot) {
        if (!currentMatches.Contains(dot))
        {
            currentMatches.Add(dot);
        }
        dot.GetComponent<Dot>().isMatched = true;
    }

    private void GetNearbyPieces(GameObject dot1, GameObject dot2, GameObject dot3) {
        AddToListAndMatch(dot1);
        AddToListAndMatch(dot2);
        AddToListAndMatch(dot3);
    }

    private IEnumerator FindAllMatchesCo() {
        yield return new WaitForSeconds(.2f);
        for (int i = 0; i < board.width; i++) {
            for (int j = 0; j < board.height; j++) {
                GameObject currentDot = board.allDots[i, j];

                if (currentDot != null) {
                    Dot currentDotDot = currentDot.GetComponent<Dot>();
                    if (i > 0 && i < board.width - 1) {
                        GameObject leftDot = board.allDots[i - 1, j];

                        GameObject rightDot = board.allDots[i + 1, j];


                        if (leftDot != null && rightDot != null)
                        {
                            Dot rightDotDot = rightDot.GetComponent<Dot>();
                            Dot leftDotDot = leftDot.GetComponent<Dot>();
                            if (leftDot.tag == currentDot.tag && rightDot.tag == currentDot.tag)
                            {

                                currentMatches.Union(IsRowBomb(leftDotDot, currentDotDot, rightDotDot));

                                currentMatches.Union(IsColumnBomb(leftDotDot, currentDotDot, rightDotDot));

                                currentMatches.Union(IsAdjacentBomb(leftDotDot, currentDotDot, rightDotDot));

                                GetNearbyPieces(leftDot, currentDot, rightDot);

                            }
                        }

                    }

                    if (j > 0 && j < board.height - 1)
                    {
                        GameObject upDot = board.allDots[i, j + 1];

                        GameObject downDot = board.allDots[i, j - 1];


                        if (upDot != null && downDot != null)
                        {
                            Dot downDotDot = downDot.GetComponent<Dot>();
                            Dot upDotDot = upDot.GetComponent<Dot>();
                            if (upDot.tag == currentDot.tag && downDot.tag == currentDot.tag)
                            {

                                currentMatches.Union(IsColumnBomb(upDotDot, currentDotDot, downDotDot));

                                currentMatches.Union(IsRowBomb(upDotDot, currentDotDot, downDotDot));

                                currentMatches.Union(IsAdjacentBomb(upDotDot, currentDotDot, downDotDot));

                                GetNearbyPieces(upDot, currentDot, downDot);

                            }
                        }
                    }
                }
            }
        }
    }
    public void CallRowSpecial(int column, int row)
    {
        pairCol = column;
        pairRo = row;
        GetRowPieces(column,row);
    }
    public void CallColumnSpecial(int column, int row)
    {
        pairCol = column;
        pairRo = row;
        GetColumnPieces(column, row);
    }
    public void CallBomb(int column, int row)
    {
        pairCol = column;
        pairRo = row;
        GetAdjacentPieces(column, row);
    }

    public void MatchPiecesOfColor(string color){
        //print("MatchPiecesOfColor");
        for (int i = 0; i < board.width; i ++){
            for (int j = 0; j < board.height; j ++){
                //Check if that piece exists
                if(board.allDots[i, j] != null){
                    //Check the tag on that dot
                    if(board.allDots[i, j].tag == color){
                        //Set that dot to be matched
                        specialColorSound.Play();
                        board.allDots[i, j].GetComponent<Dot>().isMatched = true;
                    }
                }
            }
        }
    }

    List<GameObject> GetAdjacentPieces(int column, int row){
        List<GameObject> dots = new List<GameObject>();
        for (int i = column - 1; i <= column + 1; i++)
        {
            for (int j = row - 1; j <= row + 1; j++)
            {
                //Check if the piece is inside the board
                if (i >= 0 && i < board.width && j >= 0 && j < board.height)
                {
                    if (board.allDots[i, j].GetComponent<Dot>().isMatched == false)
                    {
                        dots.Add(board.allDots[i, j]);
                        board.allDots[i, j].GetComponent<Dot>().isMatched = true;
                        switch (board.allDots[i, j].tag)
                        {
                            case "RowArrow":
                                if (board.allDots[pairCol, pairRo] != board.allDots[i, j])
                                {
                                    col = board.allDots[i, j].GetComponent<Dot>().column;
                                    ro = board.allDots[i, j].GetComponent<Dot>().row;
                                    CallRowSpecial(col, ro);
                                    print("ROW_Bomb_Destroy");
                                }
                                break;
                            case "ColumnArrow":
                                if (board.allDots[pairCol, pairRo] != board.allDots[i, j])
                                {
                                    col = board.allDots[i, j].GetComponent<Dot>().column;
                                    ro = board.allDots[i, j].GetComponent<Dot>().row;
                                    CallColumnSpecial(col, ro);
                                    print("Column_Bomb_Destroy");
                                }
                                break;
                            case "Bomb":
                                if (board.allDots[pairCol, pairRo] != board.allDots[i, j])
                                {
                                    col = board.allDots[i, j].GetComponent<Dot>().column;
                                    ro = board.allDots[i, j].GetComponent<Dot>().row;
                                    CallBomb(col, ro);

                                    print("Bomb_Destroy");
                                }
                                else print("ELSE_Bomb_Destroy");
                                break;
                            case "ColorBomb":
                                if (board.allDots[pairCol, pairRo] != board.allDots[i, j])
                                {
                                    board.allDots[i, j].GetComponent<Dot>().isMatched = false;
                                    print("ColorBomb");
                                }
                                break;
                            default: //print("Default");
                                break;
                        }
                        specialBombSound.Play();
                    }
                }
            }
        }
        return dots;
    }

    List<GameObject> GetColumnPieces(int column, int row)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < board.height; i ++){
            if(board.allDots[column, i]!= null){
                if (board.allDots[column, i].GetComponent<Dot>().isMatched == false)
                {
                    dots.Add(board.allDots[column, i]);
                    board.allDots[column, i].GetComponent<Dot>().isMatched = true;
                    switch (board.allDots[column, i].tag)
                    {
                        case "RowArrow":
                            if (board.allDots[pairCol,pairRo] != board.allDots[column, i])
                            {
                                col = board.allDots[column, i].GetComponent<Dot>().column;
                                ro = board.allDots[column, i].GetComponent<Dot>().row;
                                CallRowSpecial(col, ro);
                                print("ROW_Destroy!!!!!");
                            }
                            break;
                        case "ColumnArrow":
                            if (board.allDots[pairCol, pairRo] = board.allDots[column, i])
                            {
                                //For special Double both close
                                col = board.allDots[column, i].GetComponent<Dot>().column;
                                ro = board.allDots[column, i].GetComponent<Dot>().row;
                                CallRowSpecial(col, ro);
                                print("ROW_Destroy!!!!!");
                            }
                            break;
                        case "Bomb":
                            if (board.allDots[pairCol, pairRo] != board.allDots[column, i])
                            {
                                col = board.allDots[column, i].GetComponent<Dot>().column;
                                ro = board.allDots[column, i].GetComponent<Dot>().row;
                                CallBomb(col, ro);
                                print("Bomb_Destroy");
                            }
                            break;
                        case "ColorBomb":
                            if (board.allDots[pairCol, pairRo] != board.allDots[column, i])
                            {
                                board.allDots[column, i].GetComponent<Dot>().isMatched = false;
                                print("ColorBomb");
                            }
                            break;
                        default://print("Default");
                            break;
                    }
                    specialArrowSound.Play();
                }
            }
        }
        return dots;
    }

    List<GameObject> GetRowPieces(int column, int row)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < board.width; i++)
        {
            if (board.allDots[i, row] != null)
            {
                if (board.allDots[i, row].GetComponent<Dot>().isMatched == false)
                {
                    dots.Add(board.allDots[i, row]);
                    board.allDots[i, row].GetComponent<Dot>().isMatched = true;
                    switch (board.allDots[i, row].tag)
                    {

                        case "RowArrow":
                            if (board.allDots[pairCol, pairRo] = board.allDots[i, row])
                            {
                                //For special Double both close
                                col = board.allDots[i, row].GetComponent<Dot>().column;
                                ro = board.allDots[i, row].GetComponent<Dot>().row;
                                CallColumnSpecial(col, ro);
                                //print("Column_Destroy!");
                            }
                            
                            print("ROW_Destroy");
                            break;
                        case "ColumnArrow":
                            if(board.allDots[pairCol, pairRo] != board.allDots[i, row])
                            {
                                col = board.allDots[i, row].GetComponent<Dot>().column;
                                ro = board.allDots[i, row].GetComponent<Dot>().row;
                                CallColumnSpecial(col,ro);
                                //print("Column_Destroy!");
                            }
                            break;
                        case "Bomb":
                            if (board.allDots[pairCol, pairRo] != board.allDots[i, row])
                            {
                                col = board.allDots[i, row].GetComponent<Dot>().column;
                                ro = board.allDots[i, row].GetComponent<Dot>().row;
                                CallBomb(col, ro);
                                //print("Bomb_Destroy");
                            }
                            break;
                        case "ColorBomb":
                            if (board.allDots[pairCol, pairRo] != board.allDots[i, row])
                            {
                                board.allDots[i, row].GetComponent<Dot>().isMatched = false;
                                //print("ColorBomb");
                            }
                            break;
                        default:
                            //print("Default");
                            break;
                    }
                    specialArrowSound.Play();
                }
            }
        }
        return dots;
    }

    public void CheckBombs(Dot dota)
    {
        //Did the player move something?
        if(board.currentDot != null)
        {
            //Is the piece they moved matched?
            if (board.currentDot.isMatched)
            {
                //make it unmatched
                board.currentDot.isMatched = false;
               
                if((board.currentDot.swipeAngle > -45 && board.currentDot.swipeAngle <= 45)
                   ||(board.currentDot.swipeAngle < -135 || board.currentDot.swipeAngle >= 135)){
                    board.currentDot.MakeRowBomb();
                }else{
                    board.currentDot.MakeColumnBomb();
                }
            }
            //Is the other piece matched?
            else if(board.currentDot.otherDot != null){
                Dot otherDot = board.currentDot.otherDot.GetComponent<Dot>();
                //Is the other Dot matched?
                if(otherDot.isMatched){
                    //Make it unmatched
                    otherDot.isMatched = false;
                    
                    if ((board.currentDot.swipeAngle > -45 && board.currentDot.swipeAngle <= 45)
                   || (board.currentDot.swipeAngle < -135 || board.currentDot.swipeAngle >= 135))
                    {
                        otherDot.MakeRowBomb();
                    }
                    else
                    {
                        otherDot.MakeColumnBomb();
                    }
                }
            }
            
        }
        else
        {
            //print("NOT HUMAN Make Row ");
            if (dota.isMatched)
            {
                dota.isMatched = false;
                dota.MakeRowBomb();
            }
        }
    }

    public void CheckSpecialBombNotHuman(Dot dota)
    {
        //print(dota+" NOT HUMAN Make Bomb");
        dota.MakeAdjacentBomb();
    }
    public void CheckSpecialColorNotHuman(Dot dota)
    {
        //print(dota+" NOT HUMAN Make Color");
        dota.MakeColorBomb();
    }
}
