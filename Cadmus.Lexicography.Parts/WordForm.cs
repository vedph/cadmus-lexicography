using System.Collections.Generic;
using System.Text;

namespace Cadmus.Lexicography.Parts;

/// <summary>
/// A word form (written or spoken).
/// </summary>
public class WordForm
{
    /// <summary>
    /// The form type, e.g. "written", "spoken", "inflected", etc. Usually
    /// from thesaurus <c>lex-word-form-types</c>.
    /// </summary>
    public string Type { get; set; } = "";

    /// <summary>
    /// An optional prefix for this form, e.g. a preposition like <c>to</c> in
    /// <c>to take</c>. Optionally from thesaurus <c>lex-word-form-prefixes</c>.
    /// </summary>
    public string? Prefix { get; set; }

    /// <summary>
    /// The form's value.
    /// </summary>
    public string Value { get; set; } = "";

    /// <summary>
    /// An optional suffix for this form, e.g. a particle after a phrasal verb,
    /// e.g. <c>off</c> in <c>to take off</c>. Optionally from thesaurus
    /// <c>lex-word-form-suffixes</c>.
    /// </summary>
    public string? Suffix { get; set; }

    /// <summary>
    /// The POS tag. This can be composite, e.g. an UPOS + dot + XPOS when
    /// using Universal Dependencies tags. Usually from a hierarchical thesaurus
    /// <c>lex-word-form-pos-tags</c>. Related: TEI <c>gramGrp/pos</c>, Lemon
    /// <c>partOfSpeech</c>.
    /// </summary>
    public string? Pos { get; set; }

    /// <summary>
    /// Grammatical features, e.g. <c>Number=Plur</c>, <c>Tense=Past</c>, or
    /// any other kind of feature. Usually from hierarchical thesaurus
    /// <c>lex-word-form-features</c>.
    /// </summary>
    public List<string>? Features { get; set; }

    /// <summary>
    /// Usage or other tags, e.g. <c>archaic</c>, <c>informal</c>, etc. Usually
    /// from hierarchical thesaurus <c>lex-word-form-tags</c>. Related:
    /// TEI <c>usg</c>, Lemon <c>usage</c>, <c>context</c>.
    /// </summary>
    public List<string>? Tags { get; set; }

    /// <summary>
    /// A general note about this form. Related to TEI <c>note</c>.
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>String.</returns>
    public override string ToString()
    {
        StringBuilder sb = new();
        if (!string.IsNullOrEmpty(Type)) sb.AppendFormat("[{0}]: ", Type);

        if (!string.IsNullOrEmpty(Prefix)) sb.Append(Prefix).Append(' ');
        sb.Append(Value);
        if (!string.IsNullOrEmpty(Suffix)) sb.Append(' ').Append(Suffix);

        if (!string.IsNullOrEmpty(Pos)) sb.Append(" [").Append(Pos).Append(']');
        if (Features?.Count > 0)
            sb.Append(" {").AppendJoin(", ", Features).Append('}');

        if (Tags?.Count > 0)
            sb.Append(" <").AppendJoin(", ", Tags).Append('>');

        if (!string.IsNullOrEmpty(Note)) sb.Append(" (").Append(Note).Append(')');

        return sb.ToString();
    }
}
