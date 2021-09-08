namespace MinApi.Controllers;

interface IEndpointDefinition
{
    Task<ReturnObject> Action();
}