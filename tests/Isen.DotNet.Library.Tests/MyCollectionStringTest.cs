using System;
using Xunit;
using Isen.DotNet.Library.Lists;
using System.Collections.Generic;

namespace Isen.DotNet.Library.Tests
{
    public class MyCollectionStringTest
    {
        [Fact]
        public void CountTest()
        {
            var list = new MyCollection<string>();
            Assert.True(list.Count == 0);
            list.Add("A");
            Assert.True(list.Count == 1);
            list.Add("B");
            Assert.True(list.Count == 2);
            list.Add("C");
            Assert.True(list.Count == 3);
        }

        [Fact]
        public void AddTest()
        {
            var list = new MyCollection<string>();
            list.Add("A");
            list.Add("B");
            list.Add("C");            
            var targetArray = new string[] {"A", "B", "C"};
            Assert.Equal(targetArray, list);
        }

        [Fact]
        public void IndexTest()
        {
            var list = new MyCollection<string>();
            list.Add("A");
            list.Add("B");
            list.Add("C");
            Assert.True(list[0] == "A");
            Assert.True(list[1] == "B");
            Assert.True(list[2] == "C");

            list[0] = "Z";
            Assert.True(list[0] == "Z");
        }

        [Fact]
        public void RemoveAtTest()
        {
            var list = new MyCollection<string>();
            list.Add("A");
            list.Add("B");
            list.Add("C");
            list.Add("D");

            list.RemoveAt(0);
            Assert.True(list.Count == 3);
            Assert.True(list[0] == "B");
            Assert.True(list[1] == "C");
            Assert.True(list[2] == "D");

            list.RemoveAt(1);
            Assert.True(list.Count == 2);
            Assert.True(list[0] == "B");
            Assert.True(list[1] == "D");

            list.RemoveAt(1);
            Assert.True(list.Count == 1);
            Assert.True(list[0] == "B");

            list.RemoveAt(0);
            Assert.True(list.Count == 0);

            try
            {
                list.RemoveAt(0);
            }
            catch(Exception e)
            {
                Assert.True(
                    e is IndexOutOfRangeException);
            }

            try
            {
                list.RemoveAt(1);
            }
            catch(Exception e)
            {
                Assert.True(
                    e is IndexOutOfRangeException);
            }

            try
            {
                list.RemoveAt(-1);
            }
            catch(Exception e)
            {
                Assert.True(
                    e is IndexOutOfRangeException);
            }
            
        }
    
        [Fact]
        public void IndexOfTest()
        {
            var list = new MyCollection<string>();
            list.Add("A");
            list.Add("B");
            list.Add("B");
            list.Add("C");
            Assert.True(list.IndexOf("B") == 1);
            Assert.True(list.IndexOf("A") == 0);
            Assert.True(list.IndexOf("C") == 3);
            Assert.True(list.IndexOf("Z") < 0);
            Assert.True(list.IndexOf(null) < 0);

            #pragma warning disable xUnit2017
            Assert.True(list.Contains("A"));
            Assert.False(list.Contains("Z"));
            #pragma warning restore xUnit2017
        } 

        [Fact]
        public void RemoveTest()
        {
            var list = new MyCollection<string>();
            list.Add("A");
            list.Add("B");
            list.Add("B");
            list.Add("C");
            Assert.True(list.Remove("B"));
            Assert.True(list.Count == 3);
            Assert.True(list[2] == "C");
            Assert.False(list.Remove("Z"));
        }   

         [Fact]
        public void InsertTest()
        {
            var list = new MyCollection<string>();
            list.Insert(0, "C");
            // C
            list.Insert(0, "B");
            // B C
            list.Insert(0, "A");
            // A B C
            list.Insert(3, "D");
            // A B C D
            list.Insert(2, "b");
            // A B b C D
            var targetArray = 
                new string[] {"A", "B", "b", "C", "D"};
            Assert.Equal(targetArray, list);

            try
            {
                list.Insert(-1, "Z");
            }
            catch(Exception e)
            {
                Assert.True(
                    e is ArgumentOutOfRangeException);
            }

            try
            {
                list.Insert(6, "Z");
            }
            catch(Exception e)
            {
                Assert.True(
                    e is ArgumentOutOfRangeException);
            }
        }
    
        [Fact]
        public void EnumeratorTest()
        {
            var list = new MyCollection<string>();
            list.Add("A");
            list.Add("B");
            list.Add("C");
            list.Add("D");

            Assert.True(
                list is IEnumerable<string>);

            var targetArray = 
                new string[] { "A", "B", "C", "D" };
            foreach(var item in list)
            {
                Assert.True(true);
            }
            Assert.Equal(targetArray, list);
        }

        [Fact]
        public void CopyToTest()
        {
            // Notation litérale "Collection initializer"
            // Autorisée car :
            // - on implémente IEnumerable via IEnumerator
            // - on a une méthode Add via IList
            var list = new MyCollection<string>
            { 
                "A",
                "B",
                "C" 
            };
            var biggerArray = 
                new string[] {"0", "1", "2", "a", "b", "c", "d"};
            var biggerExpected = new string[] {"0", "1", "2", "A", "B", "C", "d"};
            list.CopyTo(biggerArray, 3);
            Assert.Equal(biggerArray, biggerExpected);
            
            var equalArray = new string[] {"0", "1", "2", "a", "b", "c"};
            var equalExpected = new string[] {"0", "1", "2", "A", "B", "C"};
            list.CopyTo(equalArray, 3);
            Assert.Equal(equalArray, equalExpected);

            var smallerArray = new string[] {"0", "1", "2", "a", "b"};
            try
            {
                list.CopyTo(smallerArray, 3);
                // Si on atteint cette ligne,
                // c'est que la ligne du dessus 
                // n'a pas planté alors qu'elle
                // devait
                Assert.True(false);
            }
            catch(Exception e)
            {
                Assert.True(e is ArgumentException);
            }

        }
    }
}
