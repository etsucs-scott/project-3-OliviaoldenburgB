# Minesweeper Console Game

## Overview
This project is a console-based implementation of the classic Minesweeper game built using C#. It demonstrates object-oriented design, game logic, and unit testing.

The game allows users to reveal tiles, place flags, and avoid hidden mines while attempting to clear the board.

---

##  Features
- Random mine placement using a seed
- Tile reveal system with cascade behavior
- Flagging and unflagging tiles
- Win and loss conditions
- Console-based user interface with row/column labels
- Unit tests for core game functionality

---

## Project Structure
src/
├── Minesweeper.Core/
│ ├── Board.cs
│ ├── Tile.cs
│
├── Minesweeper.Console/
│ ├── Program.cs
│
└── Minesweeper.Tests/
├── BoardTests.cs


---

## How to Run the Game

1. Navigate to the project root  
2. Run the command: dotnet run --project src/Minesweeper.Console


3. Enter moves using:
- `row column` → Reveal a tile  
- `f row column` → Flag/unflag a tile  

Example: 2 3, f 1 4


---

## Running Unit Tests

To run all tests: dotnet test


This project includes 10 unit tests covering:
- Board initialization
- Mine placement
- Adjacency calculations
- Tile reveal logic
- Flagging behavior
- Deterministic board generation using seed values

---

##  Concepts Demonstrated
- Object-Oriented Programming (OOP)
- 2D Arrays and grid-based logic
- Recursion (cascade reveal)
- Randomization with seeds
- Unit testing with xUnit
- Separation of concerns (Core vs Console vs Tests)

---

## Reflection
Through this project, I learned how to structure a multi-project C# application and separate core logic from user interaction. I gained experience working with 2D arrays, recursion for cascade reveals, and implementing deterministic randomness using seeds. Additionally, I developed unit tests using xUnit to verify correctness and ensure reliability of the game logic. This project strengthened my problem-solving skills and understanding of software design principles.

--
