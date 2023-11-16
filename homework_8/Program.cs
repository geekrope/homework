using System;
using System.Collections.Generic;
using System.IO;

namespace GeniusFool
{
    public class Question
    {
        private string question;
        private string answer;
        private ConsoleBridge console;

        public void Print()
        {
            console.WriteLine(question);
        }
        public int GetPoints(string givenAnswer)
        {
            return givenAnswer == answer ? 1 : 0;
        }

        public Question(string question, string answer, ConsoleBridge console)
        {
            this.question = question;
            this.answer = answer;
            this.console = console;
        }
    }

    public class Player
    {
        private string name;
        private int score;

        public string Name
        {
            get => name;
        }
        public int Score
        {
            get => score;
        }

        public void AddPoints(int points)
        {
            score += points;
        }
        public void ResetScore()
        {
            score = 0;
        }
        public string GetDiagnosis(string[] diagnoses, int maxScore)
        {
            return diagnoses[(int)Math.Ceiling((double)score / maxScore * (diagnoses.Length - 1))];
        }

        public Player(string name)
        {
            this.name = name;
        }
        public Player(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }

    public interface ConsoleBridge
    {
        string? ReadLine();
        void WriteLine(object message);
    }

    public class CSharpConsole : ConsoleBridge
    {
        public string? ReadLine()
        {
            return Console.ReadLine();
        }
        public void WriteLine(object message)
        {
            Console.WriteLine(message);
        }
    }

    public class Playground
    {
        private Question[] questions;
        private Player? player;
        private Storage memento;
        private ConsoleBridge console;
        private int[] order;
        private string[] diagnoses;

        private string RequireString()
        {
            var input = console.ReadLine();

            if (input != null)
            {
                return input;
            }
            else
            {
                throw new Exception("something went wrong");
            }
        }
        private int RequireInt()
        {
            var input = RequireString();
            var parsedInput = 0;

            if (int.TryParse(input, out parsedInput))
            {
                return parsedInput;
            }
            else
            {
                console.WriteLine("number required");
                return RequireInt();
            }
        }
        private bool RequireBool()
        {
            var input = RequireString().ToLower();

            if (input == "yes" || input == "no")
            {
                return input == "yes";
            }
            else
            {
                console.WriteLine("choice required");
                return RequireBool();
            }
        }

        private void PrintMessage(string message)
        {
            console.WriteLine(message);
        }

        private void InitialisePlayer(string name)
        {
            player = new Player(name);
        }

        public int[] SequentialOrder()
        {
            var order = new int[questions.Length];

            for (int index = 0; index < order.Length; index++)
            {
                order[index] = index;
            }

            return order;
        }
        public void Shuffle()
        {
            var indices = new int[questions.Length];
            var unusedIndices = new List<int>();
            var random = new Random();

            for (int index = 0; index < indices.Length; index++)
            {
                unusedIndices.Add(index);
            }

            var order = 0;
            for (int index = random.Next(unusedIndices.Count); unusedIndices.Count > 0; index = random.Next(unusedIndices.Count), order++)
            {
                indices[order] = unusedIndices[index];

                unusedIndices.RemoveAt(index);
            }

            this.order = indices;
        }
        public void Launch()
        {
            PrintMessage("enter your name");
            var playerName = RequireString();
            InitialisePlayer(playerName);

            var running = true;
            for (; running;)
            {
                Shuffle();

                foreach (var questionOrder in order)
                {
                    var question = questions[questionOrder];
                    question.Print();

                    var answer = RequireString();
                    player?.AddPoints(question.GetPoints(answer));
                }

                PrintMessage("previous attempts");
                foreach (var result in memento.LoadResult())
                {
                    PrintMessage($"{result.Name} {result.Score}");
                }

                if (player != null)
                {
                    memento.SaveResult(player);
                }

                PrintMessage($"{player?.Name} your diagnosis: {player?.GetDiagnosis(diagnoses, questions.Length)}, score:{player.Score}");
                PrintMessage("do you want to play once again/");
                player?.ResetScore();
                running = RequireBool();
            }
        }

        public Playground(Question[] questions, string[] diagnoses, ConsoleBridge console, string fileName)
        {
            this.questions = questions;
            this.order = SequentialOrder();
            this.memento = new Storage(fileName);
            this.console = console;

            this.diagnoses = diagnoses;
        }
    }

    public class Storage
    {
        private string _fileName;

        public void SaveResult(Player player)
        {
            using (var file = new StreamWriter(_fileName))
            {
                file.WriteLine($"{player.Name} {player.Score}\n");
                file.Close();
            }
        }
        public Player[] LoadResult()
        {
            var players = new List<Player>();

            using (var file = new StreamReader(_fileName))
            {
                while (!file.EndOfStream)
                {
                    var raw = file.ReadLine()?.Split(' ',StringSplitOptions.RemoveEmptyEntries);

                    if (raw != null && raw.Length > 0)
                    {
                        players.Add(new Player(raw[0], int.Parse(raw[1])));
                    }
                }

                file.Close();
            }

            return players.ToArray();
        }

        public Storage(string fileName)
        {
            _fileName = fileName;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var consoleImplementation = new CSharpConsole();
            var playground = new Playground(new Question[] { new Question("2+2", "4", consoleImplementation), new Question("0?", "0", consoleImplementation), new Question("11 v kvadrate", "121", consoleImplementation) }, new string[] { "nol ballov", "lox", "idiot", "geniy" }, consoleImplementation, "res.txt");
            playground.Launch();
        }
    }

}