﻿using YouTubeApiCleanArchitecture.Domain.Abstraction;

namespace YouTubeApiCleanArchitecture.Domain.Exceptions;
public class BadRequestException(
    List<string> errors) : Exception
{
    public Error Errors { get; set; } = new()
    {
        ErrorCode = "Validation.Error",
        ErrorMessages = errors
    };
}