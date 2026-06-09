using UnityEngine;

public class WinLineManager : MonoBehaviour
{
    [SerializeField] private GameObject _verticalMiddleWin;
    [SerializeField] private GameObject _verticalLeftWin;
    [SerializeField] private GameObject _verticalRightWin;
    [SerializeField] private GameObject _horizontalMiddleWin;
    [SerializeField] private GameObject _horizontalTopWin;
    [SerializeField] private GameObject _horizontalBottomWin;
    [SerializeField] private GameObject _diagonalDownWin;
    [SerializeField] private GameObject _diagonalUpWin;

    public void SetWinLine(WinLine winline)
    {
        switch(winline)
        {
            case WinLine.none:
                SetWinLineActive(null);
                break;
            case WinLine.verticalMiddle:
                SetWinLineActive(_verticalMiddleWin);
                break;
            case WinLine.verticalLeft:
                SetWinLineActive(_verticalLeftWin);
                break;
            case WinLine.verticalRight:
                SetWinLineActive(_verticalRightWin);
                break;
            case WinLine.horizontalMiddle:
                SetWinLineActive(_horizontalMiddleWin);
                break;
            case WinLine.horizontalTop:
                SetWinLineActive(_horizontalTopWin);
                break;
            case WinLine.horizontalBottom:
                SetWinLineActive(_horizontalBottomWin);
                break;
            case WinLine.diagonalDown:
                SetWinLineActive(_diagonalDownWin);
                break;
            case WinLine.diagonalUp:
                SetWinLineActive(_diagonalUpWin);
                break;
            

        }
    }
    public void SetWinLineActive(GameObject activeWinLine)
    {
        _verticalMiddleWin.SetActive(_verticalMiddleWin == activeWinLine);
        _verticalLeftWin.SetActive(_verticalLeftWin == activeWinLine);
        _verticalRightWin.SetActive(_verticalRightWin == activeWinLine);
        _horizontalMiddleWin.SetActive(_horizontalMiddleWin == activeWinLine);
        _horizontalTopWin.SetActive(_horizontalTopWin == activeWinLine);
        _horizontalBottomWin.SetActive(_horizontalBottomWin == activeWinLine);
        _diagonalDownWin.SetActive(_diagonalDownWin == activeWinLine);
        _diagonalUpWin.SetActive(_diagonalUpWin == activeWinLine);
    }
}
public enum WinLine
{
    none,
    verticalMiddle,
    verticalLeft,
    verticalRight,
    horizontalMiddle,
    horizontalTop,
    horizontalBottom,
    diagonalDown,
    diagonalUp
}