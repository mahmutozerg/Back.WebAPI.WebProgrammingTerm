﻿using System.ComponentModel.DataAnnotations;

namespace WebProgrammingTerm.Core.DTO;

public class CompanyUserDto
{
 
    [Required(ErrorMessage = "UserMail field is required")]
    [EmailAddress(ErrorMessage = "Invalid mail address")]
    public string UserMail { get; set; } = string.Empty;
}
