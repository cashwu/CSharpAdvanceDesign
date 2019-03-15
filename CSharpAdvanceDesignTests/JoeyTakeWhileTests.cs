using ExpectedObjects;
using Lab.Entities;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CSharpAdvanceDesignTests
{
    [TestFixture]
    public class JoeyTakeWhileTests
    {
        [Test]
        public void take_cards_until_separate_card()
        {
            var cards = new List<Card>
            {
                new Card { Kind = CardKind.Normal, Point = 2 },
                new Card { Kind = CardKind.Normal, Point = 3 },
                new Card { Kind = CardKind.Normal, Point = 4 },
                new Card { Kind = CardKind.Separate },
                new Card { Kind = CardKind.Normal, Point = 5 },
                new Card { Kind = CardKind.Normal, Point = 6 },
            };

            var actual = JoeyTakeWhile(cards);

            var expected = new List<Card>
            {
                new Card { Kind = CardKind.Normal, Point = 2 },
                new Card { Kind = CardKind.Normal, Point = 3 },
                new Card { Kind = CardKind.Normal, Point = 4 },
            };

            expected.ToExpectedObject().ShouldMatch(actual);
        }

        private IEnumerable<Card> JoeyTakeWhile(IEnumerable<Card> cards)
        {
            var enumerator = cards.GetEnumerator();

            // while (enumerator.MoveNext())
            // {
            //     if (enumerator.Current.Kind == CardKind.Separate)
            //     {
            //         yield break;
            //     }
            //     
            //     yield return enumerator.Current;
            // }

            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Kind != CardKind.Separate)
                {
                    yield return enumerator.Current;
                }
                else
                {
                    yield break;
                }
            }
        }
    }
}