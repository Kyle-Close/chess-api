namespace Chess;
using System.Diagnostics;

public enum StockfishDifficulty
{
    SKILL_LEVEL,
    ELO
}


public class Stockfish : IDisposable
{
    private static string ResolveEnginePath()
    {
        var env = Environment.GetEnvironmentVariable("STOCKFISH_PATH");
        if (!string.IsNullOrWhiteSpace(env) && File.Exists(env)) return env;

        var candidates = new[]
        {
            "/usr/bin/stockfish",
            "/usr/games/stockfish",
            "/usr/local/bin/stockfish",
            "/chess/app/stockfish/stockfish-ubuntu-x86-64-avx2"
        };
        foreach (var c in candidates) if (File.Exists(c)) return c;

        throw new FileNotFoundException("Stockfish binary not found. Set STOCKFISH_PATH.");
    }

    private readonly Process _proc;

    public Stockfish()
    {
        var exe = ResolveEnginePath();
        var psi = new ProcessStartInfo
        {
            FileName = exe,
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true,
            WorkingDirectory = Path.GetDirectoryName(exe) ?? "/"
        };
        _proc = new Process { StartInfo = psi };
        _proc.Start();
        _proc.StandardInput.AutoFlush = true;
        _proc.BeginOutputReadLine();
        _proc.OutputDataReceived += (_, e) => { if (!string.IsNullOrEmpty(e.Data)) Console.WriteLine("Engine: " + e.Data); };
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
                    _proc.OutputDataReceived -= handler;
                }
            }
        };

        _proc.OutputDataReceived += handler;

        _proc.StandardInput.WriteLine("uci");
        SetDifficulty(strength);
        _proc.StandardInput.WriteLine("ucinewgame");
        _proc.StandardInput.WriteLine("isready");
        _proc.StandardInput.WriteLine($"position fen {fen}");
        _proc.StandardInput.WriteLine("go movetime 500");

        return tcs.Task;
    }


    private void SetDifficulty(int strength)
    {
        var difficulty = GetStockfishDifficulty(strength);

        if (difficulty == StockfishDifficulty.SKILL_LEVEL)
        {
            _proc.StandardInput.WriteLine($"setoption name Skill Level value {strength}");
        }


        else if (difficulty == StockfishDifficulty.ELO)
        {
            _proc.StandardInput.WriteLine("setoption name UCI_LimitStrength value true");
            _proc.StandardInput.WriteLine($"setoption name UCI_Elo value {strength}");
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
        if (!_proc.HasExited)
        {
            _proc.Kill();
        }
        _proc.Dispose();
    }
}

