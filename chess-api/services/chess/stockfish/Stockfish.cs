namespace Chess;
using System.Diagnostics;

public enum StockfishDifficulty
{
    SKILL_LEVEL,
    ELO
}


public class Stockfish : IDisposable
{
    const string UBUNTU_PATH = "/home/kyle/Downloads/stockfish-ubuntu-x86-64-avx2/stockfish/stockfish-ubuntu-x86-64-avx2";
    const string MAC_PATH = "/Users/kyleclose/Downloads/stockfish 2/stockfish-macos-m1-apple-silicon";

    public Process MyProcess { get; private set; } = null!;

    public Stockfish()
    {
        try
        {
            MyProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = UBUNTU_PATH,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            MyProcess.Start();
            MyProcess.StandardInput.AutoFlush = true;

            MyProcess.OutputDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                    Console.WriteLine($"Engine: {e.Data}");
            };

            MyProcess.BeginOutputReadLine();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public Task<string> ExecuteMoveAsync(int strength, string fen)
    {
        var tcs = new TaskCompletionSource<string>();

        DataReceivedEventHandler? handler = null;
        handler = (sender, e) =>
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                Console.WriteLine("Engine: " + e.Data);

                if (e.Data.StartsWith("bestmove"))
                {
                    var parts = e.Data.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    var move = parts.Length >= 2 ? parts[1] : "none";

                    // complete the task
                    tcs.TrySetResult(move);

                    // unsubscribe after result
                    MyProcess.OutputDataReceived -= handler;
                }
            }
        };

        MyProcess.OutputDataReceived += handler;

        MyProcess.StandardInput.WriteLine("uci");
        SetDifficulty(strength);
        MyProcess.StandardInput.WriteLine("ucinewgame");
        MyProcess.StandardInput.WriteLine("isready");
        MyProcess.StandardInput.WriteLine($"position fen {fen}");
        MyProcess.StandardInput.WriteLine("go movetime 500");

        return tcs.Task;
    }


    private void SetDifficulty(int strength)
    {
        var difficulty = GetStockfishDifficulty(strength);

        if (difficulty == StockfishDifficulty.SKILL_LEVEL)
        {
            MyProcess.StandardInput.WriteLine($"setoption name Skill Level value {strength}");
        }
        else if (difficulty == StockfishDifficulty.ELO)
        {
            MyProcess.StandardInput.WriteLine("setoption name UCI_LimitStrength value true");
            MyProcess.StandardInput.WriteLine($"setoption name UCI_Elo value {strength}");
        }
    }

    private StockfishDifficulty GetStockfishDifficulty(int strength)
    {
        if (strength >= 0 && strength <= 20) return StockfishDifficulty.SKILL_LEVEL;
        else if (strength >= 1320 && strength <= 4000) return StockfishDifficulty.ELO;
        else throw new Exception("Invalid strength");
    }

    public void Dispose()
    {
        if (!MyProcess.HasExited)
        {
            MyProcess.Kill();
        }
        MyProcess.Dispose();
    }
}

