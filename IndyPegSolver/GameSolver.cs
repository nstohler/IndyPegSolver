﻿using System.Collections.Generic;

public class GameSolver
{
    private readonly IStrategy _strategy;

    public GameSolver(IStrategy strategy)
    {
        _strategy = strategy;
    }

    public List<GameState> Solve(GameState gameState)
    {
        return _strategy.FindSolution(gameState);
    }
}