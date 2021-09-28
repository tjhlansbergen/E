namespace EInterpreter
{
    /// <summary>
    /// Token types, the value determines the order used when tokenizing
    /// </summary>
    public enum ETokenType
    {
        WHITESPACE = 0,
        COMMENT = 1,
        CONSTANT = 2,
        OPEN = 3,
        CLOSE = 4,
        OBJECT = 5,
        UTILITY = 6,
        FUNCTION = 7,
        PROPERTY = 8,
        DECLARATION = 9,
        FUNCTION_CALL = 10,
        FUNCTION_STATEMENT = 11,     //if, while, etc...
        FUNCTION_RETURN = 12
        ,
    }

    public enum EStatementType
    {
        IF,
        FOREACH,
        WHILE
    }
}
