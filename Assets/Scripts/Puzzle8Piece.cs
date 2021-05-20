using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle8Piece : MonoBehaviour
{

    public piece[] puzzle;
    public GameObject[] target;

    public bool[] targetFree = new bool[9];
    private bool isOkay;
    private int ramdom;
    private int pieceInHand;
    private int pieceDrop;
    private int zoneFree = 8;

    private void Awake()
    {
        for(int i = 0;i<8;i++)
        {
            targetFree[i] = false;
            puzzle[i].zone = i;
            puzzle[i].part.transform.position = target[i].transform.position;
        }
        
        targetFree[8] = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        ramdom = Random.Range(10, 21);

        for (int y = 0; y < ramdom;y++)
        {
            switch (zoneFree)
            {
                case 0:
                    StartMove2piece(1, 3);
                    break;
                case 2:
                    StartMove2piece(1, 5);
                    break;
                case 6:
                    StartMove2piece(3, 7);
                    break;
                case 8:
                    StartMove2piece(5, 7);
                    break;

                case 1:
                    StartMove3piece(0, 2, 4);
                    break;
                case 3:
                    StartMove3piece(0, 4, 6);
                    break;
                case 5:
                    StartMove3piece(2, 4, 8);
                    break;
                case 7:
                    StartMove3piece(4, 6, 8);
                    break;

                case 4:
                    StartMove4piece(1, 3, 5, 7);
                    break;

            }
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            Vector3 zoneTouch = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, -Camera.main.transform.position.z));
            if (touch.phase == TouchPhase.Began)
            {
                for(int i = 0;i<puzzle.Length;i++)
                {
                    if ((zoneTouch.y < puzzle[i].targetTopLeft.transform.position.y && zoneTouch.y > puzzle[i].targetBotRight.transform.position.y) && (zoneTouch.x > puzzle[i].targetTopLeft.transform.position.x && zoneTouch.x < puzzle[i].targetBotRight.transform.position.x))
                        pieceInHand = i;
                }
            }

            if(touch.phase == TouchPhase.Ended)
                pieceInHand = -1;

            MovePiece(touch);
        }

        for (int i = 0; i < 8; i++)
        {
            if (i != pieceInHand)
                puzzle[i].part.transform.position = target[puzzle[i].zone].transform.position;

        }
        Win();
    }

    public void MovePiece(Touch touch)
    {
        
        if (pieceInHand != -1)
        {
            puzzle[pieceInHand].part.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, -Camera.main.transform.position.z));
            pieceDrop = pieceInHand;
        }
        else
        {
            for(int i = 0; i<9;i++)
            {
                if((puzzle[pieceDrop].part.transform.position.x < target[i].transform.position.x+1 && puzzle[pieceDrop].part.transform.position.x > target[i].transform.position.x - 1) && (puzzle[pieceDrop].part.transform.position.y < target[i].transform.position.y + 1 && puzzle[pieceDrop].part.transform.position.y > target[i].transform.position.y - 1))
                {
                    if(i == puzzle[pieceDrop].zone + 1 || i == puzzle[pieceDrop].zone - 1 || i == puzzle[pieceDrop].zone + 3 || i == puzzle[pieceDrop].zone - 3)
                    {
                        if(targetFree[i])
                        {
                            puzzle[pieceDrop].part.transform.position = target[i].transform.position;
                            targetFree[i] = false;
                            targetFree[puzzle[pieceDrop].zone] = true;
                            puzzle[pieceDrop].zone = i;
                        }
                    }
                    else
                        puzzle[pieceDrop].part.transform.position = target[puzzle[pieceDrop].zone].transform.position;
                }
            }  
        }     
    }

    public void Win()
    {
        bool win = true;
        for(int i = 0;i<8;i++)
        {
            if (puzzle[i].zone != i)
                win = false;
        }

        if (win)
            EventSystem.Manager.EndGame(true);

    }

    public void StartMove2piece(int piece1, int piece2)
    {
        if (Random.Range(0, 2) == 0)
        {
            for (int i = 0; i < 8; i++)
            {
                if (puzzle[i].zone == piece1)
                {
                    puzzle[i].part.transform.position = target[zoneFree].transform.position;
                    puzzle[i].zone = zoneFree;
                }
            }

            targetFree[zoneFree] = false;
            targetFree[piece1] = true;
            zoneFree = piece1;
        }
        else
        {
            for (int i = 0; i < 8; i++)
            {
                if (puzzle[i].zone == piece2)
                {
                    puzzle[i].part.transform.position = target[zoneFree].transform.position;
                    puzzle[i].zone = zoneFree;
                }
            }

            targetFree[zoneFree] = false;
            targetFree[piece2] = true;
            zoneFree = piece2;
        }        
    }

    public void StartMove3piece(int piece1, int piece2, int piece3)
    {
        if (Random.Range(0, 2) == 0)
        {                
            for (int i = 0; i < 8; i++)
            {
                if (puzzle[i].zone == piece1)
                {
                    puzzle[i].part.transform.position = target[zoneFree].transform.position;
                    puzzle[i].zone = zoneFree;
                }
            }

            targetFree[zoneFree] = false;
            targetFree[piece1] = true;
            zoneFree = piece1;

        }
        else
            StartMove2piece(piece2, piece3);
    }

    public void StartMove4piece(int piece1, int piece2, int piece3, int piece4)
    {
        if (Random.Range(0, 2) == 0)
            StartMove2piece(piece1, piece2);
        else
            StartMove2piece(piece3, piece4);
    }



        [System.Serializable]
    public struct piece
    {
        public GameObject part;
        public GameObject targetTopLeft;
        public GameObject targetBotRight;
        public int zone;
    }
}


