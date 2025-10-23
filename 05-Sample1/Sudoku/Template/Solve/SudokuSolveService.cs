namespace Solve;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using Solve.Abstraction;

public class SudokuSolveService : ISudokuSolveService
{
    public SudokuSolveService(IHttpClientFactory httpFactory)
    {
        _httpFactory = httpFactory;
    }

    private readonly IHttpClientFactory _httpFactory;

    //TODO Implement Server calls here, see https:https://h-aitenbichler.cloud.htl-leonding.ac.at/sudokusolve/swagger/index.html
    // 
}