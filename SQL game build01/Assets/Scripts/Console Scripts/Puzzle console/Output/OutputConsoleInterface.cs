
namespace Gameplay.UI.Elements.Puzzle
{
    interface CellElement
    {
        void SetDisplayText(string inText);
    }

    interface ColumnElement
    {
        void SetDisplayCells(string[] inTexts);
    }

    interface TableElement
    {
        void SetDisplayData(string[][] inData);
    }
}
