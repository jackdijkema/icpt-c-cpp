﻿List<string> lines = [];

string path = args[0];
string[] liners = File.ReadAllLines(path.ToString());

foreach (var line in liners)
{
    lines.Add(line);
}
int y = lines.Count - 1;

int LINE_OFFSET = 3;

int x = 0 + LINE_OFFSET;

while (true)
{

    RedrawScreen(lines, y, x);

    var key = Console.ReadKey(true);

    if (key.Key == ConsoleKey.RightArrow)
    {
        if (x >= lines[y].Length + LINE_OFFSET)
        {
            x--;
        }
        x++;
        RedrawScreen(lines, y, x);
    }

    if (key.Key == ConsoleKey.LeftArrow)
    {
        if (x <= LINE_OFFSET)
        {
            x++;
        }

        x--;
        RedrawScreen(lines, y, x);
    }

    if (key.Key == ConsoleKey.DownArrow)
    {
        if (y >= lines.Count - 1)
        {
            y--;
        }

        if (x == lines[y].Length + 2)
        {
            x = lines[y + 1].Length + 2;
        }

        y++;
        RedrawScreen(lines, y, x);
    }

    if (key.Key == ConsoleKey.UpArrow)
    {
        if (y <= 0)
        {
            y++;
        }
        y--;
        RedrawScreen(lines, y, x);
    }

    if (key.Key == ConsoleKey.Escape) break;

    bool insertMode = false;

    if (key.Key == ConsoleKey.I)
    {
        insertMode = true;

        RedrawScreen(lines, y, x);
        while (insertMode)
        {
            Console.Write("\x1b[5 q");
            key = Console.ReadKey(intercept: true);

            if (key.Key == ConsoleKey.Escape)
            {
                insertMode = false;
                Console.Write("\x1b[1 q");
                RedrawScreen(lines, y, x);
                Save(lines, path);
                continue;
            }

            if (key.Key == ConsoleKey.Backspace)
            {

                if (x == LINE_OFFSET && lines[y].Length == 0)
                {
                    if (y != 0)
                    {
                        lines.RemoveAt(y);
                        lines[y - 1] += " ";
                        y--;
                        x = lines[y].Length + LINE_OFFSET;
                        RedrawScreen(lines, y, x);
                    }
                    RedrawScreen(lines, y, x);
                }

                if (x > 0 + 3)
                {
                    x--;
                    lines[y] = lines[y].Remove(x - 3, 1);
                }
                RedrawScreen(lines, y, x);
                continue;
            }

            if (key.Key == ConsoleKey.Enter)
            {
                lines.Add("");
                y++;
                x = LINE_OFFSET;
                RedrawScreen(lines, y, x);
                continue;
            }

            string tempLine = lines[y];
            tempLine = tempLine.Insert(x - LINE_OFFSET, key.KeyChar.ToString());
            lines[y] = tempLine;
            x++;
            RedrawScreen(lines, y, x);
        }
    }
}

void RedrawScreen(List<string> lines, int x, int y)
{
    Console.Clear();

    for (int i = 0; i < lines.Count; i++)
    {
        Console.WriteLine($"{i + 1}: {lines[i]}");
    }

    Console.SetCursorPosition(y, x);
}

void Save(List<string> lines, string path)
{
    StreamWriter newFile2 = File.CreateText(path);
    foreach (var line in lines)
    {
        newFile2.WriteLine(line);
    }
    newFile2.Close();
}
