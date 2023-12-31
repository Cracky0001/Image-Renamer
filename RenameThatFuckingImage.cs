﻿using System;
using System.IO;
using System.Linq;
using System.Threading; 

class RenameThatFuckingImage
{
    static void Write(string text)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(10); 
        }
        Console.WriteLine();
    }

    static void WriteFast(string text)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(1); 
        }
        Console.WriteLine();
    }
    static void Main()
    {
        string Version = "1.6.1";
        string Logo = @"
    ____                              ____                                      
   /  _/___ ___  ____ _____ ____     / __ \___  ____  ____ _____ ___  ___  _____
   / // __ `__ \/ __ `/ __ `/ _ \   / /_/ / _ \/ __ \/ __ `/ __ `__ \/ _ \/ ___/
 _/ // / / / / / /_/ / /_/ /  __/  / _, _/  __/ / / / /_/ / / / / / /  __/ /
/___/_/ /_/ /_/\__,_/\__, /\___/  /_/ |_|\___/_/ /_/\__,_/_/ /_/ /_/\___/_/ " + Version + @"
      by Cracky     /____/       https://github.com/Cracky0001                                                    
";

        string directoryPath = "./";
        string Username = Environment.UserName;
        Username = char.ToUpper(Username[0]) + Username.Substring(1);
        string[] imageExtensions = { ".png", ".jpeg", ".jpg" };
        string[] imageFiles = imageExtensions
            .SelectMany(ext => Directory.GetFiles(directoryPath, "*" + ext))
            .ToArray();

        if (imageFiles.Length == 0)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            WriteFast(Logo);
            //Write(Logo);
            Console.ForegroundColor = ConsoleColor.Red;
            Write($"Hello {Username}! There are no images in this folder.");
            Console.ForegroundColor = ConsoleColor.White;
            Write("Choose another folder? (y/n)");
            if (Console.ReadLine() == "y")
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(Logo);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Write("Enter the path to the folder:");
                directoryPath = Console.ReadLine();
                imageFiles = imageExtensions
                    .SelectMany(ext => Directory.GetFiles(directoryPath, "*" + ext))
                    .ToArray();

                if (imageFiles.Length == 0)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine(Logo);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Write($"Hello {Username}! There are no images in this folder.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Write("Program will be closed in 3 seconds...");
                    Thread.Sleep(3000);
                    return;
                }
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(Logo);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Write("Program will be closed in 3 seconds...");
                Thread.Sleep(3000);
                Environment.Exit(999);
            }
        }

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        WriteFast(Logo);
        Write($"Hello {Username}! Following images will be renamed:");
        Console.ForegroundColor = ConsoleColor.White;
        foreach (string imagePath in imageFiles)
        {
            WriteFast(Path.GetFileName(imagePath));
        }

        Console.ForegroundColor = ConsoleColor.Green;
        WriteFast("Images: " + imageFiles.Length);
        Write("Are you sure you want to rename these images? (y/n)");

        if (Console.ReadLine() != "y")
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Logo);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Write("Program will be closed in 3 seconds...");
            Thread.Sleep(3000);
            return;
        }
        else
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(Logo);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Write("Renaming images...");
            Thread.Sleep(1500);
        }

        string backupFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Image Renamer Backup");
        Directory.CreateDirectory(backupFolderPath);

        foreach (string imagePath in imageFiles)
        {
            string backupFilePath = Path.Combine(backupFolderPath, Path.GetFileName(imagePath));
            File.Copy(imagePath, backupFilePath, true);
        }

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(Logo);
        Console.ForegroundColor = ConsoleColor.Green;
        Write("Backup created.");
        Write("Backup-Path: " + backupFolderPath);
        Console.ForegroundColor = ConsoleColor.White;
        Thread.Sleep(2000);

        DateTime desiredDateTime = new DateTime(2077, 2, 22, 22, 22, 0, DateTimeKind.Utc);

        foreach (string imagePath in imageFiles)
        {
            string extension = Path.GetExtension(imagePath);

            string randomNumbers = GenerateRandomNumbers();
            string randomLetters = GenerateRandomLetters();

            string newFileName = randomNumbers + randomLetters + extension;

            string newFilePath = Path.Combine(directoryPath, newFileName);

            File.Move(imagePath, newFilePath);

            File.SetCreationTimeUtc(newFilePath, desiredDateTime);
            File.SetLastWriteTimeUtc(newFilePath, desiredDateTime);
        }

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(Logo);
        Console.ForegroundColor = ConsoleColor.Green;
        Write("Successfully renamed images.");
        //Write("The Creation-Date and Last-Write-Date of the images has been set to 22.02.2077 22:22:00 UTC.");
        Console.ForegroundColor = ConsoleColor.White;
        Thread.Sleep(5000);
    }

    static string GenerateRandomNumbers()
    {
        Random random = new Random();
        return random.Next(10000, 999999999).ToString();
    }

    static string GenerateRandomLetters()
    {
        Random random = new Random();
        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        string randomLetters = new string(Enumerable.Repeat(letters, 5).Select(s => s[random.Next(s.Length)]).ToArray());
        return randomLetters;
    }
}
