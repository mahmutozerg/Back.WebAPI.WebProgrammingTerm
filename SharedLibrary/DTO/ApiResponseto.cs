﻿namespace SharedLibrary.DTO;

public class ApiResponseto<T>
{
    public T Data { get; set; }
    public int StatusCode { get; set; }
}