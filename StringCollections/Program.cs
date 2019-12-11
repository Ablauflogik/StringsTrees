using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringCollections
{
    class Program
    {
        public static void Main(string[] args)
        {
            UsingTree();
            //UsingList();
        }
        public static void UsingList()
        {

            TimeSpan sortTime;
            TimeSpan ZTime;
            TimeSpan HeTime;
            TimeSpan eTime;
            DateTime start;

            List<string> words = ReadInWords("words.txt");

            start = DateTime.Now;
            words.Sort((a, b) => b.CompareTo(a));
            sortTime = DateTime.Now - start;

            ///PrintWords(words, "Full List");

            start = DateTime.Now;
            List<string> wordsBeginWithZ = words.Where(x => x.ToLower().StartsWith("z")).ToList();
            ZTime = DateTime.Now - start;

            /*
            Console.WriteLine("   {0,-35} {1,20}", "Value of Minutes Component:", interval.Minutes);
            Console.WriteLine("   {0,-35} {1,20}", "Total Number of Minutes:", interval.TotalMinutes);
            Console.WriteLine("   {0,-35} {1,20:N0}", "Value of Seconds Component:", interval.Seconds);
            */
            PrintWords(wordsBeginWithZ, "Begins with Z");



            start = DateTime.Now;
            List<string> wordsBeginWithHe = words.Where(x => x.ToLower().StartsWith("he")).ToList();
            HeTime = DateTime.Now - start;

            PrintWords(wordsBeginWithHe, "Begins with He");


            start = DateTime.Now;
            List<string> wordsWithEAtSecondPos = words.Where(x => x != null && x.Length > 1 && x.ToLower()[1] == 'e').ToList();
            eTime = DateTime.Now - start;
            PrintWords(wordsWithEAtSecondPos, "Has e second position");


            Console.WriteLine("Total Time Sort: {0,2:N0}:{1,2:N0}", sortTime.Seconds, sortTime.Milliseconds);
            Console.WriteLine("Total Time Begins with Z: {0,2:N0}:{1,2:N0}", ZTime.Seconds, ZTime.Milliseconds);
            Console.WriteLine("Total Time He: {0,2:N0}:{1,2:N0}", HeTime.Seconds, HeTime.Milliseconds);
            Console.WriteLine("Total Time E: {0,2:N0}:{1,2:N0}", eTime.Seconds, eTime.Milliseconds);

            Console.ReadLine();
        }
        public static void UsingTree()
        {

            TimeSpan sortTime;
            TimeSpan ZTime;
            TimeSpan HeTime;
            TimeSpan eTime;
            DateTime start;

            List<string> words = ReadInWords("words.txt");
            Tree<string> tree = new Tree<string>();
            
            for (char i = 'a'; i < 'z'; i++)
            {
                tree.Root.AddChild(i.ToString());
            }

            
            start = DateTime.Now;
            words.Sort((a, b) => b.CompareTo(a));
            sortTime = DateTime.Now - start;

            foreach (var word in words)
            {
                if (word[0] >= 'a' && word[0] <= 'z')
                {
                    tree.Root.Nodes[word[0] - 'a'].AddChild(word);
                }
                else
                {
                    tree.Root.AddChild(word);
                }
            }

            ///PrintWords(words, "Full List");

            start = DateTime.Now;
            List<string> wordsBeginWithZ = tree.Where(x => x.ToLower().StartsWith("z")).ToList();
            ZTime = DateTime.Now - start;

            /*
            Console.WriteLine("   {0,-35} {1,20}", "Value of Minutes Component:", interval.Minutes);
            Console.WriteLine("   {0,-35} {1,20}", "Total Number of Minutes:", interval.TotalMinutes);
            Console.WriteLine("   {0,-35} {1,20:N0}", "Value of Seconds Component:", interval.Seconds);
            */
            PrintWords(wordsBeginWithZ, "Begins with Z");



            start = DateTime.Now;
            List<string> wordsBeginWithHe = tree.Where(x => x.ToLower().StartsWith("he")).ToList();
            HeTime = DateTime.Now - start;

            PrintWords(wordsBeginWithHe, "Begins with He");


            start = DateTime.Now;
            List<string> wordsWithEAtSecondPos = tree.Where(x => x != null && x.Length > 1 && x.ToLower()[1] == 'e').ToList();
            eTime = DateTime.Now - start;
            PrintWords(wordsWithEAtSecondPos, "Has e second position");


            Console.WriteLine("Total Time Sort: {0,2:N0}:{1,2:N0}", sortTime.Seconds, sortTime.Milliseconds);
            Console.WriteLine("Total Time Begins with Z: {0,2:N0}:{1,2:N0}", ZTime.Seconds, ZTime.Milliseconds);
            Console.WriteLine("Total Time He: {0,2:N0}:{1,2:N0}", HeTime.Seconds, HeTime.Milliseconds);
            Console.WriteLine("Total Time E: {0,2:N0}:{1,2:N0}", eTime.Seconds, eTime.Milliseconds);

            Console.ReadLine();
        }
        public static void PrintWords(List<string> words, string header)
        {

            Console.WriteLine("*****" + header + "*****");
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
        }

        public static List<string> ReadInWords(string fileName)
        {
            List<string> wordList = new List<string>();
            using (StreamReader streamReader = new StreamReader(fileName))
            {

                while (!streamReader.EndOfStream)
                {
                    string word = streamReader.ReadLine();
                    wordList.Add(word);
                }
            } //close off using statement ... closes file stream and disposes stream reader
            return wordList;
        }
    }
}
