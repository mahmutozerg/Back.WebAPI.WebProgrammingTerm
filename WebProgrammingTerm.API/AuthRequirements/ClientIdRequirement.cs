﻿using Microsoft.AspNetCore.Authorization;

namespace WebProgrammingTerm.API.AuthRequirements;

public class ClientIdRequirement:IAuthorizationRequirement
{
    public string ClientId { get; }

    public ClientIdRequirement(string clientId)
    {
        ClientId = clientId;
    }
}