using System.Collections;
using System.Collections.Generic;

public class SQLResult
{
    public bool IsError { get; set; }
    public bool IsCorrect { get; set; }
    public string tableResult { get; set; }

    public SQLResult()
    {
        IsError = false;
        IsCorrect = false;
    }

    public SQLResult(bool isErr, bool isCorr, string tabRe)
    {
        IsError = isErr;
        IsCorrect = isCorr;
        tableResult = tabRe;
    }
}
