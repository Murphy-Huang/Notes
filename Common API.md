[Linq.IEnumerable](https://learn.microsoft.com/zh-cn/dotnet/api/system.linq.enumerable?view=net-8.0)
> **`IEnumerable<T>.Aggregate<TSource>(IEnumerable<TSource>, Func<TSource,TSource,TSource>)`**
>> Simple
>> ```C#
>> string sentence = "the quick brown fox jumps over the lazy dog";
>> string[] words = sentence.Split(' ');
>> string reversed = words.Aggregate((workingSentence, next) => next + " " + workingSentence);
>> ```
> **`OrderBy<TSource,TKey>(IEnumerable<TSource>, Func<TSource,TKey>)`**
>> Simple
>> ```C#
>> // order by param1, then order by param2; t in T
>> List<T>.OrderBy(t => t.param1).ThenBy(t => t.param2);
>> ```

---

Lambda
> Lambda expression
>> Simple
>> ``` C#
>> // specify the type explicityly
>> Func<int, int> square = object (int x, int y) => x * y;
>> Console.WriteLine(square(5, 3));
>> ```
> Lambda to write LINQ
>> Simple
>> ```C#
>> // n in numbers
>> int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
>> int oddNumbers = numbers.Count(n => n % 2 == 1);
>> ```

---

Set Text Component alignment
> `GetComponent<Text>().alignment = CS.UnityEngine.TextAnchor.MiddleCenter;`

---

<span id = 'i'> Set Random seed with GUID </span>
> ```C#
> byte[] buffer = Guid.NewGuid().ToByteArray();
> int seed = BitCoverter.ToInt32(buffer, 0);
> Random rand = Random(seed);
> rand.Next();
> ```