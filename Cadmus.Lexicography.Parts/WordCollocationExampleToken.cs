using System.Collections.Generic;

namespace Cadmus.Lexicography.Parts;

/// <summary>
/// An example token in a word collocation pattern. Example tokens are portions
/// of the example text in their order. The example text is split into tokens
/// so that each token is either just connective text (not linked to any pattern
/// token) or is linked to a specific pattern token. So there is no 1:1
/// relationship between example tokens and pattern tokens; example tokens are
/// just portions of text resulting after splitting the example text so that
/// each portion corresponding to a pattern token is isolated. All the other
/// tokens are connective text and do not refer to any pattern token.
/// </summary>
public class WordCollocationExampleToken
{
    /// <summary>
    /// The pattern token number (1-based) this example token refers to. If this
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
        return $"[{TokenNr}] {string.Join(", ", Literals)}";
    }
}
