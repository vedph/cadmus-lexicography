using System.Collections.Generic;
using System.Text;

namespace Cadmus.Lexicography.Parts;

/// <summary>
/// A words collocation pattern. This is a sequence of tokens, each
/// representing the word class (part of speech) for each word building
/// the collocation pattern. For example, a collocation like
/// "strong tea" would have the pattern ADJ+NOUN. Usually one word in the
/// pattern is the head token, because it is the lexical entry word
/// including this collocation. So, if the lexical entry word is "tea",
/// then the 2nd token (NOUN) is the head.
/// <para>Examples then provide real-world sentences illustrating the use
/// of the collocation. There can be 0-N examples for the collocation, each
/// built of its <see cref="WordCollocationExampleToken"/>'s. These example
/// tokens are not in a 1:1 relationship with the pattern tokens; they are
/// just a way of splitting the example's text so that each portion can be
/// either just connective text or be linked to a specific pattern token.
/// For instance, for pattern tokens ADJ+NOUN an example like "my strong tea"
/// has 3 tokens: connective "my" (no pattern token), pattern token 1 "strong",
/// and pattern token 2 "tea".
/// </para>
/// </summary>
public class WordCollocation
{
    /// <summary>
    /// A generic tag for this pattern. For instance, it could be used to
    /// target a specific sense of the head word.
    /// </summary>
    public string? Tag { get; set; }

    /// <summary>
    /// The pattern frequency rank. Lower is more frequent.
    /// Zero means unknown or not specified.
    /// </summary>
    public short Rank { get; set; }

    /// <summary>
    /// The pattern's tokens, in their order.
    /// </summary>
    public List<WordCollocationToken> Tokens { get; set; } = [];

    /// <summary>
    /// The pattern's examples.
    /// </summary>
    public List<WordCollocationExampleToken>? Examples { get; set; }

    /// <summary>
    /// A free text note about this pattern.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Returns a string representation of this instance.
    /// </summary>
    /// <returns>String.</returns>
    public override string ToString()
    {
        StringBuilder sb = new();
        if (!string.IsNullOrEmpty(Tag)) sb.Append($"{Tag}: ");
        if (Rank > 0) sb.Append($"({Rank}) ");
        sb.Append(string.Join(" ", Tokens));
        return sb.ToString();
    }
}
