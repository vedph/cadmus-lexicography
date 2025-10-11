using System;
using System.Collections.Generic;
using System.Text;
using Cadmus.Core;
using Fusi.Tools.Configuration;

namespace Cadmus.Lexicography.Parts;

/// <summary>
/// A list of word forms, e.g. inflected forms of a lemma.
/// This part has role-dependent thesauri.
/// <para>Tag: <c>it.vedph.lexicography.word-forms</c>.</para>
/// </summary>
[Tag("it.vedph.lexicography.word-forms")]
public sealed class WordFormsPart : PartBase
{
    /// <summary>
    /// The word forms in this list.
    /// </summary>
    public List<WordForm> Forms { get; set; } = [];

    internal static void AddWordFormPins(IList<WordForm> forms, DataPinBuilder builder)
    {
        HashSet<string> values = [];
        HashSet<string> typedValues = [];
        HashSet<string> pos = [];
        foreach (WordForm form in forms)
        {
            values.Add(form.Value);
            if (!string.IsNullOrEmpty(form.Type) ||
                !string.IsNullOrEmpty(form.Pos))
            {
                typedValues.Add(form.Type +
                    $"{(string.IsNullOrEmpty(form.Pos) ? "" : "[" + form.Pos + "]")}" +
                    $":{form.Value}");
            }
            if (!string.IsNullOrEmpty(form.Pos)) pos.Add(form.Pos);
        }
        builder.AddValues("value", values);
        builder.AddValues("dec-value", typedValues);
        if (pos.Count > 0) builder.AddValues("pos", pos);
    }

    /// <summary>
    /// Get all the key=value pairs (pins) exposed by the implementor.
    /// </summary>
    /// <param name="item">The optional item. The item with its parts
    /// can optionally be passed to this method for those parts requiring
    /// to access further data.</param>
    /// <returns>The pins: <c>tot-count</c> and a collection of pins.</returns>
    public override IEnumerable<DataPin> GetDataPins(IItem? item = null)
    {
        DataPinBuilder builder = new();

        builder.Set("tot", Forms.Count, false);
        if (Forms.Count > 0) AddWordFormPins(Forms, builder);

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
            new DataPinDefinition(DataPinValueType.Integer,
               "tot-count",
               "The total count of forms."),
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

        sb.Append("[WordForms]");

        if (Forms?.Count > 0)
        {
            sb.Append(' ');
            int n = 0;
            foreach (WordForm form in Forms)
            {
                if (++n > 3) break;
                if (n > 1) sb.Append("; ");
                sb.Append(form);
            }
            if (Forms.Count > 3)
                sb.Append("...(").Append(Forms.Count).Append(')');
        }

        return sb.ToString();
    }
}
