﻿namespace api.core.Dtos.Shared;

public class ResponseDto<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = "";
    public T? Data { get; set; }

    // public ResponseDto(bool success, string message, T? data)
    // {
    //     Success = success;
    //     Message = message;
    //     Data = data;
    // }
}