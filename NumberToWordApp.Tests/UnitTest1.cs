using Xunit;
using NumberToWordsApp.Controllers;
public class NumberToWordsTests
{
    [Fact]
    public void Convert_ValidDecimalInput_ReturnsWord()
    {
        var converter = new NumberController();
        decimal input = 123.45M;
        string expected = "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS";

        string result = converter.ConvertNumberToWords(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Convert_NegativeInput_ReturnsWord()
    {
        var converter = new NumberController();
        decimal input = -123.00M;
        string expected = "NEGATIVE ONE HUNDRED AND TWENTY-THREE DOLLARS";

        string result = converter.ConvertNumberToWords(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Convert_LargeNumberInput_ReturnsWord()
    {
        var converter = new NumberController();
        decimal input = 1233333333.45M;
        string expected = "ONE BILLION TWO HUNDRED AND THIRTY-THREE MILLION THREE HUNDRED AND THIRTY-THREE THOUSAND THREE HUNDRED AND THIRTY-THREE DOLLARS AND FORTY-FIVE CENTS";

        string result = converter.ConvertNumberToWords(input);

        Assert.Equal(expected, result);
    }

    [Fact]
    public void Convert_InvaildInput_ReturnsWord()
    {
        var converter = new NumberController();
        string input = "12%";
        string expected = "Invalid input. Please enter a valid number.";

        string result = converter.ConvertNumberToWords(input);

        Assert.Equal(expected, result);
    }


}
