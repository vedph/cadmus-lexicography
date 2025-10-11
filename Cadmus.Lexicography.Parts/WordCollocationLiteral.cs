namespace Cadmus.Lexicography.Parts;

/// <summary>
/// A literal instance of a collocation pattern token.
/// Used to build <see cref="WordCollocationExample"/>'s.
/// </summary>
public class WordCollocationLiteral
{
    /// <summary>
    /// The literal's text value.
    /// </summary>
    public string Value { get; set; } = "";

    /// <summary>
    /// An optional tag for the literal. Usually from hierarchical thesaurus
    /// <c>lex-word-cliteral-tags</c>.
    /// </summary>
    public string? Tag { get; set; }

    /// <summary>
    /// An optional note about the literal.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Returns a string representation of this instance.
    /// </summary>
    /// <returns>String.</returns>
    public override string ToString()
    {
        return Tag != null ? $"[{Tag}] {Value}" : Value;
    }
}
