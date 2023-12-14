using Xunit;

namespace TestProject1
{
    public class Tests
    {
        [Theory]
        [InlineData("filename.txt", 0, 7)]
        [InlineData("hiker.cpp", 0, 4)]
        [InlineData("diamond.h", 0, 6)]
        public void Simple_file_name(string filename, int expectedStart, int expectedEnd)
        {
            var result = FileNameParser.Parse(filename);
            
            Assert.Equal(expectedStart, result[0]);
            Assert.Equal(expectedEnd, result[1]);
        }
        
        [Theory]
        [InlineData("Hikertest.js", 0, 4)]
        [InlineData("HikerStep.rb", 0, 4)]
        [InlineData("customerspec.elt", 0, 7)]
        [InlineData("ShopTests.elt", 0, 3)]
        public void Filename_with_special_word(string filename, int expectedStart, int expectedEnd)
        {
            var result = FileNameParser.Parse(filename);
            
            Assert.Equal(expectedStart, result[0]);
            Assert.Equal(expectedEnd, result[1]);
        }
        
        [Theory]
        [InlineData("ElephantShop-Tests.elt", 0, 11)]
        [InlineData("shop-tests.cs", 0, 3)]
        [InlineData("student.tests.feature", 0, 6)]
        public void Filename_with_special_word_and_separator(string filename, int expectedStart, int expectedEnd)
        {
            var result = FileNameParser.Parse(filename);
            
            Assert.Equal(expectedStart, result[0]);
            Assert.Equal(expectedEnd, result[1]);
        }
        
        [Theory]
        [InlineData("tests/shop-tests.cs", 0, 3)]
        [InlineData("src/tests/elephant_tests.cs", 0, 7)]
        public void Filename_with_in_a_directory(string filename, int expectedStart, int expectedEnd)
        {
            var result = FileNameParser.Parse(filename);
            
            Assert.Equal(expectedStart, result[0]);
            Assert.Equal(expectedEnd, result[1]);
        }

        [Theory]
        [InlineData("src/Hiker_spec.re", 4,9)]
        [InlineData("test/hiker_test.exs", 5,10)]
        [InlineData("wibble/test/hiker_spec.rb", 12,17)]
        [InlineData("hiker_steps.rb", 0,5)]
        [InlineData("hiker_spec.rb", 0,5)]
        [InlineData("test_hiker.rb", 5,10)]
        [InlineData("test_hiker.py", 5,10)]
        [InlineData("test_hiker.sh", 5,10)]
        [InlineData("tests_hiker.sh", 6,11)]
        [InlineData("test_hiker.coffee", 5,10)]
        [InlineData("hiker_spec.coffee", 0,5)]
        [InlineData("hikerTest.chpl", 0,5)]
        [InlineData("hiker.tests.c", 0,5)]
        [InlineData("hiker_tests.c", 0,5)]
        [InlineData("hiker_test.c", 0,5)]
        [InlineData("hiker_Test.c", 0,5)]
        [InlineData("HikerTests.cpp", 0,5)]
        [InlineData("hikerTests.cpp", 0,5)]
        [InlineData("HikerTest.cs", 0,5)]
        [InlineData("HikerTest.java", 0,5)]
        [InlineData("DiamondTest.kt", 0,7)]
        [InlineData("HikerTest.php", 0,5)]
        [InlineData("hikerTest.js", 0,5)]
        [InlineData("hiker-test.js", 0,5)]
        [InlineData("hiker-spec.js", 0,5)] [InlineData("hiker.test.js", 0,5)]
        [InlineData("hiker.tests.ts", 0,5)]
        [InlineData("hiker_tests.erl", 0,5)]
        [InlineData("hiker_test.clj", 0,5)]
        [InlineData("fizzBuzz_test.d", 0,8)]
        [InlineData("hiker_test.go", 0,5)]
        [InlineData("hiker.tests.R", 0,5)]
        [InlineData("hikertests.swift", 0,5)]
        [InlineData("HikerSpec.groovy", 0,5)]
        [InlineData("hikerSpec.feature", 0,5)]
        [InlineData("hiker.feature", 0,5)]
        [InlineData("hiker.fun", 0,5)]
        [InlineData("hiker.t", 0,5)]
        [InlineData("hiker.plt", 0,5)]
        [InlineData("hiker", 0,5)]
        public void All(string filename, int expectedStart, int expectedEnd)
        {
            var result = FileNameParser.Parse(filename);
            
            Assert.Equal(expectedStart, result[0]);
            Assert.Equal(expectedEnd, result[1]);
        }
    }

    public class FileNameParser
    {
        public static int[] Parse(string filename)
        {
            var filenameNoExtension = filename.Substring(0, filename.LastIndexOf("."));
            var filenameNoDirectory = filenameNoExtension.Contains("/")
                ? filenameNoExtension.Substring(filename.LastIndexOf("/") + 1)
                : filenameNoExtension;

            var reserved = new[] { "tests", "spec", "test", "step" };
            var separators = new[] { ".", "-", "_" };

            foreach (var reservedWord in reserved)
            {
                foreach (var separator in separators)
                {
                    if (filenameNoDirectory.ToLower().EndsWith($"{separator}{reservedWord}"))
                    {
                        return new[]
                        {
                            0,
                            filenameNoDirectory.Substring(0,
                                filenameNoDirectory.Length - reservedWord.Length - separator.Length).Length - 1
                        };
                    }
                }

                if (filenameNoDirectory.ToLower().EndsWith(reservedWord))
                {
                    return new[]
                    {
                        0, filenameNoDirectory.Substring(0, filenameNoDirectory.Length - reservedWord.Length).Length - 1
                    };
                }
            }

            return new[] { 0, filename.IndexOf(".") - 1 };
        }
    }
}

