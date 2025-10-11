using Cadmus.Core;
using Fusi.Tools.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cadmus.Lexicography.Parts;

/// <summary>
/// Lemma. Related: TEI <c>form @type="lemma"</c>, Lemon: <c>lemon:LexicalEntry</c>,
/// <c>lemon:canonicalForm</c>.
/// <para>Tag: <c>it.vedph.lexicography.lemma</c>.</para>
/// </summary>
[Tag("it.vedph.lexicography.lemma")]
public sealed class LemmaPart : PartBase
{
    /// <summary>
    /// The calculated lexicographic ID for this form. The lexicographic ID
    /// can be defined in different ways according to the project. For instance,
    /// it could just be the main word form with only lowercase letters and no
    /// diacritics, followed by the homograph number if greater than 0.
    /// In other projects, it could be a more complex string, usually built
    /// of hex text element codes.
    /// </summary>
    public string Lid { get; set; } = "";

    /// <summary>
    /// The optional homograph number (1, 2, ...) or just 0. Related: TEI
    /// <c>hom</c>, Lemon <c>sense</c> (for disambiguation).
    /// </summary>
    public short? Homograph { get; set; }

    /// <summary>
    /// The word forms for this lemma. There must be at least one form.
    /// Usually this contains also forms strictly related to the lemma,
    /// like written variants, pronunciation variants, etc. Alternatively,
    /// you can use a <see cref="WordFormsPart"/> to store them (possibly
    /// with roles).
    /// </summary>
    public List<WordForm> Forms { get; set; } = [];

    /// <summary>
    /// Get all the key=value pairs (pins) exposed by the implementor.
    /// </summary>
    /// <param name="item">The optional item. The item with its parts
    /// can optionally be passed to this method for those parts requiring
    /// to access further data.</param>
    /// <returns>The pins.</returns>
    public override IEnumerable<DataPin> GetDataPins(IItem? item = null)
    {
        DataPinBuilder builder = new();

        builder.AddValue("lid", Lid);
        WordFormsPart.AddWordFormPins(Forms, builder);

        return builder.Build(this);
    }

    /// <summary>
    /// Gets the definitions of data pins used by the implementor.
    /// </summary>
    /// <returns>Data pins definitions.</returns>
    public override IList<DataPinDefinition> GetDataPinDefinitions()
    {
        return new List<DataPinDefinition>(
        [
            new DataPinDefinition(DataPinValueType.String,
                "lid",
                "The lexicographic ID of the lemma."),
            new DataPinDefinition(DataPinValueType.String,
                "value",
                "The unique values of the forms."),
            new DataPinDefinition(DataPinValueType.String,
                "dec-value",
                "The unique decorated values of the forms, " +
                    "in the form type[pos]:value."),
            new DataPinDefinition(DataPinValueType.String,
                "pos",
                "The unique POS tags used in the forms.")
        ]);
    }

    /// <summary>
    /// Converts to string.
    /// </summary>
    /// <returns>
    /// A <see cref="string" /> that represents this instance.
    /// </returns>
    public override string ToString()
    {
        StringBuilder sb = new();

        sb.Append("[Lemma]").Append(' ').Append(Lid);
        if (Forms.Count > 0)
        {
            sb.Append(": ");
            sb.AppendJoin("; ", Forms.Select(f => f.ToString()));
        }

        return sb.ToString();
    }
}
