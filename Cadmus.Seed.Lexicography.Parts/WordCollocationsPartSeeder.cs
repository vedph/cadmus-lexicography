using Bogus;
using Cadmus.Core;
using Cadmus.Lexicography.Parts;
using Fusi.Tools.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cadmus.Seed.Lexicography.Parts;

/// <summary>
/// Seeder for <see cref="WordCollocationsPart"/>.
/// Tag: <c>seed.it.vedph.lexicography.word-collocations</c>.
/// </summary>
/// <seealso cref="PartSeederBase" />
[Tag("seed.it.vedph.lexicography.word-collocations")]
public sealed class WordCollocationsPartSeeder : PartSeederBase
{
    private static List<WordCollocation> GenerateCollocations(Faker f)
    {
        string[] posTags = ["noun", "verb", "adj", "adv", "det", "prep", "pron"];
        List<WordCollocation> collocations = [];
        HashSet<string> seen = new(StringComparer.OrdinalIgnoreCase);

        int tot = f.Random.Int(1, 6); // number of collocations to generate
        int attempts = 0;
        while (collocations.Count < tot && attempts < tot * 6)
        {
            attempts++;
            WordCollocation coll = GenerateCollocation(f, posTags);

            // uniqueness by POS sequence
            string key = string.Join("+", coll.Tokens.Select(t => t.Pos));
            if (!seen.Add(key)) continue;

            collocations.Add(coll);
        }

        return collocations;
    }

    private static WordCollocation GenerateCollocation(Faker f, string[] posTags)
    {
        int tokensCount = f.Random.Int(1, 3);
        List<WordCollocationToken> tokens = new(tokensCount);
        for (int i = 0; i < tokensCount; i++)
            tokens.Add(GenerateToken(f, posTags));

        // ensure one head
        tokens[f.Random.Int(0, tokensCount - 1)].IsHead = true;

        WordCollocation coll = new()
        {
            Rank = (short)f.Random.Int(0, 8),
            Tokens = tokens,
            Note = f.Random.Bool(0.2f) ? f.Lorem.Sentence(3) : null
        };

        if (f.Random.Bool(0.75f))
        {
            coll.Examples = [];
            int exCount = f.Random.Int(1, 2);
            for (int e = 0; e < exCount; e++)
                coll.Examples.Add(GenerateExample(f, tokensCount));
        }

        return coll;
    }

    private static WordCollocationToken GenerateToken(Faker f, string[] posTags)
    {
        return new WordCollocationToken
        {
            Pos = f.PickRandom(posTags),
            IsOptional = f.Random.Bool(0.15f),
            IsHead = false,
            Features = f.Random.Bool(0.12f) ? [f.Lorem.Word()] : null,
            Note = f.Random.Bool(0.05f) ? f.Lorem.Sentence(4) : null
        };
    }

    private static WordCollocationExample GenerateExample(Faker f, int tokensCount)
    {
        WordCollocationExample ex = new()
        {
            TokenNr = (short)(f.Random.Bool(0.2f) ? 0 : f.Random.Int(1, tokensCount)),
            Literals = []
        };

        int lits = f.Random.Int(1, 3);
        for (int l = 0; l < lits; l++)
            ex.Literals.Add(GenerateLiteral(f));

        return ex;
    }

    private static WordCollocationLiteral GenerateLiteral(Faker f)
    {
        int words = f.Random.Int(1, 2);
        string value = words == 1 ? f.Lorem.Word() : $"{f.Lorem.Word()} {f.Lorem.Word()}";
        return new WordCollocationLiteral
        {
            Value = value,
            Tag = f.Random.Bool(0.10f) ? f.Lorem.Word() : null,
            Note = f.Random.Bool(0.05f) ? f.Lorem.Sentence(3) : null
        };
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

        Faker f = new("en");

        WordCollocationsPart part = new()
        {
            Collocations = GenerateCollocations(f)
        };

        SetPartMetadata(part, roleId, item);

        return part;
    }
}
