﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E
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
        DECLARATION = 8,
        FUNCTION_ASSIGNMENT = 9,
        FUNCTION_CALL = 10,
        FUNCTION_STATEMENT = 11,     //if, for, etc...
        FUNCTION_RETURN = 12
        ,
    }

    public enum EStatement
    {
        IF,
        FOR
    }

    public enum ELevel
    {
        ROOT,
        UTILITY,
        OBJECT,
        FUNCTION
    }
}
