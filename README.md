# Quiz Game 🎮

This project is a **C# Windows Forms Application** that allows users to play a multiple-choice quiz game.  
It is connected to a **SQL Server database** where questions are stored and retrieved dynamically.  

## Features
- Fetching quiz questions from **SQL Server (QuizDB)**  
- Multiple-choice questions with options (A, B, C, D)  
- Randomized question order for each game  
- Displays correct and wrong answer counts in real time  
- Highlights correct and wrong answers with different colors  
- End game screen when all questions are completed  

## Technologies
- C# / Windows Forms (WinForms)  
- ADO.NET (with `SqlConnection`, `SqlCommand`, `SqlDataReader`)  
- SQL Server Database  

## Game Flow
1. The game loads questions from the `Questions` table in the database.  
2. Each question has four options (A, B, C, D) and one correct answer.  
3. User selects an option:
   - If correct → button turns **green**.  
   - If wrong → button turns **red** and the correct answer turns **green**.  
4. User clicks **Next** to move to the next question.  
5. At the end, the game displays *Game completed!* message with score.  

