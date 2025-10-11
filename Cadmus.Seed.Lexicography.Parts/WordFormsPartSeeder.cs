using Bogus;
using Cadmus.Core;
using Cadmus.Lexicography.Parts;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;

namespace Cadmus.Seed.Lexicography.Parts;

/// <summary>
/// Seeder for <see cref="WordFormsPart"/>.
/// Tag: <c>seed.it.vedph.lexicography.word-forms</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.lexicography.word-forms")]
public sealed class WordFormsPartSeeder : PartSeederBase
{
    internal static List<WordForm> GetWordForms(int count, Faker f,
        string? lemma = null)
    {
        List<WordForm> forms = [];
        // pools for fake values
        string[] types = ["written", "spoken", "inflected", "variant", "phonetic"];
        string[] prefixes = ["to", "the", "a", "in", "at", "on"];
        string[] suffixes = ["off", "up", "out", "down"];
        string[] posTags =
        [
            "NOUN", "VERB", "ADJ", "ADV", "PRON", "DET", "ADP", "NUM", "CONJ", "INTJ"
        ];
        string[] featurePool =
        [
            "Number=Sing", "Number=Plur", "Tense=Past", "Tense=Pres",
            "Mood=Indicative", "Degree=Cmp"
        ];
        string[] tagPool =
        [
            "archaic", "informal", "regional", "technical", "literary"
        ];

        for (int i = 0; i < count; i++)
        {
            WordForm wf = new()
            {
                // the first word form is always the lemma form, others are
                // variants, pronunciation, etc.
                Value = i == 0 && lemma != null ? lemma : f.Lorem.Word(),
                Type = f.PickRandom(types),
                Prefix = f.Random.Bool(0.2f) ? f.PickRandom(prefixes) : null,
                Suffix = f.Random.Bool(0.15f) ? f.PickRandom(suffixes) : null,
                Pos = f.Random.Bool(0.8f) ? f.PickRandom(posTags) : null,
                Note = f.Random.Bool(0.25f) ? f.Lorem.Sentence() : null
            };

            // optionally add features
            if (f.Random.Bool(0.45f))
            {
                int nf = f.Random.Number(1, 3);
                wf.Features = new List<string>(nf);
                for (int j = 0; j < nf; j++)
                    wf.Features.Add(f.PickRandom(featurePool));
            }

            // optionally add tags
            if (f.Random.Bool(0.35f))
            {
                int nt = f.Random.Number(1, 3);
                wf.Tags = new List<string>(nt);
                for (int j = 0; j < nt; j++)
                    wf.Tags.Add(f.PickRandom(tagPool));
            }

            forms.Add(wf);
        }
        return forms;
    }

    /// <summary>
    /// Creates and seeds a new part.
    /// </summary>
    /// <param name="item">The item this part should belong to.</param>
    /// <param name="roleId">The optional part role ID.</param>
    /// <param name="factory">The part seeder factory. This is used
    /// for layer parts, which need to seed a set of fragments.</param>
    /// <returns>A new part or null.</returns>
    /// <exception cref="ArgumentNullException">item or factory</exception>
    public override IPart? GetPart(IItem item, string? roleId,
        PartSeederFactory? factory)
    {
        ArgumentNullException.ThrowIfNull(item);

        WordFormsPart part = new Faker<WordFormsPart>()
           .RuleFor(p => p.Forms, f => GetWordForms(f.Random.Number(1, 3), f))
           .Generate();

        SetPartMetadata(part, roleId, item);

        return part;
    }
}
