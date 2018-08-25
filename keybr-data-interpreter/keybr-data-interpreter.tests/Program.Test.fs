module Program.Tests

open Xunit

[<Theory>]
[<InlineData(' ', "SPACE")>]
[<InlineData('c',"c")>]
let ``Get Space or char value returns SPACE when the char is a space or (char)32`` value expectedValue =
    
    let result = Program.``get space or char value`` value

    Assert.Equal(expectedValue, result)
