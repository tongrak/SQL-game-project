using PuzzleConsole;
using System;
using UnityEngine;

public class ConsoleTesterScript : MonoBehaviour
{
    enum TestingOption { CELL, COLUMN, TABLE}

    [SerializeField] private GameObject _TargetObj;
    [SerializeField] private TestingOption _SelectedTestingOption;

    [Header("Cell Testing")]
    [SerializeField] private string _displayingText = "//PlaceHolder//";

    [Header("Column Testing")]
    [SerializeField] private string[] _columnTexts;


    /*private CellEleScript _cellEleRef;*/

    public void TestingCall()
    {
        string[] fstCol = { "Name", "Sam", "Alice" };
        string[] sndCol = { "SSN", "1233456789", "123456788" };
        string[] trdCol = { "Address", "22", "12" };
        string[][] sampleData = { fstCol, sndCol, trdCol};

        switch (_SelectedTestingOption)
        {
            case TestingOption.CELL:
                CellEleScript _cellEleRef = _TargetObj.GetComponent<CellEleScript>();
                _cellEleRef.SetDisplayText(_displayingText);
                break;
            case TestingOption.COLUMN:
                ColumnElement _columnEleRef = _TargetObj.GetComponent<ColumnEleScript>();
                _columnEleRef.SetDisplayCells(_columnTexts);
                break; 
            case TestingOption.TABLE:
                TableElement _tableEleRef = _TargetObj.GetComponent<TableGenerationScript>();
                _tableEleRef.SetDisplayData(sampleData); 
                break;
        }   
    }

}
