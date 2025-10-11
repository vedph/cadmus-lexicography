using System.Collections.Generic;

namespace Cadmus.Lexicography.Parts;

/// <summary>
/// An example of a word collocation pattern.
/// </summary>
public class WordCollocationExample
{
    /// <summary>
    /// The pattern token number (1-based) this example refers to. If this
    /// is just connective text, this is 0.
    /// </summary>
    public short TokenNr { get; set; }

    /// <summary>
    /// The literals making up this example.
    /// </summary>
    public List<WordCollocationLiteral> Literals { get; set; } = [];

    /// <summary>
    /// Returns a string representation of this instance.
    /// </summary>
    /// <returns>String.</returns>
    public override string ToString()
    {
        return $"[{TokenNr}] {string.Join(" ", Literals)}";
    }
}
