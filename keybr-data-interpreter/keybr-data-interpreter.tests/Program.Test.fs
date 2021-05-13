module Program.Tests

open Xunit

[<Theory>]
[<InlineData(' ', "SPACE")>]
[<InlineData('c',"c")>]
let ``charStringOrSPACE returns SPACE when the char is a space or (char)32`` value expectedValue =
    
    let result = Program.charStringOrSPACE value

    Assert.Equal(expectedValue, result)
