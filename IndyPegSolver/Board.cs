﻿using System;
using System.IO;
using IndyPegSolver;

public class Board
{
    private const int MaxSize = 15;
    private SlotState[,] slots;
    
    public int Width { get; }
    public int Height { get; }

    public Board(int width, int height)
    {
        if (width > MaxSize || height > MaxSize)
        {
            throw new ArgumentException($"Board dimensions cannot exceed {MaxSize}x{MaxSize}");
        }

        Width = width;
        Height = height;
        slots = new SlotState[width, height];

        // Initialize the board with Hole state
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                slots[i, j] = SlotState.Hole;
            }
        }
    }

    public Board(char[,] initialState)
    {
        int width = initialState.GetLength(0);
        int height = initialState.GetLength(1);

        if (width > MaxSize || height > MaxSize)
        {
            throw new ArgumentException($"Board dimensions cannot exceed {MaxSize}x{MaxSize}");
        }

        Width = width;
        Height = height;
        slots = new SlotState[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                slots[i, j] = CharToSlotState(initialState[i, j]);
            }
        }
    }
    
    public SlotState GetSlotState(Point position)
    {
        if (position.X < 0 || position.X >= Width || position.Y < 0 || position.Y >= Height)
        {
            return SlotState.Solid; // Treat out-of-bounds as Solid
        }
        return slots[position.X, position.Y];
    }

    private void SetSlotState(Point position, SlotState state)
    {
        if (position.X < 0 || position.X >= Width || position.Y < 0 || position.Y >= Height)
        {
            throw new ArgumentException("Position out of bounds");
        }
        slots[position.X, position.Y] = state;
    }

    public bool IsSolved()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if (slots[i, j] == SlotState.Hole)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public int CountUnfilledHoles()
    {
        int count = 0;
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if (slots[i, j] == SlotState.Hole)
                {
                    count++;
                }
            }
        }
        return count;
    }

    public Board Combine(Board other)
    {
        if (Width != other.Width || Height != other.Height)
        {
            throw new ArgumentException("Boards must have the same dimensions to combine");
        }

        var newBoard = new Board(Width, Height);
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if ((slots[i, j] == SlotState.Left || slots[i, j] == SlotState.Right) &&
                    (other.slots[i, j] == SlotState.Left || other.slots[i, j] == SlotState.Right))
                {
                    throw new InvalidOperationException("Cannot combine boards with pegs in the same position");
                }
                newBoard.slots[i, j] = CombineSlotStates(slots[i, j], other.slots[i, j]);
            }
        }
        return newBoard;
    }

    internal SlotState CombineSlotStates(SlotState state1, SlotState state2)
    {
        if (state1 == SlotState.Solid || state2 == SlotState.Solid)
        {
            if (state1 != state2)
            {
                throw new InvalidOperationException("Solid state must be the same on both boards");
            }
            return SlotState.Solid;
        }

        if (state1 == SlotState.Hole && state2 == SlotState.Hole) return SlotState.Hole;
        if (state1 == SlotState.Hole && state2 == SlotState.Filled) return SlotState.Filled;
        if (state1 == SlotState.Hole && state2 == SlotState.Left) return SlotState.Left;
        if (state1 == SlotState.Hole && state2 == SlotState.Right) return SlotState.Right;
        if (state1 == SlotState.Filled && state2 == SlotState.Filled) return SlotState.Filled;
        if (state1 == SlotState.Filled && state2 == SlotState.Left) return SlotState.Left;
        if (state1 == SlotState.Filled && state2 == SlotState.Right) return SlotState.Right;
        if (state1 == SlotState.Left && state2 == SlotState.Hole) return SlotState.Left;
        if (state1 == SlotState.Left && state2 == SlotState.Filled) return SlotState.Left;
        if (state1 == SlotState.Right && state2 == SlotState.Hole) return SlotState.Right;
        if (state1 == SlotState.Right && state2 == SlotState.Filled) return SlotState.Right;

        throw new InvalidOperationException("Invalid combination of slot states");
    }

    public void PlacePeg(Point position, SlotState pegState)
    {
        if (pegState != SlotState.Left && pegState != SlotState.Right)
        {
            throw new ArgumentException("Invalid peg state");
        }

        if (GetSlotState(position) != SlotState.Hole && GetSlotState(position) != SlotState.Filled)
        {
            throw new InvalidOperationException("Cannot place a peg in a non-hole or non-filled slot");
        }

        SetSlotState(position, pegState);        
        if (pegState == SlotState.Left)  FillAffectedSlotsOnTurnLeft(position);
        if (pegState == SlotState.Right) FillAffectedSlotsOnTurnRight(position);
    }

    private void FillAffectedSlotsOnTurnLeft(Point position)
    {
        // this is only for turn left so far
        int[,] directions = new int[,]
        {
            { -1, -1 }, { -1, 0 }, { -1, 1 },
            { 0, -1 },           { 0, 1 },
            { 1, -1 }, { 1, 0 }, { 1, 1 }
        };

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int newX = position.X + directions[i, 0];
            int newY = position.Y + directions[i, 1];

            if (newX >= 0 && newX < Width && newY >= 0 && newY < Height)
            {
                SlotState currentState = slots[newX, newY];
                if (currentState == SlotState.Hole || currentState == SlotState.Filled)
                {
                    slots[newX, newY] = CombineSlotStates(currentState, SlotState.Filled);
                }
            }
        }
    }

    private void FillAffectedSlotsOnTurnRight(Point position)
    {
        // Directions for horizontal and vertical movement
        int[,] directions = new int[,]
        {
            { -1, 0 }, { 1, 0 }, // Vertical directions
            { 0, -1 }, { 0, 1 }  // Horizontal directions
        };

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int newX = position.X;
            int newY = position.Y;

            while (true)
            {
                newX += directions[i, 0];
                newY += directions[i, 1];

                if (newX < 0 || newX >= Width || newY < 0 || newY >= Height)
                {
                    break; // Out of bounds
                }

                SlotState currentState = slots[newX, newY];
                if (currentState == SlotState.Solid)
                {
                    break; // Blocked by a solid spot
                }

                if (currentState == SlotState.Hole || currentState == SlotState.Filled)
                {
                    slots[newX, newY] = CombineSlotStates(currentState, SlotState.Filled);
                }
            }
        }
    }
    
    public void PrintBoard()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                Console.Write(SlotStateToChar(slots[i, j]) + " ");
            }
            Console.WriteLine();
        }
    }

    public void SaveBoardToFile(string filePath)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    writer.Write(SlotStateToChar(slots[i, j]) + " ");
                }
                writer.WriteLine();
            }
        }
    }

    public Board Clone()
    {
        var newBoard = new Board(Width, Height);
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                newBoard.slots[i, j] = slots[i, j];
            }
        }
        return newBoard;
    }

    public void Clear()
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if (slots[i, j] != SlotState.Solid)
                {
                    slots[i, j] = SlotState.Hole;
                }
            }
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is Board other)
        {
            if (Width != other.Width || Height != other.Height)
            {
                return false;
            }

            for (int i = 0; i < Width; i++)
            {
                for (int j = 0; j < Height; j++)
                {
                    if (slots[i, j] != other.slots[i, j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        return false;
    }

    public override int GetHashCode()
    {
        int hash = HashCode.Combine(Width, Height);
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                hash = HashCode.Combine(hash, slots[i, j]);
            }
        }
        return hash;
    }

    public List<PointRating> GeneratePointRatings()
    {
        var pointRatings = new List<PointRating>();

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                if (slots[i, j] == SlotState.Hole)
                {
                    var pointRating = new PointRating(new Point(i, j));
                    //pointRating.AddFiller(new PegPlacement(new Point(i, j), SlotState.Left)); // ??? should this be added here?
                    AddFillers(pointRating);
                    pointRatings.Add(pointRating);
                }
            }
        }

        pointRatings = pointRatings.OrderBy(r => r, new PointRatingComparer()).ToList();

        return pointRatings;
    }

    private void AddFillers(PointRating pointRating)
    {
        Point holePosition = pointRating.HolePosition;

        // Check for L turn fillers (directly adjacent)
        int[,] lDirections = new int[,]
        {
        { -1, -1 }, { -1, 0 }, { -1, 1 },
        { 0, -1 },           { 0, 1 },
        { 1, -1 }, { 1, 0 }, { 1, 1 }
        };

        for (int i = 0; i < lDirections.GetLength(0); i++)
        {
            Point newPosition = new Point(holePosition.X + lDirections[i, 0], holePosition.Y + lDirections[i, 1]);

            if (newPosition.X >= 0 && newPosition.X < Width && newPosition.Y >= 0 && newPosition.Y < Height)
            {
                SlotState currentState = slots[newPosition.X, newPosition.Y];
                if (currentState != SlotState.Solid)
                {
                    pointRating.AddFiller(new PegPlacement(newPosition, SlotState.Left));
                }
            }
        }

        // Check for R turn fillers (horizontally and vertically)
        int[,] rDirections = new int[,]
        {
        { 0, -1 }, { 0, 1 },
        { -1, 0 }, { 1, 0 }
        };

        for (int i = 0; i < rDirections.GetLength(0); i++)
        {
            int dx = rDirections[i, 0];
            int dy = rDirections[i, 1];
            Point newPosition = new Point(holePosition.X + dx, holePosition.Y + dy);

            while (newPosition.X >= 0 && newPosition.X < Width && newPosition.Y >= 0 && newPosition.Y < Height)
            {
                SlotState currentState = slots[newPosition.X, newPosition.Y];
                if (currentState == SlotState.Solid)
                {
                    break;
                }
                pointRating.AddFiller(new PegPlacement(newPosition, SlotState.Right));
                newPosition = new Point(newPosition.X + dx, newPosition.Y + dy);
            }
        }
    }

    public List<PegPlacementRating> GeneratePegPlacementRatings()
    {
        var pegPlacementRatings = new List<PegPlacementRating>();

        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                Point position = new Point(i, j);
                if (GetSlotState(position) == SlotState.Hole)
                {
                    // Generate PegPlacementRating for Left turn
                    var leftPlacement = new PegPlacement(position, SlotState.Left);
                    var leftRating = new PegPlacementRating(leftPlacement);
                    AddFilledPoints(leftRating);
                    pegPlacementRatings.Add(leftRating);

                    // Generate PegPlacementRating for Right turn
                    var rightPlacement = new PegPlacement(position, SlotState.Right);
                    var rightRating = new PegPlacementRating(rightPlacement);
                    AddFilledPoints(rightRating);
                    pegPlacementRatings.Add(rightRating);
                }
            }
        }

        pegPlacementRatings = pegPlacementRatings.OrderBy(r => r, new PegPlacementRatingComparer()).ToList();

        return pegPlacementRatings;
    }

    private void AddFilledPoints(PegPlacementRating pegPlacementRating)
    {
        Point position = pegPlacementRating.PegPlacement.Point;
        SlotState state = pegPlacementRating.PegPlacement.State;

        if (state == SlotState.Left)
        {
            // Fill directly adjacent points
            int[,] directions = new int[,]
            {
            { -1, -1 }, { -1, 0 }, { -1, 1 },
            { 0, -1 },           { 0, 1 },
            { 1, -1 }, { 1, 0 }, { 1, 1 }
            };

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                Point newPosition = new Point(position.X + directions[i, 0], position.Y + directions[i, 1]);

                if (newPosition.X >= 0 && newPosition.X < Width && newPosition.Y >= 0 && newPosition.Y < Height)
                {
                    SlotState currentState = GetSlotState(newPosition);
                    if (currentState == SlotState.Hole || currentState == SlotState.Filled)
                    {
                        pegPlacementRating.AddFilledPoint(newPosition);
                    }
                }
            }
        }
        else if (state == SlotState.Right)
        {
            // Fill horizontally and vertically until a solid point or the edge of the board is reached
            int[,] directions = new int[,]
            {
            { 0, -1 }, { 0, 1 },
            { -1, 0 }, { 1, 0 }
            };

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int dx = directions[i, 0];
                int dy = directions[i, 1];
                Point newPosition = new Point(position.X + dx, position.Y + dy);

                while (newPosition.X >= 0 && newPosition.X < Width && newPosition.Y >= 0 && newPosition.Y < Height)
                {
                    SlotState currentState = GetSlotState(newPosition);
                    if (currentState == SlotState.Solid)
                    {
                        break;
                    }
                    pegPlacementRating.AddFilledPoint(newPosition);
                    newPosition = new Point(newPosition.X + dx, newPosition.Y + dy);
                }
            }
        }
    }

    private SlotState CharToSlotState(char c)
    {
        return c switch
        {
            '-' => SlotState.Solid,
            'O' => SlotState.Hole,
            'L' => SlotState.Left,
            'R' => SlotState.Right,
            'X' => SlotState.Filled,
            _ => throw new ArgumentException("Invalid character for slot state")
        };
    }

    private char SlotStateToChar(SlotState state)
    {
        return state switch
        {
            SlotState.Solid => '-',
            SlotState.Hole => 'O',
            SlotState.Left => 'L',
            SlotState.Right => 'R',
            SlotState.Filled => 'X',
            _ => throw new ArgumentException("Invalid slot state")
        };
    }
}